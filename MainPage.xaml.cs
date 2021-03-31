using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ValuteConverter
{
    public sealed partial class MainPage : Page
    {
        private Logic _logic;
        private string _url = "https://www.cbr-xml-daily.ru/daily_json.js";
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            _logic = new Logic(_url);
        }

        private void ValuteSelect_click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton _tmp = (HyperlinkButton)sender;
            bool _isFirst;
            if (_tmp.Name == "_firstValuteSelect")
                _isFirst = true;
            else
                _isFirst = false;
            this.Frame.Navigate(typeof(SelectValute), new KeyValuePair<List<Currency>, bool>(_logic.GetList(), _isFirst));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {            
            if ((e.Parameter != null) && (e.Parameter is KeyValuePair<string, bool>))
            {
                KeyValuePair<string, bool> _tmp = (KeyValuePair<string, bool>)e.Parameter;
                if (_tmp.Value == true)
                    _firstRates.Text = _tmp.Key.ToString();
                else
                    _secondRates.Text = _tmp.Key.ToString();
            }
            base.OnNavigatedTo(e);
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;
            String newText = String.Empty;
            int count = 0;
            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsDigit(c) || Char.IsControl(c) || (c == ',' && count == 0))
                {
                    newText += c;
                    if (c == ',')
                        count += 1;
                }
            }
            textBox.Text = newText;
            textBox.SelectionStart = selectionStart <= textBox.Text.Length ? selectionStart : textBox.Text.Length;

            if ( (_firstRates.Text != "") && (_secondRates.Text != "") )
            {
                if ((textBox.Name == "_firstCount") && (textBox.Text != ""))
                {
                    _secondCount.Text = _logic.Convert(_firstRates.Text, _secondRates.Text, double.Parse(textBox.Text)).ToString();
                }
                if ((textBox.Name == "_secondCount") && (textBox.Text != ""))
                {
                    _firstCount.Text = _logic.Convert(_secondRates.Text, _firstRates.Text, double.Parse(textBox.Text)).ToString();
                }
            }

        }
    }
}
