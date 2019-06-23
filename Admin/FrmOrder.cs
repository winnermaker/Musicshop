using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Admin
{
    sealed partial class FrmOrder : Form
    {
        public static readonly FrmOrder Instance = new FrmOrder();

        private List<clsMyOrder> _Orderlist = new List<clsMyOrder>();

        private FrmOrder()
        {
            InitializeComponent();
        }
        public async void UpdateDisplayAsync()
        {
            try
            {
                _Orderlist = await ServiceClient.GetOrdersAsync();
                lstOrder.DataSource = null;
                lstOrder.DataSource = _Orderlist;
                lblTotalValue.Text = TotalValue().ToString("C2");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "An Error occured");
            }
        }

        private async void btnDelete_ClickAsync(object sender, EventArgs e)
        {
            int lcIndex = lstOrder.SelectedIndex;

            if (lcIndex >= 0 && MessageBox.Show("Are you sure?", "Deleting Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MessageBox.Show(await ServiceClient.DeleteOrderAsync(lstOrder.SelectedItem as clsMyOrder));
                UpdateDisplayAsync();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            UpdateDisplayAsync();
        }

        public decimal TotalValue()
        {
            decimal lcTotal = 0;
            foreach (clsMyOrder lcOrder in _Orderlist)
                lcTotal += lcOrder.OrderPrice;
            return lcTotal;
        }

        private void lstOrder_DoubleClick(object sender, EventArgs e)
        {
            seeDetails();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            seeDetails();
        }
        public void seeDetails()
        {
            clsMyOrder lcOrder = (clsMyOrder)lstOrder.SelectedItem;
            if(lcOrder != null)
            {
                FrmOrderDetails.Instance.ShowDialog(lcOrder);
            }
        }
    }
}
