using System;
using System.Collections.Generic;
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
        {
            try
            {
                List<Item> items = itemQuery.GetInvoiceItems(invoice);
                if (items == null)
                {
                    throw new Exception("There was an error fetching the items from database.");
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
