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
    sealed partial class FrmMain : Form
    {
        private static readonly FrmMain _Instance = new FrmMain();

        internal static FrmMain Instance => _Instance;
        private FrmMain()
        {
            InitializeComponent();
            
        }
        public async void UpdateDisplayAsync()
        {
            try
            {
                lstCategory.DataSource = null;
                lstCategory.DataSource = await ServiceClient.GetCategoryNamesAsync();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "An Error occured");
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            FrmOrder.Instance.Show();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            string lcKey;

            lcKey = Convert.ToString(lstCategory.SelectedItem);
            if (lcKey != null)
                try
                {
                    FrmCategory.Run(lstCategory.SelectedItem as string);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "This should never occur");
                }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lstCategory_DoubleClick(object sender, EventArgs e)
        {
            string lcKey;

            lcKey = Convert.ToString(lstCategory.SelectedItem);
            if (lcKey != null)
                try
                {
                    FrmCategory.Run(lstCategory.SelectedItem as string);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "This should never occur");
                }
        }

        private void FrmMain_Load_1(object sender, EventArgs e)
        {
            UpdateDisplayAsync();
        }
    }
}
