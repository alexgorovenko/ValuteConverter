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
    public sealed partial class SelectValute : Page
    {
        private bool _isFirst;
        public SelectValute()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if ((e.Parameter != null) && (e.Parameter is KeyValuePair<List<Currency>, bool>))
            {
                KeyValuePair<List<Currency>, bool> _tmp = (KeyValuePair<List<Currency>, bool>)e.Parameter;
                if (_tmp.Value == true)
                {
                    _isFirst = true;
                }
                else
                {
                    _isFirst = false;
                }
                foreach (Currency _valute in _tmp.Key)
                {
                    Valutes.Items.Add(_valute._name);
                }
            }
            base.OnNavigatedTo(e);
        }

        private void Valutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KeyValuePair<string, bool> _kvp = new KeyValuePair<string, bool>(Valutes.SelectedItem.ToString(), _isFirst);
            this.Frame.Navigate(typeof(MainPage),  _kvp);
        }
    }
}
