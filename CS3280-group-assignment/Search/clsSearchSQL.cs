using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_group_assignment.Search
{
    class clsSearchSQL
    {
        /// <summary>
        /// "when the invoice is selected, the invoice ID is saved in a local variable that the main window can access"
        /// </summary>
     

        public static string SelectAllFromInvoice()

        {

            string sSQL = "SELECT InvoiceDate, TotalCost, InvoiceNum  FROM Invoices";

            return sSQL;

        }

        public static string SelectInvoiceData(string sInvoiceID)

        {

            string sSQL = "SELECT InvoiceDate, TotalCost, InvoiceNum FROM Invoices WHERE InvoiceNum = " + sInvoiceID;

            return sSQL;

        }

        public static string SelectInvoiceDatanDate(string sInvoiceID, string InvoiceDate)

        {

            string sSQL = "SELECT InvoiceDate, TotalCost, InvoiceNum FROM Invoices WHERE InvoiceNum = " + sInvoiceID + " AND  InvoiceDate LIKE '%" + InvoiceDate + "%'";

            return sSQL;

        }


        public static string SelectInvoiceDataFilterByIDAndCost(string sInvoiceID, string InvoiceCost)

        {

            string sSQL = "SELECT InvoiceDate, TotalCost, InvoiceNum FROM Invoices WHERE InvoiceNum=" + sInvoiceID + " AND TotalCost=" + InvoiceCost+"";

            return sSQL;

        }

        public static string SelectInvoiceDatanDatenCost(string sInvoiceID, string InvoiceDate, string TotalCost)

        {

            string sSQL = "SELECT InvoiceDate, TotalCost, InvoiceNum FROM Invoices WHERE InvoiceNum = " + sInvoiceID + " AND InvoiceDate LIKE '%" + InvoiceDate + "%' AND TotalCost = " + TotalCost;

            return sSQL;

        }

        public static string SelectInvoiceCost(string TotalCost)

        {

            string sSQL = "SELECT InvoiceDate, TotalCost, InvoiceNum FROM Invoices WHERE TotalCost=" + TotalCost;

            return sSQL;

        }


        public static string SelectInvoiceCostnDate(string TotalCost, string InvoiceDate)

        {

            string sSQL = "SELECT InvoiceDate, TotalCost, InvoiceNum FROM Invoices WHERE TotalCost=" + TotalCost + " AND  InvoiceDate LIKE '%" + InvoiceDate + "%'";

            return sSQL;

        }

        public static string SelectInvoiceDate(string InvoiceDate)

        {

            string sSQL = "SELECT InvoiceDate, TotalCost, InvoiceNum FROM Invoices WHERE InvoiceDate LIKE '%" + InvoiceDate + "%'";

            return sSQL;

        }


        public static string SelectUniqueNums()
        {
            string sSQL = "SELECT DISTINCT InvoiceNum FROM Invoices";
            return sSQL;
        }
        public static string SelectUniqueNumsByCostAndDate(string filterCost, string filterDate)
        {
            string sSQL = "SELECT DISTINCT InvoiceNum FROM Invoices WHERE TotalCost=" + filterCost + " AND InvoiceDate LIKE '%" + filterDate + "%'";
            return sSQL;
        }

        public static string SelectUniqueNumsByCost(string filterCost)
        {
            string sSQL = "SELECT DISTINCT InvoiceNum FROM Invoices WHERE TotalCost=" + filterCost;
            return sSQL;
        }

        public static string SelectUniqueNumsByDate(string filterDate)
        {
            string sSQL = "SELECT DISTINCT InvoiceNum FROM Invoices WHERE InvoiceDate LIKE '%" + filterDate + "%'";
            return sSQL;
        }



        public static string SelectUniqueCost()
        {
            string sSQL = "SELECT DISTINCT TotalCost FROM Invoices";
            return sSQL;
        }

        public static string SelectUniqueCostByInvoiceByInvoiceAndDate(string invoiceNum, string filterDate)
        {
            string sSQL = "SELECT DISTINCT TotalCost FROM Invoices WHERE InvoiceNum=" + invoiceNum+ " AND InvoiceDate LIKE '%" + filterDate + "%'";
            return sSQL;
        }

        public static string SelectUniqueCostByDate(string filterDate)
        {
            string sSQL = "SELECT DISTINCT TotalCost FROM Invoices WHERE InvoiceDate LIKE '%" + filterDate + "%'";
            return sSQL;
        }


        public static string SelectUniqueCostByInvoice(string invoiceNum)
        {
            string sSQL = "SELECT DISTINCT TotalCost FROM Invoices WHERE InvoiceNum=" + invoiceNum;
            return sSQL;
        }
    }
}
