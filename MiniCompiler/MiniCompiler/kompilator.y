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
        BitOr BitAnd BitNot LogNot IntCast DoubleCast Endl Eof Error 
%token <i_val> IntValue 
%token <d_val> DoubleValue 
%token <b_val> BoolValue 
%token <s_val> Ident 
%token <s_val> Text 

%token <val_type> Int Double Bool

%type <val_type> type term const 
//%type <val_type> statements statement block ifStatement whileStatement returnStatement readStatement writeStatement expStatement 
%type <val_type> exp unaryExp bitExp mulExp addExp relExp logExp 

%type <op_type> bitOp mulOp addOp relOp logOp
%%

start           :   program
                ;

program         :   Program OpenBlock declarations statements CloseBlock
                ;

declarations    : 
                |   declarations declaration
                ;
            
declaration     :   type Ident Semicolon
                    {
                        Compiler.DeclareVariable($1, $2);
                        Compiler.AddNode(new DeclarationNode(1, $1, $2)); 
                    }
                ;

type            :   Int             { $$ = ValType.Int; }
                |   Double          { $$ = ValType.Double; }
                |   Bool            { $$ = ValType.Bool; }
                ;

statements      :   
                |   statements statement 
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
                |   If OpenPar exp ClosePar statement Else statement
                ;

whileStatement  :   While OpenPar exp ClosePar statement
                ;

returnStatement :   Return Semicolon
                ;

readStatement   :   Read Ident Semicolon
                ;

writeStatement  :   Write expStatement Semicolon
                |   Write Text Semicolon
                ;

expStatement    :   exp Semicolon
                ;

exp             :   Ident Assign exp 
                    {
                        var varType = Compiler.GetVariable($1);
                        if(varType == null)
                        {
                            var error = new VariableNotDeclaredError(0, $1);
                            Compiler.AddError(error);

                            varType = ValType.Dynamic;
                        }
                        else if(varType.HasValue && varType.Value != $3)
                        {
                            var error = new InvalidTypeError(0, $3, varType.Value);
                            Compiler.AddError(error);
                        }

                        $$ = $3;
                        Compiler.AddNode(new AssignmentNode(0, $$, $1)); 
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
                            var error = new InvalidTypeError(0, $1, ValType.Bool);
                            Compiler.AddError(error);
                        }
                        if($3 != ValType.Bool && $3 != ValType.Dynamic)
                        {
                            var error = new InvalidTypeError(0, $3, ValType.Bool);
                            Compiler.AddError(error);
                        }

                        $$ = ValType.Bool;
                        Compiler.AddNode(new BinaryOperationNode(0, $$, $2)); 
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
                                var error = new InvalidTypeError(0, $1, ValType.Int, ValType.Double);
                                Compiler.AddError(error);
                            }
                            if($3 == ValType.Bool)
                            {
                                var error = new InvalidTypeError(0, $3, ValType.Int, ValType.Double);
                                Compiler.AddError(error);
                            }
                        }

                        $$ = ValType.Bool;
                        Compiler.AddNode(new BinaryOperationNode(0, $$, $2)); 
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
                            var error = new InvalidTypeError(0, $1, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                            invalidType = true;
                        }
                        if($3 == ValType.Bool)
                        {
                            var error = new InvalidTypeError(0, $3, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                            invalidType = true;
                        }

                        if(invalidType)
                            $$ = ValType.Int;
                        else
                            $$ = $1 == ValType.Int ? $3 : ValType.Double;

                        Compiler.AddNode(new BinaryOperationNode(0, $$, $2)); 
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
                            var error = new InvalidTypeError(0, $1, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                            invalidType = true;
                        }
                        if($3 == ValType.Bool)
                        {
                            var error = new InvalidTypeError(0, $3, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                            invalidType = true;
                        }

                        if(invalidType)
                            $$ = ValType.Int;
                        else
                            $$ = $1 == ValType.Int ? $3 : ValType.Double;

                        Compiler.AddNode(new BinaryOperationNode(0, $$, $2)); 
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
                            var error = new InvalidTypeError(0, $1, ValType.Int);
                            Compiler.AddError(error);
                        }
                        if($3 != ValType.Int && $1 != ValType.Dynamic)
                        {
                            var error = new InvalidTypeError(0, $3, ValType.Int);
                            Compiler.AddError(error);
                        }

                        $$ = ValType.Int;
                        Compiler.AddNode(new BinaryOperationNode(0, $$, $2)); 
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
                |   Minus term 
                    { 
                        if($2 == ValType.Bool)
                        {
                            var error = new InvalidTypeError(0, $2, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                        }

                        $$ = $2;
                        Compiler.AddNode(new UnaryOperationNode(0, $$, OpType.Minus)); 
                    }
                |   BitNot term 
                    { 
                        if($2 == ValType.Double || $2 == ValType.Bool)
                        {
                            var error = new InvalidTypeError(0, $2, ValType.Int);
                            Compiler.AddError(error);
                        }

                        $$ = $2;
                        Compiler.AddNode(new UnaryOperationNode(0, $$, OpType.BitNot)); 
                    } 
                |   LogNot term
                    {
                        if($2 == ValType.Int || $2 == ValType.Double)
                        {
                            var error = new InvalidTypeError(0, $2, ValType.Bool);
                            Compiler.AddError(error);
                        }

                        $$ = $2;
                        Compiler.AddNode(new UnaryOperationNode(0, $$, OpType.LogNot)); 
                    }
                |   IntCast term
                    {
                        $$ = ValType.Int;
                        Compiler.AddNode(new UnaryOperationNode(0, $$, OpType.IntCast)); 
                    }
                |   DoubleCast term
                    {
                        $$ = ValType.Double;
                        Compiler.AddNode(new UnaryOperationNode(0, $$, OpType.DoubleCast)); 
                    }
                ;

term            :   Ident 
                    {
                        // Throw if variable not declared, else get ValueType
                        var varType =  Compiler.GetVariable($1);
                        if(varType == null)
                        {
                            var error = new VariableNotDeclaredError(0, $1);
                            Compiler.AddError(error);

                            varType = ValType.Dynamic;
                        }

                        $$ = varType.Value;
                        Compiler.AddNode(new VariableNode(0, $$, $1)); 
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
                        Compiler.AddNode(new ConstantNode(0, $$, $1)); 
                    }
                |   DoubleValue 
                    { 
                        $$ = ValType.Double;
                        Compiler.AddNode(new ConstantNode(0, $$, $1)); 
                    }
                |   BoolValue 
                    { 
                        $$ = ValType.Bool;
                        Compiler.AddNode(new ConstantNode(0, $$, $1)); 
                    }
                ;

%%
public Parser(Scanner scanner) : base(scanner) { }
