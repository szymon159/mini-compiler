using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using MiniCompiler;

public enum ValType
{
    Int,
    Double,
    Bool,
    None,
    
    // Used for compiler declaration of undeclared variable - can be used as int, double, bool
    Dynamic 
}

public enum OpType
{
    LogOr, 
    LogAnd, 
    Equal,
    NotEqual, 
    Greater, 
    GreaterOrEqual, 
    Less, 
    LessOrEqual, 
    Assign,
    Plus, 
    Minus, 
    Multiply, 
    Divide, 
    BitOr, 
    BitAnd,
    BitNot, 
    LogNot, 
    IntCast, 
    DoubleCast
}

public class Compiler
{
    private static Stack<SyntaxTreeNode> code = new Stack<SyntaxTreeNode>();
    private static Dictionary<string, ValType> variables = new Dictionary<string, ValType>();
    private static List<MiniCompilerError> errors = new List<MiniCompilerError>();
    private static int labelNo = 0;
    private static int currentLineNo = 1;
    private static StreamWriter streamWriter;

    public static string GetReturnLabel() => "LAB_RET";

    public static int Main(string[] args)
    {
        // TODO: Remove it
#if DEBUG
        args = new string[] { "./code.mini" };
#endif

        string inputFile;
        if (args.Length >= 1)
        {
            inputFile = args[0];
        }
        else
        {
            Console.WriteLine("Invalid argment: Source code file not provided");
            return 1;
        }
        using (var source = new FileStream(inputFile, FileMode.Open))
        {
            var scanner = new Scanner(source);
            var parser = new Parser(scanner);

            parser.Parse();
            if (errors.Count == 0)
            {
                Console.WriteLine("SUCCESS");
                var parsedCode = code.Reverse();
                var outputFile = inputFile + ".il";
                streamWriter = new StreamWriter(outputFile);

                GenProlog();
                foreach (var statement in parsedCode)
                    statement.GenCode();
                GenEpilog();
                streamWriter.Dispose();
            }
            else
            {
                Console.WriteLine("FAILURE");
                Console.WriteLine($"Found {errors.Count} errors:");
                var syntaxErrors = errors.Where(e => e.IsSyntaxError);
                var otherErrors = errors.Where(e => !e.IsSyntaxError);
                if(syntaxErrors.Count() > 0)
                    Console.WriteLine($"Syntax errors ({syntaxErrors.Count()}):");
                foreach (var error in syntaxErrors)
                    Console.WriteLine($"* {error}");

                if (otherErrors.Count() > 0)
                    Console.WriteLine($"Other errors ({otherErrors.Count()}):");
                foreach (var error in otherErrors)
                    Console.WriteLine($"* {error}");

                // TODO: Remove it
#if DEBUG
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
#endif

                return 2;
            }
        }

        // TODO: Remove it
#if DEBUG
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
#endif

        return 0;
    }

    public static void Breakpoint()
    {

    }

    public static void AddNode(SyntaxTreeNode node)
    {
        code.Push(node);
    }

    public static SyntaxTreeNode GetNode()
    {
        return code.Count != 0 ? code.Pop() : null;
    }

    public static void DeclareVariable(ValType type, string name)
    {
        variables.Add(name, type);
    }

    public static ValType? GetVariable(string name)
    {
        if (variables.TryGetValue(name, out var result))
            return result;
        
        return null;
    }

    public static void AddError(MiniCompilerError error)
    {
        errors.Add(error);
    }

    public static string GenerateLabel()
    {
        return $"LAB_{labelNo++}";
    }

    public static void GenProlog()
    {
        EmitCode(".assembly extern mscorlib { }", false);
        EmitCode(".assembly kompilator { }", false);
        EmitCode(".method static void main()", false);
        EmitCode("{", false);
        EmitCode(".entrypoint", false);
        EmitCode(".maxstack 8", false);
        EmitCode(".try", false);
        EmitCode("{", false);
    }

    public static void EmitCode(string code, bool addLabel = true, string label = null)
    {
        if (label == null && addLabel)
            label = GenerateLabel();

        if(addLabel)
            streamWriter.Write($"{label}: {code}\n");
        else
            streamWriter.Write($"{code}\n");
    }

