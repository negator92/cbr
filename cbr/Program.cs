using System;
using System.Globalization;
using System.Net;
using System.Xml.Linq;
using System.Xml.XPath;

namespace cbr
{
    internal class Program
    {
        private static void Main()
        {
            using (WebClient webClient = new WebClient())
            {
                string dateName = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                string xmlString = webClient.DownloadString($"https://www.cbr.ru/scripts/XML_daily_eng.asp?date_req={dateName}");
                XDocument xDocument = XDocument.Parse(xmlString);
                XElement xElement = xDocument.XPathSelectElement("//*[@ID=\"R01235\"]");
                xElement = xElement.XPathSelectElement("Value");
                Console.WriteLine(xElement.Value);
            }
        }
    }
}