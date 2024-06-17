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
            bool isXValid = false;
            bool isYValid = false;
            bool isOrientationValid = false;
            await Task.Run(() =>
            {
                while (!isXValid || !isYValid || !isOrientationValid)
                {
                    try
                    {
                        if (!isXValid)
                        {
                            Console.Write("Enter the start X coordinate: ");
                            string? inputX = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(inputX))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("X coordinate cannot be empty. Please enter a value.");
                                Console.ResetColor();
                            }
                            else
                            {
                                x = int.Parse(inputX);
                                if (x < 0 || x >= maxWidth)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"ERROR: X coordinate out of bounds. Please enter a value between 0 and {maxWidth - 1}.");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    isXValid = true;
                                }
                            }
                        }
                        if (isXValid && !isYValid)
                        {
                            Console.Write("Enter the start Y coordinate: ");
                            string? inputY = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(inputY))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Y coordinate cannot be empty. Please enter a value.");
                                Console.ResetColor();
                            }
                            else
                            {
                                y = int.Parse(inputY);
                                if (y < 0 || y >= maxDepth)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"ERROR: Y coordinate out of bounds. Please enter a value between 0 and {maxDepth - 1}.");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    isYValid = true;
                                }
                            }
                        }
                        if (isXValid && isYValid && !isOrientationValid)
                        {
                            Console.Write("Enter the Orientation (N, E, S, W): ");
                            string inputOrientation = Console.ReadLine().ToUpper();
                            if (string.IsNullOrWhiteSpace(inputOrientation) || inputOrientation.Length != 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid orientation. Please enter one of N, E, S, W.");
                                Console.ResetColor();
                            }
                            else
                            {
                                orientation = char.Parse(inputOrientation);
                                if (orientation != 'N' && orientation != 'E' && orientation != 'S' && orientation != 'W')
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invalid orientation. Please enter one of N, E, S, W.");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    isOrientationValid = true;
                                }
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input format. Please enter numeric values for coordinates.");
                        Console.ResetColor();
                    }
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
                    newY--;
                    break;
                case 'E':
                    newX++;
                    break;
                case 'S':
                    newY++;
                    break;
                case 'W':
                    newX--;
                    break;
            }
            if (newX < 0 || newX >= floor.GetLength(0) || newY < 0 || newY >= floor.GetLength(1))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"ERROR: Out of bounds at {newX} {newY}\n");  
                Console.ResetColor();
            }
            x = newX;
            y = newY;
        }
    }
}

