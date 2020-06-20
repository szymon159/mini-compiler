%using QUT.Gppg;
%namespace GardensPoint

Integer			0|[1-9][0-9]*
Double			(0|[1-9][0-9]*)\.[0-9]+
Bool			"true"|"false"
Ident			[A-Za-z][A-Za-z0-9]*
Text			\".*\"
Comment			"//".*

%%

"program"		{ return (int)Tokens.Program; }
"if"			{ return (int)Tokens.If; }
"else"			{ return (int)Tokens.Else; }
"while"			{ return (int)Tokens.While; }
"read"			{ return (int)Tokens.Read; }
"write"			{ return (int)Tokens.Write; }
"return"        { return (int)Tokens.Return; }

{Integer}		{ yylval.val=yytext; return (int)Tokens.Integer; }
{Double}		{ yylval.val=yytext; return (int)Tokens.Double; }
{Bool}			{ yylval.val=yytext; return (int)Tokens.Bool }
{Ident}			{ yylval.val=yytext; return (int)Tokens.Ident; }
{Text}			{ yylval.val=yytext; return (int)Tokens.Text; }

"||"			{ return (int)Tokens.LogOr }
"&&"			{ return (int)Tokens.LogAnd }
"!"				{ return (int)Tokens.LogNegation }
"=="			{ return (int)Tokens.Equal }
"!="			{ return (int)Tokens.NotEqual }
">"				{ return (int)Tokens.Greater }
">="			{ return (int)Tokens.GreaterOrEqual }
"<"				{ return (int)Tokens.Less }
"<="			{ return (int)Tokens.LessOrEqual }

"~"				{ return (int)Tokens.BitNot }
"|"				{ return (int)Tokens.BitOr }
"&"				{ return (int)Tokens.BitAnd }

"="				{ return (int)Tokens.Assignment; }
"+"				{ return (int)Tokens.Plus; }
"-"				{ return (int)Tokens.Minus; }
"*"				{ return (int)Tokens.Multiplication; }
"/"				{ return (int)Tokens.Division; }
"("				{ return (int)Tokens.OpenPar; }
")"				{ return (int)Tokens.ClosePar; }
"{"				{ return (int)Tokens.OpenBlock; }
"}"				{ return (int)Tokens.CloseBlock; }

" "				{ }
"\t"			{ }
{Comment}		{ }
"\r"			{ return (int)Tokens.Endl; }

<<EOF>>			{ return (int)Tokens.Eof; }

.				{ return (int)Tokens.Error; }