using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS3280_group_assignment.Items;

namespace CS3280_group_assignment.Main
{
    class clsMainLogic
    {
        /// <summary>
        /// Store list of invoices
        /// </summary>
        List<Invoice> invoices;

        /// <summary>
        /// 
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// Sets selected invoice
        /// </summary>
        public void setSelectedInvoice()
        {
            
        }

        /// <summary>
        /// Returns list of items for combo box
        /// </summary>
        public List<Item> loadItems()
        {
            try
            {
                db = new clsDataAccess();
                List<Item> itemList = new List<Item>();
                //fetch items using SQL statement
                return itemList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
