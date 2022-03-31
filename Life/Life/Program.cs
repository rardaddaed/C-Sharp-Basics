using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Display;

namespace Life
{
    class Program
    {
        static void Main(string[] args)
        {
            // Resize window to default
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 25);

            // Log display
            Log log = new Log();

            // Default values
            int rows = 16;
            int cols = 16;
            bool periodic = false;
            float random = 0.5f;
            int generations = 50;
            float updateRate = 5;
            bool step = false;
            string filename = "N/A";

            string type = "moore";
            int order = 1;
            bool centreCount = false;
            int memory = 4;
            bool ghost = false;
            List<int> survivals = new List<int>() {2, 3};
            List<int> births = new List<int>() { 3 };

            bool sucess = false;

            // Parse arguments and display errors if necessary
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--dimensions":
                        try
                        {
                            int intRow = int.Parse(args[i + 1]);
                            int intCols = int.Parse(args[i + 2]);

                            if (intRow >= 4 && intRow <= 48 && intCols >= 4 && intCols <= 48)
                            {
                                throw new AbcException("");
                            }
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            log.Error($"You must provide the number of rows and columns! {ex.Message}");
                        }
                        if (args.Length - i <= 2)
                        {
                            log.Error("You must provide the number of rows and columns!");
                        }
                        else
                        {
                            bool isIntRow = int.TryParse(args[i + 1], out int intRow);
                            bool isIntCol = int.TryParse(args[i + 2], out int intCols);
                            if (isIntRow && isIntCol)
                            {
                                if (intRow >= 4 && intRow <= 48 && intCols >= 4 && intCols <= 48)
                                {
                                    rows = intRow;
                                    cols = intCols;
                                    sucess = true;
                                }
                                else
                                {
                                    log.Error("The number of rows and columns must be within 4 - 48 (inclusive)!");
                                }
                            }
                            else
                            {
                                log.Error("You must provide the number of rows and columns in integer forms!");
                            }
                        }
                        break;
                    case "--periodic":
                        periodic = true;
                        sucess = true;
                        break;

                    case "--random":
                        if (args.Length - i <= 1)
                        {
                            log.Error("You must provide the probability value!");
                        }
                        else
                        {
                            bool isFloat = float.TryParse(args[i + 1], out float floatrand);
                            if (isFloat)
                            {
                                if (floatrand >= 0 && floatrand <= 1)
                                {
                                    random = floatrand;
                                    sucess = true;
                                }
                                else
                                {
                                    log.Error("The probability value must be within 0 - 1 (inclusive)!");
                                }
                            }
                            else
                            {
                                log.Error("You must provide the probability value in double precision form!");
                            }
                        }
                        break;

                    case "--seed":
                        if (args.Length - i <= 1)
                        {
                            log.Error("You must provide the seed file name!");
                        }
                        else
                        {
                            string[] filenameArr = args[i + 1].Split('.');
                            if (string.Equals(filenameArr[filenameArr.Length - 1], "seed", StringComparison.OrdinalIgnoreCase))
                            {
                                if (!File.Exists(args[i + 1]))
                                {
                                    log.Error("Seed file doesn't exist!");
                                }
                                else
                                {
                                    filename = args[i + 1];
                                    sucess = true;
                                }
                            }
                            else
                            {
                                log.Error("You must provid a proper seed file!");
                            }

                        }
                        break;

                    case "--generations":
                        if (args.Length - i <= 1)
                        {
                            log.Error("You must provide the number of generations!");
                        }
                        else
                        {
                            bool isInt = int.TryParse(args[i + 1], out int generation);
                            if (isInt)
                            {
                                if (generation > 0)
                                {
                                    generations = generation;
                                    sucess = true;
                                }
                                else
                                {
                                    log.Error("The number of generations must be greater than 0!");
                                }
                            }
                            else
                            {
                                log.Error("The number of generations must be in integer form!");
                            }
                        }
                        break;

                    case "--max-update":
                        if (args.Length - i <= 1)
                        {
                            log.Error("You must provide the update rate!");
                        }
                        else
                        {
                            bool isFloat = float.TryParse(args[i + 1], out float floatrate);
                            if (isFloat)
                            {
                                if (floatrate >= 1 && floatrate <= 30)
                                {
                                    updateRate = floatrate;
                                    sucess = true;
                                }
                                else
                                {
                                    log.Error("The update rate must be within 1 - 30 (inclusive)!");
                                }
                            }
                            else
                            {
                                log.Error("The update rate must be in floating point form!");
                            }
                        }
                        break;

                    case "--step":
                        step = true;
                        sucess = true;
                        break;

