using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpoApp.Models
{
    public class Login
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public String Name { get; set; }
        public String Password { get; set; }
        public String Typ { get; set; }
    }
}
