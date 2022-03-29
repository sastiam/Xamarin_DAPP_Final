using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace App2.Models
{
    public class TecladosData
    {
        public static DataSet data;

        public TecladosData(DataSet dataSet)
        {
            data = dataSet;
        }
    }
}
