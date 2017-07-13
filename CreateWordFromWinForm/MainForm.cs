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

            lvFolderList.MouseClick += lvFolderList_OnMouseClick;
            lvFolderList.MouseDoubleClick += lvFolderList_OnMouseDoubleClick;

            LoadFileList();
        }

        public void LoadFileList()
        {
            lvFolderList.Items.Clear();

            DateTime now = DateTime.Now;
            TimeSpan localOffset = now - now.ToUniversalTime();

            string folderPath = Application.StartupPath + Path.DirectorySeparatorChar + Config.INVOICE_FOLDER;
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
            string fileName = Application.StartupPath + Path.DirectorySeparatorChar + Config.INVOICE_FOLDER + Path.DirectorySeparatorChar
                + lvFolderList.FocusedItem.Text + ".pdf";

            DialogResult dialogResult = MessageBox.Show("Are you sure you would like to delete Invoice " + lvFolderList.FocusedItem.Text + "?", "Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    File.Delete(fileName);
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
            MessageBox.Show("WorkInProgress");
        }
    }
}
