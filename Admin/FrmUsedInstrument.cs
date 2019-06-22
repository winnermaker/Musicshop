using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Admin
{
    sealed partial class FrmUsedInstrument : Admin.FrmInstrument
    {
        public static readonly FrmUsedInstrument Instance = new FrmUsedInstrument();

        private FrmUsedInstrument()
        {
            InitializeComponent();
        }       

        public static void Run(clsAllInstruments prUsedInstrument)
        {
            Instance.SetDetails(prUsedInstrument);
        }

        protected override void updateForm()
        {
            base.updateForm();
        }

        protected override void pushData()
        {
            base.pushData();
        }


    }
}
