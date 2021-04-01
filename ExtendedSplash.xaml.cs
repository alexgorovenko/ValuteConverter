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
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.Core;
using SDKTemplate.Common;
using System.Threading.Tasks;

namespace ValuteConverter
{
    partial class ExtendedSplash : Page
    {
        internal Rect splashImageRect; 
        private SplashScreen splash; 
        internal bool dismissed = false; 
        internal Frame rootFrame;

        async void DismissExtendedSplash()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            rootFrame = new Frame();
            MainPage mainPage = new MainPage();
            rootFrame.Content = mainPage;
            Window.Current.Content = rootFrame;
            rootFrame.Navigate(typeof(MainPage));
        }
        private async void RestoreStateAsync(bool loadState)
        {
            if(loadState)
            {
                await SuspensionManager.RestoreAsync();
            }
        }
        void PositionImage()
        {
            extendedSplashImage.SetValue(Canvas.LeftProperty, splashImageRect.X);
            extendedSplashImage.SetValue(Canvas.TopProperty, splashImageRect.Y);
            extendedSplashImage.Height = splashImageRect.Height;
            extendedSplashImage.Width = splashImageRect.Width;
        }
        void ExtendedSplash_OnResize(Object sender, WindowSizeChangedEventArgs e)
        {
            if (splash != null)
            {
                splashImageRect = splash.ImageLocation;
                PositionImage();

                PositionRing();
            }
        }
        void PositionRing()
        {
            splashProgressRing.SetValue(Canvas.LeftProperty, splashImageRect.X + (splashImageRect.Width * 0.5) - (splashProgressRing.Width * 0.5));
            splashProgressRing.SetValue(Canvas.TopProperty, (splashImageRect.Y + splashImageRect.Height + splashImageRect.Height * 0.1));
        }
        void DismissedEventHandler(SplashScreen sender, object e)
        {
            dismissed = true;
        }
        public ExtendedSplash(SplashScreen splashscreen, bool loadState)
        {
            InitializeComponent();
            DismissExtendedSplash();
            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplash_OnResize);

            splash = splashscreen;
            if (splash != null)
            {
                splash.Dismissed += new TypedEventHandler<SplashScreen, Object>(DismissedEventHandler);

                splashImageRect = splash.ImageLocation;
                PositionImage();

                PositionRing();
            }

            rootFrame = new Frame();

            RestoreStateAsync(loadState);
        }
    }
}
