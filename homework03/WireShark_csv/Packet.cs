using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WireShark_csv
{
    public class Packet
    {
        public string No { get; set; }
        public string Time { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Protocol { get; set; }
        public string Length { get; set; }
        public string Info { get; set; }


        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }
    }
}
