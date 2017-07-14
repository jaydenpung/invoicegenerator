using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CreateWordFromWinForm
{
    public partial class SettingForm : Form
    {
        public bool save = false;

        public SettingForm()
        {
            try
            {
                InitializeComponent();
                FillForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string gst = txtGst.Text;
            string bankName = txtBankName.Text;
            string bankAccountNo = txtBankAccountNo.Text;

            StreamWriter file = new StreamWriter(Application.StartupPath + Path.DirectorySeparatorChar + "Settings.ini");

            file.WriteLine("GST=" + gst);
            file.WriteLine("BANK_NAME=" + bankName);
            file.WriteLine("BANK_ACCOUNT_NO=" + bankAccountNo);

            file.Close();

            save = true;
            this.Close();
        }

        private void FillForm()
        {
            string gst = String.Format("{0:0.00}", Config.GST.value);
            string bankName = Config.BANK_NAME;
            string bankAccountNo = Config.BANK_ACCOUNT_NO;

            txtGst.Text = gst;
            txtBankName.Text = bankName;
            txtBankAccountNo.Text = bankAccountNo;
        }
    }
}