    public static void GenEpilog()
    {
        EmitCode($"leave {GetReturnLabel()}");
        EmitCode("}", false);

        EmitCode("catch [mscorlib]System.Exception", false);
        EmitCode("{", false);
        EmitCode("callvirt instance string [mscorlib]System.Exception::get_Message()");
        EmitCode("call void [mscorlib]System.Console::WriteLine(string)");
        EmitCode($"leave {GetReturnLabel()}");
        EmitCode("}", false);

        EmitCode("ret", true, GetReturnLabel());
        EmitCode("}",false);
    }

    public static void Pop()
    {
        // Get node from the top and add generating "pop" at CIL stack after generating code
        // Necessary to clean value on stack after expression without assignment
        var topNode = code.Peek();
        topNode.EnablePopGenerator();
    }

    public static void IncrementLineNumber()
    {
        ++currentLineNo;
    }

    public static int GetLineNumber()
    {
        return currentLineNo;
    }
}

#region  Syntax Tree

public abstract class SyntaxTreeNode
{
    public int LineNo = -1;
    public ValType Type;
    protected bool GenPop;

    public SyntaxTreeNode(int lineNo, ValType type = ValType.None)
    {
        LineNo = lineNo;
        Type = type;
    }

    public void EnablePopGenerator()
    {
        GenPop = true;
    }

    public abstract string GenCode();
}

public class DeclarationNode: SyntaxTreeNode
{
    private string Name { get; set; }

    public DeclarationNode(int lineNo, ValType type, string name)
        :base(lineNo, type)
    {
        Name = name;
    }

    public override string GenCode()
    {
        string text = "";
        switch (Type)
        {
            case ValType.Int:
                text = $".locals init ( int32 _{Name} )";
                break;
            case ValType.Double:
                text = $".locals init ( float64 _{Name} )";
                break;
            case ValType.Bool:
                text = $".locals init ( int32 _{Name} )";
                break;
            default:
                Compiler.AddError(new UndefinedError(LineNo));
                return text;
        }
        Compiler.EmitCode(text, false);

        return text;
    }
}

public class BinaryOperationNode : SyntaxTreeNode
{
    protected OpType OperatorType { get; set; }
    protected SyntaxTreeNode Left { get; set; }
    protected SyntaxTreeNode Right { get; set; }

    public BinaryOperationNode(int lineNo, ValType type, OpType operatorType, SyntaxTreeNode left, SyntaxTreeNode right)
        :base(lineNo, type)
    {
        OperatorType = operatorType;
        Left = left;
        Right = right;
    }

    public override string GenCode()
    {
        // Implicit cast int -> double
        // Generate fake node with explicit cast
        SyntaxTreeNode tempNode;
        if (Left.Type == ValType.Int && Right.Type == ValType.Double)
        {
            tempNode = new UnaryOperationNode(LineNo, ValType.Double, OpType.DoubleCast, Left);
            Left = tempNode;
        }
        else if (Left.Type == ValType.Double && Right.Type == ValType.Int)
        {
            tempNode = new UnaryOperationNode(LineNo, ValType.Double, OpType.DoubleCast, Right);
            Right = tempNode;
        }

        // Optimize calculations in those cases - done in switch below
        if(OperatorType != OpType.LogOr && OperatorType != OpType.LogAnd)
        {
            Left?.GenCode();
            Right?.GenCode();
        }

        string text = "";
        var helperNode = new UnaryOperationNode(-1, ValType.Bool, OpType.LogNot, null);
        switch (OperatorType)
        {
            case OpType.LogOr:
                var falseLabel = Compiler.GenerateLabel();
                var trueLabel = Compiler.GenerateLabel();
                var endLabel = Compiler.GenerateLabel();
                Left.GenCode();
                Compiler.EmitCode($"brtrue {trueLabel}");
                Right.GenCode();
                Compiler.EmitCode($"brtrue {trueLabel}");
                Compiler.EmitCode("ldc.i4.0", true, falseLabel);
                Compiler.EmitCode($"br {endLabel}");
                Compiler.EmitCode("ldc.i4.1", true, trueLabel);
                Compiler.EmitCode("nop", true, endLabel);
                break;
            case OpType.LogAnd:
                falseLabel = Compiler.GenerateLabel();
                trueLabel = Compiler.GenerateLabel();
                endLabel = Compiler.GenerateLabel();
                Left.GenCode();
                Compiler.EmitCode($"brfalse {falseLabel}");
                Right.GenCode();
                Compiler.EmitCode($"brtrue {trueLabel}");
                Compiler.EmitCode("ldc.i4.0", true, falseLabel);
                Compiler.EmitCode($"br {endLabel}");
                Compiler.EmitCode("ldc.i4.1", true, trueLabel);
                Compiler.EmitCode("nop", true, endLabel);
                break;
            case OpType.Equal:
                text = "ceq";
                break;
            case OpType.NotEqual:
                // Check for equality
                // And negate stack top (equality result)
                Compiler.EmitCode("ceq");
                helperNode.GenCode();
                break;
            case OpType.Greater:
                text = "cgt";
                break;
            case OpType.GreaterOrEqual:
                // Check for less
                // And negate stack top (less result)
                Compiler.EmitCode("clt");
                helperNode.GenCode();
                break;
            case OpType.Less:
                text = "clt";
                break;
            case OpType.LessOrEqual:
                // Check for greater
                // And negate stack top (greater result)
                Compiler.EmitCode("cgt");
                helperNode.GenCode();
                break;
            case OpType.Plus:
                text = "add";
                break;
            case OpType.Minus:
                text = "sub";
                break;
            case OpType.Multiply:
                text = "mul";
                break;
            case OpType.Divide:
                text = "div";
                break;
            case OpType.BitOr:
                text = "or";
                break;
            case OpType.BitAnd:
                text = "and";
                break;
            default:
                Compiler.AddError(new UndefinedError(LineNo));
                break;
        }

        Compiler.EmitCode(text);
        if (GenPop)
            Compiler.EmitCode("pop", true);

        return text;
    }
}

