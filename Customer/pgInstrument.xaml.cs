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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Customer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class pgInstrument : Page
    {
        private clsAllInstruments _Instrument;

        private delegate void LoadInstrumentControlDelegate(clsAllInstruments prInstrument);
        private Dictionary<char, Delegate> _InstrumentsControl;
        public pgInstrument()
        {
            this.InitializeComponent();
            _InstrumentsControl = new Dictionary<char, Delegate>
            {
                {'N', new LoadInstrumentControlDelegate(RunNew)},
                {'U', new LoadInstrumentControlDelegate(RunUsed)},
            };
        }

        private void updatePage(clsAllInstruments prInstrument)
        {
            _Instrument = prInstrument;
            txtInstrument.Text = _Instrument.InstrumentName.EmptyIfNull();
            txtPrice.Text = _Instrument.Price.ToString().EmptyIfNull();
            txtTuning.Text = _Instrument.Tuning.EmptyIfNull();
            (ctcNewUsed.Content as IInstrumentControl).UpdateControl(prInstrument);
            if(_Instrument.Quantity == 0)
            {
                btnOrder.IsEnabled = false;
                txtMessage.Text = "Sorry - this instrument is out of Stock";
            }
        }
        private void DispatchWorkContent(clsAllInstruments prInstrument)
        {
            _InstrumentsControl[prInstrument.InstrumentType].DynamicInvoke(prInstrument);
            updatePage(prInstrument);
        }

        private void RunNew(clsAllInstruments prInstrument)
        {
            ctcNewUsed.Content = new ucInstrmentNew();
        }
        private void RunUsed(clsAllInstruments prInstrument)
        {
            ctcNewUsed.Content = new ucInstrumentUsed();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DispatchWorkContent(e.Parameter as clsAllInstruments);
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(pgOrder), _Instrument);
        }
    }

}

    

