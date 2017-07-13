using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreateWordFromWinForm
{
    public class Config
    {
        public static Gst GST = new Gst("6%", 0.06);
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
