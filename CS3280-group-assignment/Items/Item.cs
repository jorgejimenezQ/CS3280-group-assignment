using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_group_assignment.Items
{
    /// <summary>
    /// Represents an item.
    /// </summary>
    public class Item
    {

        /// <summary>
        /// Gets or sets the item's descriptions.
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// Gets or sets the item's Code.
        /// </summary>
        public string Code { private set; get; }

        /// <summary>
        /// Gets or sets the item's cost.
        /// </summary>
        public string Cost { set; get; }

        public Item(string code, string description, string cost)
        {
            this.Code = code;
            this.Description = description;
            this.Cost = cost;
        }
    }
}
