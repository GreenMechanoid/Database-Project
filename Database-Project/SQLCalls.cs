// .NET 22 Daniel Svensson
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Project
{
    internal class SQLCalls
    {
        SqlCommand cmd;
        SqlConnection conn = new("Data Source=GREENMECHANOID;" + "Initial Catalog=ProjectDB;" + "Trusted_Connection=true");
        public SQLCalls()
        {
            cmd = new()
            {
                CommandTimeout = 60,// if command is endless or unresponsive, it will abort after 60sec
                Connection = conn,
                CommandType = CommandType.Text
            };
        }


        public void GetAllStaff()
        {

        }

        public void AddNewStaff()
        {

        }
    }
}
