%namespace MiniCompiler

%union
{
public int      i_val;
public double   d_val;
public bool     b_val;
public string   s_val;
public ValType  val_type;
public OpType   op_type;
}

%token  Program If Else While Return Read Write OpenPar ClosePar OpenBlock CloseBlock Semicolon
        LogOr LogAnd Equal NotEqual Greater GreaterOrEqual Less LessOrEqual Assign Plus Minus Multiply Divide
        BitOr BitAnd BitNot LogNot IntCast DoubleCast Eof Error 
%token <i_val> IntValue 
%token <d_val> DoubleValue 
%token <b_val> BoolValue 
%token <s_val> Ident
%token <s_val> Text

%token <val_type> Int Double Bool

%type <val_type> type term const 
%type <val_type> exp unaryExp bitExp mulExp addExp relExp logExp 

%type <op_type> bitOp mulOp addOp relOp logOp
%%

start           :   program Eof 
                    {
                        YYACCEPT;
                    }
                |   error program Eof
                    {
                        Compiler.AddError(new UnexpectedTokenError(1));
                        yyerrok();
                        YYABORT;
                    }
                |   program error Eof
                    {
                        Compiler.AddError(new UnexpectedTokenError(Compiler.GetLineNumber()-1));
                        yyerrok();
                        YYABORT;
                    }
                |   error Eof
                    {
                        Compiler.AddError(new UnexpectedTokenError(1));
                        yyerrok();
                        YYABORT;
                    }
                ;

program         :   Program OpenBlock declarations statements CloseBlock
                ;

declarations    : 
                |   declarations declaration
                ;
            
declaration     :   type Ident Semicolon
                    {
                        if(Compiler.GetVariable($2) == null)
                        {
                            Compiler.DeclareVariable($1, $2);
                            Compiler.AddNode(new DeclarationNode(Compiler.GetLineNumber(), $1, $2)); 
                        }
                        else
                        {
                            Compiler.AddError(new VariableAlreadyDeclaredError(Compiler.GetLineNumber(), $2));
                        }
                    }
                |   error Eof
                    {
                        Compiler.AddError(new UnexpectedTokenError(Compiler.GetLineNumber()-1));
                        yyerrok();
                        YYABORT;
                    }
                ;

type            :   Int             { $$ = ValType.Int; }
                |   Double          { $$ = ValType.Double; }
                |   Bool            { $$ = ValType.Bool; }
                ;

statements      :   
                {
                    Compiler.AddNode(new StatementsBlockNode(Compiler.GetLineNumber()));
                }
                |   statements statement
                    {
                        var innerNode = Compiler.GetNode();
                        var blockNode = Compiler.GetNode() as StatementsBlockNode;
                        if(blockNode != null)
                        {
                            blockNode.AddInnerNode(innerNode);
                            Compiler.AddNode(blockNode);
                        }
                        else
                        {
                            Compiler.AddNode(innerNode);
                        }
                    }
                ;

statement       :   block
                |   ifStatement
                |   whileStatement
                |   returnStatement
                |   readStatement
                |   writeStatement
                |   expStatement
                ;

block           :   OpenBlock statements CloseBlock
                ;

ifStatement     :   If OpenPar exp ClosePar statement
                    {
                        if($3 != ValType.Bool)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber()-1, $3, ValType.Bool);
                            Compiler.AddError(error);
                        }
                        else
                        {
                            var thenStatement = Compiler.GetNode();
                            var condition = Compiler.GetNode();

                            Compiler.AddNode(new IfStatementNode(Compiler.GetLineNumber(), condition, thenStatement));
                        }
                    }
                |   If OpenPar exp ClosePar statement Else statement
                    {
                        if($3 != ValType.Bool)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber()-1, $3, ValType.Bool);
                            Compiler.AddError(error);
                        }
                        else
                        {
                            var elseStatement = Compiler.GetNode();
                            var thenStatement = Compiler.GetNode();
                            var condition = Compiler.GetNode();

                            Compiler.AddNode(new IfStatementNode(Compiler.GetLineNumber(), condition, thenStatement, elseStatement));
                        }
                    }
                ;

whileStatement  :   While OpenPar exp ClosePar statement
                    {
                        if($3 != ValType.Bool)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber()-1, $3, ValType.Bool);
                            Compiler.AddError(error);
                        }
                        else
                        {
                            var thenStatement = Compiler.GetNode();
                            var condition = Compiler.GetNode();

                            Compiler.AddNode(new WhileStatementNode(Compiler.GetLineNumber(), condition, thenStatement));
                        }
                    }
                ;

returnStatement :   Return Semicolon
                    {
                        Compiler.AddNode(new ReturnNode(Compiler.GetLineNumber()));
                    }
                ;

