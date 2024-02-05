using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Models
{
    public class DCandidate
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public int age { get; set; }
        public string bloodGroup { get; set; }
        public string address { get; set; }
    }
}
