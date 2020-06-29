// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  DESKTOP-LRNG15B
// DateTime: 29.06.2020 18:01:07
// UserName: szymo
// Input file <../../kompilator.y - 29.06.2020 17:07:32>

// options: conflicts no-lines diagnose & report gplex conflicts

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace MiniCompiler
{
public enum Tokens {error=2,EOF=3,Program=4,If=5,Else=6,
    While=7,Return=8,Read=9,Write=10,OpenPar=11,ClosePar=12,
    OpenBlock=13,CloseBlock=14,Semicolon=15,LogOr=16,LogAnd=17,Equal=18,
    NotEqual=19,Greater=20,GreaterOrEqual=21,Less=22,LessOrEqual=23,Assign=24,
    Plus=25,Minus=26,Multiply=27,Divide=28,BitOr=29,BitAnd=30,
    BitNot=31,LogNot=32,IntCast=33,DoubleCast=34,Eof=35,Error=36,
    IntValue=37,DoubleValue=38,BoolValue=39,Ident=40,Text=41,Int=42,
    Double=43,Bool=44};

public struct ValueType
{
public int      i_val;
public double   d_val;
public bool     b_val;
public string   s_val;
public ValType  val_type;
public OpType   op_type;
}
// Abstract base class for GPLEX scanners
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public abstract class ScanBase : AbstractScanner<ValueType,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class ScanObj {
  public int token;
  public ValueType yylval;
  public LexLocation yylloc;
  public ScanObj( int t, ValueType val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class Parser: ShiftReduceParser<ValueType, LexLocation>
{
#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[69];
  private static State[] states = new State[110];
  private static string[] nonTerms = new string[] {
      "type", "term", "const", "exp", "unaryExp", "bitExp", "mulExp", "addExp", 
      "relExp", "logExp", "bitOp", "mulOp", "addOp", "relOp", "logOp", "start", 
      "$accept", "program", "declarations", "statements", "declaration", "statement", 
      "block", "ifStatement", "whileStatement", "returnStatement", "readStatement", 
      "writeStatement", "expStatement", };

  static Parser() {
    states[0] = new State(new int[]{4,7,2,106},new int[]{-16,1,-18,3});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{35,4,2,5});
    states[4] = new State(-2);
    states[5] = new State(new int[]{35,6});
    states[6] = new State(-4);
    states[7] = new State(new int[]{13,8});
    states[8] = new State(-7,new int[]{-19,9});
    states[9] = new State(new int[]{42,103,43,104,44,105,14,-13,13,-13,5,-13,7,-13,8,-13,9,-13,10,-13,40,-13,37,-13,38,-13,39,-13,11,-13,26,-13,31,-13,32,-13,33,-13,34,-13},new int[]{-20,10,-21,99,-1,100});
    states[10] = new State(new int[]{14,11,13,14,5,18,7,26,8,32,9,35,10,39,40,44,37,61,38,62,39,63,11,64,26,84,31,86,32,88,33,90,34,92},new int[]{-22,12,-23,13,-24,17,-25,25,-26,31,-27,34,-28,38,-29,96,-4,97,-10,47,-9,67,-8,74,-7,77,-6,80,-5,83,-2,58,-3,60});
    states[11] = new State(-6);
    states[12] = new State(-14);
    states[13] = new State(-15);
    states[14] = new State(-13,new int[]{-20,15});
    states[15] = new State(new int[]{14,16,13,14,5,18,7,26,8,32,9,35,10,39,40,44,37,61,38,62,39,63,11,64,26,84,31,86,32,88,33,90,34,92},new int[]{-22,12,-23,13,-24,17,-25,25,-26,31,-27,34,-28,38,-29,96,-4,97,-10,47,-9,67,-8,74,-7,77,-6,80,-5,83,-2,58,-3,60});
    states[16] = new State(-22);
    states[17] = new State(-16);
    states[18] = new State(new int[]{11,19});
    states[19] = new State(new int[]{40,44,37,61,38,62,39,63,11,64,26,84,31,86,32,88,33,90,34,92},new int[]{-4,20,-10,47,-9,67,-8,74,-7,77,-6,80,-5,83,-2,58,-3,60});
    states[20] = new State(new int[]{12,21});
    states[21] = new State(new int[]{13,14,5,18,7,26,8,32,9,35,10,39,40,44,37,61,38,62,39,63,11,64,26,84,31,86,32,88,33,90,34,92},new int[]{-22,22,-23,13,-24,17,-25,25,-26,31,-27,34,-28,38,-29,96,-4,97,-10,47,-9,67,-8,74,-7,77,-6,80,-5,83,-2,58,-3,60});
    states[22] = new State(new int[]{6,23,14,-23,13,-23,5,-23,7,-23,8,-23,9,-23,10,-23,40,-23,37,-23,38,-23,39,-23,11,-23,26,-23,31,-23,32,-23,33,-23,34,-23});
    states[23] = new State(new int[]{13,14,5,18,7,26,8,32,9,35,10,39,40,44,37,61,38,62,39,63,11,64,26,84,31,86,32,88,33,90,34,92},new int[]{-22,24,-23,13,-24,17,-25,25,-26,31,-27,34,-28,38,-29,96,-4,97,-10,47,-9,67,-8,74,-7,77,-6,80,-5,83,-2,58,-3,60});
    states[24] = new State(-24);
    states[25] = new State(-17);
    states[26] = new State(new int[]{11,27});
    states[27] = new State(new int[]{40,44,37,61,38,62,39,63,11,64,26,84,31,86,32,88,33,90,34,92},new int[]{-4,28,-10,47,-9,67,-8,74,-7,77,-6,80,-5,83,-2,58,-3,60});
    states[28] = new State(new int[]{12,29});
    states[29] = new State(new int[]{13,14,5,18,7,26,8,32,9,35,10,39,40,44,37,61,38,62,39,63,11,64,26,84,31,86,32,88,33,90,34,92},new int[]{-22,30,-23,13,-24,17,-25,25,-26,31,-27,34,-28,38,-29,96,-4,97,-10,47,-9,67,-8,74,-7,77,-6,80,-5,83,-2,58,-3,60});
    states[30] = new State(-25);
    states[31] = new State(-18);
    states[32] = new State(new int[]{15,33});
    states[33] = new State(-26);
    states[34] = new State(-19);
    states[35] = new State(new int[]{40,36});
    states[36] = new State(new int[]{15,37});
    states[37] = new State(-27);
    states[38] = new State(-20);
    states[39] = new State(new int[]{41,42,40,44,37,61,38,62,39,63,11,64,26,84,31,86,32,88,33,90,34,92},new int[]{-4,40,-10,47,-9,67,-8,74,-7,77,-6,80,-5,83,-2,58,-3,60});
    states[40] = new State(new int[]{15,41});
    states[41] = new State(-28);
    states[42] = new State(new int[]{15,43});
    states[43] = new State(-29);
    states[44] = new State(new int[]{24,45,29,-63,30,-63,27,-63,28,-63,25,-63,26,-63,18,-63,19,-63,20,-63,21,-63,22,-63,23,-63,16,-63,17,-63,15,-63,12,-63});
    states[45] = new State(new int[]{40,44,37,61,38,62,39,63,11,64,26,84,31,86,32,88,33,90,34,92},new int[]{-4,46,-10,47,-9,67,-8,74,-7,77,-6,80,-5,83,-2,58,-3,60});
    states[46] = new State(-31);
    states[47] = new State(new int[]{16,94,17,95,15,-32,12,-32},new int[]{-15,48});
    states[48] = new State(new int[]{40,59,37,61,38,62,39,63,11,64,26,84,31,86,32,88,33,90,34,92},new int[]{-9,49,-8,74,-7,77,-6,80,-5,83,-2,58,-3,60});
    states[49] = new State(new int[]{18,68,19,69,20,70,21,71,22,72,23,73,16,-35,17,-35,15,-35,12,-35},new int[]{-14,50});
    states[50] = new State(new int[]{40,59,37,61,38,62,39,63,11,64,26,84,31,86,32,88,33,90,34,92},new int[]{-8,51,-7,77,-6,80,-5,83,-2,58,-3,60});
    states[51] = new State(new int[]{25,75,26,76,18,-43,19,-43,20,-43,21,-43,22,-43,23,-43,16,-43,17,-43,15,-43,12,-43},new int[]{-13,52});
    states[52] = new State(new int[]{40,59,37,61,38,62,39,63,11,64,26,84,31,86,32,88,33,90,34,92},new int[]{-7,53,-6,80,-5,83,-2,58,-3,60});
    states[53] = new State(new int[]{27,78,28,79,25,-47,26,-47,18,-47,19,-47,20,-47,21,-47,22,-47,23,-47,16,-47,17,-47,15,-47,12,-47},new int[]{-12,54});
    states[54] = new State(new int[]{40,59,37,61,38,62,39,63,11,64,26,84,31,86,32,88,33,90,34,92},new int[]{-6,55,-5,83,-2,58,-3,60});
    states[55] = new State(new int[]{29,81,30,82,27,-51,28,-51,25,-51,26,-51,18,-51,19,-51,20,-51,21,-51,22,-51,23,-51,16,-51,17,-51,15,-51,12,-51},new int[]{-11,56});
    states[56] = new State(new int[]{40,59,37,61,38,62,39,63,11,64,26,84,31,86,32,88,33,90,34,92},new int[]{-5,57,-2,58,-3,60});
    states[57] = new State(-55);
    states[58] = new State(-57);
    states[59] = new State(-63);
    states[60] = new State(-64);
    states[61] = new State(-66);
    states[62] = new State(-67);
    states[63] = new State(-68);
    states[64] = new State(new int[]{40,44,37,61,38,62,39,63,11,64,26,84,31,86,32,88,33,90,34,92},new int[]{-4,65,-10,47,-9,67,-8,74,-7,77,-6,80,-5,83,-2,58,-3,60});
    states[65] = new State(new int[]{12,66});
    states[66] = new State(-65);
    states[67] = new State(new int[]{18,68,19,69,20,70,21,71,22,72,23,73,16,-36,17,-36,15,-36,12,-36},new int[]{-14,50});
    states[68] = new State(-37);
    states[69] = new State(-38);
    states[70] = new State(-39);
    states[71] = new State(-40);
    states[72] = new State(-41);
    states[73] = new State(-42);
    states[74] = new State(new int[]{25,75,26,76,18,-44,19,-44,20,-44,21,-44,22,-44,23,-44,16,-44,17,-44,15,-44,12,-44},new int[]{-13,52});
    states[75] = new State(-45);
    states[76] = new State(-46);
    states[77] = new State(new int[]{27,78,28,79,25,-48,26,-48,18,-48,19,-48,20,-48,21,-48,22,-48,23,-48,16,-48,17,-48,15,-48,12,-48},new int[]{-12,54});
    states[78] = new State(-49);
    states[79] = new State(-50);
    states[80] = new State(new int[]{29,81,30,82,27,-52,28,-52,25,-52,26,-52,18,-52,19,-52,20,-52,21,-52,22,-52,23,-52,16,-52,17,-52,15,-52,12,-52},new int[]{-11,56});
    states[81] = new State(-53);
    states[82] = new State(-54);
    states[83] = new State(-56);
    states[84] = new State(new int[]{40,59,37,61,38,62,39,63,11,64},new int[]{-2,85,-3,60});
    states[85] = new State(-58);
    states[86] = new State(new int[]{40,59,37,61,38,62,39,63,11,64},new int[]{-2,87,-3,60});
    states[87] = new State(-59);
    states[88] = new State(new int[]{40,59,37,61,38,62,39,63,11,64},new int[]{-2,89,-3,60});
    states[89] = new State(-60);
    states[90] = new State(new int[]{40,59,37,61,38,62,39,63,11,64},new int[]{-2,91,-3,60});
    states[91] = new State(-61);
    states[92] = new State(new int[]{40,59,37,61,38,62,39,63,11,64},new int[]{-2,93,-3,60});
    states[93] = new State(-62);
    states[94] = new State(-33);
    states[95] = new State(-34);
    states[96] = new State(-21);
    states[97] = new State(new int[]{15,98});
    states[98] = new State(-30);
    states[99] = new State(-8);
    states[100] = new State(new int[]{40,101});
    states[101] = new State(new int[]{15,102});
    states[102] = new State(-9);
    states[103] = new State(-10);
    states[104] = new State(-11);
    states[105] = new State(-12);
    states[106] = new State(new int[]{35,109,4,7},new int[]{-18,107});
    states[107] = new State(new int[]{35,108});
    states[108] = new State(-3);
    states[109] = new State(-5);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-17, new int[]{-16,3});
    rules[2] = new Rule(-16, new int[]{-18,35});
    rules[3] = new Rule(-16, new int[]{2,-18,35});
    rules[4] = new Rule(-16, new int[]{-18,2,35});
    rules[5] = new Rule(-16, new int[]{2,35});
    rules[6] = new Rule(-18, new int[]{4,13,-19,-20,14});
    rules[7] = new Rule(-19, new int[]{});
    rules[8] = new Rule(-19, new int[]{-19,-21});
    rules[9] = new Rule(-21, new int[]{-1,40,15});
    rules[10] = new Rule(-1, new int[]{42});
    rules[11] = new Rule(-1, new int[]{43});
    rules[12] = new Rule(-1, new int[]{44});
    rules[13] = new Rule(-20, new int[]{});
    rules[14] = new Rule(-20, new int[]{-20,-22});
    rules[15] = new Rule(-22, new int[]{-23});
    rules[16] = new Rule(-22, new int[]{-24});
    rules[17] = new Rule(-22, new int[]{-25});
    rules[18] = new Rule(-22, new int[]{-26});
    rules[19] = new Rule(-22, new int[]{-27});
    rules[20] = new Rule(-22, new int[]{-28});
    rules[21] = new Rule(-22, new int[]{-29});
    rules[22] = new Rule(-23, new int[]{13,-20,14});
    rules[23] = new Rule(-24, new int[]{5,11,-4,12,-22});
    rules[24] = new Rule(-24, new int[]{5,11,-4,12,-22,6,-22});
    rules[25] = new Rule(-25, new int[]{7,11,-4,12,-22});
    rules[26] = new Rule(-26, new int[]{8,15});
    rules[27] = new Rule(-27, new int[]{9,40,15});
    rules[28] = new Rule(-28, new int[]{10,-4,15});
    rules[29] = new Rule(-28, new int[]{10,41,15});
    rules[30] = new Rule(-29, new int[]{-4,15});
    rules[31] = new Rule(-4, new int[]{40,24,-4});
    rules[32] = new Rule(-4, new int[]{-10});
    rules[33] = new Rule(-15, new int[]{16});
    rules[34] = new Rule(-15, new int[]{17});
    rules[35] = new Rule(-10, new int[]{-10,-15,-9});
    rules[36] = new Rule(-10, new int[]{-9});
    rules[37] = new Rule(-14, new int[]{18});
    rules[38] = new Rule(-14, new int[]{19});
    rules[39] = new Rule(-14, new int[]{20});
    rules[40] = new Rule(-14, new int[]{21});
    rules[41] = new Rule(-14, new int[]{22});
    rules[42] = new Rule(-14, new int[]{23});
    rules[43] = new Rule(-9, new int[]{-9,-14,-8});
    rules[44] = new Rule(-9, new int[]{-8});
    rules[45] = new Rule(-13, new int[]{25});
    rules[46] = new Rule(-13, new int[]{26});
    rules[47] = new Rule(-8, new int[]{-8,-13,-7});
    rules[48] = new Rule(-8, new int[]{-7});
    rules[49] = new Rule(-12, new int[]{27});
    rules[50] = new Rule(-12, new int[]{28});
    rules[51] = new Rule(-7, new int[]{-7,-12,-6});
    rules[52] = new Rule(-7, new int[]{-6});
    rules[53] = new Rule(-11, new int[]{29});
    rules[54] = new Rule(-11, new int[]{30});
    rules[55] = new Rule(-6, new int[]{-6,-11,-5});
    rules[56] = new Rule(-6, new int[]{-5});
    rules[57] = new Rule(-5, new int[]{-2});
    rules[58] = new Rule(-5, new int[]{26,-2});
    rules[59] = new Rule(-5, new int[]{31,-2});
    rules[60] = new Rule(-5, new int[]{32,-2});
    rules[61] = new Rule(-5, new int[]{33,-2});
    rules[62] = new Rule(-5, new int[]{34,-2});
    rules[63] = new Rule(-2, new int[]{40});
    rules[64] = new Rule(-2, new int[]{-3});
    rules[65] = new Rule(-2, new int[]{11,-4,12});
    rules[66] = new Rule(-3, new int[]{37});
    rules[67] = new Rule(-3, new int[]{38});
    rules[68] = new Rule(-3, new int[]{39});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 2: // start -> program, Eof
{
                        YYAccept();
                    }
        break;
      case 3: // start -> error, program, Eof
{
                        Compiler.AddError(new UnexpectedTokenError(Compiler.GetLineNumber()-1));
                        yyerrok();
                        YYAbort();
                    }
        break;
      case 4: // start -> program, error, Eof
{
                        Compiler.AddError(new UnexpectedTokenError(Compiler.GetLineNumber()-1));
                        yyerrok();
                        YYAbort();
                    }
        break;
      case 5: // start -> error, Eof
{
                        Compiler.AddError(new UnexpectedTokenError(Compiler.GetLineNumber()-1));
                        yyerrok();
                        YYAbort();
                    }
        break;
      case 9: // declaration -> type, Ident, Semicolon
{
                        if(Compiler.GetVariable(ValueStack[ValueStack.Depth-2].s_val) == null)
                        {
                            Compiler.DeclareVariable(ValueStack[ValueStack.Depth-3].val_type, ValueStack[ValueStack.Depth-2].s_val);
                            Compiler.AddNode(new DeclarationNode(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-3].val_type, ValueStack[ValueStack.Depth-2].s_val)); 
                        }
                        else
                        {
                            Compiler.AddError(new VariableAlreadyDeclaredError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-2].s_val));
                        }
                    }
        break;
      case 10: // type -> Int
{ CurrentSemanticValue.val_type = ValType.Int; }
        break;
      case 11: // type -> Double
{ CurrentSemanticValue.val_type = ValType.Double; }
        break;
      case 12: // type -> Bool
{ CurrentSemanticValue.val_type = ValType.Bool; }
        break;
      case 13: // statements -> /* empty */
{
                    Compiler.AddNode(new StatementsBlockNode(Compiler.GetLineNumber()));
                }
        break;
      case 14: // statements -> statements, statement
{
                        var innerNode = Compiler.GetNode();
                        var blockNode = Compiler.GetNode() as StatementsBlockNode;
                        blockNode.AddInnerNode(innerNode);

                        Compiler.AddNode(blockNode);
                    }
        break;
      case 23: // ifStatement -> If, OpenPar, exp, ClosePar, statement
{
                        var thenStatement = Compiler.GetNode();
                        var condition = Compiler.GetNode();

                        Compiler.AddNode(new IfStatementNode(Compiler.GetLineNumber(), condition, thenStatement));
                    }
        break;
      case 24: // ifStatement -> If, OpenPar, exp, ClosePar, statement, Else, statement
{
                        var elseStatement = Compiler.GetNode();
                        var thenStatement = Compiler.GetNode();
                        var condition = Compiler.GetNode();

                        Compiler.AddNode(new IfStatementNode(Compiler.GetLineNumber(), condition, thenStatement, elseStatement));
                    }
        break;
      case 25: // whileStatement -> While, OpenPar, exp, ClosePar, statement
{
                        var thenStatement = Compiler.GetNode();
                        var condition = Compiler.GetNode();

                        Compiler.AddNode(new WhileStatementNode(Compiler.GetLineNumber(), condition, thenStatement));
                    }
        break;
      case 26: // returnStatement -> Return, Semicolon
{
                        Compiler.AddNode(new ReturnNode(Compiler.GetLineNumber()));
                    }
        break;
      case 27: // readStatement -> Read, Ident, Semicolon
{
                        Compiler.AddNode(new ReadNode(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-2].s_val));
                    }
        break;
      case 28: // writeStatement -> Write, exp, Semicolon
{
                        var expNode = Compiler.GetNode();
                        Compiler.AddNode(new WriteNode(Compiler.GetLineNumber(), expNode));
                    }
        break;
      case 29: // writeStatement -> Write, Text, Semicolon
{
                        Compiler.AddNode(new WriteNode(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-2].s_val));
                    }
        break;
      case 30: // expStatement -> exp, Semicolon
{
                        Compiler.Pop();
                    }
        break;
      case 31: // exp -> Ident, Assign, exp
{
                        var varType = Compiler.GetVariable(ValueStack[ValueStack.Depth-3].s_val);
                        if(varType == null)
                        {
                            var error = new VariableNotDeclaredError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-3].s_val);
                            Compiler.AddError(error);

                            varType = ValType.Dynamic;
                        }
                        else if(varType.HasValue && varType.Value != ValueStack[ValueStack.Depth-1].val_type)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-1].val_type, varType.Value);
                            Compiler.AddError(error);
                        }

                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;

                        var right = Compiler.GetNode();
                        Compiler.AddNode(new AssignmentNode(Compiler.GetLineNumber(), CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-3].s_val, right)); 
                    }
        break;
      case 32: // exp -> logExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 33: // logOp -> LogOr
{ CurrentSemanticValue.op_type = OpType.LogOr; }
        break;
      case 34: // logOp -> LogAnd
{ CurrentSemanticValue.op_type = OpType.LogAnd; }
        break;
      case 35: // logExp -> logExp, logOp, relExp
{
                        if(ValueStack[ValueStack.Depth-3].val_type != ValType.Bool && ValueStack[ValueStack.Depth-3].val_type != ValType.Dynamic)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-3].val_type, ValType.Bool);
                            Compiler.AddError(error);
                        }
                        if(ValueStack[ValueStack.Depth-1].val_type != ValType.Bool && ValueStack[ValueStack.Depth-1].val_type != ValType.Dynamic)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-1].val_type, ValType.Bool);
                            Compiler.AddError(error);
                        }

                        CurrentSemanticValue.val_type = ValType.Bool;

                        var right = Compiler.GetNode();
                        var left = Compiler.GetNode();
                        Compiler.AddNode(new BinaryOperationNode(Compiler.GetLineNumber(), CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-2].op_type, left, right)); 
                    }
        break;
      case 36: // logExp -> relExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 37: // relOp -> Equal
{ CurrentSemanticValue.op_type = OpType.Equal; }
        break;
      case 38: // relOp -> NotEqual
{ CurrentSemanticValue.op_type = OpType.NotEqual; }
        break;
      case 39: // relOp -> Greater
{ CurrentSemanticValue.op_type = OpType.Greater; }
        break;
      case 40: // relOp -> GreaterOrEqual
{ CurrentSemanticValue.op_type = OpType.GreaterOrEqual; }
        break;
      case 41: // relOp -> Less
{ CurrentSemanticValue.op_type = OpType.Less; }
        break;
      case 42: // relOp -> LessOrEqual
{ CurrentSemanticValue.op_type = OpType.LessOrEqual; }
        break;
      case 43: // relExp -> relExp, relOp, addExp
{
                        if(ValueStack[ValueStack.Depth-2].op_type != OpType.Equal && ValueStack[ValueStack.Depth-2].op_type != OpType.NotEqual)
                        {
                            if(ValueStack[ValueStack.Depth-3].val_type == ValType.Bool)
                            {
                                var error = new InvalidTypeError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-3].val_type, ValType.Int, ValType.Double);
                                Compiler.AddError(error);
                            }
                            if(ValueStack[ValueStack.Depth-1].val_type == ValType.Bool)
                            {
                                var error = new InvalidTypeError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-1].val_type, ValType.Int, ValType.Double);
                                Compiler.AddError(error);
                            }
                        }

                        CurrentSemanticValue.val_type = ValType.Bool;

                        var right = Compiler.GetNode();
                        var left = Compiler.GetNode();
                        Compiler.AddNode(new BinaryOperationNode(Compiler.GetLineNumber(), CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-2].op_type, left, right)); 
                    }
        break;
      case 44: // relExp -> addExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 45: // addOp -> Plus
{ CurrentSemanticValue.op_type = OpType.Plus; }
        break;
      case 46: // addOp -> Minus
{ CurrentSemanticValue.op_type = OpType.Minus; }
        break;
      case 47: // addExp -> addExp, addOp, mulExp
{
                        var invalidType = false;
                        if(ValueStack[ValueStack.Depth-3].val_type == ValType.Bool)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-3].val_type, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                            invalidType = true;
                        }
                        if(ValueStack[ValueStack.Depth-1].val_type == ValType.Bool)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-1].val_type, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                            invalidType = true;
                        }

                        if(invalidType)
                            CurrentSemanticValue.val_type = ValType.Int;
                        else
                            CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-3].val_type == ValType.Int ? ValueStack[ValueStack.Depth-1].val_type : ValType.Double;

                        var right = Compiler.GetNode();
                        var left = Compiler.GetNode();
                        Compiler.AddNode(new BinaryOperationNode(Compiler.GetLineNumber(), CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-2].op_type, left, right)); 
                    }
        break;
      case 48: // addExp -> mulExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 49: // mulOp -> Multiply
{ CurrentSemanticValue.op_type = OpType.Multiply; }
        break;
      case 50: // mulOp -> Divide
{ CurrentSemanticValue.op_type = OpType.Divide; }
        break;
      case 51: // mulExp -> mulExp, mulOp, bitExp
{
                        var invalidType = false;
                        if(ValueStack[ValueStack.Depth-3].val_type == ValType.Bool)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-3].val_type, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                            invalidType = true;
                        }
                        if(ValueStack[ValueStack.Depth-1].val_type == ValType.Bool)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-1].val_type, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                            invalidType = true;
                        }

                        if(invalidType)
                            CurrentSemanticValue.val_type = ValType.Int;
                        else
                            CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-3].val_type == ValType.Int ? ValueStack[ValueStack.Depth-1].val_type : ValType.Double;

                        var right = Compiler.GetNode();
                        var left = Compiler.GetNode();
                        Compiler.AddNode(new BinaryOperationNode(Compiler.GetLineNumber(), CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-2].op_type, left, right)); 
                    }
        break;
      case 52: // mulExp -> bitExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 53: // bitOp -> BitOr
{ CurrentSemanticValue.op_type = OpType.BitOr; }
        break;
      case 54: // bitOp -> BitAnd
{ CurrentSemanticValue.op_type = OpType.BitAnd; }
        break;
      case 55: // bitExp -> bitExp, bitOp, unaryExp
{
                        if(ValueStack[ValueStack.Depth-3].val_type != ValType.Int && ValueStack[ValueStack.Depth-3].val_type != ValType.Dynamic)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-3].val_type, ValType.Int);
                            Compiler.AddError(error);
                        }
                        if(ValueStack[ValueStack.Depth-1].val_type != ValType.Int && ValueStack[ValueStack.Depth-3].val_type != ValType.Dynamic)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-1].val_type, ValType.Int);
                            Compiler.AddError(error);
                        }

                        CurrentSemanticValue.val_type = ValType.Int;
                        
                        var right = Compiler.GetNode();
                        var left = Compiler.GetNode();
                        Compiler.AddNode(new BinaryOperationNode(Compiler.GetLineNumber(), CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-2].op_type, left, right)); 
                    }
        break;
      case 56: // bitExp -> unaryExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 57: // unaryExp -> term
{ 
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type; 
                    }
        break;
      case 58: // unaryExp -> Minus, term
{ 
                        if(ValueStack[ValueStack.Depth-1].val_type == ValType.Bool)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-1].val_type, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                        }

                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(Compiler.GetLineNumber(), CurrentSemanticValue.val_type, OpType.Minus, child)); 
                    }
        break;
      case 59: // unaryExp -> BitNot, term
{ 
                        if(ValueStack[ValueStack.Depth-1].val_type == ValType.Double || ValueStack[ValueStack.Depth-1].val_type == ValType.Bool)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-1].val_type, ValType.Int);
                            Compiler.AddError(error);
                        }

                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(Compiler.GetLineNumber(), CurrentSemanticValue.val_type, OpType.BitNot, child)); 
                    }
        break;
      case 60: // unaryExp -> LogNot, term
{
                        if(ValueStack[ValueStack.Depth-1].val_type == ValType.Int || ValueStack[ValueStack.Depth-1].val_type == ValType.Double)
                        {
                            var error = new InvalidTypeError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-1].val_type, ValType.Bool);
                            Compiler.AddError(error);
                        }

                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(Compiler.GetLineNumber(), CurrentSemanticValue.val_type, OpType.LogNot, child)); 
                    }
        break;
      case 61: // unaryExp -> IntCast, term
{
                        CurrentSemanticValue.val_type = ValType.Int;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(Compiler.GetLineNumber(), CurrentSemanticValue.val_type, OpType.IntCast, child)); 
                    }
        break;
      case 62: // unaryExp -> DoubleCast, term
{
                        CurrentSemanticValue.val_type = ValType.Double;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(Compiler.GetLineNumber(), CurrentSemanticValue.val_type, OpType.DoubleCast, child)); 
                    }
        break;
      case 63: // term -> Ident
{
                        // Throw if variable not declared, else get ValueType
                        var varType =  Compiler.GetVariable(ValueStack[ValueStack.Depth-1].s_val);
                        if(varType == null)
                        {
                            var error = new VariableNotDeclaredError(Compiler.GetLineNumber(), ValueStack[ValueStack.Depth-1].s_val);
                            Compiler.AddError(error);

                            varType = ValType.Dynamic;
                        }

                        CurrentSemanticValue.val_type = varType.Value;
                        Compiler.AddNode(new VariableNode(Compiler.GetLineNumber(), CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-1].s_val)); 
                    }
        break;
      case 64: // term -> const
{ 
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 65: // term -> OpenPar, exp, ClosePar
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-2].val_type;
                    }
        break;
      case 66: // const -> IntValue
{
                        CurrentSemanticValue.val_type = ValType.Int;
                        Compiler.AddNode(new ConstantNode(Compiler.GetLineNumber(), CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-1].i_val)); 
                    }
        break;
      case 67: // const -> DoubleValue
{ 
                        CurrentSemanticValue.val_type = ValType.Double;
                        Compiler.AddNode(new ConstantNode(Compiler.GetLineNumber(), CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-1].d_val)); 
                    }
        break;
      case 68: // const -> BoolValue
{ 
                        CurrentSemanticValue.val_type = ValType.Bool;
                        Compiler.AddNode(new ConstantNode(Compiler.GetLineNumber(), CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-1].b_val)); 
                    }
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

public Parser(Scanner scanner) : base(scanner) { }
}
}
