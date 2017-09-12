using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreateWordFromWinForm.Model
{
    public class Invoice
    {
        public string invoiceNo; //Can never be null or empty
        public string coverNoteNo;
        public DateTime date;
        public string policyNo;
        public string endorsementNo;
        public string sumInsured;
        public DateTime effectiveDate;
        public DateTime expiryDate;
        public string insuranceClass;
        public string clientName;
        public string clientAddress;
        public string agent;
        public string totalAmount;
        public string totalAmountString;
        public DateTime dateCreated;
        public string bankName;
        public string bankAccountNo;
        public List<InvoiceItem> invoiceItems = new List<InvoiceItem> ();
    }
}
