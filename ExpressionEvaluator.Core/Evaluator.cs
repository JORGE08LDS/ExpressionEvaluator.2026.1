namespace ExpressionEvaluator.Core;

public class Evaluator
{
    public static double Evaluate(string infix)
    {
        var postfix = InfixToPostfix(infix);
        return EvaluatePostfix(postfix);
    }

    private static string InfixToPostfix(string infix)
    {
        var postFix = "";
        var stack = new Stack<char>();
        int i = 0;

        while (i < infix.Length)
        {
            char item = infix[i];

            // Ignorar espacios
            if (item == ' ')
            {
                i++;
                continue;
            }

            // Si es número o decimal
            if (char.IsDigit(item) || item == '.')
            {
                string number = "";

                while (i < infix.Length && (char.IsDigit(infix[i]) || infix[i] == '.'))
                {
                    number += infix[i];
                    i++;
                }

                postFix += number + " ";
                continue;
            }

            // Si es operador
            if (IsOperator(item))
            {
                if (item == '(')
                {
                    stack.Push(item);
                }
                else if (item == ')')
                {
                    while (stack.Peek() != '(')
                    {
                        postFix += stack.Pop() + " ";
                    }
                    stack.Pop();
                }
                else
                {
                    while (stack.Count > 0 && PriorityInfix(item) <= PriorityStack(stack.Peek()))
                    {
                        postFix += stack.Pop() + " ";
                    }
                    stack.Push(item);
                }
            }

            i++;
        }

        while (stack.Count > 0)
        {
            postFix += stack.Pop() + " ";
        }

        return postFix;
    }

    private static int PriorityStack(char item) => item switch
    {
        '^' => 3,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 0,
        _ => throw new Exception("Sintax error."),
    };

    private static int PriorityInfix(char item) => item switch
    {
        '^' => 4,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 5,
        _ => throw new Exception("Sintax error."),
    };

    private static double EvaluatePostfix(string postfix)
    {
        var stack = new Stack<double>();

        var tokens = postfix.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var token in tokens)
        {
            // Si es número
            if (double.TryParse(token, out double number))
            {
                stack.Push(number);
            }
            else if (token.Length == 1 && IsOperator(token[0]))
            {
                var b = stack.Pop();
                var a = stack.Pop();

                stack.Push(token[0] switch
                {
                    '+' => a + b,
                    '-' => a - b,
                    '*' => a * b,
                    '/' => b == 0 ? throw new Exception("No se puede dividir por 0") : a / b,
                    '^' => Math.Pow(a, b),
                    _ => throw new Exception("Syntax error")
                });
            }
        }

        return stack.Pop();
    }

    private static bool IsOperator(char item) => "+-*/^()".Contains(item);
}