using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Controls;
using SimpleExpressionEngine;

namespace Calc;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var stack = new Stack<string>();

        Button_AC.Click += (_, _) =>
        {
            if (ResultTextBlock.Text is not null && ResultTextBlock.Text.Length > 0)
            {
                ResetResult();
            }
            else
            {
                ResetResult();
                ResetExpression();
            }
        };

        Button_Open.Click += (_, _) =>
        {
            UpdateExpression("(");
        };

        Button_Close.Click += (_, _) =>
        {
            UpdateExpression(")");
        };

        Button_Div.Click += (_, _) =>
        {
            UpdateExpression("/");
        };

        Button_7.Click += (_, _) =>
        {
            UpdateExpression("7");
        };

        Button_8.Click += (_, _) =>
        {
            UpdateExpression("8");
        };

        Button_9.Click += (_, _) =>
        {
            UpdateExpression("9");
        };

        Button_Mul.Click += (_, _) =>
        {
            UpdateExpression("*");
        };

        Button_4.Click += (_, _) =>
        {
            UpdateExpression("4");
        };

        Button_5.Click += (_, _) =>
        {
            UpdateExpression("5");
        };

        Button_6.Click += (_, _) =>
        {
            UpdateExpression("6");
        };

        Button_Sub.Click += (_, _) =>
        {
            UpdateExpression("-");
        };

        Button_1.Click += (_, _) =>
        {
            UpdateExpression("1");
        };

        Button_2.Click += (_, _) =>
        {
            UpdateExpression("2");
        };

        Button_3.Click += (_, _) =>
        {
            UpdateExpression("3");
        };

        Button_Add.Click += (_, _) =>
        {
            UpdateExpression("+");
        };

        Button_0.Click += (_, _) =>
        {
            UpdateExpression("0");
        };

        Button_Comma.Click += (_, _) =>
        {
            UpdateExpression(",");
        };

        Button_PlusMinus.Click += (_, _) =>
        {
            UpdateExpression("-");
        };

        Button_Equal.Click += (_, _) =>
        {
            CalculateResult();
            UpdateClearButton();
        };

        static Func<decimal>? Compile(string expr)
        {
            try
            {
                var result = Parser.Parse(expr).Eval(null!);
                return Func;
                decimal Func() => result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        void UpdateClearButton()
        {
            if (ExpressionTextBlock.Text is not null && ExpressionTextBlock.Text.Length > 0)
            {
                Button_AC.Content = "C";
            }
            else
            {
                Button_AC.Content = "AC";
            }
        }

        void ResetExpression()
        {
            if (stack.Count > 0)
            {
                var expr = stack.Pop();
                if (ExpressionTextBlock.Text is not null && ExpressionTextBlock.Text.Length > 0)
                {
                    ExpressionTextBlock.Text =
                        ExpressionTextBlock.Text.Substring(0, ExpressionTextBlock.Text.Length - expr.Length);
                }
            }
            else
            {
                ExpressionTextBlock.Text = "";
            }
            UpdateClearButton();
        }

        void ResetResult()
        {
            ResultTextBlock.Text = "";
            UpdateClearButton();
        }

        void UpdateExpression(string str)
        {
            ExpressionTextBlock.Text += str;
            UpdateClearButton();
            stack.Push(str);
        }

        void CalculateResult()
        {
            var expr = ExpressionTextBlock.Text;
            if (expr is null)
            {
                ResultTextBlock.Text = "";
                return;
            }

            var func = Compile($"{expr.Replace(',', '.')}");
            if (func is not null)
            {
                var result = func.Invoke();
                ResultTextBlock.Text = result.ToString(CultureInfo.CurrentCulture);
                UpdateClearButton();
            }
            else
            {
                ResultTextBlock.Text = "Error";
                UpdateClearButton();
            }
        }
    }
}
