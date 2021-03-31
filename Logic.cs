using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ValuteConverter
{
    class Logic
    {
        private string _url;
        private Data _data;

        public Logic(string url)
        {
            _url = url;
        }

        public string GetHtmlPage()
        {
            string HtmlText = string.Empty;
            HttpWebRequest myHttwebrequest = (HttpWebRequest)HttpWebRequest.Create(_url);
            HttpWebResponse myHttpWebresponse = (HttpWebResponse)myHttwebrequest.GetResponse();
            StreamReader strm = new StreamReader(myHttpWebresponse.GetResponseStream());

            HtmlText = strm.ReadToEnd();

            return HtmlText;
        }
        public void Parse()
        {
            string jsonString = GetHtmlPage();

            _data = JsonConvert.DeserializeObject<Data>(jsonString);
            
            if (!_data.Valute.Keys.Contains("RUB"))
            {
                Currency _tmp = new Currency("R000001", "643", "RUB", "Российский рубль", 1, 1, 1);
                _data.Valute.Add("RUB", _tmp);
            }
        }

        public List<Currency> GetList()
        {
            if (_data == null)
                Parse();
            return _data.Valute.Values.ToList();
        }

        public double Convert(string first, string second, double multiplier)
        {
            Currency _first = _data.Valute.FirstOrDefault(x => x.Value._name == first).Value;
            Currency _second = _data.Valute.FirstOrDefault(x => x.Value._name == second).Value;

            return Math.Round( multiplier * ((_first._value / _first._nominal) / (_second._value / _second._nominal)), 2);
        }
    }
}
