using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
    public class clsCategory
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public List<clsAllInstruments> InstrumentsList { get; set; }
    }
    public class clsAllInstruments {

        public int SerialNo { get; set; }
        public int Quantity { get; set; }
        public string Tuning { get; set; }
        public string InstrumentName { get; set; }        
        public decimal Price { get; set; }
        public char InstrumentType { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Manufacturer { get; set; }
        public string Condition { get; set; }
        public string CategoryName { get; set; }



        public static readonly string FACTORY_PROMPT = "Enter N for New or U for Used";

        public static clsAllInstruments NewWork(char prChoice)
        {
            return new clsAllInstruments() { InstrumentType = Char.ToUpper(prChoice) };
        }
        public override string ToString()
        {
            return string.Format("{0,-10}\t|  {1,-20}\t|  {2,-20}|  {3,-20}|  {4,-10}\t| {6,-20}   |  {5:C}", SerialNo, InstrumentName, Tuning, InstrumentType, Quantity, Price, ModifiedDate.ToString("dd/MM/yyyy hh:mm tt") );
        }
    }

    public class clsMyOrder
    {
        public int OrderID { get; set; }
        public decimal OrderPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public string CustName { get; set; }
        public string CustPhone { get; set; }
        public string CustMail { get; set; }
        public int SerialNo { get; set; }
        public override string ToString()
        {
            return String.Format("{0,-5}| {1,-20}\t| {2,-20}\t| {3,-30}\t\t| {4,-5}| {5:C}\t| {6,-20}\t", OrderID, CustName, CustPhone, CustMail, SerialNo, OrderPrice, OrderDate.ToString("dd/mm/yyyy"));
        }

    }
}
