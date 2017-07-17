﻿using System;

namespace CreateWordFromWinForm
{
    partial class AddEditForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dpExiryDate = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.dpEffectiveDate = new System.Windows.Forms.DateTimePicker();
            this.btnViewPDF = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSumInsured = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtInsuranceClass = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCoverNoteNo = new System.Windows.Forms.TextBox();
            this.radioAgentB = new System.Windows.Forms.RadioButton();
            this.radioAgentA = new System.Windows.Forms.RadioButton();
            this.dpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPolicyNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEndorsementNo = new System.Windows.Forms.TextBox();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.dpExiryDate);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.dpEffectiveDate);
            this.groupBox1.Controls.Add(this.btnViewPDF);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.txtSumInsured);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtInsuranceClass);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtCoverNoteNo);
            this.groupBox1.Controls.Add(this.radioAgentB);
            this.groupBox1.Controls.Add(this.radioAgentA);
            this.groupBox1.Controls.Add(this.dpInvoiceDate);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtPolicyNo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtEndorsementNo);
            this.groupBox1.Controls.Add(this.txtInvoiceNo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1021, 543);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Invoice";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(464, 448);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 29);
            this.label9.TabIndex = 44;
            this.label9.Text = "Expiry Date:";
            // 
            // txtAddress
            // 
            this.txtAddress.AcceptsTab = true;
            this.txtAddress.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(19, 148);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(2);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(424, 163);
            this.txtAddress.TabIndex = 2;
            this.txtAddress.Text = "";
            this.txtAddress.WordWrap = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(464, 405);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(153, 29);
            this.label8.TabIndex = 42;
            this.label8.Text = "Effective Date:";
            // 
            // dpExiryDate
            // 
            this.dpExiryDate.CustomFormat = "dd-MM-yyyy";
            this.dpExiryDate.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpExiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpExiryDate.Location = new System.Drawing.Point(627, 442);
            this.dpExiryDate.Name = "dpExiryDate";
            this.dpExiryDate.Size = new System.Drawing.Size(245, 37);
            this.dpExiryDate.TabIndex = 10;
            this.dpExiryDate.Value = new System.DateTime(2018, 7, 12, 0, 0, 0, 0);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(15, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 29);
            this.label11.TabIndex = 44;
            this.label11.Text = "Name:";
            // 
            // dpEffectiveDate
            // 
            this.dpEffectiveDate.CustomFormat = "dd-MM-yyyy";
            this.dpEffectiveDate.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpEffectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpEffectiveDate.Location = new System.Drawing.Point(627, 399);
            this.dpEffectiveDate.Name = "dpEffectiveDate";
            this.dpEffectiveDate.Size = new System.Drawing.Size(245, 37);
            this.dpEffectiveDate.TabIndex = 9;
            this.dpEffectiveDate.ValueChanged += new System.EventHandler(this.dpEffectiveDate_ValueChanged);
            // 
            // btnViewPDF
            // 
            this.btnViewPDF.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewPDF.Location = new System.Drawing.Point(885, 359);
            this.btnViewPDF.Name = "btnViewPDF";
            this.btnViewPDF.Size = new System.Drawing.Size(123, 171);
            this.btnViewPDF.TabIndex = 12;
            this.btnViewPDF.Text = "Create Invoice";
            this.btnViewPDF.UseVisualStyleBackColor = true;
            this.btnViewPDF.Click += new System.EventHandler(this.btnViewPDF_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvItems);
            this.panel1.Location = new System.Drawing.Point(469, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(539, 326);
            this.panel1.TabIndex = 43;
            // 
            // dgvItems
            // 
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Description,
            this.Amount});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.NullValue = null;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItems.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(0, 0);
            this.dgvItems.Name = "dgvItems";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.dgvItems.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvItems.RowTemplate.Height = 40;
            this.dgvItems.Size = new System.Drawing.Size(539, 326);
            this.dgvItems.TabIndex = 42;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.MinimumWidth = 370;
            this.Description.Name = "Description";
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            // 
            // txtSumInsured
            // 
            this.txtSumInsured.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSumInsured.Location = new System.Drawing.Point(627, 359);
            this.txtSumInsured.Name = "txtSumInsured";
            this.txtSumInsured.Size = new System.Drawing.Size(245, 37);
            this.txtSumInsured.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(464, 362);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(141, 29);
            this.label7.TabIndex = 40;
            this.label7.Text = "Sum Insured:";
            // 
            // txtInsuranceClass
            // 
            this.txtInsuranceClass.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsuranceClass.Location = new System.Drawing.Point(197, 488);
            this.txtInsuranceClass.Name = "txtInsuranceClass";
            this.txtInsuranceClass.Size = new System.Drawing.Size(245, 37);
            this.txtInsuranceClass.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(464, 491);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 29);
            this.label6.TabIndex = 38;
            this.label6.Text = "Agent:";
            // 
            // txtCoverNoteNo
            // 
            this.txtCoverNoteNo.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCoverNoteNo.Location = new System.Drawing.Point(197, 359);
            this.txtCoverNoteNo.Name = "txtCoverNoteNo";
            this.txtCoverNoteNo.Size = new System.Drawing.Size(245, 37);
            this.txtCoverNoteNo.TabIndex = 4;
            // 
            // radioAgentB
            // 
            this.radioAgentB.AutoSize = true;
            this.radioAgentB.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioAgentB.Location = new System.Drawing.Point(742, 489);
            this.radioAgentB.Name = "radioAgentB";
            this.radioAgentB.Size = new System.Drawing.Size(93, 33);
            this.radioAgentB.TabIndex = 29;
            this.radioAgentB.Text = "Pacific";
            this.radioAgentB.UseVisualStyleBackColor = true;
            // 
            // radioAgentA
            // 
            this.radioAgentA.AutoSize = true;
            this.radioAgentA.Checked = true;
            this.radioAgentA.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioAgentA.Location = new System.Drawing.Point(627, 489);
            this.radioAgentA.Name = "radioAgentA";
            this.radioAgentA.Size = new System.Drawing.Size(95, 33);
            this.radioAgentA.TabIndex = 11;
            this.radioAgentA.TabStop = true;
            this.radioAgentA.Text = "Kurnia";
            this.radioAgentA.UseVisualStyleBackColor = true;
            // 
            // dpInvoiceDate
            // 
            this.dpInvoiceDate.CustomFormat = "dd-MM-yyyy";
            this.dpInvoiceDate.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpInvoiceDate.Location = new System.Drawing.Point(197, 316);
            this.dpInvoiceDate.Name = "dpInvoiceDate";
            this.dpInvoiceDate.Size = new System.Drawing.Size(245, 37);
            this.dpInvoiceDate.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(13, 491);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(169, 29);
            this.label10.TabIndex = 26;
            this.label10.Text = "Insurance Class:";
            // 
            // txtPolicyNo
            // 
            this.txtPolicyNo.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPolicyNo.Location = new System.Drawing.Point(197, 402);
            this.txtPolicyNo.Name = "txtPolicyNo";
            this.txtPolicyNo.Size = new System.Drawing.Size(245, 37);
            this.txtPolicyNo.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 405);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 29);
            this.label5.TabIndex = 9;
            this.label5.Text = "Policy No:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 322);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 29);
            this.label1.TabIndex = 8;
            this.label1.Text = "Date:";
            // 
            // txtEndorsementNo
            // 
            this.txtEndorsementNo.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndorsementNo.Location = new System.Drawing.Point(197, 445);
            this.txtEndorsementNo.Name = "txtEndorsementNo";
            this.txtEndorsementNo.Size = new System.Drawing.Size(245, 37);
            this.txtEndorsementNo.TabIndex = 6;
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvoiceNo.Location = new System.Drawing.Point(198, 31);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(245, 37);
            this.txtInvoiceNo.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 448);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 29);
            this.label4.TabIndex = 3;
            this.label4.Text = "Endorsement No:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 362);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cover Note No:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Invoice No:";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(198, 79);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(245, 37);
            this.txtName.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(15, 119);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 29);
            this.label12.TabIndex = 46;
            this.label12.Text = "Address:";
            // 
            // AddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 559);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AddEditForm";
            this.Text = "Jes Services";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtEndorsementNo;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPolicyNo;
        private System.Windows.Forms.RadioButton radioAgentB;
        private System.Windows.Forms.RadioButton radioAgentA;
        private System.Windows.Forms.DateTimePicker dpInvoiceDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnViewPDF;
        private System.Windows.Forms.TextBox txtCoverNoteNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSumInsured;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtInsuranceClass;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dpEffectiveDate;
        private System.Windows.Forms.DateTimePicker dpExiryDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RichTextBox txtAddress;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtName;
    }
}

