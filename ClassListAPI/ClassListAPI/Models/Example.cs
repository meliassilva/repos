using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace ClassListAPI.Models
{
    public class Example
    {
        //put here all the fields you need to return, return this object.
        //Example:just a simple prop 
        public int ID { get; set; }
        public string name { get; set; }
        public bool cacheBust { get; set; }

    }
}