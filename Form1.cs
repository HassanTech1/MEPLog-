using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SimpleIDE
{
    public partial class MainForm : Form
    {
        private Interpreter _interpreter;

        public MainForm()
        {
            InitializeComponent();
            _interpreter = new Interpreter();

        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                string code = txtCode.Text;

                if (string.IsNullOrWhiteSpace(code))
                {
                    txtOutput.Text = "Error: No code provided. Please write some code to execute.";
                    lblStatus.Text = "Error: No code provided.";
                    return;
                }

                lblStatus.Text = "Running...";
                Application.DoEvents();
                ApplySyntaxHighlighting(code);
                string output = _interpreter.Execute(code);
                txtOutput.Text = output;
                lblStatus.Text = "Execution Complete";
            }
            catch (Exception ex)
            {
                txtOutput.Text = $"Error: {ex.Message}";
                lblStatus.Text = "Execution Error";
            }
        }
        private void ApplySyntaxHighlighting(string code)
        {
            txtCode.Text = code;
            txtCode.SelectAll();
            txtCode.SelectionColor = System.Drawing.Color.Black;

            // Example of highlighting keywords
            string[] keywords = { "print", "if", "else", "while", "for", "return" };
            foreach (var keyword in keywords)
            {
                int index = 0;
                while ((index = txtCode.Text.IndexOf(keyword, index, StringComparison.OrdinalIgnoreCase)) >= 0)
                {
                    txtCode.Select(index, keyword.Length);
                    txtCode.SelectionColor = System.Drawing.Color.Blue; // Keyword color
                    index += keyword.Length;
                }
            }

            // Highlight strings in green (basic version)
            int start = 0;
            while ((start = txtCode.Text.IndexOf("\"", start)) >= 0)
            {
                int end = txtCode.Text.IndexOf("\"", start + 1);
                if (end >= 0)
                {
                    txtCode.Select(start, end - start + 1);
                    txtCode.SelectionColor = System.Drawing.Color.Green; // String color
                    start = end + 1;
                }
                else
                {
                    break;
                }
            }
        }
    }

    // Interpreter
    public class Interpreter
    {
        private Context _context;

        public Interpreter()
        {
            _context = new Context();
        }

        public string Execute(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new Exception("Code cannot be empty.");

            var parser = new Parser(code);
            var statements = parser.Parse();
            var outputCollector = new OutputCollector();

            foreach (var statement in statements)
            {
                statement.Execute(_context, outputCollector);
            }

            return outputCollector.Output;
        }
    }

    // Context for storing variables
    public class Context
    {
        public Dictionary<string, object> Variables { get; private set; }

        public Context()
        {
            Variables = new Dictionary<string, object>();
        }
    }

    // OutputCollector
    public class OutputCollector
    {
        private StringBuilder _outputBuilder;

        public OutputCollector()
        {
            _outputBuilder = new StringBuilder();
        }
        
        public void Write(string message)
        {
            _outputBuilder.AppendLine(message);
        }

        public string Output => _outputBuilder.ToString();
    }

    // Parser class modifications for control structures
    public class Parser
    {
        private readonly string _code;

        public Parser(string code)
        {
            _code = code;
        }

        public List<IStatement> Parse()
        {
            var statements = new List<IStatement>();
            var lines = _code.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();

                if (trimmedLine.StartsWith("print "))
                {
                    var value = trimmedLine.Substring(6).Trim();
                    statements.Add(new PrintStatement(value));
                }
                else if (trimmedLine.Contains("="))
                {
                    var parts = trimmedLine.Split('=');
                    if (parts.Length == 2)
                    {
                        var variable = parts[0].Trim();
                        var expression = parts[1].Trim();
                        statements.Add(new AssignmentStatement(variable, expression));
                    }
                    else
                    {
                        throw new Exception("Invalid assignment syntax.");
                    }
                }
                else if (trimmedLine.StartsWith("if"))
                {
                    var condition = trimmedLine.Substring(2).Trim();
                    statements.Add(new IfStatement(condition));
                }
                else if (trimmedLine.StartsWith("else"))
                {
                    statements.Add(new ElseStatement());
                }
                else if (trimmedLine.StartsWith("for"))
                {
                    var forLoop = trimmedLine.Substring(3).Trim();
                    statements.Add(new ForStatement(forLoop));
                }
                else if (trimmedLine.StartsWith("while"))
                {
                    var whileLoop = trimmedLine.Substring(5).Trim();
                    statements.Add(new WhileStatement(whileLoop));
                }
                else if (trimmedLine.StartsWith("function"))
                {
                    var functionDefinition = trimmedLine.Substring(8).Trim();
                    statements.Add(new FunctionStatement(functionDefinition));
                }
                else
                {
                    throw new Exception($"Unknown statement: {trimmedLine}");
                }
            }

            return statements;
        }
    }

    // Statement Interface
    public interface IStatement
    {
        void Execute(Context context, OutputCollector outputCollector);
    }

    // PrintStatement
    public class PrintStatement : IStatement
    {
        private readonly string _value;

        public PrintStatement(string value)
        {
            _value = value;
        }

        public void Execute(Context context, OutputCollector outputCollector)
        {
            if (context.Variables.ContainsKey(_value))
            {
                outputCollector.Write(context.Variables[_value]?.ToString());
            }
            else
            {
                outputCollector.Write(_value.Trim('"'));
            }
        }
    }

    // AssignmentStatement
    public class AssignmentStatement : IStatement
    {
        private readonly string _variable;
        private readonly string _expression;

        public AssignmentStatement(string variable, string expression)
        {
            _variable = variable;
            _expression = expression;
        }

        public void Execute(Context context, OutputCollector outputCollector)
        {
            var evaluator = new ExpressionEvaluator();
            var result = evaluator.Evaluate(_expression, context);
            context.Variables[_variable] = result;
        }
    }

    // IfStatement
    public class IfStatement : IStatement
    {
        private readonly string _condition;

        public IfStatement(string condition)
        {
            _condition = condition;
        }

        public void Execute(Context context, OutputCollector outputCollector)
        {
            var evaluator = new ExpressionEvaluator();
            var conditionResult = evaluator.Evaluate(_condition, context);

            if (Convert.ToBoolean(conditionResult))
            {
                outputCollector.Write("If condition executed.");
            }
            else
            {
                outputCollector.Write("Condition is false, no action taken.");
            }
        }
    }

    // ElseStatement
    public class ElseStatement : IStatement
    {
        public void Execute(Context context, OutputCollector outputCollector)
        {
            outputCollector.Write("Else condition executed.");
        }
    }

    // ForStatement
    public class ForStatement : IStatement
    {
        private readonly string _loopCondition;

        public ForStatement(string loopCondition)
        {
            _loopCondition = loopCondition;
        }

        public void Execute(Context context, OutputCollector outputCollector)
        {
            outputCollector.Write("For loop executed.");
        }
    }

    // WhileStatement
    public class WhileStatement : IStatement
    {
        private readonly string _loopCondition;

        public WhileStatement(string loopCondition)
        {
            _loopCondition = loopCondition;
        }

        public void Execute(Context context, OutputCollector outputCollector)
        {
            outputCollector.Write("While loop executed.");
        }
    }

    // FunctionStatement
    public class FunctionStatement : IStatement
    {
        private readonly string _functionDefinition;

        public FunctionStatement(string functionDefinition)
        {
            _functionDefinition = functionDefinition;
        }

        public void Execute(Context context, OutputCollector outputCollector)
        {
            outputCollector.Write("Function defined.");
        }
    }

    // ExpressionEvaluator for mathematical expressions
    public class ExpressionEvaluator
    {
        public object Evaluate(string expression, Context context)
        {
            var tokens = Tokenize(expression);
            var postfix = ConvertToPostfix(tokens);
            return EvaluatePostfix(postfix, context);
        }

        private List<string> Tokenize(string expression)
        {
            var tokens = new List<string>();
            var currentToken = new StringBuilder();

            foreach (var ch in expression)
            {
                if (char.IsWhiteSpace(ch))
                {
                    continue;
                }
                else if ("+-*/()".Contains(ch))
                {
                    if (currentToken.Length > 0)
                    {
                        tokens.Add(currentToken.ToString());
                        currentToken.Clear();
                    }
                    tokens.Add(ch.ToString());
                }
                else
                {
                    currentToken.Append(ch);
                }
            }

            if (currentToken.Length > 0)
            {
                tokens.Add(currentToken.ToString());
            }

            return tokens;
        }

        private Queue<string> ConvertToPostfix(List<string> tokens)
        {
            var output = new Queue<string>();
            var operators = new Stack<string>();

            var precedence = new Dictionary<string, int>
            {
                { "+", 1 }, { "-", 1 }, { "*", 2 }, { "/", 2 }
            };

            foreach (var token in tokens)
            {
                if (decimal.TryParse(token, out _))
                {
                    output.Enqueue(token);
                }
                else if ("+-*/".Contains(token))
                {
                    while (operators.Count > 0 && precedence[token] <= precedence[operators.Peek()])
                    {
                        output.Enqueue(operators.Pop());
                    }
                    operators.Push(token);
                }
                else if (token == "(")
                {
                    operators.Push(token);
                }
                else if (token == ")")
                {
                    while (operators.Peek() != "(")
                    {
                        output.Enqueue(operators.Pop());
                    }
                    operators.Pop();
                }
                else
                {
                    throw new Exception($"Unexpected token: {token}");
                }
            }

            while (operators.Count > 0)
            {
                output.Enqueue(operators.Pop());
            }

            return output;
        }

        private decimal EvaluatePostfix(Queue<string> postfix, Context context)
        {
            var stack = new Stack<decimal>();

            while (postfix.Count > 0)
            {
                var token = postfix.Dequeue();

                if (decimal.TryParse(token, out var number))
                {
                    stack.Push(number);
                }
                else if (context.Variables.ContainsKey(token))
                {
                    stack.Push(Convert.ToDecimal(context.Variables[token]));
                }
                else if ("+-*/".Contains(token))
                {
                    var right = stack.Pop();
                    var left = stack.Pop();

                    switch (token)
                    {
                        case "+":
                            stack.Push(left + right);
                            break;
                        case "-":
                            stack.Push(left - right);
                            break;
                        case "*":
                            stack.Push(left * right);
                            break;
                        case "/":
                            stack.Push(left / right);
                            break;
                    }
                }
            }

            return stack.Pop();
        }
    }
}
