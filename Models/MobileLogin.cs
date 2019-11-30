using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpoApp.Models
{
    public class MobileLogin
    {
        public bool UserAndPasswordCorrect { get; set; }
        public String UserType { get; set; }
    }
}
