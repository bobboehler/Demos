using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaManiaDemo.Models
{
    /// <summary>
    /// Pizza object and its toppings
    /// </summary>
    public class Pizza
    {
        public string PizzaType { get; set; }
        public int Count { get; set; }

        [Newtonsoft.Json.JsonProperty("toppings")]
        public string[] Toppings { get; set; }
    }
}
