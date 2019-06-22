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
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Customer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pgOrder : Page
    {
        private clsAllInstruments _Instrument;
        private clsMyOrder _Order = new clsMyOrder();
        public pgOrder()
        {
            this.InitializeComponent();
        }

        private void UpdateDisplay()
        {
            txtPriceSingleInst.Text = _Instrument.Price.ToString().EmptyIfNull();
            txtQuantity.Text = _Instrument.Quantity.ToString().EmptyIfNull();
            txtInstrument.Text = _Instrument.InstrumentName.EmptyIfNull();
            txtTotalPrice.Text = TotalPrice().ToString().EmptyIfNull();
        }

        private void pushData()
        {
            _Order.CustMail = txtCustMail.Text.EmptyIfNull();
            _Order.CustName = txtCustName.Text.EmptyIfNull();
            _Order.CustPhone = txtCustPhone.Text.EmptyIfNull();
            _Order.OrderDate = DateTime.Now;
            _Order.Quantity = Convert.ToInt16(txtOrderQuantity.Text);
            _Order.SerialNo = _Instrument.SerialNo;
            _Order.OrderPrice = Convert.ToDecimal(txtTotalPrice.Text);
        }

        private decimal TotalPrice()
        {
            try
            {
                return Convert.ToInt16(txtOrderQuantity.Text) * (_Instrument.Price);
            }
            catch (Exception)
            {
                return 0;
            }
           
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
        }

        private async void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("Confirm Order","Confirm Order");
            dialog.Commands.Add(new UICommand ("Yes", null));
            dialog.Commands.Add(new UICommand("Yes", null));
            var result = await dialog.ShowAsync();

            if(result.Label == "Yes")
            {
                pushData();
                try
                {
                    await Customer.ServiceClient.InsertOrderAsync(_Order);
                    txtMessage.Text = "Order placed";
                }
                catch (Exception)
                {
                    txtMessage.Text = "An Error Occured";
                }
            }
            else
            {

                Frame.GoBack();
            }
        }

        private void BtnCancle_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _Instrument = (clsAllInstruments)e.Parameter;
        }

        private void TxtOrderQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDisplay();
        }
    }
}
