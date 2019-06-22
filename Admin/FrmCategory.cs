using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Admin
{
    public partial class FrmCategory : Form
    {
        private clsCategory _Category;
        private static Dictionary<string, FrmCategory> _CategoryFormList = new Dictionary<string, FrmCategory>();
        public FrmCategory()
        {
            InitializeComponent();
            txtCategory.Enabled = false;
        }

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                string lcReply = new InputBox(clsAllInstruments.FACTORY_PROMPT).Answer;
                if (!string.IsNullOrEmpty(lcReply)) // not cancelled?
                {
                    clsAllInstruments lcInstrument = clsAllInstruments.NewWork(lcReply[0]);
                    if (lcInstrument != null) // valid instrument created?
                    {
                        lcInstrument.CategoryName = _Category.CategoryName;
                        FrmInstrument.DispatchInstrumentForm(lcInstrument);
                        if (!string.IsNullOrEmpty(lcInstrument.InstrumentName)) // not cancelled?
                        {
                            refreshFormFromDBAsync(_Category.CategoryName);
                            FrmMain.Instance.UpdateDisplayAsync();
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private async void close_ClickAsync(object sender, EventArgs e)
        {
            pushData();
            MessageBox.Show(await ServiceClient.UpdateCategoryAsync(_Category));
            Hide();
        }

        public static void Run(string prCategory)
        {
            FrmCategory lcCategoryForm;
            
            if (string.IsNullOrEmpty(prCategory) ||
            !_CategoryFormList.TryGetValue(prCategory, out lcCategoryForm))
            {
                lcCategoryForm = new FrmCategory();
                if (string.IsNullOrEmpty(prCategory))
                    lcCategoryForm.SetDetails(new clsCategory());
                else
                {
                    _CategoryFormList.Add(prCategory, lcCategoryForm);
                    lcCategoryForm.refreshFormFromDBAsync(prCategory);
                }
            }
            else
            {
                lcCategoryForm.Show();
                lcCategoryForm.Activate();
            }
        }

        private async void refreshFormFromDBAsync(string prCategoryName)
        {
            SetDetails(await ServiceClient.GetCategoryAsync(prCategoryName));
        }

        private void updateDisplay()
        {
            lstInstruments.DataSource = null;
            if (_Category.InstrumentsList != null)
                lstInstruments.DataSource = _Category.InstrumentsList;
        }

        public void SetDetails(clsCategory prCategory)
        {
            _Category = prCategory;
            updateForm();
            updateDisplay();
            Show();
        }

        private void updateForm()
        {
            txtCategory.Text = _Category.CategoryName;
            rtxtDescription.Text = _Category.CategoryDescription;
        }

        private void pushData()
        {
            _Category.CategoryDescription = rtxtDescription.Text;
        }

        private Boolean isValid()
        {
            return true;
        }

        private void lstInstruments_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                FrmInstrument.DispatchInstrumentForm(lstInstruments.SelectedValue as clsAllInstruments);
                updateDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnDelete_ClickAsync(object sender, EventArgs e)
        {
            int lcIndex = lstInstruments.SelectedIndex;

            if (lcIndex >= 0 && MessageBox.Show("Are you sure?", "Deleting instrument", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MessageBox.Show(await ServiceClient.DeleteInstrumentAsync(lstInstruments.SelectedItem as clsAllInstruments));
                refreshFormFromDBAsync(_Category.CategoryName);
                FrmMain.Instance.UpdateDisplayAsync();
            }
        }
    }
}
