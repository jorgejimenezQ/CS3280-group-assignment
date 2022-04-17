using CS3280_group_assignment.Main;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_group_assignment.Items
{

    /// <summary>
    /// This class will handle all item-database interactions.
    /// </summary>
    public class clsItemsLogic
    {

        /// <summary>
        /// This object will be used to query the database
        /// </summary>
        private clsItemsSQL itemQuery;

        /// <summary>
        /// The current invoice being used.
        /// </summary>
        public string invoice;

        /// <summary>
        /// The database used to query the database
        /// </summary>
        public clsDataAccess db;

       
        public clsItemsLogic()
        {
            // Create a new SQL class
            itemQuery = new clsItemsSQL();
            db = new clsDataAccess();
            
        }

        /// <summary>
        /// Get all the invoices that contain the item code.
        /// </summary>
        /// <param name="code">Item code</param>
        /// <returns>A list of Invoices or null, if there are none.</returns>
        /// <exception cref="Exception"></exception>
        public List<string> GetInvoiceWithItem(string code)
        { 
            try
            {

                // Will holds the returned data from DB
                DataSet ds;
                int iRet = 0; // The number of records returned
                List<string> invoiceNums = new List<string>();

                ds = db.ExecuteSQLStatement(
                    itemQuery.GetInvoicesWithItem(code), 
                    ref iRet);

                // There are none
                if (ds.Tables[0].Rows.Count == 0)
                    return null;

                foreach (DataRow row in ds.Tables[0].Rows)
                    invoiceNums.Add(row[0].ToString());

                return invoiceNums;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        public void UpdateItem(string itemCode, string description = null, string cost = null)
        { 
            try
            {
                // Get statement string and execute
                string stmt = itemQuery.UpdateItem(itemCode, description, cost); 
                db.ExecuteNonQuery(stmt);

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Removes the item from the database
        /// </summary>
        /// <param name="code"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteItem(string code)
        { 
            try
            {
                int iRet = 0;
                iRet = db.ExecuteNonQuery(itemQuery.DeleteItem(code));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns all the items
        /// </summary>
        /// <returns>A list of items</returns>
        public List<Item> GetAllItems()
        { 
            try
            {
                List<Item> items = new List<Item>();

                // Will holds the returned data from DB
                DataSet ds;
                int iRet = 0; // The number of records returned

                ds = db.ExecuteSQLStatement(
                    itemQuery.GetItem(), 
                    ref iRet);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // Get all the item properties from the row object
                    string code = row[0].ToString();
                    string desc = row[1].ToString();
                    string cost = row[2].ToString();

                    // Create the item
                    Item item = new Item(code, desc, cost);

                    // And add it to the list
                    items.Add(item);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Returns all the items from an invoice
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public List<Item> GetAllItemsFromInvoice(string invoice)
        { 
            try
            {
                List<Item> items = new List<Item>();

                // Will holds the returned data from DB
                DataSet ds;
                int iRet = 0; // The number of records returned

                ds = db.ExecuteSQLStatement(
                    itemQuery.GetAllItemsByInvoiceNumber(invoice), 
                    ref iRet);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // Get all the item properties from the row object
                    string code = row[0].ToString();
                    string desc = row[1].ToString();
                    string cost = row[2].ToString();

                    // Create the item
                    Item item = new Item(code, desc, cost);

                    // And add it to the list
                    items.Add(item);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }

}
