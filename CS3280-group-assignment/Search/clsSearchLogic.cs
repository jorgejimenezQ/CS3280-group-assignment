using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS3280_group_assignment.Main;
using System.Reflection;
using System.Data;
using System.Linq;
using System.Diagnostics;


namespace CS3280_group_assignment.Search
{
    class clsSearchLogic
    {
        public clsDataAccess db;
       
        /// <summary>
        /// Constructor to create a logic and setup the db access.
        /// </summary>
        public clsSearchLogic()
        {
            try
            {
                db = new clsDataAccess();
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Given filters returns the correct invoice query specific to the filters given.
        /// </summary>
        public string getInvoiceQuery(string filterDate, string filterNum, string filterCost)
        {                
            string invoiceQuery = clsSearchSQL.SelectAllFromInvoice();
            
            try
            {
                if (filterDate != null && filterNum != null && filterCost != null)
                {
                    invoiceQuery = clsSearchSQL.SelectInvoiceDatanDatenCost(filterNum, filterDate, filterCost);
                }
                else if (filterDate != null && filterCost != null)
                {
                    invoiceQuery = clsSearchSQL.SelectInvoiceCostnDate(filterCost, filterDate);
                }
                else if (filterNum != null && filterCost != null)
                {
                    invoiceQuery = clsSearchSQL.SelectInvoiceDataFilterByIDAndCost(filterNum, filterCost);
                }
                else if (filterDate != null && filterNum != null )
                {
                    invoiceQuery = clsSearchSQL.SelectInvoiceDatanDate(filterNum, filterDate);
                }
                else if (filterDate != null)
                {
                    invoiceQuery = clsSearchSQL.SelectInvoiceDate(filterDate);
                }
                else if (filterCost != null)
                {
                    invoiceQuery = clsSearchSQL.SelectInvoiceCost(filterCost);
                }
                else if (filterNum != null)
                {
                    invoiceQuery = clsSearchSQL.SelectInvoiceData(filterNum);
                }
            }
            catch (Exception ex)
            {
                clsHandleError.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            
        return invoiceQuery;

        }

        /// <summary>
        /// Gets the invoices given filters.
        /// </summary>
        public List<clsInvoice> getInvoices(string filterDate, string filterNum, string filterCost)
        {
            try
            {
                List<clsInvoice> invoices = new List<clsInvoice>();
                int iRet = 0;

                DataSet ds = db.ExecuteSQLStatement(
                    getInvoiceQuery(filterDate, filterNum, filterCost),
                    ref iRet);

                // There are none
                if (ds.Tables[0].Rows.Count == 0)
                    return null;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    clsInvoice newInvoice = new clsInvoice(row[0].ToString(), row[1].ToString(), row[2].ToString());
                    invoices.Add(newInvoice);
                }
                return invoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Onbtains distinct invoice numbers as a list given various filters.
        /// </summary>
        public List<string> getInvoiceNums(string filterDate, string filterCost)
        {
            try
            {
                List<string> invoiceNums = new List<string>();
                int iRet = 0;

                string searchQuery = clsSearchSQL.SelectUniqueNums();
                if (filterCost != null && filterDate != null)
                {
                    searchQuery = clsSearchSQL.SelectUniqueNumsByCostAndDate(filterCost, filterDate);
                }
                else if (filterCost != null)
                {
                    searchQuery = clsSearchSQL.SelectUniqueNumsByCost(filterCost);
                }
                else if (filterDate != null)
                {
                    searchQuery = clsSearchSQL.SelectUniqueNumsByDate(filterDate);
                }


                DataSet ds = db.ExecuteSQLStatement(
                    searchQuery,
                    ref iRet);

                // There are none
                if (ds.Tables[0].Rows.Count == 0)
                    return null;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    invoiceNums.Add(row[0].ToString());
                }
                return invoiceNums;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Obtains distinct list of invoice costs given filters.
        /// </summary>
        public List<string> GetTotalCharges(string filterDate, string invoiceNum)
        {
            try
            {
                List<string> invoiceNums = new List<string>();
                int iRet = 0;

                string searchQuery = clsSearchSQL.SelectUniqueCost();
                if(invoiceNum != null && filterDate != null)
                {
                    searchQuery = clsSearchSQL.SelectUniqueCostByInvoiceByInvoiceAndDate(invoiceNum, filterDate);
                }
                else if(invoiceNum != null)
                {
                    searchQuery = clsSearchSQL.SelectUniqueCostByInvoice(invoiceNum);
                }
                else if (filterDate != null)
                {
                    searchQuery = clsSearchSQL.SelectUniqueCostByDate(filterDate);
                }


                DataSet ds = db.ExecuteSQLStatement(
                    searchQuery,
                    ref iRet);

                // There are none
                if (ds.Tables[0].Rows.Count == 0)
                    return null;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    invoiceNums.Add(row[0].ToString());
                }
                return invoiceNums;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
