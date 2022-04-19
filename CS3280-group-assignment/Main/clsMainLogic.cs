using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS3280_group_assignment.Items;
using System.Data;

namespace CS3280_group_assignment.Main
{
    public class clsMainLogic
    {
        /// <summary>
        /// Store list of invoices
        /// </summary>
        private List<clsInvoice> invoices;

        /// <summary>
        /// 
        /// </summary>
        private clsDataAccess db;

        private clsMainSQL sql;

        /// <summary>
        /// Searched Invoice ID
        /// </summary>
        private clsInvoice selectedInvoice;

        

        /// <summary>
        /// Returns list of items for combo box
        /// </summary>
        public List<Item> LoadItems()
        {
            try
            {
                sql = new clsMainSQL();
                db = new clsDataAccess();
                DataSet ds;
                List<Item> items = new List<Item>();
                List<String> descList = new List<String>();
                //fetch items using SQL statement
                int iRet = 0;
                ds = db.ExecuteSQLStatement(sql.SelectAllItems(), ref iRet);

                for(int i = 0; i < iRet; i++)
                {
                   // Item item = new Item(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString());
                    //items.Add(item);
                    descList.Add(ds.Tables[0].Rows[i][1].ToString());
                }
                descList.Sort();
                
                //Add items to list in alphabetical order
                for(int i = 0; i < iRet; i++)
                {
                    for(int k = 0; k < iRet; k++)
                    {
                        if (ds.Tables[0].Rows[k][1].ToString().Equals(descList[i]))
                        {
                            Item item = new Item(ds.Tables[0].Rows[k][0].ToString(),
                                ds.Tables[0].Rows[k][1].ToString(), ds.Tables[0].Rows[k][2].ToString());
                            items.Add(item);
                        }
                    }
                }
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public void setSelectedInvoice(clsInvoice invoice)
        {
            selectedInvoice = invoice;
        }

    }
}
