using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuteConverter
{
    class Currency
    {
        public string _ID { get; set; }
        public string _numCode { get; set; }
        public string _charCode { get; set; }
        public string _name { get; set; }
        public double _value { get; set; }
        public double _previous { get; set; }
        public int _nominal { get; set; }

        public Currency(string iD, string numCode, string charCode, string name, double value, double previous, int nominal)
        {
            _ID = iD;
            _numCode = numCode;
            _charCode = charCode;
            _name = name;
            _value = value;
            _previous = previous;
            _nominal = nominal;
        }
    }
}
