using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace PizzaManiaDemo.DataContext
{
    
    public class Context
    {
        /// <summary>
        /// Pull the latest pizza order list from the remote file server.
        /// </summary>
        /// <returns></returns>
        public string GetJsonString()
        {
            // Use the web IO object to read the list of pizzas
            string url = "http://files.olo.com/pizzas.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Assign the blob text to a string.
            string jsonText = "";
            using (var sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                jsonText = sr.ReadToEnd();
            }

            return jsonText;
        }
    }
}
