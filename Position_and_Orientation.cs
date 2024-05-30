using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RobotController.Place_dimensions;

namespace RobotController
{
    public class Position_and_Orientation // Here a Task to set position and orientation
    {
        public static async Task<(int x, int y, char orientation)> GetPositionAndOrientationAsync(int maxWidth, int maxDepth)
        {
            int x = 0;
            int y = 0;
            char orientation = 'W';
            await Task.Run(() =>
            {
                Console.Write("Enter the start X coordinate: ");
                x = int.Parse(Console.ReadLine());

                Console.Write("Enter the start Y coordinate: ");
                y = int.Parse(Console.ReadLine());
                Console.Write("Enter the Orientation (N, E, S, W): ");
                orientation = char.Parse(Console.ReadLine().ToUpper());             
                if (x < 0 || x >= maxWidth || y < 0 || y >= maxDepth)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"ERROR: Out of bounds at {x} {y}\n");
                    Console.ResetColor();
                }
                if (orientation != 'N' && orientation != 'E' && orientation != 'S' && orientation != 'W')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Invalid orientation. Please Enter one of N, E, S, W.");
                    Console.ResetColor();
                }
            });
            return (x, y, orientation);
        }
        public static char TurnLeft(char orientation)
        {
            return orientation switch
            {
                'N' => 'W',
                'W' => 'S',
                'S' => 'E',
                'E' => 'N',
                _ => orientation,
            };
        }
        public static char TurnRight(char orientation)
        {
            return orientation switch
            {
                'N' => 'E',
                'E' => 'S',
                'S' => 'W',
                'W' => 'N',
                _ => orientation,
            };
        }
        public static void MoveForward(Field[,] floor, ref int x, ref int y, char orientation)
        {
            int newX = x;
            int newY = y;
            switch (orientation)
            {
                case 'N':
                    newX--;
                    break;
                case 'E':
                    newY++;
                    break;
                case 'S':
                    newX++;
                    break;
                case 'W':
                    newY--;
                    break;
            }
            if (newX < 0 || newX >= floor.GetLength(0) || newY < 0 || newY >= floor.GetLength(1))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"ERROR: Out of bounds at {newY} {newX}\n");  
                Console.ResetColor();
            }
            x = newX;
            y = newY;
        }
        public static void DisplayNewMesh(Field[,] floor, ref int x, ref int y, char orientation)
        {
            int newX = x;
            int newY = y;
            for (int i = 0; i < floor.GetLength(0); i++)
            {
                for (int j = 0; j < floor.GetLength(1); j++)
                {
                    if (i == newX && j == newY)
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

