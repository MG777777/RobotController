using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController
{
    public class Place_dimensions // Here a Task to set how many fields the floor will have
    {
        public static async Task<(int width, int depth)> GetDimensionsAsync()
        {
            int width = 0;
            int depth = 0;
            bool iswidthValid = false;
            bool isdepthValid = false;

            await Task.Run(() =>
            {
                while (!iswidthValid || !isdepthValid)
                {
                    try
                    {
                        if (!iswidthValid)
                        {
                            Console.Write("Enter the width of the floor: ");
                            width  = int.Parse(Console.ReadLine());
                            if(width == null)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Width dimension cannot be empty. Please enter a value.");
                                Console.ResetColor();
                            }
                            else
                            {
                                if(width <= 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"ERROR: width dimention cannot be zero or less zero. Please enter a value above zero");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    iswidthValid = true;
                                }
                            }
                            if (iswidthValid && !isdepthValid) 
                            {
                                Console.Write("Enter the depth of the floor: ");
                                depth = int.Parse(Console.ReadLine());
                                if(depth == null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("depth dimention cannot be empty. Please enter a value.");
                                    Console.ResetColor();
                                }
                                else 
                                {

                                    if(depth <= 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine($"ERROR: depth dimention cannot be zero or less zero. Please enter a value above zero");
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        isdepthValid = true;
                                    }
                                }
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input format. Please enter numeric values for floor dimension.");
                        Console.ResetColor();
                    }
                }
            });
            return (width, depth);
        }
        public static void InitializeFields(Field[,] floor)
        {
            for (int i = 0; i < floor.GetLength(0); i++)
            {
                
                for (int j = 0; j < floor.GetLength(1); j++)
                {
                    floor[i, j] = new Field();
                }
            }
        }
        public static void InitializeWires(Field[,] floor)
        {
            for (int i = 0; i < floor.GetLength(0); i++)
            {
                for (int j = 0; j < floor.GetLength(1); j++)
                {
                    Field current = floor[i, j];
                    if (j < floor.GetLength(1) - 1)
                        current.Right = floor[i, j + 1];
                    if (i < floor.GetLength(0) - 1)
                        current.Bottom = floor[i + 1, j];
                }
            }
        }
        public static void DisplayMesh(Field[,] floor, int X, int Y, char orientation)
        {
            for (int i = 0; i < floor.GetLength(0); i++)
            {
                for (int j = 0; j < floor.GetLength(1); j++)
                {
                    if (i == X && j == Y)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{orientation} ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write($"{floor[i, j].Value} ");
                    }
                    if (floor[i, j].Right != null)
                        Console.Write("- ");
                }
                Console.WriteLine();
                for (int j = 0; j < floor.GetLength(1); j++)
                {
                    if (floor[i, j].Bottom != null)
                        Console.Write("|   ");
                    else
                        Console.Write("    ");
                }
                Console.WriteLine();
            }
        }
    }
}
