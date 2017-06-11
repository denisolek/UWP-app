using CurrencyChecker.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class CurrencyCalc : Page
    {
        public CurrencyCalc()
        {
            this.InitializeComponent();
            DataContext = new CurrencyCalcViewModel();
        }

        private CurrencyCalcViewModel GetCurrencyCalcViewModel()
        {
            return DataContext as CurrencyCalcViewModel;
        }

        private void Navigate_Back(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); 
            return !regex.IsMatch(text);
        }

        private async void Calculate(object sender, RoutedEventArgs e)
        {
            var emptyFieldsBox = new MessageDialog("Pick currency and ammount to calculate!");
            emptyFieldsBox.Title = "Warning";
            emptyFieldsBox.Commands.Add(new UICommand("Ok"));

            var numbersOnlyBox = new MessageDialog("You can calculate only numbers!");
            numbersOnlyBox.Title = "Warning";
            numbersOnlyBox.Commands.Add(new UICommand("Ok"));

            if (GetCurrencyCalcViewModel().ChoosenCurrency == null || GetCurrencyCalcViewModel().AmountToCalculate == null)
            {
                await emptyFieldsBox.ShowAsync();
            }
            else
            {
                if(!IsTextAllowed(GetCurrencyCalcViewModel().AmountToCalculate))
                {
                    await numbersOnlyBox.ShowAsync();
                }

                GetCurrencyCalcViewModel().Calculate();
            }
        }
    }
}
