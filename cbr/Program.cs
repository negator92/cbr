using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace cbr
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args == null || args.Length > 1)
                args = new string[] { "R01235" };
            GetCurrency(args[0]).Wait();
        }

        public static async Task GetCurrency(string currency)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string address = $"https://www.cbr.ru/scripts/XML_daily_eng.asp?date_req={DateTime.Today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}";
                    byte[] bytes = await httpClient.GetByteArrayAsync(address);
                    string xmlStringUTF = Encoding.UTF8.GetString(bytes);
                    XDocument xDocument = XDocument.Parse(xmlStringUTF);
                    XElement xElement = xDocument.XPathSelectElement($"//*[@ID=\"{currency}\"]");
                    xElement = xElement.XPathSelectElement("Value");
                    Console.WriteLine(xElement.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}