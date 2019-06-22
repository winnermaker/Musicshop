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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Customer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pgCategory : Page
    {
        private clsCategory _Category;
        public pgCategory()
        {
            this.InitializeComponent();
        }
        private void UpdateDisplay()
        {
            txtDescription.Text = _Category.CategoryDescription;
            TxtCategoryLable.Text = _Category.CategoryName;
            lstInstruments.ItemsSource = _Category.InstrumentsList;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            try
            {
                string lcCategoryName = e.Parameter.ToString();
                _Category = await Customer.ServiceClient.GetCategoryAsync(lcCategoryName);
                UpdateDisplay();
            }
            catch (Exception)
            {

            }
         
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            if (lstInstruments.SelectedItem != null)
                Frame.Navigate(typeof(pgInstrument), lstInstruments.SelectedItem);
        }

        private void LstInstruments_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (lstInstruments.SelectedItem != null)
                Frame.Navigate(typeof(pgInstrument), lstInstruments.SelectedItem);
        }
    }
}
