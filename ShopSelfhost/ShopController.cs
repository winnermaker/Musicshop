using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSelfhost
{
    public class ShopController : System.Web.Http.ApiController
    {
        private Dictionary<string, object> prepareCategoryParameters(clsCategory prCategory)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(2);
            par.Add("CategoryName", prCategory.CategoryName);
            par.Add("CategoryDescription", prCategory.CategoryDescription);
            return par;
        }

        private Dictionary<string, object> prepareInstrumentParameters(clsAllInstruments prInstrument)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(10);
            par.Add("SerialNo", prInstrument.SerialNo);
            par.Add("Quantity", prInstrument.Quantity);
            par.Add("Tuning", prInstrument.Tuning);
            par.Add("InstrumentName", prInstrument.InstrumentName);
            par.Add("Price", prInstrument.Price);
            par.Add("InstrumentType", prInstrument.InstrumentType);
            par.Add("ModifiedDate", prInstrument.ModifiedDate);
            par.Add("Manufacturer", prInstrument.Manufacturer);
            par.Add("Condition", prInstrument.Condition);
            par.Add("CategoryName", prInstrument.CategoryName);
            return par;
        }
        private Dictionary<string, object> prepareInstrumentParameters(int SerialNo)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("SerialNo", SerialNo);
            return par;
        }

        private Dictionary<string, object> prepareOrderParameters(int OrderID)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("OrderID", OrderID);
            return par;
        }


        private Dictionary<string, object> prepareOrderParameters(clsMyOrder prOrder)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(7);
            par.Add("OrderPrice", prOrder.OrderPrice);
            par.Add("OrderDate", prOrder.OrderDate);
            par.Add("Quantity", prOrder.Quantity);
            par.Add("CustName", prOrder.CustName);
            par.Add("CustPhone", prOrder.CustPhone);
            par.Add("CustMail", prOrder.CustMail);
            par.Add("SerialNo", prOrder.SerialNo);
            return par;
        }

        public List<string> GetCategoryNames()
        {
            DataTable lcResult = clsDbConnection.GetDataTable("SELECT CategoryName FROM Category", null);
            List<string> lcNames = new List<string>();
            foreach (DataRow dr in lcResult.Rows)
                lcNames.Add((string)dr[0]);
            return lcNames;
        }

        public clsCategory GetCategory(string CategoryName)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("CategoryName", CategoryName);
            DataTable lcResult =
            clsDbConnection.GetDataTable("SELECT * FROM Category WHERE CategoryName = @CategoryName", par);
            if (lcResult.Rows.Count > 0)
                return new clsCategory()
                {
                    CategoryName = (string)lcResult.Rows[0]["CategoryName"],
                    CategoryDescription = (string)lcResult.Rows[0]["CategoryDescription"],
                    InstrumentsList = GetCategoryInstrument(CategoryName)
                };
            else
                return null;
        }

        private List<clsAllInstruments> GetCategoryInstrument(string CategoryName)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("CategoryName", CategoryName);
            DataTable lcResult = clsDbConnection.GetDataTable("SELECT * FROM Instrument WHERE CategoryName = @CategoryName", par);
            List<clsAllInstruments> lcInstruments = new List<clsAllInstruments>();
            foreach (DataRow dr in lcResult.Rows)
                lcInstruments.Add(dataRow2AllInstrument(dr));
            return lcInstruments;
        }

        private clsAllInstruments dataRow2AllInstrument(DataRow prDataRow)
        {
            return new clsAllInstruments()
            {
                SerialNo = Convert.ToInt16(prDataRow["SerialNo"]),
                Quantity = Convert.ToInt16(prDataRow["Quantity"]),
                Tuning = Convert.ToString(prDataRow["Tuning"]),
                InstrumentName = Convert.ToString(prDataRow["InstrumentName"]),
                Price = Convert.ToDecimal(prDataRow["Price"]),
                InstrumentType = Convert.ToChar(prDataRow["InstrumentType"]),
                ModifiedDate = Convert.ToDateTime(prDataRow["ModifiedDate"]),
                Manufacturer = Convert.ToString(prDataRow["Manufacturer"]),
                Condition = Convert.ToString(prDataRow["Condition"]),
                CategoryName = Convert.ToString(prDataRow["CategoryName"]),

            };
        }
        private clsMyOrder dataRow2AOrder(DataRow prDataRow)
        {
            return new clsMyOrder()
            {
                OrderID = Convert.ToInt16(prDataRow["OrderID"]),
                OrderPrice = Convert.ToDecimal(prDataRow["OrderPrice"]),
                OrderDate = Convert.ToDateTime(prDataRow["OrderDate"]),
                Quantity = Convert.ToInt16(prDataRow["Quantity"]),
                CustName = Convert.ToString(prDataRow["CustName"]),
                CustPhone = Convert.ToString(prDataRow["CustPhone"]),
                CustMail = Convert.ToString(prDataRow["CustMail"]),
                SerialNo = Convert.ToInt16(prDataRow["SerialNo"]),

            };
        }

        public List<clsMyOrder> GetOrders()
        {
            DataTable lcResult = clsDbConnection.GetDataTable("SELECT * FROM MyOrder", null);
           
            List<clsMyOrder> lcOrders = new List<clsMyOrder>();
            foreach (DataRow dr in lcResult.Rows)
                lcOrders.Add(dataRow2AOrder(dr));
            return lcOrders;
        }

        public string PutCategory(clsCategory prCategory)
        {   //update
            try
            {
                int lcRecCount = clsDbConnection.Execute(
                "UPDATE Category SET CategoryDescription = @CategoryDescription WHERE  CategoryName = @CategoryName",
                prepareCategoryParameters(prCategory));
                if (lcRecCount == 1)
                    return "One category updated";
                else
                    return "Unexpected category update count: " + lcRecCount;
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }
        public string PutInstrument(clsAllInstruments prInstrument)
        {   //update
            try
            {
                int lcRecCount = clsDbConnection.Execute(
               "UPDATE Instrument SET InstrumentType = @InstrumentType, Quantity = @Quantity, Tuning = @Tuning, Price = @Price, ModifiedDate = @ModifiedDate, Manufacturer = @Manufacturer, Condition = @Condition WHERE CategoryName = @CategoryName AND InstrumentName = @InstrumentName",
                prepareInstrumentParameters(prInstrument));
                if (lcRecCount == 1)
                    return "One Instrument updated";
                else
                    return "Unexpected Instrument update count: " + lcRecCount;
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }
        public string PostInstrument(clsAllInstruments prInstrument)
        {   //insert
            try
            {
                int lcRecCount = clsDbConnection.Execute("INSERT INTO Instrument"+
                "(SerialNo, Quantity, Tuning, InstrumentName, Price, InstrumentType, ModifiedDate, Manufacturer, Condition, CategoryName)" +
                "VALUES (@SerialNo, @Quantity, @Tuning, @InstrumentName, @Price, @InstrumentType, @ModifiedDate, @Manufacturer, @Condition, @CategoryName)",
                prepareInstrumentParameters(prInstrument));
                if (lcRecCount == 1)
                    return "One instrument inserted";
                else
                    return "Unexpected instrument insert count: " + lcRecCount;
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }
        public string DeleteInstrument(int SerialNo)
        {   //delete
            try
            {
                int lcRecCount = clsDbConnection.Execute("DELETE FROM Instrument WHERE SerialNo = @SerialNo",
                    prepareInstrumentParameters(SerialNo));

                if (lcRecCount == 1)
                    return "One instrument deleted";
                else
                    return "Unexpected instrument delete count: " + lcRecCount;
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }
        public string PostOrder(clsMyOrder prOrder)
        {   //insert
            try
            {
                int lcRecCount = clsDbConnection.Execute("INSERT INTO MyOrder (OrderPrice, OrderDate, Quantity, CustName, CustPhone, CustMail, SerialNo)" +
                "VALUES (@OrderPrice, @OrderDate, @Quantity, @CustName, @CustPhone, @CustMail, @SerialNo)",
                prepareOrderParameters(prOrder));
                if (lcRecCount == 1)
                    return "One Order inserted";
                else
                    return "Unexpected order insert count: " + lcRecCount;
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }

        public string DeleteOrder(int OrderID)
        {   //delete
            try
            {
                int lcRecCount = clsDbConnection.Execute("DELETE FROM MyOrder WHERE OrderID = @OrderID",
                    prepareOrderParameters(OrderID));

                if (lcRecCount == 1)
                    return "One Order deleted";
                else
                    return "Unexpected Order delete count: " + lcRecCount;
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }
    }
}
