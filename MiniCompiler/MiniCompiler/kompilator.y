// Based on productions from following grammar
// Symbols in quotation marks are terminal symbols matched to tokens by rules in kompilator.lex file
//
//
// program         = "program", "{", {declaration}, {statement}, "}";
//
// declaration     = type, "ident", ";";
// type            = "int" 
//                 | "double" 
//                 | "bool";
//
// statement       =  block 
//                 | ifStatement 
//                 | whileStatement 
//                 | returnStatement 
//                 | readStatement 
//                 | writeStatement
//                 | expStatement;
//
// block           = "{", {statement}, "}";
// ifStatement     = "if", "(", condition, ")", statement, ["else", statement];
// whileStatement  = "while", "(", condition, ")", statement;
// returnStatement = "return", ";";
// readStatement   = "read", "ident", ";";
// writeStatement  = "write", expStatement, ";" 
//                 | "write", "text", ";";
// expStatement    = exp, ";";
//
// exp             = "ident", "=", exp
//                 | logExp;
//
// logOp           = "||" | "&&";
// logExp          = logExp, logOp, relExp
//                 | relExp;
//
// relOp           = "==" | "!=" | ">" | ">=" | "<" | "<="; 
// relExp          = relExp, relOp, addExp
//                 | addExp;
//
// addOp           = "+" | "-"; 
// addExp          = addExp, addOp, mulExp
//                 | mulExp;
//
// mulOp           = "*" | "/";
// mulExp         = mulExp, mulOp, bitExp
//                 | bitExp;
//
// bitOp           = "|" | "&";
// bitExp          = bitExp, bitOp, unaryExp
//                 | unaryExp;
//
// unaryExp        = "-", term
//                 | "~", term
//                 | "!", term
//                 | "(int)", term
//                 | "(double)", term;
//
// term            = "ident"
//                 | const
//                 | "(", exp, ")";
//
// const           = "intValue"
//                 | "doubleValue"
//                 | "boolValue";