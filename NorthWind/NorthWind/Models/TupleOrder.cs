using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWind.Models
{
    public class TupleOrder
    {
        public EditableOrder EditableOrder { get; set; }
        public List<EditableOrderDetail> EditableOrderDetList { get; set; }

        public TupleOrder() {
            EditableOrder = new EditableOrder();
            EditableOrderDetList = new List<EditableOrderDetail>();
        }
    }
}