public class AssignmentNode : BinaryOperationNode
{
    private string Name { get; set; }

    public AssignmentNode(int lineNo, ValType type, string name, SyntaxTreeNode right)
        :base(lineNo, type, OpType.Assign, null, right)
    {
        Name = name;
    }

    public override string GenCode()
    {
        var text = $"stloc _{Name}";

        Right?.GenCode();
        Compiler.EmitCode(text);
        if (!GenPop)
            Compiler.EmitCode($"ldloc _{Name}");

        return text;
    }
}

public class UnaryOperationNode : SyntaxTreeNode
{
    private OpType OperatorType { get; set; }
    private SyntaxTreeNode Child { get; set; }

    public UnaryOperationNode(int lineNo, ValType type, OpType operatorType, SyntaxTreeNode child)
        : base(lineNo, type)
    {
        OperatorType = operatorType;
        Child = child;
    }

    public override string GenCode()
    {
        Child?.GenCode();

        string text = "";
        switch (OperatorType)
        {
            case OpType.Minus:
                text = "neg";
                break;
            case OpType.BitNot:
                text = "not";
                break;
            case OpType.LogNot:
                // Create fake node with const 1
                // Add it to stack and call bit xor with old stack top
                var helperNode = new ConstantNode(-1, ValType.Bool, true);
                helperNode.GenCode();
                text = "xor";
                break;
            case OpType.IntCast:
                text = "conv.i4";
                break;
            case OpType.DoubleCast:
                text = "conv.r8";
                break;
            default:
                Compiler.AddError(new UndefinedError(LineNo));
                break;
        }

        Compiler.EmitCode(text);
        if (GenPop)
            Compiler.EmitCode("pop", true);

        return text;
    }
}

public class VariableNode : SyntaxTreeNode
{
    private string Name { get; set; }

    public VariableNode(int lineNo, ValType type, string name)
        : base(lineNo, type)
    {
        Name = name;
    }

    public override string GenCode()
    {
        var text = $"ldloc _{Name}";
        Compiler.EmitCode(text);
        if (GenPop)
            Compiler.EmitCode("pop", true);

        return text;
    }
}

public class ConstantNode : SyntaxTreeNode
{
    private int IntValue { get; set; }
    private double DoubleValue { get; set; }
    private bool BoolValue { get; set; }

    public ConstantNode(int lineNo, ValType type, int intValue)
        : base(lineNo, type)
    {
        IntValue = intValue;
    }

    public ConstantNode(int lineNo, ValType type, double doubleValue)
        : base(lineNo, type)
    {
        DoubleValue = doubleValue;
    }

    public ConstantNode(int lineNo, ValType type, bool boolValue)
        : base(lineNo, type)
    {
        BoolValue = boolValue;
    }

