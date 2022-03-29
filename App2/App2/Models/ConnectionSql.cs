using System;
using System.Data;
using System.Data.SqlClient;

namespace App2.Models
{
    public class ConnectionSql
    {

        public static SqlConnection conn { get; set; }
        public static SqlDataAdapter adapter;
        public static SqlCommand cmd;

        public static DataSet data;

        public DataSet getData()
        {
            return data;
        }

        public SqlConnection connect()
        {
            string url = "Server=tcp:bdventas2021.database.windows.net,1433" +
                         ";Initial Catalog=BDVentas2021" +
                        ";Persist Security Info=False;User ID=sastiam;" +
                        "Password=Minino2001;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            try
            {
                conn = new SqlConnection(url);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                return conn;

            } catch (Exception ex)
            {
                Console.WriteLine("error: " + ex.Message);

                return null;
            }
        }

        public DataSet show(string query)
        {
            try
            {
                adapter = new SqlDataAdapter(query, conn);
                data = new DataSet();
                adapter.Fill(data, "show");

                return data;

            } catch(Exception ex)
            {
                Console.WriteLine("error: " + ex.Message);

                throw ex;

            
            }
        }

        public void execute(string query)
        {
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();


            } catch (Exception ex)
            {
                Console.WriteLine("error: " + ex.Message);
            }
        }
    }
}
