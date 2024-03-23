using System;
using System.Globalization;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Calc;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Button_AC.Click += (_, _) =>
        {
            ResetExpression();
            ResetResult();
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

        Button_Equal.Click += async (_, _) =>
        {
            await CalculateResult();
        };

        Task.Run(async () =>
        {
            try
            {
                var func = await Compile("2 + 2 + Math.Abs(-10)");
               _ = func?.Invoke();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        });

        static async Task<Func<decimal>?> Compile(string expr)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    var code = $"() => {expr}";
                    var options = ScriptOptions.Default.WithImports([ "System" ]);
                    var compiledFilter = await CSharpScript.EvaluateAsync<Func<decimal>>(code, options);
                    return compiledFilter;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return null;
            });
        }
        
        
        void ResetExpression()
        {
            ExpressionTextBlock.Text = "";
        }

        void ResetResult()
        {
            ResultTextBlock.Text = "";
        }

        void UpdateExpression(string str)
        {
            ExpressionTextBlock.Text += str;
        }

        async Task CalculateResult()
        {
            var expr = ExpressionTextBlock.Text;
            if (expr is null)
            {
                ResultTextBlock.Text = "";
                return;
            }

            await Task.Run(async () =>
            {
                var func = await Compile($"(decimal)({expr.Replace(',', '.')})");
                if (func is not null)
                {
                    var result = func.Invoke();
                    Dispatcher.UIThread.Post(() => ResultTextBlock.Text = result.ToString(CultureInfo.CurrentCulture));
                }
                else
                {
                    Dispatcher.UIThread.Post(() => ResultTextBlock.Text = "");
                }
            });
        }
    }
}
