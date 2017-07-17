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

namespace CreateWordFromWinForm
{
    public partial class AddEditForm : Form
    {
        //sample file path
        string samplePath = Application.StartupPath + Path.DirectorySeparatorChar + "Template.docx";

        bool isEditMode = false;

        //word document object
        Document document = null;

        public AddEditForm(string invoiceNo = "")
        {
            InitializeComponent();

            this.ActiveControl = txtName;

            GetNextInvoiceNo();

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

            if(!string.IsNullOrEmpty(invoiceNo))
            {
                isEditMode = true;
                btnViewPDF.Text = "Update PDF";
                FillForm(invoiceNo);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private string CreateFile(FileFormat fileFormat)
        {
            //initialize word object
            document = new Document();
            try
            {
                document.LoadFromFile(samplePath, FileFormat.Docx);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            //get strings to replace
            Dictionary<string, string> dictReplace = GetReplaceDictionary();
            //Replace text
            foreach (KeyValuePair<string, string> kvp in dictReplace)
            {
                string hiddenKey = "#Hidden" + kvp.Key.Trim('#') + "#";

                if (kvp.Key == "#Address#")
                {
                    Section section = document.Sections[0];
                    TextSelection selection = document.FindString(kvp.Key, true, true);
                    TextRange range = selection.GetAsOneRange();
                    Paragraph paragraph = range.OwnerParagraph;

                    paragraph.Text = "";
                    paragraph.AppendText(hiddenKey);
                    paragraph.AppendRTF(kvp.Value);
                }
                else
                {
                    document.Replace(kvp.Key, hiddenKey + kvp.Value, true, true);
                }

                //Hide Hidden keys
                TextSelection selectionHiddenKey = document.FindString(hiddenKey, true, false);
                TextRange rangeHiddenKey = selectionHiddenKey.GetAsOneRange();
                rangeHiddenKey.CharacterFormat.Hidden = true;
            }

            string pdfPath = Application.StartupPath + Path.DirectorySeparatorChar + Config.INVOICE_FOLDER + Path.DirectorySeparatorChar
                + dictReplace["#InvoiceNo#"] + ".pdf";
            string docPath = Application.StartupPath + Path.DirectorySeparatorChar + Config.DOC_FOLDER + Path.DirectorySeparatorChar
                + dictReplace["#InvoiceNo#"] + ".docx";

            document.SaveToFile(docPath, FileFormat.Docx);
            document.SaveToFile(pdfPath, FileFormat.PDF);

            document.Close();

            return pdfPath;            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnViewPDF_Click(object sender, EventArgs e)
        {
            string pdfPath = CreateFile(FileFormat.PDF);
            ToViewFile(pdfPath);
        }

        private void txtSumInsured_OnLostFocus(object sender, EventArgs e)
        {
            double value;
            if (Double.TryParse(txtSumInsured.Text, out value))
            {
                txtSumInsured.Text = string.Format("{0:#,##0.00}", value);
            }
        }

        private void dgvItems_OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string headerText = this.dgvItems.Columns[e.ColumnIndex].HeaderText;

            // Abort validation if cell is not in the CompanyName column.
            if (!headerText.Equals("Amount") || dgvItems.Rows[e.RowIndex].Cells[1].Value == null) return;

            // Confirm that the cell is not empty.
            double value;
            if(Double.TryParse(this.dgvItems.Rows[e.RowIndex].Cells[1].Value.ToString(), out value))
            {
                this.dgvItems.Rows[e.RowIndex].Cells[1].Value = string.Format("{0:#,##0.00}", value);
            }

            if(e.RowIndex == 0)
            {
                dgvItems.Rows[1].Cells[1].Value = string.Format("{0:#,##0.00}", (value * Config.GST.value));
            }
        }

        private void dgvItems_RowCountChanged(object sender, EventArgs e)
        {
            CheckRowCount();
        }

        private void CheckRowCount()
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
                process.WaitForExit();

                Cursor.Current = Cursors.Arrow;
                this.Close();
            }
            catch { }
        }

        Dictionary<string, string> GetReplaceDictionary()
        {
            Dictionary<string, string> replaceDict = new Dictionary<string, string>();
            replaceDict.Add("#Date#", dpInvoiceDate.Text);
            replaceDict.Add("#InvoiceNo#", txtInvoiceNo.Text.Trim());
            replaceDict.Add("#CoverNoteNo#", txtCoverNoteNo.Text);
            replaceDict.Add("#PolicyNo#", txtPolicyNo.Text.Trim());
            replaceDict.Add("#EndorsementNo#", txtEndorsementNo.Text);

            if (!String.IsNullOrWhiteSpace(txtSumInsured.Text))
            {
                replaceDict.Add("#SumInsured#", "RM " + txtSumInsured.Text);
            }
            else
            {
                replaceDict.Add("#SumInsured#", txtSumInsured.Text);
            }

            replaceDict.Add("#EffDate#", dpEffectiveDate.Text);
            replaceDict.Add("#ExpDate#", dpExiryDate.Text);            
            replaceDict.Add("#InsuranceClass#", txtInsuranceClass.Text);
            replaceDict.Add("#Name#", txtName.Text);

            //Rich Text Format special handling
            var oriFont = txtAddress.Font;
            txtAddress.Font = new Font("Courier New", 10, FontStyle.Regular);
            replaceDict.Add("#Address#", txtAddress.Rtf);
            txtAddress.Font = oriFont;

            replaceDict.Add("#BankAccountNo#", Config.BANK_ACCOUNT_NO);
            replaceDict.Add("#BankName#", Config.BANK_NAME);

            if (this.radioAgentA.Checked)
            {
                replaceDict.Add("#Agent#", "Kurnia");
            }
            else if (this.radioAgentB.Checked)
            {
                replaceDict.Add("#Agent#", "Pacific");
            }

            //Items Calculation and Setting Value
            double total = 0;

            for (int i = 0; i < Config.MAX_ROW; i++)
            {
                if (i < dgvItems.Rows.Count && dgvItems.Rows[i].Cells[0].Value != null && dgvItems.Rows[i].Cells[1].Value != null)
                {
                    string description = dgvItems.Rows[i].Cells[0].Value.ToString();
                    string amount = dgvItems.Rows[i].Cells[1].Value.ToString();

                    if (!String.IsNullOrWhiteSpace(description) && !String.IsNullOrWhiteSpace(amount))
                    {
                        total += Double.Parse(amount);

                        replaceDict.Add("#Description" + (i + 1) + "#", description);
                        replaceDict.Add("#Amount" + (i + 1) + "#", "RM " + amount);
                    }
                }
                else
                {
                    replaceDict.Add("#Description" + (i + 1) + "#", "");
                    replaceDict.Add("#Amount" + (i + 1) + "#", "");
                }
            }

            replaceDict.Add("#Total#", string.Format("RM {0:#,##0.00}", total));
            replaceDict.Add("#RinggitMalaysia#", NumberToWords(total));            

            return replaceDict;
        }

        public static string NumberToWords(double doubleNumber)
        {
            var beforeFloatingPoint = (int)Math.Floor(doubleNumber);
            var beforeFloatingPointWord = $"{NumberToWords(beforeFloatingPoint)} Ringgit";
            var afterFloatingPointWord =
                $"{SmallNumberToWord((int)((doubleNumber - beforeFloatingPoint) * 100), "")} Cents";
            return $"{beforeFloatingPointWord} and {afterFloatingPointWord} Only.";
        }

        private static string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            var words = "";

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
            if (words != "")
                words += " ";

            var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
            return words;
        }