                    case "--neighbour":
                        if (args.Length - i <= 3)
                        {
                            log.Error("You must provide the neighbour type, order and wether the centre is counted!");
                        }
                        else
                        {
                            if (!(args[i + 1] == "moore" || args[i + 1] == "vonNeumann"))
                            {
                                log.Error("You must provide the correct type of neighbour, either moore or vonNeumann!");
                            }
                            else
                            {
                                type = args[i + 1] == "moore" ? "moore" : "vonNeumann";
                            }

                            bool isIntOrder = int.TryParse(args[i + 2], out int intOrder);

                            if (isIntOrder)
                            {
                                if (intOrder >= 1 && intOrder <= 10)
                                {
                                    order = intOrder;
                                }
                                else
                                {
                                    log.Error("The number of order must be within 1 - 10 (inclusive)!");
                                }
                            }
                            if (!(args[i + 3] == "true" || args[i + 3] == "false"))
                            {
                                log.Error("You must state whether centre is counted (true or false)!");
                            }
                            else
                            {
                                centreCount = args[i + 3] == "true" ? true : false;
                                sucess = true;
                            }
                        }
                        break;

                    case "--memory":
                        if (args.Length - i <= 1)
                        {
                            log.Error("You must provide the stored generation in memory!");
                        }
                        else
                        {
                            bool isInt = int.TryParse(args[i + 1], out int memoryInt);
                            if (isInt)
                            {
                                if (memoryInt >= 4 && memoryInt <= 512)
                                {
                                    memory = memoryInt;
                                    sucess = true;
                                }
                                else
                                {
                                    log.Error("The generational memeory must be within 4 - 512 (inclusive)!");
                                }
                            }
                            else
                            {
                                log.Error("The generational memory must be in integer form!");
                            }
                        }
                        break;

                    case "--survival":
                        if (args.Length - i <= 1)
                        {
                            log.Error("You must provide the survival values!");
                        }
                        else
                        {
                            survivals.Clear();
                            for (int j = 1; j <= args.Length - i - 1; j++)
                            {
                                bool isInt = int.TryParse(args[i + j], out int survivalInt);
                                if (isInt)
                                {
                                    if (type == "moore")
                                    {
                                        if (survivalInt <= (Math.Sqrt(2 * order + 1) - 1) && survivalInt >= 0)
                                        {
                                            survivals.Add(survivalInt);
                                        } else
                                        {
                                            log.Error("Survival rule out of range!");
                                        }
                                    } else if (type == "vonNeumann")
                                    {
                                        if (survivalInt <= (2 * Math.Sqrt(order) + (order * 2)) && survivalInt >= 0)
                                        {
                                            survivals.Add(survivalInt);
                                        } else
                                        {
                                            log.Error("Survival rule out of range!");
                                        }
                                    }

                                } else if (args[i + j].StartsWith("("))
                                {
                                    string arg = args[i + j].Replace("(", "").Replace(")", "");
                                    string[] rangeArr = arg.Split(new[] { "..." }, StringSplitOptions.None);
                                    if (rangeArr.Length == 2)
                                    {
                                        bool isRangeInt1 = int.TryParse(rangeArr[0], out int rangeInt1);
                                        bool isRangeInt2 = int.TryParse(rangeArr[1], out int rangeInt2);
                                        if (isRangeInt1 && isRangeInt2 && rangeInt1 <= rangeInt2)
                                        {
                                            if (type == "moore")
                                            {
                                                if (rangeInt2 <= (Math.Sqrt(2 * order + 1) - 1) && rangeInt2 >= 0)
                                                {
                                                    for (int k = rangeInt1; k <= rangeInt2; k++)
                                                    {
                                                        survivals.Add(k);
                                                    }
                                                } else
                                                {
                                                    log.Error("Survival rule out of range!");
                                                }
                                            }
                                            else if (type == "vonNeumann")
                                            {
                                                if (rangeInt2 <= (2 * Math.Sqrt(order) + (order * 2)) && rangeInt2 >= 0)
                                                {
                                                    for (int k = rangeInt1; k <= rangeInt2; k++)
                                                    {
                                                        survivals.Add(k);
                                                    }
                                                } else
                                                {
                                                    log.Error("Survival rule out of range!");
                                                }
                                            }
                                        }
                                    }
                                } else if (args[i + j].StartsWith("--"))
                                {
                                    break;
                                } else
                                {
                                    log.Error("You must provide the birth rule in integers!");
                                }
                            }
                            sucess = true;
                        }
                        break;

