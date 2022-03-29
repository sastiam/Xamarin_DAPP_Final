using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace App2.Models
{
    public class AudifonosData
    {
        public static DataSet data;

        public AudifonosData(DataSet dataSet)
        {
            data = dataSet;
        }
    }
}
