using static RobotController.Place_dimensions;
using static RobotController.Position_and_Orientation;
using static RobotController.Navigation;

namespace RobotController
{
    public class Program
    {
        static async Task Main() // One main task to run all tasks in the app
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("----------ROBOT CONTROLLER----------\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nPlease follow the steps to MOVE your Robot\n");
            Console.ResetColor();
            while (true)
            {
                var dimensions = await GetDimensionsAsync();
                int width = dimensions.width;
                int depth = dimensions.depth;
                Field[,] floor = new Field[depth, width];
                InitializeFields(floor);
                InitializeWires(floor);
                var position = await GetPositionAndOrientationAsync(width, depth);
                int X = position.x;
                int Y = position.y;
                char orientation = position.orientation;
                DisplayMesh(floor, X, Y, orientation);

                string commands = await GetNavigationsAsync();
                try
                {
                    ProcessCommands(floor, ref X, ref Y, ref orientation, commands);
                    DisplayNewMesh(floor, ref X, ref Y, orientation);
                    if (X >= 0 && Y >= 0)
                    {
                        Console.WriteLine($"Report: {Y} {X} {orientation}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Environment.Exit(1);
                }
                Console.WriteLine("\nDo you want to start move Robot again? (y/n): ");
                string restart = Console.ReadLine().ToLower();
                if (restart != "y")
                {
                    break;
                }

            }
        }
    }
}
