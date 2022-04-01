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

        public clsItemsLogic()
        {
            // Create a new SQL class
            itemQuery = new clsItemsSQL();
        }

        /// <summary>
        /// Returns all the items from an invoice
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public List<Item> GetAllItems(string invoice)
        {/*
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
                    "WHERE InvoiceNum = " + invoice, ref iRet);

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
            }*/
            return new List<Item>();
        }

    }

}
