%namespace GardensPoint

%union
{
public int      i_val;
public double   d_val;
public bool     b_val;
public string   s_val;
public ValType  valType;
}

%token  Program If Else While Return Read Write OpenPar ClosePar OpenBlock CloseBlock Semicolon
        LogOr LogAnd Equal NotEqual Greater GreaterOrEqual Less LessOrEqual Assign Plus Minus Multiply Divide
        BitOr BitAnd BitNot LogNot IntCast DoubleCast Endl Eof Error 
%token <i_val> IntValue 
%token <d_val> DoubleValue 
%token <b_val> BoolValue 
%token <s_val> Ident 
%token <s_val> Text 

%token <valType> Int
%token <valType> Double
%token <valType> Bool

%type <valType> type
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
                        Compiler.DeclareVariable($2);
                        Compiler.AddNode(new Declaration($1, $2, 1)); 
                    }
                ;

type            :   Int { $$ = ValType.Int; }
                |   Double { $$ = ValType.Double; }
                |   Bool { $$ = ValType.Bool; }
                ;

statements      :   { }
                |   statements statement 
                    {
                        //Compiler.AddNode($2);
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
                    { 
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

exp             : Ident Assign exp
                | logExp
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

unaryExp        : Minus term
                | BitNot term
                | LogNot term
                | IntCast term
                | DoubleCast term
                ;

term            : Ident
                | const
                | OpenPar exp ClosePar
                ;

const           : IntValue { $$.i_val = $1; }
                | DoubleValue { $$.d_val = $1; }
                | BoolValue { $$.b_val = $1; }
                ;

%%
public Parser(Scanner scanner) : base(scanner) { }
