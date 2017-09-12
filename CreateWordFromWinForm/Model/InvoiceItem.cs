using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreateWordFromWinForm.Model
{
    public class InvoiceItem
    {
        public string itemNo;
        public string description;
        public string amount;

        public InvoiceItem()
        {
        }

        public InvoiceItem(string itemNo, string description, string amount)
        {
            this.itemNo = itemNo;
            this.description = description;
            this.amount = amount;
        }
    }
}