    public override string GenCode()
    {
        string text = "";
        switch (Type)
        {
            case ValType.Int:
                text = $"ldc.i4 {IntValue}";
                break;
            case ValType.Double:
                text = $"ldc.r8 {string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0}",DoubleValue)}";
                break;
            case ValType.Bool:
                text = $"ldc.i4 {(BoolValue ? 1 : 0)}";
                break;
            default:
                break;
        }

        Compiler.EmitCode(text);
        if (GenPop)
            Compiler.EmitCode("pop", true);

        return text;
    }
}

public class StatementsBlockNode : SyntaxTreeNode
{
    private List<SyntaxTreeNode> innerNodes;

    public StatementsBlockNode(int lineNo)
        :base(lineNo)
    {
        innerNodes = new List<SyntaxTreeNode>();
    }

    public void AddInnerNode(SyntaxTreeNode node)
    {
        innerNodes.Add(node);
    }

    public override string GenCode()
    {
        foreach (var node in innerNodes)
            node.GenCode();

        return "";
    }
}

public class IfStatementNode : SyntaxTreeNode
{
    private SyntaxTreeNode Condition { get; set; }
    private SyntaxTreeNode ThenStatement { get; set; }
    private SyntaxTreeNode ElseStatement { get; set; }

    public IfStatementNode(int lineNo, SyntaxTreeNode condition, SyntaxTreeNode thenStatement, SyntaxTreeNode elseStatement = null)
        :base(lineNo)
    {
        Condition = condition;
        ThenStatement = thenStatement;
        ElseStatement = elseStatement;
    }

    public override string GenCode()
    {
        Condition.GenCode();
        string elseLabel = "";

        if (ElseStatement != null)
        {
            elseLabel = Compiler.GenerateLabel();
            Compiler.EmitCode($"brfalse {elseLabel}");
        }
        ThenStatement.GenCode();

        if (ElseStatement != null)
        {
            var afterLabel = Compiler.GenerateLabel();
            Compiler.EmitCode($"br {afterLabel}");
            Compiler.EmitCode("nop", true, elseLabel);
            ElseStatement.GenCode();
            Compiler.EmitCode("nop", true, afterLabel);
        }

        return "";
    }
}

public class WhileStatementNode : SyntaxTreeNode
{
    private SyntaxTreeNode Condition { get; set; }
    private SyntaxTreeNode ThenStatement { get; set; }

    public WhileStatementNode(int lineNo, SyntaxTreeNode condition, SyntaxTreeNode thenStatement)
        : base(lineNo)
    {
        Condition = condition;
        ThenStatement = thenStatement;
    }

    public override string GenCode()
    {
        var endLabel = Compiler.GenerateLabel();
        var condLabel = Compiler.GenerateLabel();
        Compiler.EmitCode("nop", true, condLabel);
        Condition.GenCode();
        Compiler.EmitCode($"brfalse {endLabel}");

        ThenStatement.GenCode();
        Compiler.EmitCode($"br {condLabel}");

        Compiler.EmitCode("nop", true, endLabel);

        return "";
    }
}

public class ReturnNode: SyntaxTreeNode
{
    public ReturnNode(int lineNo)
        : base(lineNo)
    {

    }

    public override string GenCode()
    {
        Compiler.EmitCode($"br {Compiler.GetReturnLabel()}");

        return "";
    }
}

public class ReadNode : SyntaxTreeNode
{
    private string Name { get; set; }

    public ReadNode(int lineNo, string name)
        :base(lineNo)
    {
        Name = name;
    }

    public override string GenCode()
    {
        var type = Compiler.GetVariable(Name);

        Compiler.EmitCode("call string [mscorlib]System.Console::ReadLine()");
        switch (type)
        {
            case ValType.Int:
                Compiler.EmitCode("call int32 [mscorlib]System.Int32::Parse(string)");
                break;
            case ValType.Double:
                Compiler.EmitCode("call class [mscorlib]System.Globalization.CultureInfo [mscorlib]System.Globalization.CultureInfo::get_InvariantCulture()");
                Compiler.EmitCode("call float64 [mscorlib]System.Double::Parse(string, class [mscorlib]System.IFormatProvider)");
                break;
            case ValType.Bool:
                Compiler.EmitCode("call bool [mscorlib]System.Boolean::Parse(string)");
                break;
            default:
                Compiler.AddError(new UndefinedError(LineNo));
                break;
        }
        Compiler.EmitCode($"stloc _{Name}");

        return "";
    }
}

public class WriteNode : SyntaxTreeNode
{
    private SyntaxTreeNode ExpressionNode { get; set; }
    private string Text { get; set; }

