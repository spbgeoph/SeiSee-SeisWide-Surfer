namespace SeisWide_Surfer
{
    partial class UncertaintiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UncertaintiesForm));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelUncertainty = new System.Windows.Forms.Label();
            this.textBoxUnc = new System.Windows.Forms.TextBox();
            this.buttonSubstitute = new System.Windows.Forms.Button();
            this.labelNote = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.labelUncertainty, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.textBoxUnc, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.buttonSubstitute, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.labelNote, 1, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(391, 273);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // labelUncertainty
            // 
            this.labelUncertainty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelUncertainty.AutoSize = true;
            this.tableLayoutPanel.SetColumnSpan(this.labelUncertainty, 2);
            this.labelUncertainty.Location = new System.Drawing.Point(3, 0);
            this.labelUncertainty.Name = "labelUncertainty";
            this.labelUncertainty.Size = new System.Drawing.Size(195, 13);
            this.labelUncertainty.TabIndex = 0;
            this.labelUncertainty.Text = "Погрешности для каждого типа волн";
            // 
            // textBoxUnc
            // 
            this.textBoxUnc.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SeisWide_Surfer.Properties.Settings.Default, "uncs", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxUnc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxUnc.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxUnc.Location = new System.Drawing.Point(3, 16);
            this.textBoxUnc.Multiline = true;
            this.textBoxUnc.Name = "textBoxUnc";
            this.tableLayoutPanel.SetRowSpan(this.textBoxUnc, 2);
            this.textBoxUnc.Size = new System.Drawing.Size(189, 254);
            this.textBoxUnc.TabIndex = 1;
            this.textBoxUnc.Text = global::SeisWide_Surfer.Properties.Settings.Default.uncs;
            // 
            // buttonSubstitute
            // 
            this.buttonSubstitute.Location = new System.Drawing.Point(200, 18);
            this.buttonSubstitute.Margin = new System.Windows.Forms.Padding(5);
            this.buttonSubstitute.Name = "buttonSubstitute";
            this.buttonSubstitute.Size = new System.Drawing.Size(131, 49);
            this.buttonSubstitute.TabIndex = 2;
            this.buttonSubstitute.Text = "Произвести замену значений погрешности";
            this.buttonSubstitute.UseVisualStyleBackColor = true;
            this.buttonSubstitute.Click += new System.EventHandler(this.buttonSubstitute_Click);
            // 
            // labelNote
            // 
            this.labelNote.AutoSize = true;
            this.labelNote.Location = new System.Drawing.Point(200, 77);
            this.labelNote.Margin = new System.Windows.Forms.Padding(5);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(183, 169);
            this.labelNote.TabIndex = 3;
            this.labelNote.Text = resources.GetString("labelNote.Text");
            // 
            // UncertaintiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 283);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "UncertaintiesForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Установить уровень погрешности";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelUncertainty;
        private System.Windows.Forms.TextBox textBoxUnc;
        private System.Windows.Forms.Button buttonSubstitute;
        private System.Windows.Forms.Label labelNote;
    }
}