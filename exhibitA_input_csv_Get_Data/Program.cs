using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace CSV_Edit
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = null;
            int line_number = 0;
            string file_path = "";
            string connectionString = "Data Source =DB2; Initial Catalog = StudyZone; Integrated Security = True;";

            DirectoryInfo dirInfo = new DirectoryInfo("D:\\Import\\");
            FileInfo[] files = dirInfo.GetFiles("exhibitA-input.csv");

            if (files.Length > 0)
            {


                foreach (var path in files)
                {
                    file_path = dirInfo + path.ToString();

                    line_number = 0;
                    using (StreamReader reader = new StreamReader(file_path))
                    {
                        using (FileStream fs = new FileStream(file_path, FileMode.Append, FileAccess.Write))

                            while ((line = reader.ReadLine()) != null)
                            {
                                line_number++;

                                using (SqlConnection connection = new SqlConnection(connectionString))
                                {
                                    string sep = "\t";
                                    string[] split_line = line.Split(sep.ToCharArray());

                                    SqlCommand cmd = new SqlCommand();

                                    cmd.CommandText = "INSERT INTO StudyZone.dbo.exhibitA_input (PLAY_ID,SONG_ID,CLIENT_ID,PLAY_TS)"
                                        + " VALUES ('" + split_line[0] + "'," + split_line[1] + "," + split_line[2] + ",'" split_line[3] + "');";
                                    cmd.Connection = connection;

                                    connection.Open();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                    }
                }

            }
        }
    }
}
