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
        /// peforms database actions
        /// </summary>
        private clsDataAccess db;

        /// <summary>
        /// holds sql statements
        /// </summary>
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
        
        /// <summary>
        /// Gets all items belonging to the selected invoice
        /// </summary>
        /// <returns></returns>
        public List<Item> getInvoiceItems()
        {
            try
            {
                List<Item> items = new List<Item>();
                sql = new clsMainSQL();
                db = new clsDataAccess();
                DataSet ds;
                int iRet = 0;

                ds = db.ExecuteSQLStatement(sql.SelectInvoiceItems(selectedInvoice.InvoiceNum),
                        ref iRet);
                for (int i = 0; i < iRet; i++)
                {
                    Item item = new Item(ds.Tables[0].Rows[i][0].ToString(),
                            ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString());
                    items.Add(item);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// gets details of selected invoice
        /// </summary>
        public void getInvoiceDetails()
        {
            try
            {
                sql = new clsMainSQL();
                db = new clsDataAccess();
                DataSet ds;
                int iRet = 0;

                ds = db.ExecuteSQLStatement(sql.SelectInvoiceData(selectedInvoice.InvoiceNum), ref iRet);

                selectedInvoice.InvoiceDate = ds.Tables[0].Rows[0][1].ToString();
                selectedInvoice.TotalCost = ds.Tables[0].Rows[0][2].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Inserts invoice into database
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public String insertInvoice(clsInvoice invoice)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Inserts line item into database
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <param name="LineItemNum"></param>
        /// <param name="ItemCode"></param>
        public void insertInvoiceItem(String InvoiceNum, String LineItemNum, String ItemCode)
        {
            try
            {
                sql = new clsMainSQL();
                db = new clsDataAccess();
                int iRet = 0;

                iRet = db.ExecuteNonQuery(sql.InsertLineItem(InvoiceNum, LineItemNum, ItemCode));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes all line items belonging to given invoice
        /// </summary>
        /// <param name="InvoiceNum"></param>
        public void deleteInvoiceItems(String InvoiceNum)
        {
            try
            {
                sql = new clsMainSQL();
                db = new clsDataAccess();
                int iRet = 0;

                iRet = db.ExecuteNonQuery(sql.DeleteLineItem(InvoiceNum));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates total cost of an invoice
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <param name="TotalCost"></param>
        public void updateInvoice(String InvoiceNum, String TotalCost)
        {
            try
            {
                sql = new clsMainSQL();
                db = new clsDataAccess();
                int iRet = 0;

                iRet = db.ExecuteNonQuery(sql.UpdateTotalCost(InvoiceNum, TotalCost));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Delete an invoice from the database
        /// </summary>
        /// <param name="InvoiceNum"></param>
        public void deleteInvoice(String InvoiceNum)
        {
            try
            {
                sql = new clsMainSQL();
                db = new clsDataAccess();
                int iRet = 0;

                iRet = db.ExecuteNonQuery(sql.DeleteInvoice(InvoiceNum));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
