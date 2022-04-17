using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_group_assignment.Items
{

    /// <summary>
    /// Holds SQL statements strings
    /// </summary>
    public class clsItemsSQL
    {

        /// <summary>
        /// Returns the string select ItemCode, ItemDesc, Cost from ItemDesc
        /// </summary>
        /// <returns></returns>
        public string GetItem()
        {
            return "select ItemCode, ItemDesc, Cost from ItemDesc";
        }

        /// <summary>
        /// Returns the string select distinct(InvoiceNum) from LineItems where ItemCode =  Code
        /// </summary>
        public string GetItemWhereCodeIs(string code)
        {
            return "select distinct(InvoiceNum) from LineItems where ItemCode =" + code;
        }

        /// <summary>
        /// Returns string in form Update ItemDesc Set ItemDesc = 'abcdef', Cost = 123 where ItemCode = 'A'
        /// </summary>
        /// <param name="desc">description</param>
        /// <param name="cost">cost</param>
        /// <param name="code">code</param>
        /// <returns></returns>
        public string UpdateItemDesc(string desc, string cost, string code)
        {
            return "Update ItemDesc Set ItemDesc =" + desc + ", Cost = " + cost + "where ItemCode =" + code;
        }

        /// <summary>
        /// returns string in form:
        /// Insert into ItemDesc(ItemCode, ItemDesc, Cost) Values('ABC', 'blah', 321)
        /// </summary>
        /// <param name="code"></param>
        /// <param name="desc"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public string InsertItem(string code, string desc, string cost)
        {
            return "Insert into ItemDesc(ItemCode, ItemDesc, Cost) Values(" + code + " ," + desc + ", " + cost + ")";
        }

        /// <summary>
        /// Gets the invoices that contain the item
        /// </summary>
        /// <param name="code">The code for the item we are looking up.</param>
        /// <exception cref="Exception"></exception>
        public string GetInvoicesWithItem(string code)
        {
            try
            {
                return $"" +
                    $"SELECT InvoiceNum " +
                    $"FROM LineItems " +
                    $"WHERE ItemCode ='{code}'";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an item
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="description"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string UpdateItem(string itemCode, string description = "", string cost = "") 
        {
            try
            {

                string stmt = $"UPDATE ItemDesc " +
                    $"SET ";

                // At a minimum description or cost must be passed in
                if (String.IsNullOrEmpty(cost) && String.IsNullOrEmpty(description))
                    throw new Exception("The description or the cost must be set. ");

                // Set the cost
                if (!String.IsNullOrEmpty(cost))
                    stmt += $"Cost = {cost} ";

                // Set the description
                if (!String.IsNullOrEmpty(description))
                {
                    if (!String.IsNullOrEmpty(cost))
                        stmt += ", ";

                    stmt += $"ItemDesc = '{description}' ";

                }

                // Add the where clause
                stmt += $"WHERE ItemCode = '{itemCode}'";
                
                return stmt;    

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// return string in form:
        /// Delete from ItemDesc Where ItemCode = 'ABC'
        /// </summary>
        /// <param name="code">code</param>
        /// <returns></returns>
        public string DeleteItem(string code)
        {
            return $"Delete from ItemDesc Where ItemCode = '{code}'";
        }

        public string GetAllItemsByInvoiceNumber(string invoice)
        {
            try
            {
                return $"SELECT " +
                    $"i.ItemCode, i.ItemDesc, i.Cost " +
                    $"FROM LineItems l " +
                    $"INNER JOIN ItemDesc i " +
                    $"ON l.ItemCode = i.ItemCode " +
                    $"WHERE InvoiceNum =  {invoice}";

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }


}
