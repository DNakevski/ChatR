using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatR.Models
{
    public class ConnectedUserModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
    }
}