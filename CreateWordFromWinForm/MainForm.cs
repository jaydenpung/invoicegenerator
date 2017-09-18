using CreateWordFromWinForm.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spire.Doc;
using Spire.Doc.Documents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CreateWordFromWinForm
{
    public partial class MainForm : Form
    {
        List<Invoice> invoices = new List<Invoice> ();
        Logger logger = new Logger();

        public MainForm()
        {
            try
            {
                InitializeComponent();

                cbFilterMonth.SelectedIndex = 0;
                cbFilterYear.SelectedIndex = 0;

                CreateDefaultFolders();
                ReadSettingFile();
                UpdateProcedure();
                LoadData();

                lvFolderList.MouseClick += lvFolderList_OnMouseClick;
                lvFolderList.MouseDoubleClick += lvFolderList_OnMouseDoubleClick;

                LoadFileList();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        public void LoadFileList(List<ListFilter> listFilters = null)
        {
            try
            {
                lvFolderList.Items.Clear();

                //Get Values for each file
                foreach (Invoice invoice in invoices)
                {
                    //Filter
                    if (listFilters != null)
                    {
                        bool filter = false; //If true, remove the item from list, else continue as usual

                        foreach (ListFilter listFilter in listFilters)
                        {
                            //Filter by expiry month
                            if (listFilter.key == "expiryMonth")
                            {
                                DateTime filterExpDate = Convert.ToDateTime(listFilter.value);

                                if (invoice.expiryDate.Month != filterExpDate.Month)
                                {
                                    filter = true;
                                    continue; //Stop checking filter since item already filtered
                                }
                            }

                            //Filter by expiry year
                            if (listFilter.key == "expiryYear")
                            {

                                DateTime filterExpDate = Convert.ToDateTime(listFilter.value);

                                if (invoice.expiryDate.Year != filterExpDate.Year)
                                {
                                    filter = true;
                                    continue; //Stop checking filter since item already filtered
                                }
                            }
                        }

                        if (filter)
                        {
                            continue; //Skip and check next file
                        }
                    }

                    ListViewItem item = new ListViewItem(
                        new string[]
                        {
                            invoice.invoiceNo,
                            invoice.clientName,
                            invoice.insuranceClass,
                            invoice.effectiveDate.ToString(Config.FORMAT_DATETIME),
                            invoice.expiryDate.ToString(Config.FORMAT_DATETIME),
                            invoice.totalAmount,
                            (invoice.dateCreated).ToString("dd/MM/yyyy  hh:mm tt")
                        }
                    );

                    lvFolderList.Items.Add(item);
                }
                lvFolderList.ListViewItemSorter = new ListViewItemComparer(0);
                lvFolderList.Sort();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        class ListViewItemComparer : IComparer
        {
            private int col;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                int returnVal = -1;
                //returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                returnVal = Int32.Parse(((ListViewItem)y).SubItems[col].Text) - Int32.Parse(((ListViewItem)x).SubItems[col].Text);
                return returnVal;
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                AddEditForm addEditForm = new AddEditForm(invoices);
                addEditForm.FormClosed += addEditForm_OnFormClosed;
                addEditForm.ShowDialog();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void addEditForm_OnFormClosed(object sender, EventArgs e)
        {
            try
            {
                LoadFileList();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void lvFolderList_OnMouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (lvFolderList.FocusedItem.Bounds.Contains(e.Location) == true)
                    {
                        cmsListViewRightClick.Show(Cursor.Position);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void lvFolderList_OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (lvFolderList.FocusedItem.Bounds.Contains(e.Location) == true)
                    {
                        string fileName = Application.StartupPath + Path.DirectorySeparatorChar + Config.INVOICE_FOLDER + Path.DirectorySeparatorChar
                            + lvFolderList.FocusedItem.Text + ".pdf";
                        System.Diagnostics.Process.Start(fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = Application.StartupPath + Path.DirectorySeparatorChar + Config.INVOICE_FOLDER + Path.DirectorySeparatorChar
                    + lvFolderList.FocusedItem.Text + ".pdf";
                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you would like to delete Invoice " + lvFolderList.FocusedItem.Text + "?", "Delete", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    string invoiceNo = lvFolderList.FocusedItem.Text;
                    Invoice invoice = invoices.Find(it => it.invoiceNo == invoiceNo);

                    string pdfFile = Application.StartupPath + Path.DirectorySeparatorChar + Config.INVOICE_FOLDER + Path.DirectorySeparatorChar
                    + invoiceNo + ".pdf";

                    string docFile = Application.StartupPath + Path.DirectorySeparatorChar + Config.DOC_FOLDER + Path.DirectorySeparatorChar
                    + invoiceNo + ".docx";

                    invoices.Remove(invoice);
                    File.Delete(pdfFile);
                    File.Delete(docFile);

                    LoadFileList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddEditForm addEditForm = new AddEditForm(invoices, lvFolderList.FocusedItem.Text);
                addEditForm.FormClosed += addEditForm_OnFormClosed;
                addEditForm.ShowDialog();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SettingForm settingForm = new SettingForm();
                settingForm.FormClosed += SettingForm_OnFormClosed;
                settingForm.ShowDialog();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void SettingForm_OnFormClosed(object sender, EventArgs e)
        {
            try
            {
                if (((SettingForm)sender).save)
                {
                    ReadSettingFile();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }        

        private void ReadSettingFile()
        {
            try
            {
                string line;

                // Read the file and display it line by line.
                string settingFilePath = Application.StartupPath + Path.DirectorySeparatorChar + "Settings.ini";

                if (!File.Exists(settingFilePath))
                {
                    CreateDefaultSettingFile(settingFilePath);
                }

                StreamReader file = new StreamReader(settingFilePath);

                while ((line = file.ReadLine()) != null)
                {
                    var key = line.Split('=')[0];
                    var value = line.Split('=')[1];

                    if (key == "GST")
                    {
                        double gstDouble = Double.Parse(value);
                        string gstDescription = (gstDouble * 100) + "%";
                        Config.GST = new Gst(gstDescription, gstDouble);
                    }
                    else if (key == "BANK_NAME")
                    {
                        Config.BANK_NAME = value;
                    }
                    else if (key == "BANK_ACCOUNT_NO")
                    {
                        Config.BANK_ACCOUNT_NO = value;
                    }
                    else if (key == "UPDATE_URL")
                    {
                        Config.UPDATE_URL = value;
                    }
                }

                file.Close();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void CreateDefaultSettingFile(string settingFilePath)
        {
            try
            {
                StreamWriter file = new StreamWriter(settingFilePath);

                file.WriteLine("GST=0.06");
                file.WriteLine("BANK_NAME=Maybank");
                file.WriteLine("BANK_ACCOUNT_NO=5144 0430 4301");

                file.Close();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void CreateDefaultFolders()
        {
            try
            {
                Directory.CreateDirectory(Application.StartupPath + Path.DirectorySeparatorChar + Config.INVOICE_FOLDER);
                Directory.CreateDirectory(Application.StartupPath + Path.DirectorySeparatorChar + Config.DOC_FOLDER);
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void cbFilterExpDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int year;
                string month = cbFilterMonth.Text;

                List<ListFilter> filterLists = new List<ListFilter>();

                //Check if month is valid
                if (Config.MONTHS.Contains(month))
                {
                    filterLists.Add(
                            new ListFilter("expiryMonth", DateTime.ParseExact(month, "MMMM", null))
                        );
                }

                //Check if year is valid
                if (int.TryParse(cbFilterYear.Text, out year))
                {
                    filterLists.Add(
                            new ListFilter("expiryYear", DateTime.ParseExact(year.ToString(), "yyyy", null))
                        );
                }

                //Refresh list with filters
                LoadFileList(filterLists);
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }
        
        //Deprecated, keep for updateProcedure()
        public Dictionary<string, string> getValueFromFile(Document doc)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            try
            {
                int beginIndex;
                int endIndex;
                List<string> values = new List<string>();

                //Get textbox values
                #region Get Textbox Values
                foreach (Section section in doc.Sections)
                {
                    foreach (Paragraph p in section.Paragraphs)
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
                #endregion

                //Extract key and value
                foreach (string value in values)
                {
                    beginIndex = value.IndexOf('#');
                    endIndex = value.LastIndexOf('#');

                    //HiddenKey found
                    if (beginIndex != -1 && endIndex != -1)
                    {
                        endIndex++;
                        int length = value.Length - endIndex;
                        string hiddenKey = value.Substring(beginIndex, endIndex - beginIndex);
                        string fieldValue = value.Substring(endIndex, length);

                        dictionary.Add(hiddenKey, fieldValue);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }

            return dictionary;
        }

        private void UpdateProcedure() {
            try
            {
                //V2 Procedure
                string dataFile = Application.StartupPath + Path.DirectorySeparatorChar + "data.dat";
                if (!File.Exists(dataFile))
                {

                    List<Invoice> invoicesToTransform = new List<Invoice>();

                    //Load Files
                    DateTime now = DateTime.Now;
                    TimeSpan localOffset = now - now.ToUniversalTime();

                    string folderPath = Application.StartupPath + Path.DirectorySeparatorChar + Config.DOC_FOLDER + Path.DirectorySeparatorChar;
                    DirectoryInfo dinfo = new DirectoryInfo(folderPath);
                    FileInfo[] Files = new FileInfo[] { };

                    try
                    {
                        Files = dinfo.GetFiles("*.docx");
                    }
                    catch (Exception ex)
                    {

                    }

                    Document doc = new Document();
                    Dictionary<string, string> dictionary;

                    //Get Values for each file
                    foreach (FileInfo file in Files)
                    {
                        doc.LoadFromFile(file.FullName);
                        dictionary = getValueFromFile(doc);

                        Invoice invoice = new Invoice();
                        invoice.date = DateTime.ParseExact(dictionary["#HiddenDate#"], Config.FORMAT_DATETIME, null);
                        invoice.invoiceNo = dictionary["#HiddenInvoiceNo#"];
                        invoice.coverNoteNo = dictionary["#HiddenCoverNoteNo#"];
                        invoice.policyNo = dictionary["#HiddenPolicyNo#"];
                        invoice.endorsementNo = dictionary["#HiddenEndorsementNo#"];
                        invoice.sumInsured = dictionary["#HiddenSumInsured#"];
                        invoice.effectiveDate = DateTime.ParseExact(dictionary["#HiddenEffDate#"], Config.FORMAT_DATETIME, null); ;
                        invoice.expiryDate = DateTime.ParseExact(dictionary["#HiddenExpDate#"], Config.FORMAT_DATETIME, null);
                        invoice.insuranceClass = dictionary["#HiddenInsuranceClass#"];
                        invoice.clientName = dictionary["#HiddenName#"];
                        invoice.clientAddress = dictionary["#HiddenAddress#"];
                        invoice.agent = dictionary["#HiddenAgent#"];
                        invoice.totalAmount = dictionary["#HiddenTotal#"];
                        invoice.dateCreated = file.CreationTimeUtc + localOffset;
                        invoice.bankAccountNo = dictionary["#HiddenBankAccountNo#"];
                        invoice.bankName = dictionary["#HiddenBankName#"];
                        invoice.totalAmountString = dictionary["#HiddenRinggitMalaysia#"];

                        for (int i = 0; i < Config.MAX_ROW; i++)
                        {
                            invoice.invoiceItems.Add(
                                    new InvoiceItem((i + 1).ToString(), dictionary["#HiddenDescription" + (i + 1) + "#"], dictionary["#HiddenAmount" + (i + 1) + "#"])
                                );
                        }

                        invoicesToTransform.Add(invoice);
                    }

                    //transform data
                    StreamWriter streamWriter = new StreamWriter(Application.StartupPath + Path.DirectorySeparatorChar + "data.dat");

                    streamWriter.WriteLine(JsonConvert.SerializeObject(invoicesToTransform));
                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                string line;

                // Read the file and display it line by line.
                string dataFile = Application.StartupPath + Path.DirectorySeparatorChar + "data.dat";

                StreamReader streamReader = new StreamReader(dataFile);

                while ((line = streamReader.ReadLine()) != null)
                {
                    invoices = ((JArray)JsonConvert.DeserializeObject(line)).ToObject<List<Invoice>>();
                }

                streamReader.Close();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProgressForm progressForm = new ProgressForm();
                //progressForm.FormClosed += progressForm_OnFormClosed;
                progressForm.ShowDialog();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }
    }
}
