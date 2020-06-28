// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  DESKTOP-LRNG15B
// DateTime: 28.06.2020 19:05:34
// UserName: szymo
// Input file <../../kompilator.y - 28.06.2020 15:18:22>

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
    BitNot=31,LogNot=32,IntCast=33,DoubleCast=34,Endl=35,Eof=36,
    Error=37,IntValue=38,DoubleValue=39,BoolValue=40,Ident=41,Text=42,
    Int=43,Double=44,Bool=45};

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
  private static Rule[] rules = new Rule[66];
  private static State[] states = new State[103];
  private static string[] nonTerms = new string[] {
      "type", "term", "const", "exp", "unaryExp", "bitExp", "mulExp", "addExp", 
      "relExp", "logExp", "bitOp", "mulOp", "addOp", "relOp", "logOp", "start", 
      "$accept", "program", "declarations", "statements", "declaration", "statement", 
      "block", "ifStatement", "whileStatement", "returnStatement", "readStatement", 
      "writeStatement", "expStatement", };

  static Parser() {
    states[0] = new State(new int[]{4,4},new int[]{-16,1,-18,3});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(new int[]{13,5});
    states[5] = new State(-4,new int[]{-19,6});
    states[6] = new State(new int[]{43,100,44,101,45,102,14,-10,13,-10,5,-10,7,-10,8,-10,9,-10,10,-10,41,-10,38,-10,39,-10,40,-10,11,-10,26,-10,31,-10,32,-10,33,-10,34,-10},new int[]{-20,7,-21,96,-1,97});
    states[7] = new State(new int[]{14,8,13,11,5,15,7,23,8,29,9,32,10,36,41,41,38,58,39,59,40,60,11,61,26,81,31,83,32,85,33,87,34,89},new int[]{-22,9,-23,10,-24,14,-25,22,-26,28,-27,31,-28,35,-29,93,-4,94,-10,44,-9,64,-8,71,-7,74,-6,77,-5,80,-2,55,-3,57});
    states[8] = new State(-3);
    states[9] = new State(-11);
    states[10] = new State(-12);
    states[11] = new State(-10,new int[]{-20,12});
    states[12] = new State(new int[]{14,13,13,11,5,15,7,23,8,29,9,32,10,36,41,41,38,58,39,59,40,60,11,61,26,81,31,83,32,85,33,87,34,89},new int[]{-22,9,-23,10,-24,14,-25,22,-26,28,-27,31,-28,35,-29,93,-4,94,-10,44,-9,64,-8,71,-7,74,-6,77,-5,80,-2,55,-3,57});
    states[13] = new State(-19);
    states[14] = new State(-13);
    states[15] = new State(new int[]{11,16});
    states[16] = new State(new int[]{41,41,38,58,39,59,40,60,11,61,26,81,31,83,32,85,33,87,34,89},new int[]{-4,17,-10,44,-9,64,-8,71,-7,74,-6,77,-5,80,-2,55,-3,57});
    states[17] = new State(new int[]{12,18});
    states[18] = new State(new int[]{13,11,5,15,7,23,8,29,9,32,10,36,41,41,38,58,39,59,40,60,11,61,26,81,31,83,32,85,33,87,34,89},new int[]{-22,19,-23,10,-24,14,-25,22,-26,28,-27,31,-28,35,-29,93,-4,94,-10,44,-9,64,-8,71,-7,74,-6,77,-5,80,-2,55,-3,57});
    states[19] = new State(new int[]{6,20,14,-20,13,-20,5,-20,7,-20,8,-20,9,-20,10,-20,41,-20,38,-20,39,-20,40,-20,11,-20,26,-20,31,-20,32,-20,33,-20,34,-20});
    states[20] = new State(new int[]{13,11,5,15,7,23,8,29,9,32,10,36,41,41,38,58,39,59,40,60,11,61,26,81,31,83,32,85,33,87,34,89},new int[]{-22,21,-23,10,-24,14,-25,22,-26,28,-27,31,-28,35,-29,93,-4,94,-10,44,-9,64,-8,71,-7,74,-6,77,-5,80,-2,55,-3,57});
    states[21] = new State(-21);
    states[22] = new State(-14);
    states[23] = new State(new int[]{11,24});
    states[24] = new State(new int[]{41,41,38,58,39,59,40,60,11,61,26,81,31,83,32,85,33,87,34,89},new int[]{-4,25,-10,44,-9,64,-8,71,-7,74,-6,77,-5,80,-2,55,-3,57});
    states[25] = new State(new int[]{12,26});
    states[26] = new State(new int[]{13,11,5,15,7,23,8,29,9,32,10,36,41,41,38,58,39,59,40,60,11,61,26,81,31,83,32,85,33,87,34,89},new int[]{-22,27,-23,10,-24,14,-25,22,-26,28,-27,31,-28,35,-29,93,-4,94,-10,44,-9,64,-8,71,-7,74,-6,77,-5,80,-2,55,-3,57});
    states[27] = new State(-22);
    states[28] = new State(-15);
    states[29] = new State(new int[]{15,30});
    states[30] = new State(-23);
    states[31] = new State(-16);
    states[32] = new State(new int[]{41,33});
    states[33] = new State(new int[]{15,34});
    states[34] = new State(-24);
    states[35] = new State(-17);
    states[36] = new State(new int[]{42,39,41,41,38,58,39,59,40,60,11,61,26,81,31,83,32,85,33,87,34,89},new int[]{-4,37,-10,44,-9,64,-8,71,-7,74,-6,77,-5,80,-2,55,-3,57});
    states[37] = new State(new int[]{15,38});
    states[38] = new State(-25);
    states[39] = new State(new int[]{15,40});
    states[40] = new State(-26);
    states[41] = new State(new int[]{24,42,29,-60,30,-60,27,-60,28,-60,25,-60,26,-60,18,-60,19,-60,20,-60,21,-60,22,-60,23,-60,16,-60,17,-60,15,-60,12,-60});
    states[42] = new State(new int[]{41,41,38,58,39,59,40,60,11,61,26,81,31,83,32,85,33,87,34,89},new int[]{-4,43,-10,44,-9,64,-8,71,-7,74,-6,77,-5,80,-2,55,-3,57});
    states[43] = new State(-28);
    states[44] = new State(new int[]{16,91,17,92,15,-29,12,-29},new int[]{-15,45});
    states[45] = new State(new int[]{41,56,38,58,39,59,40,60,11,61,26,81,31,83,32,85,33,87,34,89},new int[]{-9,46,-8,71,-7,74,-6,77,-5,80,-2,55,-3,57});
    states[46] = new State(new int[]{18,65,19,66,20,67,21,68,22,69,23,70,16,-32,17,-32,15,-32,12,-32},new int[]{-14,47});
    states[47] = new State(new int[]{41,56,38,58,39,59,40,60,11,61,26,81,31,83,32,85,33,87,34,89},new int[]{-8,48,-7,74,-6,77,-5,80,-2,55,-3,57});
    states[48] = new State(new int[]{25,72,26,73,18,-40,19,-40,20,-40,21,-40,22,-40,23,-40,16,-40,17,-40,15,-40,12,-40},new int[]{-13,49});
    states[49] = new State(new int[]{41,56,38,58,39,59,40,60,11,61,26,81,31,83,32,85,33,87,34,89},new int[]{-7,50,-6,77,-5,80,-2,55,-3,57});
    states[50] = new State(new int[]{27,75,28,76,25,-44,26,-44,18,-44,19,-44,20,-44,21,-44,22,-44,23,-44,16,-44,17,-44,15,-44,12,-44},new int[]{-12,51});
    states[51] = new State(new int[]{41,56,38,58,39,59,40,60,11,61,26,81,31,83,32,85,33,87,34,89},new int[]{-6,52,-5,80,-2,55,-3,57});
    states[52] = new State(new int[]{29,78,30,79,27,-48,28,-48,25,-48,26,-48,18,-48,19,-48,20,-48,21,-48,22,-48,23,-48,16,-48,17,-48,15,-48,12,-48},new int[]{-11,53});
    states[53] = new State(new int[]{41,56,38,58,39,59,40,60,11,61,26,81,31,83,32,85,33,87,34,89},new int[]{-5,54,-2,55,-3,57});
    states[54] = new State(-52);
    states[55] = new State(-54);
    states[56] = new State(-60);
    states[57] = new State(-61);
    states[58] = new State(-63);
    states[59] = new State(-64);
    states[60] = new State(-65);
    states[61] = new State(new int[]{41,41,38,58,39,59,40,60,11,61,26,81,31,83,32,85,33,87,34,89},new int[]{-4,62,-10,44,-9,64,-8,71,-7,74,-6,77,-5,80,-2,55,-3,57});
    states[62] = new State(new int[]{12,63});
    states[63] = new State(-62);
    states[64] = new State(new int[]{18,65,19,66,20,67,21,68,22,69,23,70,16,-33,17,-33,15,-33,12,-33},new int[]{-14,47});
    states[65] = new State(-34);
    states[66] = new State(-35);
    states[67] = new State(-36);
    states[68] = new State(-37);
    states[69] = new State(-38);
    states[70] = new State(-39);
    states[71] = new State(new int[]{25,72,26,73,18,-41,19,-41,20,-41,21,-41,22,-41,23,-41,16,-41,17,-41,15,-41,12,-41},new int[]{-13,49});
    states[72] = new State(-42);
    states[73] = new State(-43);
    states[74] = new State(new int[]{27,75,28,76,25,-45,26,-45,18,-45,19,-45,20,-45,21,-45,22,-45,23,-45,16,-45,17,-45,15,-45,12,-45},new int[]{-12,51});
    states[75] = new State(-46);
    states[76] = new State(-47);
    states[77] = new State(new int[]{29,78,30,79,27,-49,28,-49,25,-49,26,-49,18,-49,19,-49,20,-49,21,-49,22,-49,23,-49,16,-49,17,-49,15,-49,12,-49},new int[]{-11,53});
    states[78] = new State(-50);
    states[79] = new State(-51);
    states[80] = new State(-53);
    states[81] = new State(new int[]{41,56,38,58,39,59,40,60,11,61},new int[]{-2,82,-3,57});
    states[82] = new State(-55);
    states[83] = new State(new int[]{41,56,38,58,39,59,40,60,11,61},new int[]{-2,84,-3,57});
    states[84] = new State(-56);
    states[85] = new State(new int[]{41,56,38,58,39,59,40,60,11,61},new int[]{-2,86,-3,57});
    states[86] = new State(-57);
    states[87] = new State(new int[]{41,56,38,58,39,59,40,60,11,61},new int[]{-2,88,-3,57});
    states[88] = new State(-58);
    states[89] = new State(new int[]{41,56,38,58,39,59,40,60,11,61},new int[]{-2,90,-3,57});
    states[90] = new State(-59);
    states[91] = new State(-30);
    states[92] = new State(-31);
    states[93] = new State(-18);
    states[94] = new State(new int[]{15,95});
    states[95] = new State(-27);
    states[96] = new State(-5);
    states[97] = new State(new int[]{41,98});
    states[98] = new State(new int[]{15,99});
    states[99] = new State(-6);
    states[100] = new State(-7);
    states[101] = new State(-8);
    states[102] = new State(-9);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-17, new int[]{-16,3});
    rules[2] = new Rule(-16, new int[]{-18});
    rules[3] = new Rule(-18, new int[]{4,13,-19,-20,14});
    rules[4] = new Rule(-19, new int[]{});
    rules[5] = new Rule(-19, new int[]{-19,-21});
    rules[6] = new Rule(-21, new int[]{-1,41,15});
    rules[7] = new Rule(-1, new int[]{43});
    rules[8] = new Rule(-1, new int[]{44});
    rules[9] = new Rule(-1, new int[]{45});
    rules[10] = new Rule(-20, new int[]{});
    rules[11] = new Rule(-20, new int[]{-20,-22});
    rules[12] = new Rule(-22, new int[]{-23});
    rules[13] = new Rule(-22, new int[]{-24});
    rules[14] = new Rule(-22, new int[]{-25});
    rules[15] = new Rule(-22, new int[]{-26});
    rules[16] = new Rule(-22, new int[]{-27});
    rules[17] = new Rule(-22, new int[]{-28});
    rules[18] = new Rule(-22, new int[]{-29});
    rules[19] = new Rule(-23, new int[]{13,-20,14});
    rules[20] = new Rule(-24, new int[]{5,11,-4,12,-22});
    rules[21] = new Rule(-24, new int[]{5,11,-4,12,-22,6,-22});
    rules[22] = new Rule(-25, new int[]{7,11,-4,12,-22});
    rules[23] = new Rule(-26, new int[]{8,15});
    rules[24] = new Rule(-27, new int[]{9,41,15});
    rules[25] = new Rule(-28, new int[]{10,-4,15});
    rules[26] = new Rule(-28, new int[]{10,42,15});
    rules[27] = new Rule(-29, new int[]{-4,15});
    rules[28] = new Rule(-4, new int[]{41,24,-4});
    rules[29] = new Rule(-4, new int[]{-10});
    rules[30] = new Rule(-15, new int[]{16});
    rules[31] = new Rule(-15, new int[]{17});
    rules[32] = new Rule(-10, new int[]{-10,-15,-9});
    rules[33] = new Rule(-10, new int[]{-9});
    rules[34] = new Rule(-14, new int[]{18});
    rules[35] = new Rule(-14, new int[]{19});
    rules[36] = new Rule(-14, new int[]{20});
    rules[37] = new Rule(-14, new int[]{21});
    rules[38] = new Rule(-14, new int[]{22});
    rules[39] = new Rule(-14, new int[]{23});
    rules[40] = new Rule(-9, new int[]{-9,-14,-8});
    rules[41] = new Rule(-9, new int[]{-8});
    rules[42] = new Rule(-13, new int[]{25});
    rules[43] = new Rule(-13, new int[]{26});
    rules[44] = new Rule(-8, new int[]{-8,-13,-7});
    rules[45] = new Rule(-8, new int[]{-7});
    rules[46] = new Rule(-12, new int[]{27});
    rules[47] = new Rule(-12, new int[]{28});
    rules[48] = new Rule(-7, new int[]{-7,-12,-6});
    rules[49] = new Rule(-7, new int[]{-6});
    rules[50] = new Rule(-11, new int[]{29});
    rules[51] = new Rule(-11, new int[]{30});
    rules[52] = new Rule(-6, new int[]{-6,-11,-5});
    rules[53] = new Rule(-6, new int[]{-5});
    rules[54] = new Rule(-5, new int[]{-2});
    rules[55] = new Rule(-5, new int[]{26,-2});
    rules[56] = new Rule(-5, new int[]{31,-2});
    rules[57] = new Rule(-5, new int[]{32,-2});
    rules[58] = new Rule(-5, new int[]{33,-2});
    rules[59] = new Rule(-5, new int[]{34,-2});
    rules[60] = new Rule(-2, new int[]{41});
    rules[61] = new Rule(-2, new int[]{-3});
    rules[62] = new Rule(-2, new int[]{11,-4,12});
    rules[63] = new Rule(-3, new int[]{38});
    rules[64] = new Rule(-3, new int[]{39});
    rules[65] = new Rule(-3, new int[]{40});
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
      case 6: // declaration -> type, Ident, Semicolon
{
                        Compiler.DeclareVariable(ValueStack[ValueStack.Depth-3].val_type, ValueStack[ValueStack.Depth-2].s_val);
                        Compiler.AddNode(new DeclarationNode(1, ValueStack[ValueStack.Depth-3].val_type, ValueStack[ValueStack.Depth-2].s_val)); 
                    }
        break;
      case 7: // type -> Int
{ CurrentSemanticValue.val_type = ValType.Int; }
        break;
      case 8: // type -> Double
{ CurrentSemanticValue.val_type = ValType.Double; }
        break;
      case 9: // type -> Bool
{ CurrentSemanticValue.val_type = ValType.Bool; }
        break;
      case 10: // statements -> /* empty */
{
                    Compiler.AddNode(new StatementsBlockNode(0));
                }
        break;
      case 11: // statements -> statements, statement
{
                        var innerNode = Compiler.GetNode();
                        var blockNode = Compiler.GetNode() as StatementsBlockNode;
                        blockNode.AddInnerNode(innerNode);

                        Compiler.AddNode(blockNode);
                    }
        break;
      case 20: // ifStatement -> If, OpenPar, exp, ClosePar, statement
{
                        var thenStatement = Compiler.GetNode();
                        var condition = Compiler.GetNode();

                        Compiler.AddNode(new IfStatementNode(0, condition, thenStatement));
                    }
        break;
      case 21: // ifStatement -> If, OpenPar, exp, ClosePar, statement, Else, statement
{
                        var elseStatement = Compiler.GetNode();
                        var thenStatement = Compiler.GetNode();
                        var condition = Compiler.GetNode();

                        Compiler.AddNode(new IfStatementNode(0, condition, thenStatement, elseStatement));
                    }
        break;
      case 22: // whileStatement -> While, OpenPar, exp, ClosePar, statement
{
                        var thenStatement = Compiler.GetNode();
                        var condition = Compiler.GetNode();

                        Compiler.AddNode(new WhileStatementNode(0, condition, thenStatement));
                    }
        break;
      case 23: // returnStatement -> Return, Semicolon
{
                        Compiler.AddNode(new ReturnNode(0));
                    }
        break;
      case 24: // readStatement -> Read, Ident, Semicolon
{
                        Compiler.AddNode(new ReadNode(0, ValueStack[ValueStack.Depth-2].s_val));
                    }
        break;
      case 25: // writeStatement -> Write, exp, Semicolon
{
                        var expNode = Compiler.GetNode();
                        Compiler.AddNode(new WriteNode(0, expNode));
                    }
        break;
      case 26: // writeStatement -> Write, Text, Semicolon
{
                        Compiler.AddNode(new WriteNode(0, ValueStack[ValueStack.Depth-2].s_val));
                    }
        break;
      case 28: // exp -> Ident, Assign, exp
{
                        var varType = Compiler.GetVariable(ValueStack[ValueStack.Depth-3].s_val);
                        if(varType == null)
                        {
                            var error = new VariableNotDeclaredError(0, ValueStack[ValueStack.Depth-3].s_val);
                            Compiler.AddError(error);

                            varType = ValType.Dynamic;
                        }
                        else if(varType.HasValue && varType.Value != ValueStack[ValueStack.Depth-1].val_type)
                        {
                            var error = new InvalidTypeError(0, ValueStack[ValueStack.Depth-1].val_type, varType.Value);
                            Compiler.AddError(error);
                        }

                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;

                        var right = Compiler.GetNode();
                        Compiler.AddNode(new AssignmentNode(0, CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-3].s_val, right)); 
                    }
        break;
      case 29: // exp -> logExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 30: // logOp -> LogOr
{ CurrentSemanticValue.op_type = OpType.LogOr; }
        break;
      case 31: // logOp -> LogAnd
{ CurrentSemanticValue.op_type = OpType.LogAnd; }
        break;
      case 32: // logExp -> logExp, logOp, relExp
{
                        if(ValueStack[ValueStack.Depth-3].val_type != ValType.Bool && ValueStack[ValueStack.Depth-3].val_type != ValType.Dynamic)
                        {
                            var error = new InvalidTypeError(0, ValueStack[ValueStack.Depth-3].val_type, ValType.Bool);
                            Compiler.AddError(error);
                        }
                        if(ValueStack[ValueStack.Depth-1].val_type != ValType.Bool && ValueStack[ValueStack.Depth-1].val_type != ValType.Dynamic)
                        {
                            var error = new InvalidTypeError(0, ValueStack[ValueStack.Depth-1].val_type, ValType.Bool);
                            Compiler.AddError(error);
                        }

                        CurrentSemanticValue.val_type = ValType.Bool;

                        var right = Compiler.GetNode();
                        var left = Compiler.GetNode();
                        Compiler.AddNode(new BinaryOperationNode(0, CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-2].op_type, left, right)); 
                    }
        break;
      case 33: // logExp -> relExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 34: // relOp -> Equal
{ CurrentSemanticValue.op_type = OpType.Equal; }
        break;
      case 35: // relOp -> NotEqual
{ CurrentSemanticValue.op_type = OpType.NotEqual; }
        break;
      case 36: // relOp -> Greater
{ CurrentSemanticValue.op_type = OpType.Greater; }
        break;
      case 37: // relOp -> GreaterOrEqual
{ CurrentSemanticValue.op_type = OpType.GreaterOrEqual; }
        break;
      case 38: // relOp -> Less
{ CurrentSemanticValue.op_type = OpType.Less; }
        break;
      case 39: // relOp -> LessOrEqual
{ CurrentSemanticValue.op_type = OpType.LessOrEqual; }
        break;
      case 40: // relExp -> relExp, relOp, addExp
{
                        if(ValueStack[ValueStack.Depth-2].op_type != OpType.Equal && ValueStack[ValueStack.Depth-2].op_type != OpType.NotEqual)
                        {
                            if(ValueStack[ValueStack.Depth-3].val_type == ValType.Bool)
                            {
                                var error = new InvalidTypeError(0, ValueStack[ValueStack.Depth-3].val_type, ValType.Int, ValType.Double);
                                Compiler.AddError(error);
                            }
                            if(ValueStack[ValueStack.Depth-1].val_type == ValType.Bool)
                            {
                                var error = new InvalidTypeError(0, ValueStack[ValueStack.Depth-1].val_type, ValType.Int, ValType.Double);
                                Compiler.AddError(error);
                            }
                        }

                        CurrentSemanticValue.val_type = ValType.Bool;

                        var right = Compiler.GetNode();
                        var left = Compiler.GetNode();
                        Compiler.AddNode(new BinaryOperationNode(0, CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-2].op_type, left, right)); 
                    }
        break;
      case 41: // relExp -> addExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 42: // addOp -> Plus
{ CurrentSemanticValue.op_type = OpType.Plus; }
        break;
      case 43: // addOp -> Minus
{ CurrentSemanticValue.op_type = OpType.Minus; }
        break;
      case 44: // addExp -> addExp, addOp, mulExp
{
                        var invalidType = false;
                        if(ValueStack[ValueStack.Depth-3].val_type == ValType.Bool)
                        {
                            var error = new InvalidTypeError(0, ValueStack[ValueStack.Depth-3].val_type, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                            invalidType = true;
                        }
                        if(ValueStack[ValueStack.Depth-1].val_type == ValType.Bool)
                        {
                            var error = new InvalidTypeError(0, ValueStack[ValueStack.Depth-1].val_type, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                            invalidType = true;
                        }

                        if(invalidType)
                            CurrentSemanticValue.val_type = ValType.Int;
                        else
                            CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-3].val_type == ValType.Int ? ValueStack[ValueStack.Depth-1].val_type : ValType.Double;

                        var right = Compiler.GetNode();
                        var left = Compiler.GetNode();
                        Compiler.AddNode(new BinaryOperationNode(0, CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-2].op_type, left, right)); 
                    }
        break;
      case 45: // addExp -> mulExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 46: // mulOp -> Multiply
{ CurrentSemanticValue.op_type = OpType.Multiply; }
        break;
      case 47: // mulOp -> Divide
{ CurrentSemanticValue.op_type = OpType.Divide; }
        break;
      case 48: // mulExp -> mulExp, mulOp, bitExp
{
                        var invalidType = false;
                        if(ValueStack[ValueStack.Depth-3].val_type == ValType.Bool)
                        {
                            var error = new InvalidTypeError(0, ValueStack[ValueStack.Depth-3].val_type, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                            invalidType = true;
                        }
                        if(ValueStack[ValueStack.Depth-1].val_type == ValType.Bool)
                        {
                            var error = new InvalidTypeError(0, ValueStack[ValueStack.Depth-1].val_type, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                            invalidType = true;
                        }

                        if(invalidType)
                            CurrentSemanticValue.val_type = ValType.Int;
                        else
                            CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-3].val_type == ValType.Int ? ValueStack[ValueStack.Depth-1].val_type : ValType.Double;

                        var right = Compiler.GetNode();
                        var left = Compiler.GetNode();
                        Compiler.AddNode(new BinaryOperationNode(0, CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-2].op_type, left, right)); 
                    }
        break;
      case 49: // mulExp -> bitExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 50: // bitOp -> BitOr
{ CurrentSemanticValue.op_type = OpType.BitOr; }
        break;
      case 51: // bitOp -> BitAnd
{ CurrentSemanticValue.op_type = OpType.BitAnd; }
        break;
      case 52: // bitExp -> bitExp, bitOp, unaryExp
{
                        if(ValueStack[ValueStack.Depth-3].val_type != ValType.Int && ValueStack[ValueStack.Depth-3].val_type != ValType.Dynamic)
                        {
                            var error = new InvalidTypeError(0, ValueStack[ValueStack.Depth-3].val_type, ValType.Int);
                            Compiler.AddError(error);
                        }
                        if(ValueStack[ValueStack.Depth-1].val_type != ValType.Int && ValueStack[ValueStack.Depth-3].val_type != ValType.Dynamic)
                        {
                            var error = new InvalidTypeError(0, ValueStack[ValueStack.Depth-1].val_type, ValType.Int);
                            Compiler.AddError(error);
                        }

                        CurrentSemanticValue.val_type = ValType.Int;
                        
                        var right = Compiler.GetNode();
                        var left = Compiler.GetNode();
                        Compiler.AddNode(new BinaryOperationNode(0, CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-2].op_type, left, right)); 
                    }
        break;
      case 53: // bitExp -> unaryExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 54: // unaryExp -> term
{ 
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type; 
                    }
        break;
      case 55: // unaryExp -> Minus, term
{ 
                        if(ValueStack[ValueStack.Depth-1].val_type == ValType.Bool)
                        {
                            var error = new InvalidTypeError(0, ValueStack[ValueStack.Depth-1].val_type, ValType.Int, ValType.Double);
                            Compiler.AddError(error);
                        }

                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(0, CurrentSemanticValue.val_type, OpType.Minus, child)); 
                    }
        break;
      case 56: // unaryExp -> BitNot, term
{ 
                        if(ValueStack[ValueStack.Depth-1].val_type == ValType.Double || ValueStack[ValueStack.Depth-1].val_type == ValType.Bool)
                        {
                            var error = new InvalidTypeError(0, ValueStack[ValueStack.Depth-1].val_type, ValType.Int);
                            Compiler.AddError(error);
                        }

                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(0, CurrentSemanticValue.val_type, OpType.BitNot, child)); 
                    }
        break;
      case 57: // unaryExp -> LogNot, term
{
                        if(ValueStack[ValueStack.Depth-1].val_type == ValType.Int || ValueStack[ValueStack.Depth-1].val_type == ValType.Double)
                        {
                            var error = new InvalidTypeError(0, ValueStack[ValueStack.Depth-1].val_type, ValType.Bool);
                            Compiler.AddError(error);
                        }

                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(0, CurrentSemanticValue.val_type, OpType.LogNot, child)); 
                    }
        break;
      case 58: // unaryExp -> IntCast, term
{
                        CurrentSemanticValue.val_type = ValType.Int;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(0, CurrentSemanticValue.val_type, OpType.IntCast, child)); 
                    }
        break;
      case 59: // unaryExp -> DoubleCast, term
{
                        CurrentSemanticValue.val_type = ValType.Double;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(0, CurrentSemanticValue.val_type, OpType.DoubleCast, child)); 
                    }
        break;
      case 60: // term -> Ident
{
                        // Throw if variable not declared, else get ValueType
                        var varType =  Compiler.GetVariable(ValueStack[ValueStack.Depth-1].s_val);
                        if(varType == null)
                        {
                            var error = new VariableNotDeclaredError(0, ValueStack[ValueStack.Depth-1].s_val);
                            Compiler.AddError(error);

                            varType = ValType.Dynamic;
                        }

                        CurrentSemanticValue.val_type = varType.Value;
                        Compiler.AddNode(new VariableNode(0, CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-1].s_val)); 
                    }
        break;
      case 61: // term -> const
{ 
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 62: // term -> OpenPar, exp, ClosePar
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-2].val_type;
                    }
        break;
      case 63: // const -> IntValue
{
                        CurrentSemanticValue.val_type = ValType.Int;
                        Compiler.AddNode(new ConstantNode(0, CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-1].i_val)); 
                    }
        break;
      case 64: // const -> DoubleValue
{ 
                        CurrentSemanticValue.val_type = ValType.Double;
                        Compiler.AddNode(new ConstantNode(0, CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-1].d_val)); 
                    }
        break;
      case 65: // const -> BoolValue
{ 
                        CurrentSemanticValue.val_type = ValType.Bool;
                        Compiler.AddNode(new ConstantNode(0, CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-1].b_val)); 
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
