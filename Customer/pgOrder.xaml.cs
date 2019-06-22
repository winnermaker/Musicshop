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
using System.Text.RegularExpressions;

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
            _Instrument.Quantity -= _Order.Quantity;
        }

        private decimal TotalPrice()
        {
            try
            {
                return Convert.ToInt16(txtOrderQuantity.Text) * (_Instrument.Price);
            }
            catch
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
            if (Convert.ToInt16(txtOrderQuantity.Text) != 0 && txtCustName.Text != "" && txtCustPhone.Text != "" && txtCustMail.Text != "") {

                MessageDialog dialog = new MessageDialog("Do you want to confirm your Order?", "Confirm Order");
                dialog.Commands.Add(new UICommand ("Yes", null));
                dialog.Commands.Add(new UICommand("No", null));
                var result = await dialog.ShowAsync();

                if(result.Label == "Yes")
                {
                    try
                    {
                        pushData();
                        await Customer.ServiceClient.InsertOrderAsync(_Order);
                        await Customer.ServiceClient.UpdateInstrumentAsync(_Instrument);
                        UpdateDisplay();
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
            else
            {
                txtMessage.Text = "At least one Field is empty or Quantity is 0";
            }
        }
      
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _Instrument = (clsAllInstruments)e.Parameter;
            txtOrderQuantity.Text = "1";
            if (_Instrument.Quantity == 0)
            {
                btnOrder.IsEnabled = false;
                txtMessage.Text = "Sorry - this instrument is out of Stock";
            }
        }

        private void TxtOrderQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int enteredQuantity = Convert.ToInt16(txtOrderQuantity.Text);
                if (enteredQuantity > _Instrument.Quantity)
                {
                    txtOrderQuantity.Text = _Instrument.Quantity.ToString();
                    txtMessage.Text = "Quantity to big - Quantity was set maximum Value";
                }
                txtMessage.Text = "";
                btnOrder.IsEnabled = true;
                UpdateDisplay();
            }
            catch (Exception)
            {
                txtMessage.Text = "Please put a number into Quantity Field";
                btnOrder.IsEnabled = false;
            }

        }

        private void TxtCustName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex(@"^([a-zA-z]+(-[a-zA-Z]+)?) ([a-zA-z]+(-[a-zA-Z]+)?)$");
            Match match = regex.Match(txtCustName.Text);
            if (!match.Success)
            {
                txtMessage.Text = "Please enter valid Name. Valid names look like this: Max Mustermann, Max-Peter Mustermann, Max Muster-Mann, Max-Peter Muster-Mann ";
            }
            else
            {
                txtMessage.Text = "";
            }


        }

        private void TxtCustPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex(@"^((\+[1-9]{1,4})?[1-9]+|0[1-9]+)(([-/][0-9]+)?|[0-9]+)$");
            Match match = regex.Match(txtCustPhone.Text);
            if (!match.Success)
            {
                txtMessage.Text = "Please enter valid Phonenumber. Valid number look like this: +491768042789, 01768042789, 0176/8042789 or 0176-8042789";
            }
            else
            {
                txtMessage.Text = "";
            }
        }

        private void TxtCustMail_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Regex regex = new Regex(@"\d+");
            //Match match = regex.Match("Dot 55 Perls");
            //if (match.Success)
            Regex regex = new Regex(@"^([a-zA-Z0-9]+([-_.]*[a-zA-Z0-9]+)*)+@([a-zA-Z0-9]+([-_.]*[a-zA-Z0-9]+)*)+\.[a-zA-Z]{2,3}$");
            Match match = regex.Match(txtCustMail.Text);
            if (!match.Success)
            {
                txtMessage.Text = "Please enter valid E-Mailaddress like exmaple@examplemail.com";
            }
            else
            {
                txtMessage.Text = "";
            }
        }
    }
}
