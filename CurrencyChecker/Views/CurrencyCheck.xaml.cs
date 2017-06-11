using CurrencyChecker.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CurrencyChecker.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CurrencyCheck : Page
    {
        public CurrencyCheck()
        {
            this.InitializeComponent();
            DataContext = new CurrencyCheckViewModel();
        }

        private CurrencyCheckViewModel GetCurrencyCheckViewModel()
        {
            return DataContext as CurrencyCheckViewModel;
        }

        private void Navigate_Back(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private async void FetchCurrencyData(object sender, RoutedEventArgs e)
        {
            var warningBox = new MessageDialog("Pick a currency to check!");
            warningBox.Title = "Warning";
            warningBox.Commands.Add(new UICommand("Ok"));

            if(GetCurrencyCheckViewModel().ChoosenCurrency == null)
            {
                await warningBox.ShowAsync();
            }
            else
            {
                GetCurrencyCheckViewModel().UpdateCurrencyList();
            }    

        }
    }
}
