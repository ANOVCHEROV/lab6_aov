using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCproject_Zoo.Models
{
    public class Animals
    {
        public Animals(string d)
        {
            Information = d;
        }
        public Animals() { }
        public int Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public DateTime AddingDate { get; set; }
        public List<Animals> animals = new List<Animals>();
    }
}