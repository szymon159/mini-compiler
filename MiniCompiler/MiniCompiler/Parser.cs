// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  DESKTOP-LRNG15B
// DateTime: 27.06.2020 23:34:21
// UserName: szymo
// Input file <../../kompilator.y - 27.06.2020 23:32:32>

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
  private static Rule[] rules = new Rule[67];
  private static State[] states = new State[104];
  private static string[] nonTerms = new string[] {
      "type", "term", "const", "exp", "unaryExp", "bitExp", "mulExp", "addExp", 
      "relExp", "logExp", "bitOp", "mulOp", "addOp", "relOp", "logOp", "start", 
      "$accept", "program", "declarations", "statement", "declaration", "statements", 
      "block", "ifStatement", "whileStatement", "returnStatement", "readStatement", 
      "writeStatement", "expStatement", };

  static Parser() {
    states[0] = new State(new int[]{4,4},new int[]{-16,1,-18,3});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(new int[]{13,5});
    states[5] = new State(-5,new int[]{-19,6});
    states[6] = new State(new int[]{14,7,13,12,5,17,7,25,8,31,9,34,10,38,41,45,38,62,39,63,40,64,11,65,26,85,31,87,32,89,33,91,34,93,43,101,44,102,45,103},new int[]{-20,8,-21,10,-23,11,-24,16,-25,24,-26,30,-27,33,-28,37,-29,97,-4,43,-10,48,-9,68,-8,75,-7,78,-6,81,-5,84,-2,59,-3,61,-1,98});
    states[7] = new State(-3);
    states[8] = new State(new int[]{14,9});
    states[9] = new State(-4);
    states[10] = new State(-6);
    states[11] = new State(-13);
    states[12] = new State(-11,new int[]{-22,13});
    states[13] = new State(new int[]{14,14,13,12,5,17,7,25,8,31,9,34,10,38,41,45,38,62,39,63,40,64,11,65,26,85,31,87,32,89,33,91,34,93},new int[]{-20,15,-23,11,-24,16,-25,24,-26,30,-27,33,-28,37,-29,97,-4,43,-10,48,-9,68,-8,75,-7,78,-6,81,-5,84,-2,59,-3,61});
    states[14] = new State(-20);
    states[15] = new State(-12);
    states[16] = new State(-14);
    states[17] = new State(new int[]{11,18});
    states[18] = new State(new int[]{41,45,38,62,39,63,40,64,11,65,26,85,31,87,32,89,33,91,34,93},new int[]{-4,19,-10,48,-9,68,-8,75,-7,78,-6,81,-5,84,-2,59,-3,61});
    states[19] = new State(new int[]{12,20});
    states[20] = new State(new int[]{13,12,5,17,7,25,8,31,9,34,10,38,41,45,38,62,39,63,40,64,11,65,26,85,31,87,32,89,33,91,34,93},new int[]{-20,21,-23,11,-24,16,-25,24,-26,30,-27,33,-28,37,-29,97,-4,43,-10,48,-9,68,-8,75,-7,78,-6,81,-5,84,-2,59,-3,61});
    states[21] = new State(new int[]{6,22,14,-21,13,-21,5,-21,7,-21,8,-21,9,-21,10,-21,41,-21,38,-21,39,-21,40,-21,11,-21,26,-21,31,-21,32,-21,33,-21,34,-21});
    states[22] = new State(new int[]{13,12,5,17,7,25,8,31,9,34,10,38,41,45,38,62,39,63,40,64,11,65,26,85,31,87,32,89,33,91,34,93},new int[]{-20,23,-23,11,-24,16,-25,24,-26,30,-27,33,-28,37,-29,97,-4,43,-10,48,-9,68,-8,75,-7,78,-6,81,-5,84,-2,59,-3,61});
    states[23] = new State(-22);
    states[24] = new State(-15);
    states[25] = new State(new int[]{11,26});
    states[26] = new State(new int[]{41,45,38,62,39,63,40,64,11,65,26,85,31,87,32,89,33,91,34,93},new int[]{-4,27,-10,48,-9,68,-8,75,-7,78,-6,81,-5,84,-2,59,-3,61});
    states[27] = new State(new int[]{12,28});
    states[28] = new State(new int[]{13,12,5,17,7,25,8,31,9,34,10,38,41,45,38,62,39,63,40,64,11,65,26,85,31,87,32,89,33,91,34,93},new int[]{-20,29,-23,11,-24,16,-25,24,-26,30,-27,33,-28,37,-29,97,-4,43,-10,48,-9,68,-8,75,-7,78,-6,81,-5,84,-2,59,-3,61});
    states[29] = new State(-23);
    states[30] = new State(-16);
    states[31] = new State(new int[]{15,32});
    states[32] = new State(-24);
    states[33] = new State(-17);
    states[34] = new State(new int[]{41,35});
    states[35] = new State(new int[]{15,36});
    states[36] = new State(-25);
    states[37] = new State(-18);
    states[38] = new State(new int[]{42,41,41,45,38,62,39,63,40,64,11,65,26,85,31,87,32,89,33,91,34,93},new int[]{-29,39,-4,43,-10,48,-9,68,-8,75,-7,78,-6,81,-5,84,-2,59,-3,61});
    states[39] = new State(new int[]{15,40});
    states[40] = new State(-26);
    states[41] = new State(new int[]{15,42});
    states[42] = new State(-27);
    states[43] = new State(new int[]{15,44});
    states[44] = new State(-28);
    states[45] = new State(new int[]{24,46,29,-61,30,-61,27,-61,28,-61,25,-61,26,-61,18,-61,19,-61,20,-61,21,-61,22,-61,23,-61,16,-61,17,-61,15,-61,12,-61});
    states[46] = new State(new int[]{41,45,38,62,39,63,40,64,11,65,26,85,31,87,32,89,33,91,34,93},new int[]{-4,47,-10,48,-9,68,-8,75,-7,78,-6,81,-5,84,-2,59,-3,61});
    states[47] = new State(-29);
    states[48] = new State(new int[]{16,95,17,96,15,-30,12,-30},new int[]{-15,49});
    states[49] = new State(new int[]{41,60,38,62,39,63,40,64,11,65,26,85,31,87,32,89,33,91,34,93},new int[]{-9,50,-8,75,-7,78,-6,81,-5,84,-2,59,-3,61});
    states[50] = new State(new int[]{18,69,19,70,20,71,21,72,22,73,23,74,16,-33,17,-33,15,-33,12,-33},new int[]{-14,51});
    states[51] = new State(new int[]{41,60,38,62,39,63,40,64,11,65,26,85,31,87,32,89,33,91,34,93},new int[]{-8,52,-7,78,-6,81,-5,84,-2,59,-3,61});
    states[52] = new State(new int[]{25,76,26,77,18,-41,19,-41,20,-41,21,-41,22,-41,23,-41,16,-41,17,-41,15,-41,12,-41},new int[]{-13,53});
    states[53] = new State(new int[]{41,60,38,62,39,63,40,64,11,65,26,85,31,87,32,89,33,91,34,93},new int[]{-7,54,-6,81,-5,84,-2,59,-3,61});
    states[54] = new State(new int[]{27,79,28,80,25,-45,26,-45,18,-45,19,-45,20,-45,21,-45,22,-45,23,-45,16,-45,17,-45,15,-45,12,-45},new int[]{-12,55});
    states[55] = new State(new int[]{41,60,38,62,39,63,40,64,11,65,26,85,31,87,32,89,33,91,34,93},new int[]{-6,56,-5,84,-2,59,-3,61});
    states[56] = new State(new int[]{29,82,30,83,27,-49,28,-49,25,-49,26,-49,18,-49,19,-49,20,-49,21,-49,22,-49,23,-49,16,-49,17,-49,15,-49,12,-49},new int[]{-11,57});
    states[57] = new State(new int[]{41,60,38,62,39,63,40,64,11,65,26,85,31,87,32,89,33,91,34,93},new int[]{-5,58,-2,59,-3,61});
    states[58] = new State(-53);
    states[59] = new State(-55);
    states[60] = new State(-61);
    states[61] = new State(-62);
    states[62] = new State(-64);
    states[63] = new State(-65);
    states[64] = new State(-66);
    states[65] = new State(new int[]{41,45,38,62,39,63,40,64,11,65,26,85,31,87,32,89,33,91,34,93},new int[]{-4,66,-10,48,-9,68,-8,75,-7,78,-6,81,-5,84,-2,59,-3,61});
    states[66] = new State(new int[]{12,67});
    states[67] = new State(-63);
    states[68] = new State(new int[]{18,69,19,70,20,71,21,72,22,73,23,74,16,-34,17,-34,15,-34,12,-34},new int[]{-14,51});
    states[69] = new State(-35);
    states[70] = new State(-36);
    states[71] = new State(-37);
    states[72] = new State(-38);
    states[73] = new State(-39);
    states[74] = new State(-40);
    states[75] = new State(new int[]{25,76,26,77,18,-42,19,-42,20,-42,21,-42,22,-42,23,-42,16,-42,17,-42,15,-42,12,-42},new int[]{-13,53});
    states[76] = new State(-43);
    states[77] = new State(-44);
    states[78] = new State(new int[]{27,79,28,80,25,-46,26,-46,18,-46,19,-46,20,-46,21,-46,22,-46,23,-46,16,-46,17,-46,15,-46,12,-46},new int[]{-12,55});
    states[79] = new State(-47);
    states[80] = new State(-48);
    states[81] = new State(new int[]{29,82,30,83,27,-50,28,-50,25,-50,26,-50,18,-50,19,-50,20,-50,21,-50,22,-50,23,-50,16,-50,17,-50,15,-50,12,-50},new int[]{-11,57});
    states[82] = new State(-51);
    states[83] = new State(-52);
    states[84] = new State(-54);
    states[85] = new State(new int[]{41,60,38,62,39,63,40,64,11,65},new int[]{-2,86,-3,61});
    states[86] = new State(-56);
    states[87] = new State(new int[]{41,60,38,62,39,63,40,64,11,65},new int[]{-2,88,-3,61});
    states[88] = new State(-57);
    states[89] = new State(new int[]{41,60,38,62,39,63,40,64,11,65},new int[]{-2,90,-3,61});
    states[90] = new State(-58);
    states[91] = new State(new int[]{41,60,38,62,39,63,40,64,11,65},new int[]{-2,92,-3,61});
    states[92] = new State(-59);
    states[93] = new State(new int[]{41,60,38,62,39,63,40,64,11,65},new int[]{-2,94,-3,61});
    states[94] = new State(-60);
    states[95] = new State(-31);
    states[96] = new State(-32);
    states[97] = new State(-19);
    states[98] = new State(new int[]{41,99});
    states[99] = new State(new int[]{15,100});
    states[100] = new State(-7);
    states[101] = new State(-8);
    states[102] = new State(-9);
    states[103] = new State(-10);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-17, new int[]{-16,3});
    rules[2] = new Rule(-16, new int[]{-18});
    rules[3] = new Rule(-18, new int[]{4,13,-19,14});
    rules[4] = new Rule(-18, new int[]{4,13,-19,-20,14});
    rules[5] = new Rule(-19, new int[]{});
    rules[6] = new Rule(-19, new int[]{-19,-21});
    rules[7] = new Rule(-21, new int[]{-1,41,15});
    rules[8] = new Rule(-1, new int[]{43});
    rules[9] = new Rule(-1, new int[]{44});
    rules[10] = new Rule(-1, new int[]{45});
    rules[11] = new Rule(-22, new int[]{});
    rules[12] = new Rule(-22, new int[]{-22,-20});
    rules[13] = new Rule(-20, new int[]{-23});
    rules[14] = new Rule(-20, new int[]{-24});
    rules[15] = new Rule(-20, new int[]{-25});
    rules[16] = new Rule(-20, new int[]{-26});
    rules[17] = new Rule(-20, new int[]{-27});
    rules[18] = new Rule(-20, new int[]{-28});
    rules[19] = new Rule(-20, new int[]{-29});
    rules[20] = new Rule(-23, new int[]{13,-22,14});
    rules[21] = new Rule(-24, new int[]{5,11,-4,12,-20});
    rules[22] = new Rule(-24, new int[]{5,11,-4,12,-20,6,-20});
    rules[23] = new Rule(-25, new int[]{7,11,-4,12,-20});
    rules[24] = new Rule(-26, new int[]{8,15});
    rules[25] = new Rule(-27, new int[]{9,41,15});
    rules[26] = new Rule(-28, new int[]{10,-29,15});
    rules[27] = new Rule(-28, new int[]{10,42,15});
    rules[28] = new Rule(-29, new int[]{-4,15});
    rules[29] = new Rule(-4, new int[]{41,24,-4});
    rules[30] = new Rule(-4, new int[]{-10});
    rules[31] = new Rule(-15, new int[]{16});
    rules[32] = new Rule(-15, new int[]{17});
    rules[33] = new Rule(-10, new int[]{-10,-15,-9});
    rules[34] = new Rule(-10, new int[]{-9});
    rules[35] = new Rule(-14, new int[]{18});
    rules[36] = new Rule(-14, new int[]{19});
    rules[37] = new Rule(-14, new int[]{20});
    rules[38] = new Rule(-14, new int[]{21});
    rules[39] = new Rule(-14, new int[]{22});
    rules[40] = new Rule(-14, new int[]{23});
    rules[41] = new Rule(-9, new int[]{-9,-14,-8});
    rules[42] = new Rule(-9, new int[]{-8});
    rules[43] = new Rule(-13, new int[]{25});
    rules[44] = new Rule(-13, new int[]{26});
    rules[45] = new Rule(-8, new int[]{-8,-13,-7});
    rules[46] = new Rule(-8, new int[]{-7});
    rules[47] = new Rule(-12, new int[]{27});
    rules[48] = new Rule(-12, new int[]{28});
    rules[49] = new Rule(-7, new int[]{-7,-12,-6});
    rules[50] = new Rule(-7, new int[]{-6});
    rules[51] = new Rule(-11, new int[]{29});
    rules[52] = new Rule(-11, new int[]{30});
    rules[53] = new Rule(-6, new int[]{-6,-11,-5});
    rules[54] = new Rule(-6, new int[]{-5});
    rules[55] = new Rule(-5, new int[]{-2});
    rules[56] = new Rule(-5, new int[]{26,-2});
    rules[57] = new Rule(-5, new int[]{31,-2});
    rules[58] = new Rule(-5, new int[]{32,-2});
    rules[59] = new Rule(-5, new int[]{33,-2});
    rules[60] = new Rule(-5, new int[]{34,-2});
    rules[61] = new Rule(-2, new int[]{41});
    rules[62] = new Rule(-2, new int[]{-3});
    rules[63] = new Rule(-2, new int[]{11,-4,12});
    rules[64] = new Rule(-3, new int[]{38});
    rules[65] = new Rule(-3, new int[]{39});
    rules[66] = new Rule(-3, new int[]{40});
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
      case 7: // declaration -> type, Ident, Semicolon
{
                        Compiler.DeclareVariable(ValueStack[ValueStack.Depth-3].val_type, ValueStack[ValueStack.Depth-2].s_val);
                        Compiler.AddNode(new DeclarationNode(1, ValueStack[ValueStack.Depth-3].val_type, ValueStack[ValueStack.Depth-2].s_val)); 
                    }
        break;
      case 8: // type -> Int
{ CurrentSemanticValue.val_type = ValType.Int; }
        break;
      case 9: // type -> Double
{ CurrentSemanticValue.val_type = ValType.Double; }
        break;
      case 10: // type -> Bool
{ CurrentSemanticValue.val_type = ValType.Bool; }
        break;
      case 11: // statements -> /* empty */
{
                    Compiler.AddNode(new StatementsBlockNode(0));
                }
        break;
      case 12: // statements -> statements, statement
{
                        var innerNode = Compiler.GetNode();
                        var blockNode = Compiler.GetNode() as StatementsBlockNode;
                        blockNode.AddInnerNode(innerNode);

                        Compiler.AddNode(blockNode);
                    }
        break;
      case 21: // ifStatement -> If, OpenPar, exp, ClosePar, statement
{
                        var thenStatement = Compiler.GetNode();
                        var condition = Compiler.GetNode();

                        Compiler.AddNode(new IfStatementNode(0, condition, thenStatement));
                    }
        break;
      case 22: // ifStatement -> If, OpenPar, exp, ClosePar, statement, Else, statement
{
                        var elseStatement = Compiler.GetNode();
                        var thenStatement = Compiler.GetNode();
                        var condition = Compiler.GetNode();

                        Compiler.AddNode(new IfStatementNode(0, condition, thenStatement, elseStatement));
                    }
        break;
      case 29: // exp -> Ident, Assign, exp
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
      case 30: // exp -> logExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 31: // logOp -> LogOr
{ CurrentSemanticValue.op_type = OpType.LogOr; }
        break;
      case 32: // logOp -> LogAnd
{ CurrentSemanticValue.op_type = OpType.LogAnd; }
        break;
      case 33: // logExp -> logExp, logOp, relExp
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
      case 34: // logExp -> relExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 35: // relOp -> Equal
{ CurrentSemanticValue.op_type = OpType.Equal; }
        break;
      case 36: // relOp -> NotEqual
{ CurrentSemanticValue.op_type = OpType.NotEqual; }
        break;
      case 37: // relOp -> Greater
{ CurrentSemanticValue.op_type = OpType.Greater; }
        break;
      case 38: // relOp -> GreaterOrEqual
{ CurrentSemanticValue.op_type = OpType.GreaterOrEqual; }
        break;
      case 39: // relOp -> Less
{ CurrentSemanticValue.op_type = OpType.Less; }
        break;
      case 40: // relOp -> LessOrEqual
{ CurrentSemanticValue.op_type = OpType.LessOrEqual; }
        break;
      case 41: // relExp -> relExp, relOp, addExp
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
      case 42: // relExp -> addExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 43: // addOp -> Plus
{ CurrentSemanticValue.op_type = OpType.Plus; }
        break;
      case 44: // addOp -> Minus
{ CurrentSemanticValue.op_type = OpType.Minus; }
        break;
      case 45: // addExp -> addExp, addOp, mulExp
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
      case 46: // addExp -> mulExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 47: // mulOp -> Multiply
{ CurrentSemanticValue.op_type = OpType.Multiply; }
        break;
      case 48: // mulOp -> Divide
{ CurrentSemanticValue.op_type = OpType.Divide; }
        break;
      case 49: // mulExp -> mulExp, mulOp, bitExp
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
      case 50: // mulExp -> bitExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 51: // bitOp -> BitOr
{ CurrentSemanticValue.op_type = OpType.BitOr; }
        break;
      case 52: // bitOp -> BitAnd
{ CurrentSemanticValue.op_type = OpType.BitAnd; }
        break;
      case 53: // bitExp -> bitExp, bitOp, unaryExp
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
      case 54: // bitExp -> unaryExp
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 55: // unaryExp -> term
{ 
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type; 
                    }
        break;
      case 56: // unaryExp -> Minus, term
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
      case 57: // unaryExp -> BitNot, term
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
      case 58: // unaryExp -> LogNot, term
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
      case 59: // unaryExp -> IntCast, term
{
                        CurrentSemanticValue.val_type = ValType.Int;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(0, CurrentSemanticValue.val_type, OpType.IntCast, child)); 
                    }
        break;
      case 60: // unaryExp -> DoubleCast, term
{
                        CurrentSemanticValue.val_type = ValType.Double;

                        var child = Compiler.GetNode();
                        Compiler.AddNode(new UnaryOperationNode(0, CurrentSemanticValue.val_type, OpType.DoubleCast, child)); 
                    }
        break;
      case 61: // term -> Ident
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
      case 62: // term -> const
{ 
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-1].val_type;
                    }
        break;
      case 63: // term -> OpenPar, exp, ClosePar
{
                        CurrentSemanticValue.val_type = ValueStack[ValueStack.Depth-2].val_type;
                    }
        break;
      case 64: // const -> IntValue
{
                        CurrentSemanticValue.val_type = ValType.Int;
                        Compiler.AddNode(new ConstantNode(0, CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-1].i_val)); 
                    }
        break;
      case 65: // const -> DoubleValue
{ 
                        CurrentSemanticValue.val_type = ValType.Double;
                        Compiler.AddNode(new ConstantNode(0, CurrentSemanticValue.val_type, ValueStack[ValueStack.Depth-1].d_val)); 
                    }
        break;
      case 66: // const -> BoolValue
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
