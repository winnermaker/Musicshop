using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer
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
        public string MyCondition { get; set; }
        public string CategoryName { get; set; }



        public static readonly string FACTORY_PROMPT = "Enter N for New or U for Used";

        public static clsAllInstruments NewWork(char prChoice)
        {
            return new clsAllInstruments() { InstrumentType = Char.ToUpper(prChoice) };
        }
        public string extendType()
        {
            switch (InstrumentType)
            {
                case 'N':
                    return "new";
                default:
                    return "used";

            }
        }
        public override string ToString()
        {
            return String.Format("{0,-20}\t|   {1,-10}\t|   {2,-20}\t|   {3,-20}\t|   {4:C}", InstrumentName, extendType(),Tuning, Quantity, Price);
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

    }
}
