using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Admin
{
    sealed partial class FrmNewInstrument : Admin.FrmInstrument
    {

        public static readonly FrmNewInstrument Instance = new FrmNewInstrument();
        private FrmNewInstrument()
        {
            InitializeComponent();
        }

        public static void Run(clsAllInstruments prNewInstrument)
        {
            Instance.SetDetails(prNewInstrument);
        }

        protected override void updateForm()
        {
            base.updateForm();
            txtManufacturer.Text = _Instrument.Manufacturer;
        }

        protected override void pushData()
        {
            base.pushData();
            _Instrument.Manufacturer = txtManufacturer.Text;
        }
        public override bool isValid()
        {
            if(txtManufacturer.Text != "")
            {
                return base.isValid();
            }
            else
            {
                return false;
            }
            
        }
    }
}
