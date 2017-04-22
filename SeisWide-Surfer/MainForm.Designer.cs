using System.Windows.Forms;
namespace SeisWide_Surfer
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonOpenFolder = new System.Windows.Forms.Button();
            this.buttonOpenTXIN = new System.Windows.Forms.Button();
            this.buttonOpenSW = new System.Windows.Forms.Button();
            this.buttonInterpolate = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.t = new System.Windows.Forms.ToolTip(this.components);
            this.buttonCorrectAll = new System.Windows.Forms.Button();
            this.buttonCorrect = new System.Windows.Forms.Button();
            this.buttonOpenSeiSee = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.TableLayoutPanel();
            this.pathToFolder = new System.Windows.Forms.Label();
            this.panelManual = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.pathToTXIN = new System.Windows.Forms.TextBox();
            this.pathToSW = new System.Windows.Forms.TextBox();
            this.pathToSeiSee = new System.Windows.Forms.TextBox();
            this.panelAutomatic = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelInterpolation = new System.Windows.Forms.TableLayoutPanel();
            this.singleCheckBox = new System.Windows.Forms.CheckBox();
            this.deltaTextBox = new System.Windows.Forms.TextBox();
            this.panelParameters = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonShowParameters = new System.Windows.Forms.Button();
            this.useProjectionsBox = new System.Windows.Forms.CheckBox();
            this.log = new System.Windows.Forms.TextBox();
            this.panelMain.SuspendLayout();
            this.panelManual.SuspendLayout();
            this.panelAutomatic.SuspendLayout();
            this.panelInterpolation.SuspendLayout();
            this.panelParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpenFolder
            // 
            this.buttonOpenFolder.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonOpenFolder.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonOpenFolder.FlatAppearance.BorderSize = 2;
            this.buttonOpenFolder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonOpenFolder.Location = new System.Drawing.Point(23, 21);
            this.buttonOpenFolder.Name = "buttonOpenFolder";
            this.buttonOpenFolder.Size = new System.Drawing.Size(99, 40);
            this.buttonOpenFolder.TabIndex = 3;
            this.buttonOpenFolder.Text = "Открыть каталог";
            this.buttonOpenFolder.UseVisualStyleBackColor = false;
            this.buttonOpenFolder.Click += new System.EventHandler(this.openFolderButton_Click);
            // 
            // buttonOpenTXIN
            // 
            this.buttonOpenTXIN.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonOpenTXIN.Location = new System.Drawing.Point(3, 23);
            this.buttonOpenTXIN.Name = "buttonOpenTXIN";
            this.buttonOpenTXIN.Size = new System.Drawing.Size(138, 24);
            this.buttonOpenTXIN.TabIndex = 5;
            this.buttonOpenTXIN.Text = "Указать .in";
            this.buttonOpenTXIN.UseVisualStyleBackColor = false;
            this.buttonOpenTXIN.Click += new System.EventHandler(this.openInButton_Click);
            // 
            // buttonOpenSW
            // 
            this.buttonOpenSW.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonOpenSW.Location = new System.Drawing.Point(3, 53);
            this.buttonOpenSW.Name = "buttonOpenSW";
            this.buttonOpenSW.Size = new System.Drawing.Size(138, 24);
            this.buttonOpenSW.TabIndex = 5;
            this.buttonOpenSW.Text = "Указать SeisWide header";
            this.buttonOpenSW.UseVisualStyleBackColor = false;
            this.buttonOpenSW.Click += new System.EventHandler(this.openSWButton_Click);
            // 
            // buttonInterpolate
            // 
            this.buttonInterpolate.AutoSize = true;
            this.buttonInterpolate.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonInterpolate.Location = new System.Drawing.Point(148, 23);
            this.buttonInterpolate.Name = "buttonInterpolate";
            this.panelInterpolation.SetRowSpan(this.buttonInterpolate, 2);
            this.buttonInterpolate.Size = new System.Drawing.Size(135, 52);
            this.buttonInterpolate.TabIndex = 6;
            this.buttonInterpolate.Text = "Интерполировать все файлы привязки";
            this.buttonInterpolate.UseVisualStyleBackColor = false;
            this.buttonInterpolate.Click += new System.EventHandler(this.interpolateButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.panelInterpolation.SetColumnSpan(this.label5, 2);
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Шаг дискретизации времени";
            // 
            // t
            // 
            this.t.AutoPopDelay = 10000;
            this.t.InitialDelay = 500;
            this.t.ReshowDelay = 100;
            // 
            // buttonCorrectAll
            // 
            this.buttonCorrectAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCorrectAll.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelAutomatic.SetColumnSpan(this.buttonCorrectAll, 2);
            this.buttonCorrectAll.Location = new System.Drawing.Point(3, 23);
            this.buttonCorrectAll.Name = "buttonCorrectAll";
            this.buttonCorrectAll.Size = new System.Drawing.Size(280, 63);
            this.buttonCorrectAll.TabIndex = 11;
            this.buttonCorrectAll.Text = " Для каждой пары файлов \"заголовок - .in\" записать привязку к трассе";
            this.t.SetToolTip(this.buttonCorrectAll, resources.GetString("buttonCorrectAll.ToolTip"));
            this.buttonCorrectAll.UseVisualStyleBackColor = false;
            this.buttonCorrectAll.Click += new System.EventHandler(this.correctAllButton_Click);
            // 
            // buttonCorrect
            // 
            this.buttonCorrect.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonCorrect.Location = new System.Drawing.Point(3, 113);
            this.buttonCorrect.Name = "buttonCorrect";
            this.buttonCorrect.Size = new System.Drawing.Size(138, 48);
            this.buttonCorrect.TabIndex = 13;
            this.buttonCorrect.Text = "Записать привязку к трассе, взятую из заголовка SeisWide";
            this.t.SetToolTip(this.buttonCorrect, "Привязка времен файла корелляции (*.in) к трассам, взятым из заголовка.");
            this.buttonCorrect.UseVisualStyleBackColor = false;
            this.buttonCorrect.Click += new System.EventHandler(this.correct_Click);
            // 
            // buttonOpenSeiSee
            // 
            this.buttonOpenSeiSee.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonOpenSeiSee.Location = new System.Drawing.Point(3, 83);
            this.buttonOpenSeiSee.Name = "buttonOpenSeiSee";
            this.buttonOpenSeiSee.Size = new System.Drawing.Size(138, 24);
            this.buttonOpenSeiSee.TabIndex = 5;
            this.buttonOpenSeiSee.Text = "Указать SeiSee header";
            this.buttonOpenSeiSee.UseVisualStyleBackColor = false;
            this.buttonOpenSeiSee.Click += new System.EventHandler(this.openSeiSeeButton_Click);
            // 
            // panelMain
            // 
            this.panelMain.AutoSize = true;
            this.panelMain.ColumnCount = 6;
            this.panelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.panelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.25125F));
            this.panelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.74875F));
            this.panelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.panelMain.Controls.Add(this.buttonOpenFolder, 1, 1);
            this.panelMain.Controls.Add(this.pathToFolder, 2, 1);
            this.panelMain.Controls.Add(this.panelManual, 1, 2);
            this.panelMain.Controls.Add(this.panelAutomatic, 4, 2);
            this.panelMain.Controls.Add(this.label1, 4, 3);
            this.panelMain.Controls.Add(this.panelInterpolation, 4, 4);
            this.panelMain.Controls.Add(this.panelParameters, 1, 5);
            this.panelMain.Controls.Add(this.log, 1, 7);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.RowCount = 8;
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelMain.Size = new System.Drawing.Size(754, 432);
            this.panelMain.TabIndex = 14;
            // 
            // pathToFolder
            // 
            this.pathToFolder.AutoSize = true;
            this.pathToFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SeisWide_Surfer.Properties.Settings.Default, "Folder", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pathToFolder.Location = new System.Drawing.Point(129, 18);
            this.pathToFolder.Name = "pathToFolder";
            this.pathToFolder.Size = new System.Drawing.Size(110, 13);
            this.pathToFolder.TabIndex = 7;
            this.pathToFolder.Text = global::SeisWide_Surfer.Properties.Settings.Default.Folder;
            // 
            // panelManual
            // 
            this.panelManual.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelManual.ColumnCount = 2;
            this.panelMain.SetColumnSpan(this.panelManual, 2);
            this.panelManual.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            this.panelManual.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelManual.Controls.Add(this.buttonOpenSeiSee, 0, 3);
            this.panelManual.Controls.Add(this.buttonCorrect, 0, 4);
            this.panelManual.Controls.Add(this.buttonOpenSW, 0, 2);
            this.panelManual.Controls.Add(this.buttonOpenTXIN, 0, 1);
            this.panelManual.Controls.Add(this.label3, 0, 0);
            this.panelManual.Controls.Add(this.pathToTXIN, 1, 1);
            this.panelManual.Controls.Add(this.pathToSW, 1, 2);
            this.panelManual.Controls.Add(this.pathToSeiSee, 1, 3);
            this.panelManual.Location = new System.Drawing.Point(23, 67);
            this.panelManual.Name = "panelManual";
            this.panelManual.RowCount = 5;
            this.panelMain.SetRowSpan(this.panelManual, 3);
            this.panelManual.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelManual.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.panelManual.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.panelManual.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.panelManual.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.panelManual.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelManual.Size = new System.Drawing.Size(394, 178);
            this.panelManual.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.panelManual.SetColumnSpan(this.label3, 2);
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(388, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ручная обработка";
            // 
            // pathToTXIN
            // 
            this.pathToTXIN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathToTXIN.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SeisWide_Surfer.Properties.Settings.Default, "PathToIn", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pathToTXIN.Location = new System.Drawing.Point(147, 23);
            this.pathToTXIN.Name = "pathToTXIN";
            this.pathToTXIN.Size = new System.Drawing.Size(244, 20);
            this.pathToTXIN.TabIndex = 4;
            this.pathToTXIN.Text = global::SeisWide_Surfer.Properties.Settings.Default.PathToIn;
            // 
            // pathToSW
            // 
            this.pathToSW.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathToSW.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SeisWide_Surfer.Properties.Settings.Default, "PathToSW", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pathToSW.Location = new System.Drawing.Point(147, 53);
            this.pathToSW.Name = "pathToSW";
            this.pathToSW.Size = new System.Drawing.Size(244, 20);
            this.pathToSW.TabIndex = 4;
            this.pathToSW.Text = global::SeisWide_Surfer.Properties.Settings.Default.PathToSW;
            // 
            // pathToSeiSee
            // 
            this.pathToSeiSee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathToSeiSee.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SeisWide_Surfer.Properties.Settings.Default, "PathToSeiSee", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pathToSeiSee.Location = new System.Drawing.Point(147, 83);
            this.pathToSeiSee.Name = "pathToSeiSee";
            this.pathToSeiSee.Size = new System.Drawing.Size(244, 20);
            this.pathToSeiSee.TabIndex = 4;
            this.pathToSeiSee.Text = global::SeisWide_Surfer.Properties.Settings.Default.PathToSeiSee;
            // 
            // panelAutomatic
            // 
            this.panelAutomatic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAutomatic.ColumnCount = 2;
            this.panelAutomatic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.82051F));
            this.panelAutomatic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.17949F));
            this.panelAutomatic.Controls.Add(this.label2, 0, 0);
            this.panelAutomatic.Controls.Add(this.buttonCorrectAll, 0, 1);
            this.panelAutomatic.Location = new System.Drawing.Point(443, 67);
            this.panelAutomatic.Name = "panelAutomatic";
            this.panelAutomatic.RowCount = 2;
            this.panelAutomatic.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelAutomatic.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.panelAutomatic.Size = new System.Drawing.Size(286, 89);
            this.panelAutomatic.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.panelAutomatic.SetColumnSpan(this.label2, 2);
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(280, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Автоматическая обработка";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(443, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Интерполяция";
            // 
            // panelInterpolation
            // 
            this.panelInterpolation.ColumnCount = 2;
            this.panelInterpolation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.70922F));
            this.panelInterpolation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.29078F));
            this.panelInterpolation.Controls.Add(this.label5, 0, 0);
            this.panelInterpolation.Controls.Add(this.buttonInterpolate, 1, 1);
            this.panelInterpolation.Controls.Add(this.singleCheckBox, 0, 2);
            this.panelInterpolation.Controls.Add(this.deltaTextBox, 0, 1);
            this.panelInterpolation.Location = new System.Drawing.Point(443, 182);
            this.panelInterpolation.Name = "panelInterpolation";
            this.panelInterpolation.RowCount = 3;
            this.panelMain.SetRowSpan(this.panelInterpolation, 2);
            this.panelInterpolation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelInterpolation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelInterpolation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.panelInterpolation.Size = new System.Drawing.Size(286, 97);
            this.panelInterpolation.TabIndex = 16;
            // 
            // singleCheckBox
            // 
            this.singleCheckBox.AutoSize = true;
            this.singleCheckBox.Checked = global::SeisWide_Surfer.Properties.Settings.Default.SingleFile;
            this.singleCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.singleCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SeisWide_Surfer.Properties.Settings.Default, "SingleFile", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.singleCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.singleCheckBox.Location = new System.Drawing.Point(3, 62);
            this.singleCheckBox.Name = "singleCheckBox";
            this.singleCheckBox.Size = new System.Drawing.Size(135, 20);
            this.singleCheckBox.TabIndex = 12;
            this.singleCheckBox.Text = "Всё в один файл";
            this.singleCheckBox.UseVisualStyleBackColor = true;
            // 
            // deltaTextBox
            // 
            this.deltaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SeisWide_Surfer.Properties.Settings.Default, "Delta", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.deltaTextBox.Location = new System.Drawing.Point(3, 23);
            this.deltaTextBox.Name = "deltaTextBox";
            this.deltaTextBox.Size = new System.Drawing.Size(100, 20);
            this.deltaTextBox.TabIndex = 9;
            this.deltaTextBox.Text = global::SeisWide_Surfer.Properties.Settings.Default.Delta;
            // 
            // panelParameters
            // 
            this.panelParameters.AutoSize = true;
            this.panelParameters.ColumnCount = 1;
            this.panelMain.SetColumnSpan(this.panelParameters, 2);
            this.panelParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelParameters.Controls.Add(this.label4, 0, 0);
            this.panelParameters.Controls.Add(this.buttonShowParameters, 0, 2);
            this.panelParameters.Controls.Add(this.useProjectionsBox, 0, 1);
            this.panelParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelParameters.Location = new System.Drawing.Point(23, 251);
            this.panelParameters.Name = "panelParameters";
            this.panelParameters.RowCount = 3;
            this.panelParameters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelParameters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelParameters.Size = new System.Drawing.Size(394, 89);
            this.panelParameters.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(252, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Параметры створа профиля";
            // 
            // buttonShowParameters
            // 
            this.buttonShowParameters.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonShowParameters.Location = new System.Drawing.Point(3, 46);
            this.buttonShowParameters.Name = "buttonShowParameters";
            this.buttonShowParameters.Size = new System.Drawing.Size(150, 40);
            this.buttonShowParameters.TabIndex = 12;
            this.buttonShowParameters.Text = "Показать параметры створа профиля";
            this.buttonShowParameters.UseVisualStyleBackColor = false;
            this.buttonShowParameters.Click += new System.EventHandler(this.showParameters_Click);
            // 
            // useProjectionsBox
            // 
            this.useProjectionsBox.AutoSize = true;
            this.useProjectionsBox.Checked = global::SeisWide_Surfer.Properties.Settings.Default.UseSeiSeeHeader;
            this.useProjectionsBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SeisWide_Surfer.Properties.Settings.Default, "UseSeiSeeHeader", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.useProjectionsBox.Location = new System.Drawing.Point(3, 23);
            this.useProjectionsBox.Name = "useProjectionsBox";
            this.useProjectionsBox.Size = new System.Drawing.Size(231, 17);
            this.useProjectionsBox.TabIndex = 14;
            this.useProjectionsBox.Text = "Вычислять проекцию на линию профиля";
            this.useProjectionsBox.UseVisualStyleBackColor = true;
            this.useProjectionsBox.CheckedChanged += new System.EventHandler(this.useSeiSeeCheckBox_CheckedChanged);
            // 
            // log
            // 
            this.log.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelMain.SetColumnSpan(this.log, 4);
            this.log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.log.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.log.Location = new System.Drawing.Point(23, 366);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log.Size = new System.Drawing.Size(706, 63);
            this.log.TabIndex = 18;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(755, 432);
            this.Controls.Add(this.panelMain);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Дружим SeisWide и Surfer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelManual.ResumeLayout(false);
            this.panelManual.PerformLayout();
            this.panelAutomatic.ResumeLayout(false);
            this.panelAutomatic.PerformLayout();
            this.panelInterpolation.ResumeLayout(false);
            this.panelInterpolation.PerformLayout();
            this.panelParameters.ResumeLayout(false);
            this.panelParameters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenFolder;
        private System.Windows.Forms.Button buttonOpenTXIN;
        private System.Windows.Forms.TextBox pathToTXIN;
        private System.Windows.Forms.TextBox pathToSW;
        private System.Windows.Forms.Button buttonOpenSW;
        private System.Windows.Forms.Button buttonInterpolate;
        private System.Windows.Forms.TextBox deltaTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonCorrectAll;
        private System.Windows.Forms.CheckBox singleCheckBox;
        private System.Windows.Forms.Button buttonCorrect;
        private ToolTip t;
        private TextBox pathToSeiSee;
        private Button buttonOpenSeiSee;
        private TableLayoutPanel panelMain;
        private TableLayoutPanel panelAutomatic;
        private Label label2;
        private TableLayoutPanel panelManual;
        public Label pathToFolder;
        private CheckBox useProjectionsBox;
        private Label label3;
        private Button buttonShowParameters;
        private TableLayoutPanel panelInterpolation;
        private Label label1;
        private TableLayoutPanel panelParameters;
        private Label label4;
        private TextBox log;
    }
}

