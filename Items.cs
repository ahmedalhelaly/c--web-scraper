using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{
    public class Items
    {
        public object country { get; set; }
        public object total_cases { get; set; }
        public object new_cases { get; set; }
        public object total_deaths { get; set; }
        public object new_deaths { get; set; }
        public object total_recovered { get; set; }
        public object active_cases { get; set; }
        public object critical_cases { get; set; }
        public object total_tests { get; set; }
        public object last_updated { get; set; }

    }
}
