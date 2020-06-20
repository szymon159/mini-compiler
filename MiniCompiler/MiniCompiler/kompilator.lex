%using QUT.Gppg;
%namespace GardensPoint

Int 			0|[1-9][0-9]*
Double			(0|[1-9][0-9]*)\.[0-9]+
Bool			"true"|"false"
Ident			[A-Za-z][A-Za-z0-9]*
Text			\".*\"
Comment			"//".*

%%

{Integer}		{ yylval.val=yytext; return (int)Tokens.IntValue; }
{Double}		{ yylval.val=yytext; return (int)Tokens.DoubleValue; }
{Bool}			{ yylval.val=yytext; return (int)Tokens.BoolValue; }
{Ident}			{ yylval.val=yytext; return (int)Tokens.Ident; }
{Text}			{ yylval.val=yytext; return (int)Tokens.Text; }

"program"		{ return (int)Tokens.Program; }
"int"           { return (int)Tokens.Int; }
"double"        { return (int)Tokens.Double; }
"bool"          { return (int)Tokens.Bool; }

"if"			{ return (int)Tokens.If; }
"else"			{ return (int)Tokens.Else; }
"while"			{ return (int)Tokens.While; }
"return"        { return (int)Tokens.Return; }
"read"			{ return (int)Tokens.Read; }
"write"			{ return (int)Tokens.Write; }

"("				{ return (int)Tokens.OpenPar; }
")"				{ return (int)Tokens.ClosePar; }
"{"				{ return (int)Tokens.OpenBlock; }
"}"				{ return (int)Tokens.CloseBlock; }

"||"			{ return (int)Tokens.LogOr; }
"&&"			{ return (int)Tokens.LogAnd }

"=="			{ return (int)Tokens.Equal; }
"!="			{ return (int)Tokens.NotEqual; }
">"				{ return (int)Tokens.Greater; }
">="			{ return (int)Tokens.GreaterOrEqual; }
"<"				{ return (int)Tokens.Less;}
"<="			{ return (int)Tokens.LessOrEqual; }

"="				{ return (int)Tokens.Assign; }
"+"				{ return (int)Tokens.Plus; }
"-"				{ return (int)Tokens.Minus; }
"*"				{ return (int)Tokens.Multiply; }
"/"				{ return (int)Tokens.Divide; }

"|"				{ return (int)Tokens.BitOr; }
"&"				{ return (int)Tokens.BitAnd; }
"~"				{ return (int)Tokens.BitNot; }
"!"				{ return (int)Tokens.LogNegation; }
"(int)"         { return (int) Tokens.IntCast; }
"(double)"      { return (int) Tokens.DoubleCast; }

" "				{ }
"\t"			{ }
{Comment}		{ }
"\r"			{ return (int)Tokens.Endl; }

<<EOF>>			{ return (int)Tokens.Eof; }

.				{ return (int)Tokens.Error; }