using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_group_assignment.Items
{


    public class clsItemsSQL
    {

        /// <summary>
        /// Instance of the database access class that will be 
        /// shared will all the classes that will handle the logic.
        /// </summary>
        clsDataAccess db;

        public clsItemsSQL()
        {
            // Get access to SQL queries
            db = new clsDataAccess();
        }

        /// <summary>
        /// Fetches all the items from an invoice from the database
        /// </summary>
        /// <param name="invoiceNumber">The invoice to query</param>
        /// <returns></returns>
        public List<Item> GetInvoiceItems(string invoiceNumber)
        {
            try
            {
                List<Item> items = new List<Item>();

                // Will holds the returned data from DB
                DataSet ds;
                int iRet = 0; // The number of records returned

                ds = db.ExecuteSQLStatement(
                    "SELECT " +
                    "i.ItemCode, i.ItemDesc, i.Cost " +
                    "FROM LineItems l " +
                    "INNER JOIN ItemDesc i " +
                    "ON l.ItemCode = i.ItemCode " +
                    "WHERE InvoiceNum = " + invoiceNumber, ref iRet);

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
