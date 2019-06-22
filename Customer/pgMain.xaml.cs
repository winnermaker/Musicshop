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
    public sealed partial class pgMain : Page
    {
        public pgMain()
        {
            this.InitializeComponent();
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            if (lstCategory.SelectedItem != null)
                Frame.Navigate(typeof(pgCategory), lstCategory.SelectedItem);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                lstCategory.ItemsSource = await ServiceClient.GetCategoryNamesAsync();
            }
            catch (Exception)
            {
                
            }
        }

        private void LstCategory_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (lstCategory.SelectedItem != null)
                Frame.Navigate(typeof(pgCategory), lstCategory.SelectedItem);
        }
    }
}
