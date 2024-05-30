using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RobotController.Position_and_Orientation;

namespace RobotController
{
    public class Navigation // Here a Task to set a navigation command to navigate the robot with special characters
    {
        public static async Task<string> GetNavigationsAsync()
        {
            string commands = "";
            await Task.Run(() =>
            {
                Console.Write("Enter the navigation commands (L, R, F): ");
                commands = Console.ReadLine().ToUpper();
                foreach (char command in commands)
                {
                    if (command != 'L' && command != 'R' && command != 'F')
                    {
                        throw new ArgumentException("Invalid command. Must be one of L, R, F.");
                    }
                }
            });
            return commands;
        }
        public static void ProcessCommands(Field[,] floor, ref int x, ref int y, ref char orientation, string commands)
        {
            foreach (char command in commands)
            {
                switch (command)
                {
                    case 'L':
                        orientation = TurnLeft(orientation);
                        break;
                    case 'R':
                        orientation = TurnRight(orientation);
                        break;
                    case 'F':
                        MoveForward(floor, ref x, ref y, orientation);
                        break;
                }
            }
        }
    }
}