readStatement   :   Read Ident Semicolon
                    {
                        if(Compiler.GetVariable($2) == null)
                        {
                            Compiler.AddError(new VariableNotDeclaredError(Compiler.GetLineNumber(), $2));
                        }
                        else
                        {
                            Compiler.AddNode(new ReadNode(Compiler.GetLineNumber(), $2));
                        }
                    }
                |   Read error
                    {
                        Compiler.AddError(new UnexpectedTokenError(Compiler.GetLineNumber()-1));
                        yyerrok();
                        yyclearin();
                    }
                |   Read error Eof
                    {
                        Compiler.AddError(new UnexpectedTokenError(Compiler.GetLineNumber()-1));
                        yyerrok();
                        YYABORT;
                    }
                ;

writeStatement  :   Write exp Semicolon
                    {
                        var expNode = Compiler.GetNode();
                        Compiler.AddNode(new WriteNode(Compiler.GetLineNumber(), expNode));
                    }
                |   Write Text Semicolon
                    {
                        Compiler.AddNode(new WriteNode(Compiler.GetLineNumber(), $2));
                    }
                |   Write error
                    {
                        Compiler.AddError(new UnexpectedTokenError(Compiler.GetLineNumber()-1));
                        yyerrok();
                        yyclearin();
                    }
                |   Write error Eof
                    {
                        Compiler.AddError(new UnexpectedTokenError(Compiler.GetLineNumber()));  
                        yyerrok();                        
                        YYABORT;
                    }
                ;

expStatement    :   exp Semicolon
                    {
                        Compiler.Pop();
                    }
                |   error
                    {
                        Compiler.AddError(new UnexpectedTokenError(Compiler.GetLineNumber()));  
                        yyerrok();
                        yyclearin();
                    }
                |   error Eof
                    {
                        Compiler.AddError(new UnexpectedTokenError(Compiler.GetLineNumber()));  
                        yyerrok();
                        YYABORT;
                    }
                ;

exp             :   Ident Assign exp 
                    {
                        var varType = Compiler.GetVariable($1);
                        SyntaxTreeNode right;
                        if(varType == null)
                        {
                            var error = new VariableNotDeclaredError(Compiler.GetLineNumber(), $1);
                            Compiler.AddError(error);

                            right = Compiler.GetNode();
                            $$ = $3;
                        }
                        else if(varType == ValType.Double && $3 == ValType.Int)
                        {
                            // Implicit cast from int to double
                            var tempNode = Compiler.GetNode();

                            $$ = varType.Value;
                            right = new UnaryOperationNode(Compiler.GetLineNumber(), $$, OpType.DoubleCast, tempNode);
                        }
                        else if(varType.Value != $3)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), $3, varType.Value);
                            Compiler.AddError(error);

                            right = Compiler.GetNode();
                            $$ = varType.Value;
                        }
                        else
                        {
                            right = Compiler.GetNode();
                            $$ = $3;
                        }
                        
                        Compiler.AddNode(new AssignmentNode(Compiler.GetLineNumber(), $$, $1, right)); 
                    }
                |   logExp
                    {
                        $$ = $1;
                    }
                ;

logOp           :   LogOr           { $$ = OpType.LogOr; }
                |   LogAnd          { $$ = OpType.LogAnd; }
                ;

logExp          :   logExp logOp relExp
                    {
                        if($1 != ValType.Bool && $1 != ValType.Dynamic)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), $1, ValType.Bool);
                            Compiler.AddError(error);
                        }
                        if($3 != ValType.Bool && $3 != ValType.Dynamic)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), $3, ValType.Bool);
                            Compiler.AddError(error);
                        }

                        $$ = ValType.Bool;

                        var right = Compiler.GetNode();
                        var left = Compiler.GetNode();
                        Compiler.AddNode(new BinaryOperationNode(Compiler.GetLineNumber(), $$, $2, left, right)); 
                    }
                |   relExp
                    {
                        $$ = $1;
                    }
                ;

relOp           :   Equal           { $$ = OpType.Equal; } 
                |   NotEqual        { $$ = OpType.NotEqual; } 
                |   Greater         { $$ = OpType.Greater; } 
                |   GreaterOrEqual  { $$ = OpType.GreaterOrEqual; } 
                |   Less            { $$ = OpType.Less; } 
                |   LessOrEqual     { $$ = OpType.LessOrEqual; } 
                ;

relExp          :   relExp relOp addExp
                    {
                        if($2 != OpType.Equal && $2 != OpType.NotEqual)
                        {
                            if($1 == ValType.Bool)
                            {
                                var error = new InvalidTypeError(Compiler.GetLineNumber(), $1, ValType.Int, ValType.Double);
                                Compiler.AddError(error);
                            }
                            if($3 == ValType.Bool)
                            {
                                var error = new InvalidTypeError(Compiler.GetLineNumber(), $3, ValType.Int, ValType.Double);
                                Compiler.AddError(error);
                            }
                        }

                        $$ = ValType.Bool;

                        var right = Compiler.GetNode();
                        var left = Compiler.GetNode();
                        Compiler.AddNode(new BinaryOperationNode(Compiler.GetLineNumber(), $$, $2, left, right)); 
                    }
                |   addExp
                    {
                        $$ = $1;
                    }
                ;

addOp           :   Plus              { $$ = OpType.Plus; }
                |   Minus             { $$ = OpType.Minus; }
                ;