    public WriteNode(int lineNo, SyntaxTreeNode expressionNode)
        : base(lineNo)
    {
        ExpressionNode = expressionNode;
        Text = null;
    }

    public WriteNode(int lineNo, string text)
        : base(lineNo)
    {
        ExpressionNode = null;
        Text = text;
    }
    public override string GenCode()
    {
        if(Text != null)
        {
            Compiler.EmitCode($"ldstr {Text}");
            Compiler.EmitCode($"call void [mscorlib]System.Console::Write(string)");
        }
        else
        {
            switch (ExpressionNode.Type)
            {
                case ValType.Int:
                    ExpressionNode.GenCode();
                    Compiler.EmitCode($"call void [mscorlib]System.Console::Write(int32)");
                    break;
                case ValType.Double:
                    Compiler.EmitCode($"call class [mscorlib]System.Globalization.CultureInfo [mscorlib]System.Globalization.CultureInfo::get_InvariantCulture()");
                    Compiler.EmitCode("ldstr \"{0:0.000000}\"");
                    ExpressionNode.GenCode();
                    Compiler.EmitCode("box [mscorlib]System.Double");
                    Compiler.EmitCode("call string [mscorlib]System.String::Format(class [mscorlib]System.IFormatProvider, string, object)");
                    Compiler.EmitCode("call void [mscorlib]System.Console::Write(string)");
                    break;
                case ValType.Bool:
                    ExpressionNode.GenCode();
                    Compiler.EmitCode($"call void [mscorlib]System.Console::Write(bool)");
                    break;
                default:
                    Compiler.AddError(new UndefinedError(LineNo));
                    break;
            }
        }

        return "";
    }
}

#endregion

#region Errors

public abstract class MiniCompilerError
{
    public int LineNumber { get; set; }
    public bool IsSyntaxError { get; set; }

    protected MiniCompilerError(int lineNumber, bool isSyntaxError = false)
    {
        LineNumber = lineNumber;
        IsSyntaxError = isSyntaxError;
    }

    public override string ToString()
    {
        return (IsSyntaxError? "Syntax " : "") + $"Error occured in line {LineNumber}. ";
    }
}

public class UnexpectedTokenError : MiniCompilerError
{
    public UnexpectedTokenError(int lineNumber)
    : base(lineNumber, true)
    {

    }

    public override string ToString()
    {
        return base.ToString() + $"Unexpected symbol. Maybe missing bracket/semicolon or code outside program scope";
    }
}

public class InvalidSymbolError: MiniCompilerError
{
    public InvalidSymbolError(int lineNumber)
        :base(lineNumber, true)
    {

    }

    public override string ToString()
    {
        return base.ToString() + $"Invalid character (not allowed in grammar).";
    }
}

public class VariableNotDeclaredError: MiniCompilerError
{
    private string VariableName { get; set; }

    public VariableNotDeclaredError(int lineNumber, string variableName)
        :base(lineNumber)
    {
        VariableName = variableName;
    }

    public override string ToString()
    {
        return base.ToString() + $"Variable {VariableName} has not been declared.";
    }
}

public class VariableAlreadyDeclaredError: MiniCompilerError
{
    private string VariableName { get; set; }

    public VariableAlreadyDeclaredError(int lineNumber, string variableName)
        : base(lineNumber)
    {
        VariableName = variableName;
    }

    public override string ToString()
    {
        return base.ToString() + $"Variable {VariableName} has already been declared.";
    }
}

public class InvalidTypeError : MiniCompilerError
{
    private ValType ActualType { get; set; }
    private ValType[] ExpectedTypes { get; set; }

    private string ExpectedTypesList => string.Join(" or ", ExpectedTypes.Select(type => type.ToString().ToLower()));

    public InvalidTypeError(int lineNumber, ValType actualType, params ValType[] expectedTypes)
        : base(lineNumber)
    {
        ActualType = actualType;
        ExpectedTypes = expectedTypes;
    }

    public override string ToString()
    {
        return base.ToString() + $"Invalid type: expected {ExpectedTypesList} but got {ActualType.ToString().ToLower()}.";
    }
}

public class UndefinedError : MiniCompilerError
{
    public UndefinedError(int lineNumber)
        :base(lineNumber)
    {

    }

    public override string ToString()
    {
        return base.ToString() + "Undefined error.";
    }
}
 
#endregion
