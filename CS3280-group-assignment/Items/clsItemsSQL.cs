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
        /// return string in form:
        /// Delete from ItemDesc Where ItemCode = 'ABC'
        /// </summary>
        /// <param name="code">code</param>
        /// <returns></returns>
        public string DeleteItem(string code)
        {
            return "Delete from ItemDesc Where ItemCode = " + code;
        }
    }


}
