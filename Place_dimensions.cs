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
            await Task.Run(() =>
            {
                Console.Write("Enter the width of the floor: ");
                width = int.Parse(Console.ReadLine());
                Console.Write("Enter the depth of the floor: ");
                depth = int.Parse(Console.ReadLine());
            });
            return (width, depth);
        }
        public static void InitializeFields(Field[,] floor)
        {
            for (int i = 0; i < floor.GetLength(0); i++)
            {
                for (int j = 0; j < floor.GetLength(1); j++)
                {
                    floor[i, j] = new Field
                    {
                        X = i,
                        Y = j,
                        Value = 0
                    };
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
