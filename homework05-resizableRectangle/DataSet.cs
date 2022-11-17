using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework05_resizableRectangle
{
    internal class DataSet
    {
        List<DataPoint> dataPoints = new List<DataPoint>();

        public void SetDatapoints(List<DataPoint> dataPoints)
        {
            this.dataPoints = dataPoints;
        }

        public List<DataPoint> GetDataPoints()
        {
            return this.dataPoints;
        }

        public void Add(DataPoint data)
        {
            dataPoints.Add(data);
        }
    }
}
