using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Wshark_C
{
    internal class Interval
    {
        public int up;
        public int down;

        public Interval(int down, int up)
        {
            this.up = up;
            this.down = down;
        }

        public override string ToString()
        {
            return "[" +  down.ToString() + " - " + up.ToString() + ")";
        }
    }
}