using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Web;

namespace WebScraper
{
    public class Scraper
    {
        private Items _items;
        public Items Items
        {
            get { return _items; }
            set { _items = value; }
        }
        public List<Items> ScrapData(string pageUrl)
        {
            var web = new HtmlWeb();
            var doc = web.Load(pageUrl);
            var table = doc.DocumentNode.SelectNodes("//*[@id='main_table_countries_today']//tbody//tr");

            List<Items> Item_List = new List<Items>();

            try
            {
                foreach (var row in table)
                {
                    var country = row.SelectSingleNode("td[2]").InnerText.Trim().ToLower();
                    if (country.Length > 0 && !country.StartsWith("total"))
                    {
                        _items = new Items();
                        _items.country = country;
                        // why use HttpUtility.HtmlDecode ?
                        _items.total_cases = row.SelectSingleNode("td[3]").InnerText.Trim();
                        _items.new_cases = row.SelectSingleNode("td[4]").InnerText.Trim();
                        _items.total_deaths = row.SelectSingleNode("td[5]").InnerText.Trim();
                        _items.new_deaths = row.SelectSingleNode("td[6]").InnerText.Trim();
                        _items.total_recovered = row.SelectSingleNode("td[7]").InnerText.Trim();
                        _items.active_cases = row.SelectSingleNode("td[8]").InnerText.Trim();
                        _items.critical_cases = row.SelectSingleNode("td[9]").InnerText.Trim();
                        _items.total_tests = row.SelectSingleNode("td[12]").InnerText.Trim();
                        _items.last_updated = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss").Trim();

                        Item_List.Add(_items);
                        //yield return _items;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Item_List;
        }


    }
}
