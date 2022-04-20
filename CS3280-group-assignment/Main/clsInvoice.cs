using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_group_assignment.Main
{
    public class clsInvoice
    {
        public String InvoiceNum { get; set; }

        public String InvoiceDate { get; set; }

        public String TotalCost { get; set; }

        public clsInvoice(String InvoiceDate, String TotalCost)
        {
            this.InvoiceDate = InvoiceDate;
            this.TotalCost = TotalCost;
        }

    }
}
