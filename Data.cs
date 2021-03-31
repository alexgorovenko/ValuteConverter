using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuteConverter
{
    class Data
    {
        public DateTime Date { get; set; }
        public DateTime PreviousDate { get; set; }
        public string PreviousURL { get; set; }
        public DateTime Timestamp { get; set; }
        public Dictionary<string, Currency> Valute { get; set; }

        public Data(DateTime date, DateTime previousDate, string previousURL, DateTime timestamp, Dictionary<string, Currency> valute)
        {
            Date = date;
            PreviousDate = previousDate;
            PreviousURL = previousURL;
            Timestamp = timestamp;
            Valute = valute;
        }
    }
}
