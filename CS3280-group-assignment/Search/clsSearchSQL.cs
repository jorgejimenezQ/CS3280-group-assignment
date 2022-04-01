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
     

        public string SelectAllFromInvoice()

        {

            string sSQL = "SELECT * FROM Invoices";

            return sSQL;

        }

        public string SelectInvoiceData(string sInvoiceID)

        {

            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID;

            return sSQL;

        }

        public string SelectInvoiceDatanDate(string sInvoiceID, string InvoiceDate)

        {

            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID + " AND InvoiceDate = " + InvoiceDate;

            return sSQL;

        }

        public string SelectInvoiceDatanDatenCost(string sInvoiceID, string InvoiceDate, string TotalCost)

        {

            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID + " AND InvoiceDate = " + InvoiceDate + " AND TotalCost = " + TotalCost;

            return sSQL;

        }

        public string SelectInvoiceCost(string TotalCost)

        {

            string sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + TotalCost;

            return sSQL;

        }


        public string SelectInvoiceCostnDate(string TotalCost, string InvoiceDate)

        {

            string sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + TotalCost + " AND InvoiceDate = " +  InvoiceDate;

            return sSQL;

        }
        
        public string SelectInvoiceDate(string InvoiceDate)

        {

            string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = " + InvoiceDate;

            return sSQL;

        }
    }
}
