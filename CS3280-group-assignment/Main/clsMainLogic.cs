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
        /// 
        /// </summary>
        private clsDataAccess db;

        private clsMainSQL sql;

        /// <summary>
        /// Searched Invoice ID
        /// </summary>
        public clsInvoice selectedInvoice;

        

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
        
        public List<Item> getInvoiceItems()
        {
            List<Item> items = new List<Item>();
            sql = new clsMainSQL();
            db = new clsDataAccess();
            DataSet ds;
            int iRet = 0;

            ds = db.ExecuteSQLStatement(sql.SelectInvoiceItems(selectedInvoice.InvoiceNum),
                    ref iRet);
            for(int i = 0; i < iRet; i++)
            {
                Item item = new Item(ds.Tables[0].Rows[i][0].ToString(),
                        ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString());
                items.Add(item);
            }

            return items;
        }

        public void getInvoiceDetails()
        {
            sql = new clsMainSQL();
            db = new clsDataAccess();
            DataSet ds;
            int iRet = 0;

            ds = db.ExecuteSQLStatement(sql.SelectInvoiceData(selectedInvoice.InvoiceNum), ref iRet);

            selectedInvoice.InvoiceDate = ds.Tables[0].Rows[0][1].ToString();
            selectedInvoice.TotalCost = ds.Tables[0].Rows[0][2].ToString();
        }

        public String insertInvoice(clsInvoice invoice)
        {
            sql = new clsMainSQL();
            db = new clsDataAccess();
            int iRet = 0;
            String invoiceNum;

            iRet = db.ExecuteNonQuery(sql.InsertInvoice(invoice.InvoiceDate, invoice.TotalCost));

            String max = "SELECT MAX(InvoiceNum) FROM Invoices";

            invoiceNum = db.ExecuteScalarSQL(max);

            return invoiceNum;
        }

        public void insertInvoiceItem(String InvoiceNum, String LineItemNum, String ItemCode)
        {
            sql = new clsMainSQL();
            db = new clsDataAccess();
            int iRet = 0;

            iRet = db.ExecuteNonQuery(sql.InsertLineItem(InvoiceNum, LineItemNum, ItemCode));
        }

        public void deleteInvoiceItems(String InvoiceNum)
        {
            sql = new clsMainSQL();
            db = new clsDataAccess();
            int iRet = 0;

            iRet = db.ExecuteNonQuery(sql.DeleteLineItem(InvoiceNum));
        }

        public void updateInvoice(String InvoiceNum, String TotalCost)
        {
            sql = new clsMainSQL();
            db = new clsDataAccess();
            int iRet = 0;

            iRet = db.ExecuteNonQuery(sql.UpdateTotalCost(InvoiceNum, TotalCost));
        }

        public void deleteInvoice(String InvoiceNum)
        {
            sql = new clsMainSQL();
            db = new clsDataAccess();
            int iRet = 0;

            iRet = db.ExecuteNonQuery(sql.DeleteInvoice(InvoiceNum));
        }
    }
}
