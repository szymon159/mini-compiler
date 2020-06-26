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
                        Console.WriteLine("Declaration: {0}\t{1}", $1, $2);
                        Compiler.DeclareVariable($2, $1);
                        Compiler.AddNode(new Declaration($1, $2, 1)); 
                    }
                ;

type            :   Int { $$ = ValType.Int; }
                |   Double { $$ = ValType.Double; }
                |   Bool { $$ = ValType.Bool; }
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
                        Console.WriteLine("Assignment: {0}\t{1}", $1, $3);

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

                        Console.WriteLine("Logic expression: {0}\t{1}\t{2}", $1, $2, $3);
                        $$ = ValType.Bool;
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

                        Console.WriteLine("Relation expression: {0}\t{1}\t{2}", $1, $2, $3);
                        $$ = ValType.Bool;
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

                        Console.WriteLine("Add expression: {0}\t{1}\t{2}", $1, $2, $3);
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

                        Console.WriteLine("Multiplicative expression: {0}\t{1}\t{2}", $1, $2, $3);
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

                        Console.WriteLine("Bit expression: {0}\t{1}\t{2}", $1, $2, $3);
                        $$ = ValType.Int;
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

                        Console.WriteLine("Negative number: {0}", $2);
                        $$ = $2;
                    }
                |   BitNot term 
                    { 
                        if($2 == ValType.Double || $2 == ValType.Bool)
                        {
                            var error = new InvalidTypeError(0, $2, ValType.Int);
                            Compiler.AddError(error);
                        }

                        Console.WriteLine("Bit negation number: {0}", $2);
                        $$ = $2;

                    } 
                |   LogNot term
                    {
                        if($2 == ValType.Int || $2 == ValType.Double)
                        {
                            var error = new InvalidTypeError(0, $2, ValType.Bool);
                            Compiler.AddError(error);
                        }

                        Console.WriteLine("Logic negation number: {0}", $2);
                        $$ = $2;
                    }
                |   IntCast term
                    {
                        Console.WriteLine("Int cast: {0}", $2);
                        $$ = ValType.Int;
                    }
                |   DoubleCast term
                    {
                        Console.WriteLine("Int cast: {0}", $2);
                        $$ = ValType.Double;
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

                        Console.WriteLine("Ident: {0}", $1);
                        $$ = varType.Value; 
                    }
                |   const 
                    { 
                        $$ = $1;
                    } 
                |   OpenPar exp ClosePar 
                    {
                        Console.WriteLine("Expression in (): {0}", $2);
                        $$ = $2;
                    }
                ;

const           :   IntValue
                    {
                        Console.WriteLine("Const int: {0}", $1);
                        $$ = ValType.Int;
                    }
                |   DoubleValue 
                    { 
                        Console.WriteLine("Const double: {0}", $1);
                        $$ = ValType.Double;
                    }
                |   BoolValue 
                    { 
                        Console.WriteLine("Const bool: {0}", $1);
                        $$ = ValType.Bool;
                    }
                ;

%%
public Parser(Scanner scanner) : base(scanner) { }
