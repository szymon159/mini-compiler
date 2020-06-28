using System;
using System.Collections.Generic;
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

    public static int Main(string[] args)
    {
        //var codeSample = "program \n" +
        //    "{\n" +
        //    "int a;\n" +
        //    "double b;\n" +
        //    "bool c;\n" +
        //    "while(true){\n" +
        //    "write a = -1;\n" +
        //    "write \"HELLO WORLD\";\n" +
        //    "b = (a+(int)b)*a;\n" +
        //    "return;\n" +
        //    "}\n" +
        //    "}";

        var codeSample = "program \n" +
            "{\n" +
            "int a;\n" +
            "int b;\n" +
            "bool c;\n" +
            "double d;\n" +
            "c = !c\n;" +
            "a = -1;\n" +
            "b = (a+b)*a;\n" +
            "}";

        Console.WriteLine("Code:\n");
        Console.WriteLine(codeSample);
        Console.WriteLine("\n");

        var bytes = Encoding.ASCII.GetBytes(codeSample);

        var stream = new MemoryStream(bytes);

        var scanner = new Scanner(stream);
        var parser = new Parser(scanner);

        parser.Parse();
        if (errors.Count == 0)
        {
            Console.WriteLine("SUCCESS\n");
            var parsedCode = code.Reverse();
            foreach (var statement in parsedCode)
                statement.GenCode();
        }
        else
        {
            Console.WriteLine("FAILURE");
            Console.WriteLine($"Found {errors.Count} errors:");
            foreach (var error in errors)
                Console.WriteLine(error);
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
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

    public static bool IsVariableDeclared(string name)
    {
        return variables.ContainsKey(name);
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
}

#region  Syntax Tree

public abstract class SyntaxTreeNode
{
    public int LineNo = -1;
    public ValType Type;

    public SyntaxTreeNode(int lineNo, ValType type = ValType.None)
    {
        LineNo = lineNo;
        Type = type;
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
                //TODO: add error
                break;
        }
        Console.WriteLine(text);

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
        Left?.GenCode();
        Right?.GenCode();

        string text = "";
        var helperNode = new UnaryOperationNode(-1, ValType.Bool, OpType.LogNot, null);
        switch (OperatorType)
        {
            case OpType.LogOr:
                text = "or";
                break;
            case OpType.LogAnd:
                text = "and";
                break;
            case OpType.Equal:
                text = "ceq";
                break;
            case OpType.NotEqual:
                // Check for equality
                // And negate stack top (equality result)
                Console.WriteLine("ceq");
                helperNode.GenCode();
                break;
            case OpType.Greater:
                text = "cgt";
                break;
            case OpType.GreaterOrEqual:
                // Check for less
                // And negate stack top (less result)
                Console.WriteLine("clt");
                helperNode.GenCode();
                break;
            case OpType.Less:
                text = "clt";
                break;
            case OpType.LessOrEqual:
                // Check for greater
                // And negate stack top (greater result)
                Console.WriteLine("cgt");
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
                // ADD ERROR
                break;
        }

        Console.WriteLine(text);

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
        var text = $"stloc.s _{Name}";

        Right?.GenCode();
        Console.WriteLine(text);

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
                // Add it to stack and call bit and with old stack top
                var helperNode = new ConstantNode(-1, ValType.Bool, true);
                helperNode.GenCode();
                text = "and";
                break;
            case OpType.IntCast:
                text = "conv.i4";
                break;
            case OpType.DoubleCast:
                text = "conv.r8";
                break;
            default:
                // TODO: ADD ERROR
                break;
        }

        Console.WriteLine(text);

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
        var text = $"ldloc.s _{Name}";
        Console.WriteLine(text);

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
                text = $"ldc.r8 {BoolValue}";
                break;
            case ValType.Bool:
                text = $"ldc.i4 {(BoolValue ? 1 : 0)}";
                break;
            default:
                break;
        }

        Console.WriteLine(text);

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
        Console.WriteLine("BLOCK BEGIN");
        foreach (var node in innerNodes)
            node.GenCode();
        Console.WriteLine("BLOCK END");

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
        var text = "IF:";
        Console.WriteLine(text);
        Console.WriteLine("COND:");
        Condition.GenCode();
        Console.WriteLine("THEN:");
        ThenStatement.GenCode();
        Console.WriteLine("ELSE:");
        if (ElseStatement != null)
            ElseStatement.GenCode();
        else
            Console.WriteLine("NONE");

        return text;
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
        var text = "WHILE:";
        Console.WriteLine(text);
        Console.WriteLine("COND:");
        Condition.GenCode();
        Console.WriteLine("THEN:");
        ThenStatement.GenCode();

        return text;
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
        var text = "RETURN";
        Console.WriteLine(text);

        return text;
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
        var text = $"READ TO {Name}";

        return text;
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
        string text;
        if(Text != null)
        {
            text = $"WRITE TEXT: {Text}";
            Console.WriteLine(text);
        }
        else
        {
            text = "WRITE EXP:";
            Console.WriteLine(text);
            ExpressionNode.GenCode();
        }

        return text;
    }
}

#endregion

#region Errors

public abstract class MiniCompilerError
{
    protected int LineNumber { get; set; }

    protected MiniCompilerError(int lineNumber)
    {
        LineNumber = lineNumber;
    }

    public override string ToString()
    {
        return $"Error occured in line {LineNumber}. ";
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

public class InvalidTypeError : MiniCompilerError
{
    private ValType ActualType { get; set; }
    private ValType[] ExpectedTypes { get; set; }

    private string ExpectedTypesList => string.Join(", ", ExpectedTypes);

    public InvalidTypeError(int lineNumber, ValType actualType, params ValType[] expectedTypes)
        : base(lineNumber)
    {
        ActualType = actualType;
        ExpectedTypes = expectedTypes;
    }

    public override string ToString()
    {
        return base.ToString() + $"Invalid type: expected {ExpectedTypesList} but got {ActualType}.";
    }
}
#endregion
