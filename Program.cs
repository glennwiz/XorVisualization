using Spectre.Console;
using Spectre.Console.Rendering;

namespace XorVisualization
{
    class Program
    {
        class State
        {
            public bool A { get; set; }
            public bool B { get; set; }
        }

        static void Main()
        {
            var simulationData = new[]
            {
                new State { A = false, B = false },
                new State { A = false, B = true },
                new State { A = true, B = false },
                new State { A = true, B = true }
            };

            while (true)
            {
                foreach (var data in simulationData)
                {

                    Line("XoR loop");

                    var xorValue = Xor(data.A, data.B);
                    Color Input1 = Color.Grey;
                    Color Input2 = Color.Grey;
                    Color resultColor = Color.Grey;

                    string Input1Text = "Off";
                    string Input2Text = "Off";
                    string resultText = "Off";

                    if (data.A)
                    {
                        Input1 = Color.Green;
                        Input1Text = "On ";
                    }
                    else
                    {
                        Input1 = Color.Red;
                        Input1Text = "Off";
                    }

                    if (data.B)
                    {
                        Input2 = Color.Green;
                        Input2Text = "ON ";
                    }
                    else
                    {
                        Input2 = Color.Red;
                        Input2Text = "Off";
                    }

                    if (xorValue)
                    {
                        resultColor = Color.Yellow;
                        resultText = "On ";
                    }
                    else
                    {
                        resultColor = Color.Grey;
                        resultText = "Off";
                    }

                    var items = new Grid()
                        .AddColumn(new GridColumn().NoWrap().PadRight(4))
                        .AddColumn(new GridColumn().NoWrap().PadRight(4))
                        .AddRow(
                            CreatePanel("Input1", BoxBorder.Double, Input1, Input1Text),
                            CreatePanel("Input2", BoxBorder.Double, Input2, Input2Text)
                        );

                    var result = CreatePanel("Result", BoxBorder.Double, resultColor, resultText);

                    AnsiConsole.Write(
                                new Padder(
                                    new Columns(items).PadRight(2),
                                    new Padding(2, 0, 0, 0)));

                    Console.WriteLine();
                    AnsiConsole.Write(
                    new Padder(
                        new Columns(result).PadRight(2),
                        new Padding(2, 0, 0, 0)));

                    // Toggle the inputs for the next iteration
                    var temp = data.A;
                    data.A = xorValue;
                    data.B = temp;

                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
        }

        static bool Xor(bool a, bool b)
        {
            return a ^ b;
        }
        static IRenderable CreatePanel(string name, BoxBorder border, Color color, string text)
        {
            return new Panel($"[{color.ToString()}]--{text}--[/]")
                .Header($" [{color}]{name}[/] ", Justify.Center)
                .Border(border)
                .BorderStyle(color);
        }

        private static void Line(string title)
        {
            AnsiConsole.WriteLine($"----------{title}----------");

            AnsiConsole.WriteLine();
        }
    }
}