        private void dpEffectiveDate_ValueChanged(object sender, EventArgs e)
        {
            dpExiryDate.Value = dpEffectiveDate.Value.AddYears(1);
        }

        private void GetNextInvoiceNo()
        {
            string folderPath = Application.StartupPath + Path.DirectorySeparatorChar + Config.INVOICE_FOLDER + Path.DirectorySeparatorChar;
            DirectoryInfo dinfo = new DirectoryInfo(folderPath);
            FileInfo[] Files = dinfo.GetFiles("*.pdf");

            int invoiceNo = 0;

            if (Files.Any())
            {
                invoiceNo = Files.Select(file => int.Parse(file.Name.Split('.')[0])).ToList().Max();
            }

            txtInvoiceNo.Text = (invoiceNo + 1).ToString();
        }

        private void FillForm(string invoiceNo)
        {
            //Get document
            string filePath = Application.StartupPath + Path.DirectorySeparatorChar + Config.DOC_FOLDER + Path.DirectorySeparatorChar
                + invoiceNo + ".docx";
            document = new Document();
            document.LoadFromFile(filePath, FileFormat.Docx);
            
            //Get textbox values
            List<string> values = new List<string>();
            foreach (Section section in document.Sections)
            {
                foreach(Paragraph p in section.Paragraphs)
                {
                    foreach (DocumentObject obj in p.ChildObjects)
                    {
                        if (obj.DocumentObjectType == DocumentObjectType.TextBox)
                        {
                            Spire.Doc.Fields.TextBox textbox = obj as Spire.Doc.Fields.TextBox;
                            string value = "";

                            foreach (DocumentObject objt in textbox.ChildObjects)
                            {

                                if (objt.DocumentObjectType == DocumentObjectType.Paragraph)
                                {
                                    if (!string.IsNullOrWhiteSpace(value))
                                    {
                                        value += "\n";
                                    }

                                    value += (objt as Paragraph).Text;
                                }
                            }

                            values.Add(value);
                        }
                    }
                }
            }

            //Extract key and value
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (string value in values)
            {
                int beginIndex = value.IndexOf('#');
                int endIndex = value.LastIndexOf('#');
                
                //HiddenKey found
                if(beginIndex != -1 && endIndex != -1)
                {
                    endIndex ++;
                    int length = value.Length - endIndex;
                    string hiddenKey = value.Substring(beginIndex, endIndex);
                    string fieldValue = value.Substring(endIndex, length);

                    dictionary.Add(hiddenKey, fieldValue);
                }
            }

            //Set Value
            dpInvoiceDate.Text = dictionary["#HiddenDate#"];
            txtInvoiceNo.Text = dictionary["#HiddenInvoiceNo#"];
            txtCoverNoteNo.Text = dictionary["#HiddenCoverNoteNo#"];
            txtPolicyNo.Text = dictionary["#HiddenPolicyNo#"];
            txtEndorsementNo.Text = dictionary["#HiddenEndorsementNo#"];
            if (!String.IsNullOrWhiteSpace(dictionary["#HiddenSumInsured#"]))
            {
                txtSumInsured.Text = dictionary["#HiddenSumInsured#"].Remove(0, 3); //Remove "RM "
            }
            dpEffectiveDate.Text = dictionary["#HiddenEffDate#"];
            dpExiryDate.Text = dictionary["#HiddenExpDate#"];
            txtInsuranceClass.Text = dictionary["#HiddenInsuranceClass#"];
            txtName.Text = dictionary["#HiddenName#"];
            txtAddress.Text = dictionary["#HiddenAddress#"];
            string agent = dictionary["#HiddenAgent#"];

            if (agent == "Kurnia")
            {
                radioAgentA.Checked = true;
            }
            else if (agent == "Pacific")
            {
                radioAgentB.Checked = true;
            }

            dgvItems.Rows.Clear();

            for (int i = 0; i < Config.MAX_ROW; i++)
            {
                string description = dictionary["#HiddenDescription" + (i + 1) + "#"];
                string amount = dictionary["#HiddenAmount" + (i + 1) + "#"];

                if(!string.IsNullOrWhiteSpace(description) && !string.IsNullOrWhiteSpace(amount))
                {
                    amount = amount.Substring(3, amount.Length - 3); //Remove "RM "

                    dgvItems.Rows.Insert(i, description, amount);
                }
            }
        }
    }
}
