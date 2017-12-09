using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaManiaDemo.Models;
using Newtonsoft.Json.Linq;

namespace PizzaManiaDemo.ViewModels
{
    public class MainViewModel
    {
        /// <summary>
        /// Writes the top 20 pizza orders to the screen.
        /// </summary>
        public static void WriteToScreen()
        {
            // Pull in the data source from the context object.
            string jsonText = new PizzaManiaDemo.DataContext.Context().GetJsonString();

            // Deserialize the array into a strongly typed pizza array.
            var pizzaList = Newtonsoft.Json.JsonConvert.DeserializeObject<Pizza[]>(jsonText);
            HashSet<Pizza> pizzaHash = new HashSet<Pizza>();

            // Iterate through the pizzas, grabbing on the unique entries for the summary table.
            int grandTotal = 0;
            foreach (var p in pizzaList)
            {
                bool exists = pizzaHash.Where(p2 => p2.Toppings.SequenceEqual(p.Toppings)).Count() > 0;
                if (!exists)
                {

                    // Grab the total orders for the current pizza
                    var count = pizzaList.Where(p1 => p1.Toppings.SequenceEqual(p.Toppings)).Count();
                    grandTotal += count;

                    // Clone the Pizza object so we can add the Count 
                    // and have only one instance
                    Pizza sumPizza = new Pizza();
                    sumPizza.Toppings = new string[p.Toppings.Count()];
                    p.Toppings.CopyTo(sumPizza.Toppings, 0);
                    sumPizza.Count = count;
                    sumPizza.PizzaType = string.Join(",", sumPizza.Toppings);
                    pizzaHash.Add(sumPizza);

                    // Prepare objects and write to screen.
                    //Console.WriteLine("pizza key: {0}  total: {1}", sumPizza.PizzaType, count);
                }

            }

            // Create the final list with the top 20 ordered pizzas
            Pizza[] final = (Pizza[])pizzaHash.ToArray().OrderByDescending(pz => pz.Count).ToArray();

            //Audit the results to verify that the counts match between the input and summary
            var sumTotals = final.Sum(p => p.Count);
            if(grandTotal != sumTotals)
            {
                Console.WriteLine("Some Pizza totals are missing {0} != {1}", grandTotal, sumTotals);
            }

            int rank = 1;
            var top20 = final.Take(20);
            var max = (top20.Max(p => p.PizzaType.Length) + 1);
            // Write the totals to the screen
            foreach (var pizza in top20)
            {
                var ranks = String.Format("{0,3}", rank);
                var types =  pizza.PizzaType.PadRight(max);
                Console.WriteLine("Rank: {0} - Type: {1} - Total Orders: {2}", ranks, types, pizza.Count);
            }
        }
    }
}
