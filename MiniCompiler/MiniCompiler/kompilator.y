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

%token <val_type> Int
%token <val_type> Double
%token <val_type> Bool

%type <val_type> type
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
                        var a = $1;
                        var b = $3;

                        Console.WriteLine("Assignment: {0}\t{1}", a, b);

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

bitOp           : BitOr
                | BitAnd
                ;

bitExp          : bitExp bitOp unaryExp
                | unaryExp
                ;

unaryExp        :   term 
                    { 
                        $$ = $1; 
                    }
                |   Minus term 
                    { 
                        switch($2.val_type)
                        {
                            case ValType.Int:
                                $$ = $2;
                                $$.i_val = (-1) * $2.i_val;
                                break;
                            case ValType.Double:
                                $$ = $2;
                                $$.d_val = (-1) * $2.d_val;
                                break;
                            case ValType.Bool:
                                var error = new InvalidTypeError(0, $2.val_type, ValType.Int, ValType.Double);
                                Compiler.AddError(error);
                                $$ = $2;
                                break;
                            case ValType.Dynamic:
                                $$ = $2;
                                $$.d_val = (-1) * $2.d_val;
                                $$.i_val = (-1) * $2.i_val;
                                break;
                            default:
                                break;
                        }
                    }
                |   BitNot term 
                    { 
                        switch($2.val_type)
                        {
                            case ValType.Int:
                            case ValType.Dynamic:
                                $$ = $2;
                                $$.i_val = ~$2.i_val;
                                break;
                            case ValType.Double:
                            case ValType.Bool:
                                var error = new InvalidTypeError(0, $2.val_type, ValType.Int);
                                Compiler.AddError(error);
                                $$ = $2;
                                break;
                            default:
                                break;
                        }
                    } 
                |   LogNot term
                    {
                        switch($2.val_type)
                        {
                            case ValType.Bool:
                            case ValType.Dynamic:
                                $$ = $2;
                                $$.b_val = !$2.b_val;
                                break;
                            case ValType.Int:
                            case ValType.Double:
                                var error = new InvalidTypeError(0, $2.val_type, ValType.Bool);
                                Compiler.AddError(error);
                                $$ = $2;
                                break;
                            default:
                                break;
                        }
                    }
                |   IntCast term
                    {
                        switch($2.val_type)
                        {
                            case ValType.Int:
                            case ValType.Dynamic:
                                $$ = $2;
                                break;
                            case ValType.Double:
                                $$.i_val = (int)$2.d_val;
                                $$.val_type = ValType.Int;
                                break;
                            case ValType.Bool:
                                $$.i_val = $2.b_val ? 1 : 0;
                                $$.val_type = ValType.Int;
                                break;
                            default:
                                break;
                        }
                    }
                |   DoubleCast term
                    {
                        switch($2.val_type)
                        {
                            case ValType.Int:
                                $$.d_val = (double)$2.i_val;
                                $$.val_type = ValType.Double;
                                break;
                            case ValType.Double:
                            case ValType.Dynamic:
                                $$ = $2;
                                break;
                            case ValType.Bool:
                                $$.d_val = $2.b_val ? 1.0 : 0.0;
                                $$.val_type = ValType.Double;
                                break;
                            default:
                                break;
                        }
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

                            // Add declaring variable to run rest of the code
                            varValue = new ValueType() { val_type = ValType.Dynamic };
                            Compiler.DeclareVariable($1, ValType.Dynamic);
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
                        $$.i_val = $1;
                        $$.val_type = ValType.Int;
                    }
                |   DoubleValue 
                    { 
                        $$.d_val = $1;
                        $$.val_type = ValType.Double;
                    }
                |   BoolValue 
                    { 
                        $$.b_val = $1;
                        $$.val_type = ValType.Bool;
                    }
                ;

%%
public Parser(Scanner scanner) : base(scanner) { }
