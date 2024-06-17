using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController
{
    public class Field 
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; } = 0;
        public Field? Right { get; set; } 
        public Field? Bottom { get; set; }
    }
}
