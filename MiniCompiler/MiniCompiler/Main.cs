using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using MiniCompiler;

public enum ValType
{
    Int,
    Double,
    Bool,
    Statement,
    
    // Used for compiler declaration of undeclared variable - can be used as int, double, bool
    Dynamic 
}

public class Compiler
{
    private static Stack<SyntaxTreeNode> code = new Stack<SyntaxTreeNode>();
    private static Dictionary<string, MiniCompiler.ValueType> variables = new Dictionary<string, MiniCompiler.ValueType>();
    private static List<MiniCompilerError> errors = new List<MiniCompilerError>();

    public static int Main(string[] args)
    {
        Console.WriteLine("BUILD SUCCEEDED");

        var codeSample = "program \n" +
            "{\n" +
            "int a;\n" +
            "int b;\n" +
            "a = 1;\n" +
            "b = 2;\n" +
            "c = c;\n" +
            "return;\n" +
            "}";

        var bytes = Encoding.ASCII.GetBytes(codeSample);

        var stream = new MemoryStream(bytes);

        var scanner = new Scanner(stream);
        var parser = new Parser(scanner);

        parser.Parse();

        if(errors.Count != 0)
        {
            Console.WriteLine("FAILURE");
            Console.WriteLine($"Found {errors.Count} errors:");
            foreach (var error in errors)
                Console.WriteLine(error);
        }
        else
        {
            Console.WriteLine("SUCCESS");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        return 0;
    }

    public static void AddNode(SyntaxTreeNode node)
    {
        code.Push(node);
    }

    public static void DeclareVariable(string name, ValType type)
    {
        var value = new MiniCompiler.ValueType() { val_type = type };

        variables.Add(name, value);
    }

    public static bool IsVariableDeclared(string name)
    {
        return variables.ContainsKey(name);
    }

    public static MiniCompiler.ValueType? GetVariable(string name)
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

    public abstract string GenCode();
}

public class Declaration: SyntaxTreeNode
{
    private string name;
    private ValType valType;

    public Declaration(ValType type, string name, int lineNo)
    {
        LineNo = lineNo;
        Type = ValType.Statement;

        this.valType = type;
        this.name = name;
    }

    // TODO: Implement
    public override string GenCode()
    {
        throw new NotImplementedException();
    }
}

public abstract class Statement: SyntaxTreeNode
{

}

public abstract class Expression: SyntaxTreeNode
{

}

//public class Block: Statement
//{
//    private List<Statement> statements;

//    public Block()
//}

public class IfStatement: Statement
{
    private Expression condition;
    private Statement trueStatement;
    private Statement falseStatement;

    public IfStatement(Expression condition, Statement trueStatement, Statement falseStatement, int lineNo)
    {
        LineNo = lineNo;
        Type = ValType.Statement;

        this.condition = condition;
        this.trueStatement = trueStatement;
        this.falseStatement = falseStatement;
    }

    // TODO: Implement
    public override string GenCode()
    {
        throw new NotImplementedException();
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