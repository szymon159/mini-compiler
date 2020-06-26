%namespace MiniCompiler

%union
{
public int      i_val;
public double   d_val;
public bool     b_val;
public string   s_val;
public ValType  val_type;
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
%type <val_type> exp unaryExp bitExp mulExp addExp relExp logExp 
// %type <type> line exp term factor

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
                        var a = $1;
                        var b = $2;

                        Console.WriteLine("Declaration: {0}\t{1}", a, b);
                        Compiler.DeclareVariable($2, $1);
                        Compiler.AddNode(new Declaration($1, $2, 1)); 
                    }
                ;

type            :   Int { $$ = ValType.Int; }
                |   Double { $$ = ValType.Double; }
                |   Bool { $$ = ValType.Bool; }
                ;

statements      :   {
                        Console.WriteLine("Statement");
                    }
                |   statements statement 
                    {

                    }
                ;

statement       :   block
                |   ifStatement
                |   whileStatement
                |   returnStatement
                |   readStatement
                |   writeStatement
                |   expStatement { $$ = $1; }
                ;

block           :   OpenBlock statements CloseBlock
                    { 
                    Console.WriteLine("A");
                        $$ = $2; 
                    }
                ;

ifStatement     :   If OpenPar exp ClosePar statement {
                        //var st = new IfStatement($3, $5, null, 1);
                        //Compiler.AddNode(st);
                    }
                |   If OpenPar exp ClosePar statement Else statement {
                        //var st = new IfStatement($3, $5, $7, 1);
                        //Compiler.AddNode(st);
                    }
                ;

whileStatement  : While OpenPar exp ClosePar statement
                ;

returnStatement : Return Semicolon
                ;

readStatement   : Read Ident Semicolon
                ;

writeStatement  : Write expStatement Semicolon
                | Write Text Semicolon
                ;

expStatement    : exp Semicolon
                ;

exp             :   Ident Assign exp {
                        Console.WriteLine("Assignment: {0}\t{1}", $1, $3);

                        $$ = $3;
                    }
                |   logExp
                ;

logOp           : LogOr
                | LogAnd
                ;

logExp          : logExp logOp relExp
                | relExp
                ;

relOp           : Equal
                | NotEqual
                | Greater
                | GreaterOrEqual
                | Less
                | LessOrEqual
                ;

relExp          : relExp relOp addExp
                | addExp
                ;

addOp           : Plus
                | Minus
                ;

addExp          : addExp addOp mulExp
                | mulExp
                ;

mulOp           : Multiply
                | Divide
                ;

mulExp          : mulExp mulOp bitExp
                | bitExp

                ;

bitOp           :   BitOr
                |   BitAnd
                ;

bitExp          :   bitExp bitOp unaryExp
                    {
                        if($1 != ValType.Int)
                        {
                            var error = new InvalidTypeError(0, $1, ValType.Int);
                            Compiler.AddError(error);
                        }
                        if($3 != ValType.Int)
                        {
                            var error = new InvalidTypeError(0, $3, ValType.Int);
                            Compiler.AddError(error);
                        }

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

                        $$ = $2;
                    }
                |   BitNot term 
                    { 
                        if($2 == ValType.Double || $2 == ValType.Bool)
                        {
                            var error = new InvalidTypeError(0, $2, ValType.Int);
                            Compiler.AddError(error);
                        }

                        $$ = $2;

                    } 
                |   LogNot term
                    {
                        if($2 == ValType.Int || $2 == ValType.Double)
                        {
                            var error = new InvalidTypeError(0, $2, ValType.Bool);
                            Compiler.AddError(error);
                        }

                        $$ = $2;
                    }
                |   IntCast term
                    {
                        $$ = ValType.Int;
                    }
                |   DoubleCast term
                    {
                        $$ = ValType.Double;
                    }
                ;

term            :   Ident 
                    {
                        // Throw if variable not declared, else get ValueType
                        var varValue =  Compiler.GetVariable($1);
                        if(varValue == null)
                        {
                            var error = new VariableNotDeclaredError(0, $1);
                            Compiler.AddError(error);

                            varValue = ValType.Dynamic;
                        }

                        $$ = varValue.Value; 
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
                    }
                |   DoubleValue 
                    { 
                        $$ = ValType.Double;
                    }
                |   BoolValue 
                    { 
                        $$ = ValType.Bool;
                    }
                ;

%%
public Parser(Scanner scanner) : base(scanner) { }
