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
        public static string FORMAT_DATETIME = "dd-MM-yyyy";
        public static string FORMAT_LOGGER_DATETIME = "yyyy-MM-dd HH:mm:ss.fff";

        public static int MAX_ROW = 5;
        public static string INVOICE_FOLDER = "InvoiceFolder";
        public static string DOC_FOLDER = "DocFolder";
        public static string ERROR_LOG_FILE = "error.log";

        public static bool UPDATED; //Temporary setting for V2

        public static List<string> MONTHS = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
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

    public class ListFilter
    {
        public string key;
        public Object value;

        public ListFilter(string key, Object value)
        {
            this.key = key;
            this.value = value;
        }
    }
}
