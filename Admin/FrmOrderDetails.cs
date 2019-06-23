using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admin
{
    sealed partial class FrmOrderDetails : Form
    {
        private clsMyOrder _Order;
        private clsAllInstruments _Instrument;
        public static readonly FrmOrderDetails Instance = new FrmOrderDetails();
        private FrmOrderDetails()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        public async void ShowDialog(clsMyOrder prOrder)
        {
            _Order = prOrder;
            _Instrument = await Admin.ServiceClient.GetInstrumentAsync(_Order.SerialNo);
            UpdateDisplay();
            ShowDialog();
        }

        private void UpdateDisplay()
        {
            lblPriceInstrument.Text = _Instrument.Price.ToString("C2");
            lblInstrument.Text = _Instrument.InstrumentName;
            lblSerialNo.Text = _Instrument.SerialNo.ToString();
            lblOrderId.Text = _Order.OrderID.ToString();
            lblCustName.Text = _Order.CustName;
            lblMail.Text = _Order.CustMail;
            lblPhone.Text = _Order.CustPhone;
            lblOrderDate.Text = _Order.OrderDate.ToShortDateString();
            lblOrderPrice.Text = _Order.OrderPrice.ToString("C2");
        }
    }
}
