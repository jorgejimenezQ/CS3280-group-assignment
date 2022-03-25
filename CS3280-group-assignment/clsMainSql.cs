using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_group_assignment
{
    class clsMainSql
    {
        /// <summary>
        /// This SQL gets all data on an invoice for a given invoiceID
        /// </summary>
        /// <param name="sInvoiceID"></param>
        /// <returns></returns>
        public string SelectInvoiceData(string sInvoiceID)

        {

            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID;

            return sSQL;

        }
    }
}
