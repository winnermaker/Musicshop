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
    public partial class FrmInstrument : Form
    {

        protected clsAllInstruments _Instrument;

        public FrmInstrument()
        {
            InitializeComponent();
        }

        private async void btnOK_ClickAsync(object sender, EventArgs e)
        {
            if (isValid())
            {
                pushData();
                _Instrument.ModifiedDate = DateTime.Now;
                if (nudSerialNo.Enabled)
                    MessageBox.Show(await ServiceClient.InsertInstrumentAsync(_Instrument));
                else
                    MessageBox.Show(await ServiceClient.UpdateInstrumentAsync(_Instrument));
                Close();
            }
            else
            {
                MessageBox.Show("One or more fields are empty or null", "Empty Field");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void SetDetails(clsAllInstruments prInstrument)
        {
            _Instrument = prInstrument;
            updateForm();
            ShowDialog();
        }
        public virtual bool isValid()
        {
            if(txtName.Text != "" && txtTuning.Text != "" && nudPrice.Value != 0 && nudQuantity.Value != 0 && nudSerialNo.Value != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        protected virtual void updateForm()
        {
            nudSerialNo.Value = _Instrument.SerialNo;
            txtName.Text = _Instrument.InstrumentName;
            txtTuning.Text = _Instrument.Tuning;
            nudPrice.Value  = _Instrument.Price;
            nudQuantity.Value = _Instrument.Quantity;
            nudSerialNo.Enabled = _Instrument.SerialNo.Equals(0);
        }

        protected virtual void pushData()
        {
            _Instrument.InstrumentName = txtName.Text;
            _Instrument.Price = nudPrice.Value;
            _Instrument.Quantity = Convert.ToInt32(nudQuantity.Value);
            _Instrument.Tuning = txtTuning.Text.ToUpper();
            _Instrument.SerialNo = Convert.ToInt32(nudSerialNo.Value);
        }
        public delegate void LoadInstrumentFormDelegate(clsAllInstruments prInstrument);
        public static Dictionary<char, Delegate> _InstrumentForm = new Dictionary<char, Delegate>
        {
            {'N', new LoadInstrumentFormDelegate(FrmNewInstrument.Run)},
            {'U', new LoadInstrumentFormDelegate(FrmUsedInstrument.Run)}
        };
        public static void DispatchInstrumentForm(clsAllInstruments prInstrument)
        {
            _InstrumentForm[prInstrument.InstrumentType].DynamicInvoke(prInstrument);
        }
    }
}
