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
    sealed partial class FrmOrder : Form
    {
        private static readonly FrmOrder _Instance = new FrmOrder();

        internal static FrmOrder Instance => _Instance;
        private List<clsMyOrder> _Orderlist = new List<clsMyOrder>();

        private FrmOrder()
        {
            InitializeComponent();
            txtValue.Enabled = false;
        }
        public async void UpdateDisplayAsync()
        {
            try
            {
                _Orderlist = await ServiceClient.GetOrdersAsync();
                lstOrder.DataSource = null;
                lstOrder.DataSource = _Orderlist;
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
    }
}
