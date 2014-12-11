using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace Wieszak_Helper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        /// 

        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //TODO: handle auto-clear loader
            
            
            //load numerek
            string currentSettingsNumerekVal = readNumerek();
            numerek.Text = currentSettingsNumerekVal;
            string currentTexFieldVal = numerek.Text;
            if (currentTexFieldVal.Length > 0)
                if (currentTexFieldVal[currentTexFieldVal.Length - 1] == '0')
                {
                    numerek.Foreground = czerwony;
                }
                else
                {
                    numerek.Foreground = czarny;
                }
            

           
        }

        MessageDialog msgbox_about = new MessageDialog("Wieszak helper is app by\n Daniel Skowroński <daniel@dsinf.net>\n\nBackgroud clothes hangers photo by\n Keeonb2012 released under Creative Commons Share Alike 3.0.");
        MessageDialog msgbox_wrongnumer = new MessageDialog("Number you entered is not valid\n [can be empty or 0..999]");
        MessageDialog msgbox_notimplemented = new MessageDialog("Not yet implemented, senpai :(");

        SolidColorBrush czerwony = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 120, 0, 0));
        SolidColorBrush czarny   = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));


        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await msgbox_about.ShowAsync();  
        }


        private void numere_zmienione(object sender, TextChangedEventArgs e)
        {
            string currentTexFieldVal = numerek.Text;
            if (currentTexFieldVal.Length > 0)
                if (currentTexFieldVal[currentTexFieldVal.Length - 1] == '0')
                {
                    numerek.Foreground = czerwony;
                }
                else
                {
                    numerek.Foreground = czarny;
                }
            
            if (validNumerek(currentTexFieldVal)) saveNumerek(currentTexFieldVal);
            else msgbox_wrongnumer.ShowAsync();

            

        }

        private void numere_focus(object sender, RoutedEventArgs e)
        {
            numerek.Text = "";//semi smart clear
        }

        private void numere_unfocus(object sender, RoutedEventArgs e)
        {
            string currentTexFieldVal = numerek.Text;
            if (validNumerek(currentTexFieldVal)) saveNumerek(currentTexFieldVal);
            else { msgbox_wrongnumer.ShowAsync(); numerek.Text = ""; }
        }

        private bool isDigit(char c)
        {
            if (c >= '0' && c <= '9') return true; else return false;
        }
        /*private bool areSimiliar(string a, string b)
        {
            //ale zryta funkcja...
            if (a == b) return true;
            if (a.Length > b.Length) return false;
            for (int i = 1; i < b.Length; i++ )
                if (a[a.Length - i] != b[b.Length - i]) return false; //ostatnia pozycja


            return true;
        }
        */
        private bool validNumerek(string val)
        {
            if (val.Length > 3) return false;
            for (int i = 0; i < val.Length; i++)
                if (!isDigit(val[i]))
                    return false;

            return true;
        }

        private void saveNumerek(string val)
        {
            XmlDocument tileXml = new XmlDocument();
            tileXml.LoadXml("<tile><visual><binding template=\"TileSquareText01\"><text id=\"1\">WIESZAK "+val+"</text></binding>  </visual></tile>");

            TileNotification tileNotification = new TileNotification(tileXml);

            tileNotification.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(60*60*24);

            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);




            var _settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            _settings.Values["NUMEREK"]=val;
            
        }

        private string readNumerek()
        {
            var _settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Object value = _settings.Values["NUMEREK"];
            if (value == null) value = "";
            return value.ToString();
        }

        private void AppBarButton2_Click(object sender, RoutedEventArgs e)
        {
            //toggle auto-clear
            msgbox_notimplemented.ShowAsync();
        }

        private void pokaz_poprzedni(object sender, RoutedEventArgs e)
        {
            msgbox_notimplemented.ShowAsync();
        }

        private void przypnij(object sender, RoutedEventArgs e)
        {
            msgbox_notimplemented.ShowAsync();
        }

    }
}
