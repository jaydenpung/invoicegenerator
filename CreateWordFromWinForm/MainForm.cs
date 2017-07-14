using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CreateWordFromWinForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ReadSettingFile();

            lvFolderList.MouseClick += lvFolderList_OnMouseClick;
            lvFolderList.MouseDoubleClick += lvFolderList_OnMouseDoubleClick;

            LoadFileList();
        }

        public void LoadFileList()
        {
            lvFolderList.Items.Clear();

            DateTime now = DateTime.Now;
            TimeSpan localOffset = now - now.ToUniversalTime();

            string folderPath = Application.StartupPath + Path.DirectorySeparatorChar + Config.INVOICE_FOLDER + Path.DirectorySeparatorChar;
            DirectoryInfo dinfo = new DirectoryInfo(folderPath);
            FileInfo[] Files = dinfo.GetFiles("*.pdf");
            foreach (FileInfo file in Files)
            {
                ListViewItem item = new ListViewItem(
                    new string[]
                    {
                        file.Name.Split('.')[0],
                        file.Name,
                        (file.CreationTimeUtc + localOffset).ToString("dd/MM/yyyy  hh:mm tt")
                    }    
                );

                lvFolderList.Items.Add(item);
            }
            lvFolderList.ListViewItemSorter = new ListViewItemComparer(0);
            lvFolderList.Sort();

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
            AddEditForm addEditForm = new AddEditForm();
            addEditForm.FormClosed += addEditForm_OnFormClosed;
            addEditForm.ShowDialog();
        }

        private void addEditForm_OnFormClosed(object sender, EventArgs e)
        {
            LoadFileList();
        }

        private void lvFolderList_OnMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (lvFolderList.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    cmsListViewRightClick.Show(Cursor.Position);
                }
            }
        }

        private void lvFolderList_OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (lvFolderList.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    string fileName = Application.StartupPath + Path.DirectorySeparatorChar + Config.INVOICE_FOLDER + Path.DirectorySeparatorChar
                        + lvFolderList.FocusedItem.Text + ".pdf";
                    try
                    {
                        System.Diagnostics.Process.Start(fileName);
                    }
                    catch { }
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = Application.StartupPath + Path.DirectorySeparatorChar + Config.INVOICE_FOLDER + Path.DirectorySeparatorChar
                + lvFolderList.FocusedItem.Text + ".pdf";
            try
            {
                System.Diagnostics.Process.Start(fileName);
            }
            catch { }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you would like to delete Invoice " + lvFolderList.FocusedItem.Text + "?", "Delete", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                string pdfFile = Application.StartupPath + Path.DirectorySeparatorChar + Config.INVOICE_FOLDER + Path.DirectorySeparatorChar
                + lvFolderList.FocusedItem.Text + ".pdf";

                string docFile = Application.StartupPath + Path.DirectorySeparatorChar + Config.DOC_FOLDER + Path.DirectorySeparatorChar
                + lvFolderList.FocusedItem.Text + ".docx";  

                try
                {
                    File.Delete(pdfFile);
                    File.Delete(docFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

                LoadFileList();
            }           
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditForm addEditForm = new AddEditForm(lvFolderList.FocusedItem.Text);
            addEditForm.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingForm settingForm = new SettingForm();
            settingForm.FormClosed += SettingForm_OnFormClosed;
            settingForm.ShowDialog();
        }

        private void SettingForm_OnFormClosed(object sender, EventArgs e)
        {
            if (((SettingForm)sender).save)
            {
                ReadSettingFile();
            }
        }        

        private void ReadSettingFile()
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
            }

            file.Close();
        }

        private void CreateDefaultSettingFile(string settingFilePath)
        {
            StreamWriter file = new StreamWriter(settingFilePath);

            file.WriteLine("GST=0.06");
            file.WriteLine("BANK_NAME=Maybank");
            file.WriteLine("BANK_ACCOUNT_NO=5144 0430 4301");

            file.Close();
        }
    }
}
