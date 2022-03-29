using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace App2.Models
{
    public class ComputadorasData
    {

        public static DataSet data;

        public ComputadorasData(DataSet dataSet)
        {
            data = dataSet;
        }
    }
}
