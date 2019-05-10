using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityProject {
    class SQLServerConnection {
        // Laptop
        //public const String connection = "Server=LAPTOP-PKU44692; Database=UniversityProject; Trusted_Connection=True";

        // PC
        //public const String connection = "Server=DESKTOP-G5R0M5H\\SQLEXPRESS; Database=UniversityProject; Trusted_Connection=True";
      
        // Local
        public const String connection = "Server=.\\SQLExpress; Database=UniversityProject; Trusted_Connection=True";
    }
}
