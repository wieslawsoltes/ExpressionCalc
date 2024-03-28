using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using SimpleExpressionEngine;

namespace ExpressionCalc;

public partial class MainWindow : Window
{
    private Stack<string> stack = new Stack<string>();

    public MainWindow()
    {
        InitializeComponent();

        InitializeButtonHandlers();
    }

    private void InitializeButtonHandlers()
    {
        Button_AC.Click += (_, _) =>
        {
            Clear();
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
        
        Button_Equal.Click += (_, _) =>
        {
            Execute();
        };
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        Focus();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        Focus();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        switch (e.Key)
        {
            case Key.C:
            {
                Clear();
                e.Handled = true;
                break;
            }
            case Key.Back:
            case Key.Delete:
            {
                Clear();
                e.Handled = true;
                break;
            }
            case Key.OemOpenBrackets:
            {
                UpdateExpression("(");
                e.Handled = true;
                break;
            }
            case Key.OemCloseBrackets:
            {
                UpdateExpression(")");
                e.Handled = true;
                break;
            }
            case Key.Divide:
            case Key.OemQuestion:
            {
                if (e.KeySymbol == "/")
                {
                    UpdateExpression("/");
                    e.Handled = true;
                }
                break;
            }
            case Key.D7:
            {
                if (e.KeySymbol == "7")
                {
                    UpdateExpression("7");
                    e.Handled = true;
                }
                break;
            }
            case Key.D8:
            {
                if (e.KeySymbol == "8")
                {
                    UpdateExpression("8");
                    e.Handled = true;
                }
                if (e.KeySymbol == "*")
                {
                    UpdateExpression("*");
                    e.Handled = true;
                }
                break;
            }
            case Key.D9:
            {
                if (e.KeySymbol == "9")
                {
                    UpdateExpression("9");
                    e.Handled = true;
                }
                if (e.KeySymbol == "(")
                {
                    UpdateExpression("(");
                    e.Handled = true;
                }
                break;
            }
            case Key.Multiply:
            {
                UpdateExpression("*");
                e.Handled = true;
                break;
            }
            case Key.D4:
            {
                if (e.KeySymbol == "4")
                {
                    UpdateExpression("4"); 
                    e.Handled = true;                  
                }
                break;
            }
            case Key.D5:
            {
                if (e.KeySymbol == "5")
                {
                    UpdateExpression("5");
                    e.Handled = true;
                }
                break;
            }
            case Key.D6:
            {
                if (e.KeySymbol == "6")
                {
                    UpdateExpression("6");
                    e.Handled = true;
                }
                break;
            }
            case Key.Subtract:
            {
                UpdateExpression("-");
                e.Handled = true;
                break;
            }
            case Key.D1:
            {
                if (e.KeySymbol == "1")
                {
                    UpdateExpression("1");
                    e.Handled = true;
                }
                break;
            }
            case Key.D2:
            {
                if (e.KeySymbol == "2")
                {
                    UpdateExpression("2");
                    e.Handled = true;
                }
                break;
            }
            case Key.D3:
            {
                if (e.KeySymbol == "3")
                {
                    UpdateExpression("3");
                    e.Handled = true;
                }
                break;
            }
            case Key.Add:
            {
                UpdateExpression("+");
                e.Handled = true;
                break;
            }
            case Key.D0:
            {
                if (e.KeySymbol == "0")
                {
                    UpdateExpression("0");
                    e.Handled = true;
                }
                if (e.KeySymbol == ")")
                {
                    UpdateExpression(")");
                    e.Handled = true;
                }
                break;
            }
            case Key.OemComma:
            case Key.OemPeriod:
            {
                if (e.KeySymbol == "," || e.KeySymbol == ".")
                {
                    UpdateExpression(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    e.Handled = true;
                }
                break;
            }
            case Key.Enter:
            case Key.Execute:
            {
                Execute();
                e.Handled = true;
                break;
            }
            case Key.OemPlus:
            {
                if (e.KeySymbol == "+")
                {
                    UpdateExpression("+");
                    e.Handled = true;
                }
                if (e.KeySymbol == "=")
                {
                    Execute();
                    e.Handled = true;
                }
                break;
            }
            case Key.OemMinus:
            {
                if (e.KeySymbol == "-")
                {
                    UpdateExpression("-");
                    e.Handled = true;
                }
                break;
            }
        }
    }

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

    private void Execute()
    {
        CalculateResult();
        UpdateClearButton();
    }

    private void Clear()
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
