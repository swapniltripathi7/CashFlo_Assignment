using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsService1
{  
    class DB_Link
    { 
        // calling the path to the local database stored in "DemoDB.db"
        public static string conn_link = "Data Source=" + Path.GetFullPath("DemoDB.db") + ";Version=3");
    }
}
