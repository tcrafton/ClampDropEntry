using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfterJackClampDropEntry.Models
{
    public class AfterJack
    {
        public int ID { get; set; }
        public DateTime EntryDate { get; set; }
        public string Crew { get; set; }
        public string Room { get; set; }
        public int Pot { get; set; }
        public int AnodeNum { get; set; }
        public int ClampDrop { get; set; }
    }
}