                    case "--birth":
                        if (args.Length - i <= 1)
                        {
                            log.Error("You must provide the birth values!");
                        }
                        else
                        {
                            births.Clear();
                            for (int j = 1; j <= args.Length - i - 1; j++)
                            {
                                bool isInt = int.TryParse(args[i + j], out int birthInt);
                                if (isInt)
                                {
                                    if (type == "moore")
                                    {
                                        if (birthInt <= (Math.Sqrt(2 * order + 1) - 1) && birthInt >= 0)
                                        {
                                            births.Add(birthInt);
                                        }
                                        else
                                        {
                                            log.Error("Birth rule out of range!");
                                        }
                                    }
                                    else if (type == "vonNeumann")
                                    {
                                        if (birthInt <= (2 * Math.Sqrt(order) + (order * 2)) && birthInt >= 0)
                                        {
                                            births.Add(birthInt);
                                        }
                                        else
                                        {
                                            log.Error("Birth rule out of range!");
                                        }
                                    }

                                }
                                else if (args[i + j].StartsWith("("))
                                {
                                    string arg = args[i + j].Replace("(", "").Replace(")", "");
                                    string[] rangeArr = arg.Split(new[] { "..." }, StringSplitOptions.None);
                                    if (rangeArr.Length == 2)
                                    {
                                        bool isRangeInt1 = int.TryParse(rangeArr[0], out int rangeInt1);
                                        bool isRangeInt2 = int.TryParse(rangeArr[1], out int rangeInt2);
                                        if (isRangeInt1 && isRangeInt2 && rangeInt1 <= rangeInt2)
                                        {
                                            if (type == "moore")
                                            {
                                                if (rangeInt2 <= (Math.Sqrt(2 * order + 1) - 1) && rangeInt2 >= 0)
                                                {
                                                    for (int k = rangeInt1; k <= rangeInt2; k++)
                                                    {
                                                        births.Add(k);
                                                    }
                                                }
                                                else
                                                {
                                                    log.Error("Birth rule out of range!");
                                                }
                                            }
                                            else if (type == "vonNeumann")
                                            {
                                                if (rangeInt2 <= (2 * Math.Sqrt(order) + (order * 2)) && rangeInt2 >= 0)
                                                {
                                                    for (int k = rangeInt1; k <= rangeInt2; k++)
                                                    {
                                                        births.Add(k);
                                                    }
                                                }
                                                else
                                                {
                                                    log.Error("Birth rule out of range!");
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (args[i + j].StartsWith("--"))
                                {
                                    break;
                                } else
                                {
                                    log.Error("You must provide the birth rule in integers!");
                                }
                            }
                            sucess = true;
                        }
                        break;

                    case "--ghost":
                        ghost = true;
                        sucess = true;
                        break;
                }
            }

            // Show sucess message for parsing arguments
            if (sucess)
            {
                log.Success("Command line arguments processed.");
            }

            // Display values
            log.Information($"The program will use the following settings:\n\n" +
                                    $"{"Input File",20}: {filename}\n" +
                                    $"{"Generations",20}: {generations}\n" +
                                    $"{"Refresh Rate",20}: {updateRate} updates/s\n" +
                                    $"{"Periodic",20}: {periodic}\n" +
                                    $"{"Rows",20}: {rows}\n" +
                                    $"{"Columns",20}: {cols}\n" +
                                    $"{"Random Factor",20}: {random * 100:0.00}%\n" +
                                    $"{"Step Mode",20}: {step}\n" +

                                    $"{"Neighbourhood Type",20}: {type}\n" +
                                    $"{"Neighbourhood Order",20}: {order}\n" +
                                    $"{"Centre Count",20}: {centreCount}\n" +
                                    $"{"Survivals",20}: {string.Join(", ", survivals.ToArray())}\n" +
                                    $"{"Births",20}: {string.Join(", ", births.ToArray())}\n" +
                                    $"{"Ghost Mode",20}: {ghost}\n");

            // Create new grid and stimulation
            Grid grid = new Grid(rows, cols);
            Life life = new Life(rows, cols, periodic, random, generations, updateRate, step, filename, grid, type, order, centreCount, survivals, births, memory, ghost);

            // Wait for user to press a key...
            Console.WriteLine("Press spacebar to start stimulation...");
            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) { }
            Console.Clear();

            // Initialize the grid window (this will resize the window and buffer)
            grid.InitializeWindow();

            // Add stopwatch
            Stopwatch watch = new Stopwatch();

            // Initialize grid with cells
            life.Initialise();
            grid.SetFootnote("Initialised");
            grid.Render();

            // Loop the grid with assigned generations and update rate
            int updateCount = 0;
            while (updateCount < generations)
            {
                // Start watch for required update rate
                watch.Restart();
                while (watch.ElapsedMilliseconds < (1000 / updateRate)) ;

                // Begin evolution by appling rules of Life
                life.Evolve();

                // Generational memory
                if (life.countGen >= memory)
                {
                    grid.IsComplete = true;
                    grid.Render();
                    grid.RevertWindow();
                    Console.Clear();
                    if (!life.nonPeriodic)
                    {
                        Console.WriteLine($"A steady state is reached, the periodicity of steady state is {generations - updateCount}");
                    } else
                    {
                        Console.WriteLine("A steady state is reached, the periodicity of steady state is N/A.");
                    }
                    break;
                }

                // Step mode
                if (step)
                {
                    while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) { }
                }

                // Display iterations
                grid.SetFootnote($"Iterations: {updateCount + 1}");

                // Render the grid to complete display of one iteration
                grid.Render();
                updateCount++;
            }

            // Set complete marker as true
            grid.IsComplete = true;

            // Render updates to the console window (grid should now display COMPLETE)...
            grid.Render();

            // Wait for user to press a key...
            Console.ReadKey();

            // Revert grid window size and buffer to normal
            grid.RevertWindow();
        }
    }
}
