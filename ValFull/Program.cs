using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ValFull
{
    class Program
    {
        static void Main(string[] args)
        {
            GetValues().Wait();
        }

        public static async Task GetValues()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string address = "http://www.cbr.ru/scripts/XML_valFull.asp";
                    byte[] bytes = await httpClient.GetByteArrayAsync(address);
                    string xmlStringUTF = Encoding.UTF8.GetString(bytes);
                    XDocument xDocument = XDocument.Parse(xmlStringUTF);
                    IEnumerable<XElement> elements = xDocument.Element("Valuta").Elements("Item");
                    foreach (XElement element in elements)
                    {
                        Console.WriteLine($"case (\"{element.Element("ISO_Char_Code").Value}\"):");
                        Console.WriteLine($"\treturn \"{element.Element("ParentCode").Value.TrimEnd()}\";");
                    }
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}