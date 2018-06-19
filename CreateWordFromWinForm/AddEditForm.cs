using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Diagnostics;
using CreateWordFromWinForm.Model;
using Newtonsoft.Json;

namespace CreateWordFromWinForm
{
    public partial class AddEditForm : Form
    {
        public List<Invoice> invoices;
        Invoice invoice;
        Logger logger = new Logger();

        //sample file path
        string samplePath = Application.StartupPath + Path.DirectorySeparatorChar + "Template.docx";

        //List of Invoice passed by reference
        public AddEditForm(List<Invoice> argInvoices, string invoiceNo = "")
        {
            try
            {
                InitializeComponent();

                this.ActiveControl = txtName;
                invoices = argInvoices;

                dgvItems.CellEndEdit += dgvItems_OnCellEndEdit;
                dgvItems.UserDeletedRow += dgvItems_RowCountChanged;
                dgvItems.UserAddedRow += dgvItems_RowCountChanged;
                dgvItems.DefaultCellStyle.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                dgvItems.RowCount = 3;
                dgvItems.Rows[0].Cells[0].Value = "PREMIUM";
                dgvItems.Rows[1].Cells[0].Value = Config.GST.description + " GST";
                dgvItems.CurrentCell = dgvItems.Rows[0].Cells[1];

                txtSumInsured.LostFocus += txtSumInsured_OnLostFocus;

                dpExiryDate.Value = DateTime.Today.AddYears(1).AddDays(-1);

                if (!string.IsNullOrEmpty(invoiceNo))
                {
                    btnViewPDF.Text = "Update PDF";
                    invoice = invoices.Where(it => it.invoiceNo == invoiceNo).ToArray()[0];
                    FillForm(invoice);
                }
                else
                {
                    SetNextInvoiceNo();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private string CreateFile(FileFormat fileFormat)
        {
            string pdfPath = "";
            try
            {
                //initialize word object
                Document document = new Document();
                try
                {
                    document.LoadFromFile(samplePath, FileFormat.Docx);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

                //Create Invoice
                document.Replace("#Date#", invoice.date.ToString(Config.FORMAT_DATETIME), true, true);
                document.Replace("#InvoiceNo#", invoice.invoiceNo, true, true);
                document.Replace("#CoverNoteNo#", invoice.coverNoteNo, true, true);
                document.Replace("#PolicyNo#", invoice.policyNo, true, true);
                document.Replace("#EndorsementNo#", invoice.endorsementNo, true, true);
                document.Replace("#SumInsured#", invoice.sumInsured, true, true);
                document.Replace("#EffDate#", invoice.effectiveDate.ToString(Config.FORMAT_DATETIME), true, true);
                document.Replace("#ExpDate#", invoice.expiryDate.ToString(Config.FORMAT_DATETIME), true, true);
                document.Replace("#InsuranceClass#", invoice.insuranceClass, true, true);
                document.Replace("#BankAccountNo#", invoice.bankAccountNo, true, true);
                document.Replace("#BankName#", invoice.bankName, true, true);
                document.Replace("#Agent#", invoice.agent, true, true);
                document.Replace("#Total#", invoice.totalAmount, true, true);
                document.Replace("#RinggitMalaysia#", invoice.totalAmountString, true, true);
                document.Replace("#Name#", invoice.clientName, true, true);

                //Special handling for address
                TextSelection selection = document.FindString("#Address#", true, true);
                TextRange range = selection.GetAsOneRange();
                Paragraph paragraph = range.OwnerParagraph;

                paragraph.Text = "";
                paragraph.AppendRTF(invoice.clientAddress);

                foreach (var invoiceItem in invoice.invoiceItems)
                {
                    document.Replace("#Description" + invoiceItem.itemNo + "#", invoiceItem.description, true, true);
                    document.Replace("#Amount" + invoiceItem.itemNo + "#", invoiceItem.amount, true, true);
                }

                pdfPath = Application.StartupPath + Path.DirectorySeparatorChar + Config.INVOICE_FOLDER + Path.DirectorySeparatorChar
                    + invoice.invoiceNo + ".pdf";
                string docPath = Application.StartupPath + Path.DirectorySeparatorChar + Config.DOC_FOLDER + Path.DirectorySeparatorChar
                    + invoice.invoiceNo + ".docx";

                document.SaveToFile(docPath, FileFormat.Docx);
                document.SaveToFile(pdfPath, FileFormat.PDF);

                document.Close();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }

            return pdfPath;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void btnViewPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SetInvoice();
                string pdfPath = CreateFile(FileFormat.PDF);
                ToViewFile(pdfPath);
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void txtSumInsured_OnLostFocus(object sender, EventArgs e)
        {
            try
            {
                double value;
                if (Double.TryParse(txtSumInsured.Text, out value))
                {
                    txtSumInsured.Text = string.Format("{0:#,##0.00}", value);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void dgvItems_OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string headerText = this.dgvItems.Columns[e.ColumnIndex].HeaderText;

                // Abort validation if cell is not in the CompanyName column.
                if (!headerText.Equals("Amount") || dgvItems.Rows[e.RowIndex].Cells[1].Value == null) return;

                // Confirm that the cell is not empty.
                double value;
                if (Double.TryParse(this.dgvItems.Rows[e.RowIndex].Cells[1].Value.ToString(), out value))
                {
                    this.dgvItems.Rows[e.RowIndex].Cells[1].Value = string.Format("{0:#,##0.00}", value);
                }

                if (e.RowIndex == 0)
                {
                    dgvItems.Rows[1].Cells[1].Value = string.Format("{0:#,##0.00}", (value * Config.GST.value));
                }
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void dgvItems_RowCountChanged(object sender, EventArgs e)
        {
            try
            {
                CheckRowCount();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void CheckRowCount()
        {
            try
            {
                if (dgvItems.Rows != null && dgvItems.Rows.Count > 5)
                {
                    dgvItems.AllowUserToAddRows = false;
                }
                else
                {
                    dgvItems.AllowUserToAddRows = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void ToViewFile(string fileName)
        {
            try
            {
                //TODO: Not working as expected
                this.Visible = false;
                Cursor.Current = Cursors.WaitCursor;

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = fileName
                    }
                };
                process.Start();
                //process.WaitForExit();

                Cursor.Current = Cursors.Arrow;
                this.Close();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void SetInvoice()
        {
            try
            {
                invoice = new Invoice();

                //Create object
                invoice.invoiceNo = txtInvoiceNo.Text.Trim();
                invoice.date = DateTime.ParseExact(dpInvoiceDate.Text, Config.FORMAT_DATETIME, null);
                invoice.coverNoteNo = txtCoverNoteNo.Text;
                invoice.policyNo = txtPolicyNo.Text.Trim();
                invoice.endorsementNo = txtEndorsementNo.Text;
                invoice.sumInsured = txtSumInsured.Text;
                invoice.effectiveDate = DateTime.ParseExact(dpEffectiveDate.Text, Config.FORMAT_DATETIME, null);
                invoice.expiryDate = DateTime.ParseExact(dpExiryDate.Text, Config.FORMAT_DATETIME, null);
                invoice.insuranceClass = txtInsuranceClass.Text;
                invoice.clientName = txtName.Text;

                //Rich Text Format special handling
                RichTextBox tempAddress = new RichTextBox();
                tempAddress.Rtf = txtAddress.Rtf;
                tempAddress.Font = new Font("Courier New", 10, FontStyle.Regular);
                invoice.clientAddress = tempAddress.Rtf;

                invoice.bankName = Config.BANK_NAME;
                invoice.bankAccountNo = Config.BANK_ACCOUNT_NO;

                if (this.radioAgentA.Checked)
                {
                    invoice.agent = "Kurnia";
                }
                else if (this.radioAgentB.Checked)
                {
                    invoice.agent = "Pacific";
                }

                //Items Calculation and Setting Value
                double total = 0;

                for (int i = 0; i < Config.MAX_ROW; i++)
                {
                    InvoiceItem invoiceItem = new InvoiceItem();

                    if (i < dgvItems.Rows.Count && dgvItems.Rows[i].Cells[0].Value != null && dgvItems.Rows[i].Cells[1].Value != null)
                    {
                        string description = dgvItems.Rows[i].Cells[0].Value.ToString();
                        string amount = dgvItems.Rows[i].Cells[1].Value.ToString();

                        if (!String.IsNullOrWhiteSpace(description) && !String.IsNullOrWhiteSpace(amount))
                        {
                            total += Double.Parse(amount);

                            invoiceItem.itemNo = (i + 1).ToString();
                            invoiceItem.description = description;
                            invoiceItem.amount = "RM " + amount;
                        }
                    }
                    else
                    {
                        invoiceItem.itemNo = (i + 1).ToString();
                        invoiceItem.description = "";
                        invoiceItem.amount = "";
                    }

                    invoice.invoiceItems.Add(invoiceItem);
                }

                invoice.totalAmount = string.Format("RM {0:#,##0.00}", total);
                invoice.totalAmountString = NumberToWords(total);

                invoice.dateCreated = DateTime.Now;

                invoices.Remove(invoices.Find(it => it.invoiceNo == invoice.invoiceNo));
                invoices.Add(invoice);

                //Save Data
                SaveData();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        public void SaveData()
        {
            try
            {
                StreamWriter steamWriter = new StreamWriter(Application.StartupPath + Path.DirectorySeparatorChar + "data.dat", false);

                steamWriter.WriteLine(JsonConvert.SerializeObject(invoices));
                steamWriter.Close();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        public static string NumberToWords(double doubleNumber)
        {
            int intBefore = (int)Math.Floor(doubleNumber);
            int intAfter = (int)(Math.Round((doubleNumber - intBefore), 2) * 100);

            var beforeFloatingPointWord = $"{NumberToWords(intBefore)} Ringgit";
            var afterFloatingPointWord =
                $"{SmallNumberToWord(intAfter, "")} Cents";
            return $"{beforeFloatingPointWord} and {afterFloatingPointWord} Only.";
        }

        private static string NumberToWords(int number)
        {
            var words = "";
            
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            if (number / 1000000000 > 0)
            {
                words += NumberToWords(number / 1000000000) + " Billion ";
                number %= 1000000000;
            }

            if (number / 1000000 > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if (number / 1000 > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if (number / 100 > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            words = SmallNumberToWord(number, words);

            return words;
        }

        private static string SmallNumberToWord(int number, string words)
        {
            if (number <= 0) return "Zero";
            //if (words != "")
            //    words += " ";

            var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += " " + unitsMap[number % 10];
            }

            return words;
        }

        private void dpEffectiveDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dpExiryDate.Value = dpEffectiveDate.Value.AddYears(1).AddDays(-1);
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void SetNextInvoiceNo()
        {
            try
            {
                txtInvoiceNo.Text = (invoices.Max(it => int.Parse(it.invoiceNo)) + 1).ToString();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void FillForm(Invoice argInvoice)
        {
            try
            {
                dpInvoiceDate.Value = argInvoice.date;
                txtInvoiceNo.Text = argInvoice.invoiceNo;
                txtCoverNoteNo.Text = argInvoice.coverNoteNo;
                txtPolicyNo.Text = argInvoice.policyNo;
                txtEndorsementNo.Text = argInvoice.endorsementNo;
                txtSumInsured.Text = argInvoice.sumInsured;
                dpEffectiveDate.Value = argInvoice.effectiveDate;
                dpExiryDate.Value = argInvoice.expiryDate;
                txtInsuranceClass.Text = argInvoice.insuranceClass;
                txtName.Text = argInvoice.clientName;
                try
                {
                    txtAddress.Rtf = argInvoice.clientAddress;
                }
                catch (Exception ex)
                {
                    txtAddress.Text = argInvoice.clientAddress;
                }

                string agent = argInvoice.agent;

                if (agent == "Kurnia")
                {
                    radioAgentA.Checked = true;
                }
                else if (agent == "Pacific")
                {
                    radioAgentB.Checked = true;
                }

                dgvItems.Rows.Clear();

                foreach (var item in argInvoice.invoiceItems)
                {
                    if (!string.IsNullOrWhiteSpace(item.description) && !string.IsNullOrWhiteSpace(item.amount))
                    {
                        dgvItems.Rows.Insert(int.Parse(item.itemNo) - 1, item.description, item.amount.Substring(3, item.amount.Length - 3));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }
    }
}
