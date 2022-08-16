using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFrameworkusingCSharp.Utilites
{
    public class jsonReader
    {
        public jsonReader()
        {

        }
        public string extractData(string tokenName)
        {
            string myjsonData = File.ReadAllText("Utilites/TestData.json");
            var jsonobject = JToken.Parse(myjsonData);
            return jsonobject.SelectToken(tokenName).Value<string>();
        }

        public string[] extractDataArray(string tokenName)
        {
            string myjsonData = File.ReadAllText("Utilites/TestData.json");
            var jsonobject = JToken.Parse(myjsonData);
            List<string> productList = jsonobject.SelectTokens(tokenName).Values<string>().ToList();
            return productList.ToArray();   
        }
    }
}