addExp          :   addExp addOp mulExp
                    {
                        var invalidType = false;
                        if($1 == ValType.Bool)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), $1, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                            invalidType = true;
                        }
                        if($3 == ValType.Bool)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), $3, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                            invalidType = true;
                        }

                        if(invalidType)
                            $$ = ValType.Int;
                        else
                            $$ = $1 == ValType.Int ? $3 : ValType.Double;

                        var right = Compiler.GetNode();
                        var left = Compiler.GetNode();
                        Compiler.AddNode(new BinaryOperationNode(Compiler.GetLineNumber(), $$, $2, left, right)); 
                    }
                |   mulExp
                    {
                        $$ = $1;
                    }
                ;

mulOp           :   Multiply            { $$ = OpType.Multiply; }
                |   Divide              { $$ = OpType.Divide; }
                ;

mulExp          :   mulExp mulOp bitExp
                    {
                        var invalidType = false;
                        if($1 == ValType.Bool)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), $1, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                            invalidType = true;
                        }
                        if($3 == ValType.Bool)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), $3, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                            invalidType = true;
                        }

                        if(invalidType)
                            $$ = ValType.Int;
                        else
                            $$ = $1 == ValType.Int ? $3 : ValType.Double;

                        var right = Compiler.GetNode();
                        var left = Compiler.GetNode();
                        Compiler.AddNode(new BinaryOperationNode(Compiler.GetLineNumber(), $$, $2, left, right)); 
                    }
                |   bitExp
                    {
                        $$ = $1;
                    }
                ;

bitOp           :   BitOr               { $$ = OpType.BitOr; }
                |   BitAnd              { $$ = OpType.BitAnd; }
                ;

bitExp          :   bitExp bitOp unaryExp
                    {
                        if($1 != ValType.Int && $1 != ValType.Dynamic)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), $1, ValType.Int);
                            Compiler.AddError(error);
                        }
                        if($3 != ValType.Int && $1 != ValType.Dynamic)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), $3, ValType.Int);
                            Compiler.AddError(error);
                        }

                        $$ = ValType.Int;
                        
                        var right = Compiler.GetNode();
                        var left = Compiler.GetNode();
                        Compiler.AddNode(new BinaryOperationNode(Compiler.GetLineNumber(), $$, $2, left, right)); 
                    }
                |   unaryExp
                    {
                        $$ = $1;
                    }
                ;

unaryExp        :   term 
                    { 
                        $$ = $1; 
                    }
                |   Minus unaryExp 
                    { 
                        if($2 == ValType.Bool)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), $2, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                        }

                        $$ = $2;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(Compiler.GetLineNumber(), $$, OpType.Minus, child)); 
                    }
                |   BitNot unaryExp 
                    { 
                        if($2 == ValType.Double || $2 == ValType.Bool)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), $2, ValType.Int);
                            Compiler.AddError(error);
                        }

                        $$ = $2;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(Compiler.GetLineNumber(), $$, OpType.BitNot, child)); 
                    } 
                |   LogNot unaryExp
                    {
                        if($2 == ValType.Int || $2 == ValType.Double)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), $2, ValType.Bool);
                            Compiler.AddError(error);
                        }

                        $$ = $2;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(Compiler.GetLineNumber(), $$, OpType.LogNot, child)); 
                    }
                |   OpenPar Int ClosePar unaryExp
                    {
                        $$ = ValType.Int;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(Compiler.GetLineNumber(), $$, OpType.IntCast, child)); 
                    }
                |   OpenPar Double ClosePar unaryExp
                    {
                        $$ = ValType.Double;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(Compiler.GetLineNumber(), $$, OpType.DoubleCast, child)); 
                    }
                ;

term            :   Ident 
                    {
                        // Throw if variable not declared, else get ValueType
                        var varType =  Compiler.GetVariable($1);
                        if(varType == null)
                        {
                            var error = new VariableNotDeclaredError(Compiler.GetLineNumber(), $1);
                            Compiler.AddError(error);

                            varType = ValType.Dynamic;
                        }

                        $$ = varType.Value;
                        Compiler.AddNode(new VariableNode(Compiler.GetLineNumber(), $$, $1)); 
                    }
                |   const 
                    { 
                        $$ = $1;
                    } 
                |   OpenPar exp ClosePar 
                    {
                        $$ = $2;
                    }
                ;

const           :   IntValue
                    {
                        $$ = ValType.Int;
                        Compiler.AddNode(new ConstantNode(Compiler.GetLineNumber(), $$, $1)); 
                    }
                |   DoubleValue 
                    { 
                        $$ = ValType.Double;
                        Compiler.AddNode(new ConstantNode(Compiler.GetLineNumber(), $$, $1)); 
                    }
                |   BoolValue 
                    { 
                        $$ = ValType.Bool;
                        Compiler.AddNode(new ConstantNode(Compiler.GetLineNumber(), $$, $1)); 
                    }
                ;

%%
public Parser(Scanner scanner) : base(scanner) { }