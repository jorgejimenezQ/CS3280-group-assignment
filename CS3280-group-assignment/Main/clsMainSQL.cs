using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_group_assignment.Main
{
    class clsMainSQL
    {
        /// <summary>
        /// Update total cost of given invoice
        /// </summary>
        /// <param name="TotalCost"></param>
        /// <param name="InvoiceNum"></param>
        /// <returns></returns>
        public string UpdateTotalCost(string TotalCost, string InvoiceNum)
        {
            string sSQL = "UPDATE Invoices SET TotalCost = " + TotalCost + " WHERE InvoiceNum = " + InvoiceNum;
            return sSQL;
        }

        /// <summary>
        /// Delete a line item for given invoice number and item code
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <returns></returns>
        public string DeleteLineItem(string InvoiceNum, string ItemCode)
        {
            string sSQL = "DELETE From LineItems WHERE InvoiceNum = " + InvoiceNum + " AND ItemCode = " + ItemCode;
            return sSQL;
        }

        /// <summary>
        /// Delete an invoice for given invoice number
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <returns></returns>
        public string DeleteInvoice(string InvoiceNum)
        {
            string sSQL = "DELETE From Invoices WHERE InvoiceNum = " + InvoiceNum;
            return sSQL;
        }

        /// <summary>
        /// Insert a row into LineItem
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <param name="LineItemNum"></param>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        public string InsertLineItem(string InvoiceNum, string LineItemNum, string ItemCode)
        {
            string sSQL = "INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) Values(" + InvoiceNum + ", " +
                            LineItemNum + ", '" + ItemCode + "')";
            return sSQL;
        }

        /// <summary>
        /// Insert an invoice, autogenerate the invoice number
        /// </summary>
        /// <param name="InvoiceDate"></param>
        /// <param name="TotalCost"></param>
        /// <returns></returns>
        public string InsertInvoice(string InvoiceDate, string TotalCost)
        {
            string sSQL = "INSERT INTO Invoices(InvoiceDate, TotalCost) Values('#" + InvoiceDate + "#', " +
                            TotalCost + ")";
            return sSQL;
        }

        /// <summary>
        /// Retrieves all data for a given invoice number
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <returns></returns>
        public string SelectInvoiceData(string InvoiceNum)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + InvoiceNum;
            return sSQL;
        }

        /// <summary>
        /// Selects all items from ItemDesc
        /// </summary>
        /// <returns></returns>
        public string SelectAllItems()
        {
            string sSQL = "SELECT * FROM ItemDesc";
            return sSQL;
        }

        /// <summary>
        /// Select all items under a given invoice
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <returns></returns>
        public string SelectInvoiceItems(string InvoiceNum)
        {
            string sSQL = "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = " + InvoiceNum;
            return sSQL;
        }
    }
}
