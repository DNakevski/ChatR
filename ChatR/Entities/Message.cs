using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatR.Entities
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string MessageText { get; set; }
    }
}