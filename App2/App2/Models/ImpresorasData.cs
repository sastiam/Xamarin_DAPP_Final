using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace App2.Models
{
    public class ImpresorasData 
    {
        public static DataSet data;

        public ImpresorasData(DataSet dataSet)
        {
            data = dataSet;
        }
        
    }
}
