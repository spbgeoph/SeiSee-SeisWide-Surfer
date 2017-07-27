namespace SeisWide_Surfer
{
    partial class WaveBinder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.TableLayoutPanel();
            this.listViewTxins = new System.Windows.Forms.ListView();
            this.textBoxInternals = new System.Windows.Forms.TextBox();
            this.textBoxWaves = new System.Windows.Forms.TextBox();
            this.labelCurrentTxin = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSubstitute = new System.Windows.Forms.Button();
            this.buttonSort = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.ColumnCount = 4;
            this.panelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.29632F));
            this.panelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.40736F));
            this.panelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.29632F));
            this.panelMain.Controls.Add(this.listViewTxins, 0, 1);
            this.panelMain.Controls.Add(this.textBoxInternals, 2, 1);
            this.panelMain.Controls.Add(this.textBoxWaves, 3, 2);
            this.panelMain.Controls.Add(this.labelCurrentTxin, 2, 0);
            this.panelMain.Controls.Add(this.label1, 0, 0);
            this.panelMain.Controls.Add(this.labelDescription, 3, 1);
            this.panelMain.Controls.Add(this.buttonClose, 3, 5);
            this.panelMain.Controls.Add(this.buttonSubstitute, 3, 4);
            this.panelMain.Controls.Add(this.buttonSort, 3, 3);
            this.panelMain.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(5, 5);
            this.panelMain.Name = "panelMain";
            this.panelMain.RowCount = 6;
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelMain.Size = new System.Drawing.Size(811, 546);
            this.panelMain.TabIndex = 0;
            // 
            // listViewTxins
            // 
            this.listViewTxins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewTxins.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listViewTxins.GridLines = true;
            this.listViewTxins.Location = new System.Drawing.Point(3, 16);
            this.listViewTxins.MultiSelect = false;
            this.listViewTxins.Name = "listViewTxins";
            this.panelMain.SetRowSpan(this.listViewTxins, 4);
            this.listViewTxins.Size = new System.Drawing.Size(194, 392);
            this.listViewTxins.TabIndex = 0;
            this.listViewTxins.UseCompatibleStateImageBehavior = false;
            this.listViewTxins.View = System.Windows.Forms.View.List;
            this.listViewTxins.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewTxins_ItemSelectionChanged);
            // 
            // textBoxInternals
            // 
            this.textBoxInternals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxInternals.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxInternals.Location = new System.Drawing.Point(223, 16);
            this.textBoxInternals.Multiline = true;
            this.textBoxInternals.Name = "textBoxInternals";
            this.panelMain.SetRowSpan(this.textBoxInternals, 5);
            this.textBoxInternals.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInternals.Size = new System.Drawing.Size(384, 527);
            this.textBoxInternals.TabIndex = 1;
            // 
            // textBoxWaves
            // 
            this.textBoxWaves.Location = new System.Drawing.Point(613, 149);
            this.textBoxWaves.Name = "textBoxWaves";
            this.textBoxWaves.ShortcutsEnabled = false;
            this.textBoxWaves.Size = new System.Drawing.Size(149, 20);
            this.textBoxWaves.TabIndex = 2;
            this.textBoxWaves.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWaves_KeyPress);
            // 
            // labelCurrentTxin
            // 
            this.labelCurrentTxin.AutoSize = true;
            this.labelCurrentTxin.Location = new System.Drawing.Point(223, 0);
            this.labelCurrentTxin.Name = "labelCurrentTxin";
            this.labelCurrentTxin.Size = new System.Drawing.Size(0, 13);
            this.labelCurrentTxin.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Список файлов корреляции";
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(613, 107);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(188, 39);
            this.labelDescription.TabIndex = 7;
            this.labelDescription.Text = "Введите через запятую по порядку номера волн. Нули-разделители годографов писать " +
    "не нужно.";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(613, 414);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(113, 35);
            this.buttonClose.TabIndex = 6;
            this.buttonClose.Text = "Выйти";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSubstitute
            // 
            this.buttonSubstitute.Location = new System.Drawing.Point(613, 348);
            this.buttonSubstitute.Name = "buttonSubstitute";
            this.buttonSubstitute.Size = new System.Drawing.Size(113, 49);
            this.buttonSubstitute.TabIndex = 5;
            this.buttonSubstitute.Text = "Осуществить подстановку для всех файлов";
            this.buttonSubstitute.UseVisualStyleBackColor = true;
            this.buttonSubstitute.Click += new System.EventHandler(this.buttonSubstitute_Click);
            // 
            // buttonSort
            // 
            this.buttonSort.Location = new System.Drawing.Point(613, 282);
            this.buttonSort.Name = "buttonSort";
            this.buttonSort.Size = new System.Drawing.Size(113, 49);
            this.buttonSort.TabIndex = 8;
            this.buttonSort.Text = "Отсортировать файл по волнам";
            this.buttonSort.UseVisualStyleBackColor = true;
            this.buttonSort.Click += new System.EventHandler(this.buttonSort_Click);
            // 
            // WaveBinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 556);
            this.Controls.Add(this.panelMain);
            this.Name = "WaveBinder";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "WaveBinder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WaveBinder_FormClosing);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel panelMain;
        private System.Windows.Forms.ListView listViewTxins;
        private System.Windows.Forms.TextBox textBoxInternals;
        private System.Windows.Forms.TextBox textBoxWaves;
        private System.Windows.Forms.Label labelCurrentTxin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSubstitute;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Button buttonSort;
    }
}