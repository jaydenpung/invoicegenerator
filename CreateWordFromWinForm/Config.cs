using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreateWordFromWinForm
{
    public class Config
    {
        public static Gst GST;
        public static string BANK_NAME;
        public static string BANK_ACCOUNT_NO;

        public static int MAX_ROW = 5;
        public static string INVOICE_FOLDER = "InvoiceFolder";
        public static string DOC_FOLDER = "DocFolder";
    }

    public class Gst
    {
        public string description;
        public double value;

        public Gst(string description, double value)
        {
            this.description = description;
            this.value = value;
        }
    }
}
