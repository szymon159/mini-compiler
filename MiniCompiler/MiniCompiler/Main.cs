using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum ValType
{
    Int,
    Double,
    Bool,
    Statement
}

public class Compiler
{
    private static Stack<SyntaxTreeNode> code;
    private static List<string> variables;

    public static int Main(string[] args)
    {
        Console.WriteLine("BUILD SUCCEEDED");


        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        return 0;
    }

    public static void AddNode(SyntaxTreeNode node)
    {
        code.Push(node);
    }

    public static void DeclareVariable(string name)
    {
        variables.Add(name);
    }

    public static bool IsVariableDeclared(string name)
    {
        return variables.Contains(name);
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