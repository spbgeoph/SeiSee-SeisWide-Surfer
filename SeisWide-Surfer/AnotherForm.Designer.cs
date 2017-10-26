namespace SeisWide_Surfer
{
    partial class AnotherForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnotherForm));
            this.panelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelWorkspace = new System.Windows.Forms.TableLayoutPanel();
            this.pathToFolder = new System.Windows.Forms.Label();
            this.buttonSetWorkspace = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabFirstEntry = new System.Windows.Forms.TabPage();
            this.panel_fe = new System.Windows.Forms.TableLayoutPanel();
            this.panel_fe_Checks = new System.Windows.Forms.TableLayoutPanel();
            this.check_fe_Bind = new System.Windows.Forms.CheckBox();
            this.check_fe_Project = new System.Windows.Forms.CheckBox();
            this.check_fe_SplitHodographs = new System.Windows.Forms.CheckBox();
            this.check_fe_Interpolate = new System.Windows.Forms.CheckBox();
            this.panel_fe_Aux = new System.Windows.Forms.TableLayoutPanel();
            this.buttonShowParameters = new System.Windows.Forms.Button();
            this.deltaTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_fe_BindWaves = new System.Windows.Forms.Button();
            this.button_fe_BindUncertainy = new System.Windows.Forms.Button();
            this.button_fe_Process = new System.Windows.Forms.Button();
            this.tabReflected = new System.Windows.Forms.TabPage();
            this.panel_refl = new System.Windows.Forms.TableLayoutPanel();
            this.panel_refl_FirstStage = new System.Windows.Forms.TableLayoutPanel();
            this.labelFirstStage = new System.Windows.Forms.Label();
            this.check_refl_Bind = new System.Windows.Forms.CheckBox();
            this.check_refl_Square = new System.Windows.Forms.CheckBox();
            this.button_refl_FirstStage = new System.Windows.Forms.Button();
            this.label_refl_Wave = new System.Windows.Forms.Label();
            this.textBox_refl_Wave = new System.Windows.Forms.TextBox();
            this.panel_refl_SecondStage = new System.Windows.Forms.TableLayoutPanel();
            this.label_refl_Contours = new System.Windows.Forms.Label();
            this.labelSecondStage = new System.Windows.Forms.Label();
            this.button_refl_SecondStage = new System.Windows.Forms.Button();
            this.textBox_refl_Contours = new System.Windows.Forms.TextBox();
            this.check_refl_SoundCenters = new System.Windows.Forms.CheckBox();
            this.check_refl_Pyramid = new System.Windows.Forms.CheckBox();
            this.check_refl_MeanVelocity = new System.Windows.Forms.CheckBox();
            this.button_refl_SelectContours = new System.Windows.Forms.Button();
            this.log = new System.Windows.Forms.TextBox();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panelMain.SuspendLayout();
            this.panelWorkspace.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabFirstEntry.SuspendLayout();
            this.panel_fe.SuspendLayout();
            this.panel_fe_Checks.SuspendLayout();
            this.panel_fe_Aux.SuspendLayout();
            this.tabReflected.SuspendLayout();
            this.panel_refl.SuspendLayout();
            this.panel_refl_FirstStage.SuspendLayout();
            this.panel_refl_SecondStage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.ColumnCount = 1;
            this.panelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelMain.Controls.Add(this.panelWorkspace, 0, 0);
            this.panelMain.Controls.Add(this.tabControl, 0, 1);
            this.panelMain.Controls.Add(this.log, 0, 2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(5, 5);
            this.panelMain.Name = "panelMain";
            this.panelMain.RowCount = 3;
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.04762F));
            this.panelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.95238F));
            this.panelMain.Size = new System.Drawing.Size(737, 539);
            this.panelMain.TabIndex = 0;
            // 
            // panelWorkspace
            // 
            this.panelWorkspace.AutoSize = true;
            this.panelWorkspace.ColumnCount = 2;
            this.panelWorkspace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelWorkspace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelWorkspace.Controls.Add(this.pathToFolder, 1, 0);
            this.panelWorkspace.Controls.Add(this.buttonSetWorkspace, 0, 0);
            this.panelWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWorkspace.Location = new System.Drawing.Point(3, 3);
            this.panelWorkspace.Name = "panelWorkspace";
            this.panelWorkspace.RowCount = 1;
            this.panelWorkspace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelWorkspace.Size = new System.Drawing.Size(731, 29);
            this.panelWorkspace.TabIndex = 1;
            // 
            // pathToFolder
            // 
            this.pathToFolder.AutoSize = true;
            this.pathToFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SeisWide_Surfer.Properties.Settings.Default, "Folder", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pathToFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pathToFolder.Location = new System.Drawing.Point(84, 0);
            this.pathToFolder.Name = "pathToFolder";
            this.pathToFolder.Size = new System.Drawing.Size(157, 13);
            this.pathToFolder.TabIndex = 8;
            this.pathToFolder.Text = global::SeisWide_Surfer.Properties.Settings.Default.Folder;
            // 
            // buttonSetWorkspace
            // 
            this.buttonSetWorkspace.Location = new System.Drawing.Point(3, 3);
            this.buttonSetWorkspace.Name = "buttonSetWorkspace";
            this.buttonSetWorkspace.Size = new System.Drawing.Size(75, 23);
            this.buttonSetWorkspace.TabIndex = 0;
            this.buttonSetWorkspace.Text = "Указать...";
            this.buttonSetWorkspace.UseVisualStyleBackColor = true;
            this.buttonSetWorkspace.Click += new System.EventHandler(this.buttonSetWorkspace_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabFirstEntry);
            this.tabControl.Controls.Add(this.tabReflected);
            this.tabControl.Location = new System.Drawing.Point(3, 38);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(731, 342);
            this.tabControl.TabIndex = 2;
            // 
            // tabFirstEntry
            // 
            this.tabFirstEntry.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabFirstEntry.Controls.Add(this.panel_fe);
            this.tabFirstEntry.Location = new System.Drawing.Point(4, 22);
            this.tabFirstEntry.Name = "tabFirstEntry";
            this.tabFirstEntry.Padding = new System.Windows.Forms.Padding(3);
            this.tabFirstEntry.Size = new System.Drawing.Size(723, 316);
            this.tabFirstEntry.TabIndex = 0;
            this.tabFirstEntry.Text = "Первое вступление";
            // 
            // panel_fe
            // 
            this.panel_fe.BackColor = System.Drawing.SystemColors.Menu;
            this.panel_fe.ColumnCount = 2;
            this.panel_fe.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.panel_fe.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.panel_fe.Controls.Add(this.panel_fe_Checks, 0, 0);
            this.panel_fe.Controls.Add(this.panel_fe_Aux, 1, 0);
            this.panel_fe.Controls.Add(this.button_fe_Process, 0, 1);
            this.panel_fe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_fe.Location = new System.Drawing.Point(3, 3);
            this.panel_fe.Name = "panel_fe";
            this.panel_fe.RowCount = 2;
            this.panel_fe.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel_fe.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_fe.Size = new System.Drawing.Size(717, 310);
            this.panel_fe.TabIndex = 0;
            // 
            // panel_fe_Checks
            // 
            this.panel_fe_Checks.ColumnCount = 1;
            this.panel_fe_Checks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel_fe_Checks.Controls.Add(this.check_fe_Bind, 0, 0);
            this.panel_fe_Checks.Controls.Add(this.check_fe_Project, 0, 1);
            this.panel_fe_Checks.Controls.Add(this.check_fe_SplitHodographs, 0, 2);
            this.panel_fe_Checks.Controls.Add(this.check_fe_Interpolate, 0, 3);
            this.panel_fe_Checks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_fe_Checks.Location = new System.Drawing.Point(3, 3);
            this.panel_fe_Checks.Name = "panel_fe_Checks";
            this.panel_fe_Checks.RowCount = 4;
            this.panel_fe_Checks.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_fe_Checks.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_fe_Checks.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_fe_Checks.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_fe_Checks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel_fe_Checks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel_fe_Checks.Size = new System.Drawing.Size(388, 261);
            this.panel_fe_Checks.TabIndex = 2;
            // 
            // check_fe_Bind
            // 
            this.check_fe_Bind.AutoSize = true;
            this.check_fe_Bind.Checked = global::SeisWide_Surfer.Properties.Settings.Default.fe_Bind;
            this.check_fe_Bind.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SeisWide_Surfer.Properties.Settings.Default, "fe_Bind", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.check_fe_Bind.Location = new System.Drawing.Point(3, 3);
            this.check_fe_Bind.Name = "check_fe_Bind";
            this.check_fe_Bind.Size = new System.Drawing.Size(137, 17);
            this.check_fe_Bind.TabIndex = 0;
            this.check_fe_Bind.Text = "Произвести привязку";
            this.toolTip.SetToolTip(this.check_fe_Bind, "Производит привязку к номеру трассы значений прокоррелированных времён.");
            this.check_fe_Bind.UseVisualStyleBackColor = true;
            this.check_fe_Bind.CheckedChanged += new System.EventHandler(this.check_fe_Bind_CheckedChanged);
            // 
            // check_fe_Project
            // 
            this.check_fe_Project.AutoSize = true;
            this.check_fe_Project.Location = new System.Drawing.Point(3, 26);
            this.check_fe_Project.Name = "check_fe_Project";
            this.check_fe_Project.Size = new System.Drawing.Size(130, 17);
            this.check_fe_Project.TabIndex = 0;
            this.check_fe_Project.Text = "Посчитать проекции";
            this.toolTip.SetToolTip(this.check_fe_Project, "Рассчитывает проекции пунктов приема на линию профиля. \r\nЭтот шаг требует предвар" +
        "ительно произвести привязку.");
            this.check_fe_Project.UseVisualStyleBackColor = true;
            this.check_fe_Project.CheckedChanged += new System.EventHandler(this.check_fe_Project_CheckedChanged);
            // 
            // check_fe_SplitHodographs
            // 
            this.check_fe_SplitHodographs.AutoSize = true;
            this.check_fe_SplitHodographs.Location = new System.Drawing.Point(3, 49);
            this.check_fe_SplitHodographs.Name = "check_fe_SplitHodographs";
            this.check_fe_SplitHodographs.Size = new System.Drawing.Size(314, 17);
            this.check_fe_SplitHodographs.TabIndex = 0;
            this.check_fe_SplitHodographs.Text = "Разделить годографы; вычислить центры зондирования";
            this.toolTip.SetToolTip(this.check_fe_SplitHodographs, resources.GetString("check_fe_SplitHodographs.ToolTip"));
            this.check_fe_SplitHodographs.UseVisualStyleBackColor = true;
            this.check_fe_SplitHodographs.CheckedChanged += new System.EventHandler(this.check_fe_SplitHodographs_CheckedChanged);
            // 
            // check_fe_Interpolate
            // 
            this.check_fe_Interpolate.AutoSize = true;
            this.check_fe_Interpolate.Location = new System.Drawing.Point(3, 72);
            this.check_fe_Interpolate.Name = "check_fe_Interpolate";
            this.check_fe_Interpolate.Size = new System.Drawing.Size(134, 17);
            this.check_fe_Interpolate.TabIndex = 0;
            this.check_fe_Interpolate.Text = "Проинтерполировать";
            this.toolTip.SetToolTip(this.check_fe_Interpolate, "Интерполирует привязанные данные с заданным шагом по времени.\r\nЕсли также выбран " +
        "расчёт проекций, то интерполяция проводится по проектированным данным.");
            this.check_fe_Interpolate.UseVisualStyleBackColor = true;
            this.check_fe_Interpolate.CheckedChanged += new System.EventHandler(this.check_fe_Interpolate_CheckedChanged);
            // 
            // panel_fe_Aux
            // 
            this.panel_fe_Aux.ColumnCount = 1;
            this.panel_fe_Aux.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel_fe_Aux.Controls.Add(this.buttonShowParameters, 0, 2);
            this.panel_fe_Aux.Controls.Add(this.deltaTextBox, 0, 1);
            this.panel_fe_Aux.Controls.Add(this.label5, 0, 0);
            this.panel_fe_Aux.Controls.Add(this.button_fe_BindWaves, 0, 4);
            this.panel_fe_Aux.Controls.Add(this.button_fe_BindUncertainy, 0, 6);
            this.panel_fe_Aux.Location = new System.Drawing.Point(397, 3);
            this.panel_fe_Aux.Name = "panel_fe_Aux";
            this.panel_fe_Aux.RowCount = 7;
            this.panel_fe_Aux.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_fe_Aux.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_fe_Aux.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_fe_Aux.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.panel_fe_Aux.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_fe_Aux.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel_fe_Aux.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel_fe_Aux.Size = new System.Drawing.Size(200, 225);
            this.panel_fe_Aux.TabIndex = 3;
            // 
            // buttonShowParameters
            // 
            this.buttonShowParameters.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonShowParameters.Location = new System.Drawing.Point(3, 42);
            this.buttonShowParameters.Name = "buttonShowParameters";
            this.buttonShowParameters.Size = new System.Drawing.Size(150, 39);
            this.buttonShowParameters.TabIndex = 13;
            this.buttonShowParameters.Text = "Показать параметры створа профиля";
            this.buttonShowParameters.UseVisualStyleBackColor = false;
            this.buttonShowParameters.Click += new System.EventHandler(this.buttonShowParameters_Click);
            // 
            // deltaTextBox
            // 
            this.deltaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SeisWide_Surfer.Properties.Settings.Default, "Delta", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.deltaTextBox.Location = new System.Drawing.Point(3, 16);
            this.deltaTextBox.Name = "deltaTextBox";
            this.deltaTextBox.Size = new System.Drawing.Size(100, 20);
            this.deltaTextBox.TabIndex = 12;
            this.deltaTextBox.Text = global::SeisWide_Surfer.Properties.Settings.Default.Delta;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Шаг дискретизации времени";
            // 
            // button_fe_BindWaves
            // 
            this.button_fe_BindWaves.Location = new System.Drawing.Point(3, 108);
            this.button_fe_BindWaves.Name = "button_fe_BindWaves";
            this.button_fe_BindWaves.Size = new System.Drawing.Size(150, 39);
            this.button_fe_BindWaves.TabIndex = 14;
            this.button_fe_BindWaves.Text = "Привязать по волнам";
            this.button_fe_BindWaves.UseVisualStyleBackColor = true;
            this.button_fe_BindWaves.Click += new System.EventHandler(this.button_fe_BindWaves_Click);
            // 
            // button_fe_BindUncertainy
            // 
            this.button_fe_BindUncertainy.Location = new System.Drawing.Point(3, 173);
            this.button_fe_BindUncertainy.Name = "button_fe_BindUncertainy";
            this.button_fe_BindUncertainy.Size = new System.Drawing.Size(150, 49);
            this.button_fe_BindUncertainy.TabIndex = 14;
            this.button_fe_BindUncertainy.Text = "Установить уровень погрешности в файлах корелляции";
            this.button_fe_BindUncertainy.UseVisualStyleBackColor = true;
            this.button_fe_BindUncertainy.Click += new System.EventHandler(this.button_fe_BindUncertainy_Click);
            // 
            // button_fe_Process
            // 
            this.button_fe_Process.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_fe_Process.Location = new System.Drawing.Point(3, 270);
            this.button_fe_Process.Name = "button_fe_Process";
            this.button_fe_Process.Size = new System.Drawing.Size(119, 37);
            this.button_fe_Process.TabIndex = 1;
            this.button_fe_Process.Text = "Обработать первое вступление";
            this.button_fe_Process.UseVisualStyleBackColor = true;
            this.button_fe_Process.Click += new System.EventHandler(this.buttonProcessFirstEntry_Click);
            // 
            // tabReflected
            // 
            this.tabReflected.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabReflected.Controls.Add(this.panel_refl);
            this.tabReflected.Location = new System.Drawing.Point(4, 22);
            this.tabReflected.Name = "tabReflected";
            this.tabReflected.Padding = new System.Windows.Forms.Padding(3);
            this.tabReflected.Size = new System.Drawing.Size(723, 316);
            this.tabReflected.TabIndex = 1;
            this.tabReflected.Text = "Отраженная волна";
            // 
            // panel_refl
            // 
            this.panel_refl.ColumnCount = 1;
            this.panel_refl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel_refl.Controls.Add(this.panel_refl_FirstStage, 0, 0);
            this.panel_refl.Controls.Add(this.panel_refl_SecondStage, 0, 1);
            this.panel_refl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_refl.Location = new System.Drawing.Point(3, 3);
            this.panel_refl.Name = "panel_refl";
            this.panel_refl.RowCount = 2;
            this.panel_refl.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_refl.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_refl.Size = new System.Drawing.Size(717, 310);
            this.panel_refl.TabIndex = 1;
            // 
            // panel_refl_FirstStage
            // 
            this.panel_refl_FirstStage.BackColor = System.Drawing.SystemColors.Menu;
            this.panel_refl_FirstStage.ColumnCount = 2;
            this.panel_refl_FirstStage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.panel_refl_FirstStage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.panel_refl_FirstStage.Controls.Add(this.labelFirstStage, 0, 0);
            this.panel_refl_FirstStage.Controls.Add(this.check_refl_Bind, 0, 1);
            this.panel_refl_FirstStage.Controls.Add(this.check_refl_Square, 0, 2);
            this.panel_refl_FirstStage.Controls.Add(this.button_refl_FirstStage, 0, 4);
            this.panel_refl_FirstStage.Controls.Add(this.label_refl_Wave, 1, 0);
            this.panel_refl_FirstStage.Controls.Add(this.textBox_refl_Wave, 1, 1);
            this.panel_refl_FirstStage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_refl_FirstStage.Location = new System.Drawing.Point(0, 0);
            this.panel_refl_FirstStage.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel_refl_FirstStage.Name = "panel_refl_FirstStage";
            this.panel_refl_FirstStage.RowCount = 5;
            this.panel_refl_FirstStage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.panel_refl_FirstStage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_refl_FirstStage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_refl_FirstStage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel_refl_FirstStage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_refl_FirstStage.Size = new System.Drawing.Size(717, 137);
            this.panel_refl_FirstStage.TabIndex = 0;
            // 
            // labelFirstStage
            // 
            this.labelFirstStage.AutoSize = true;
            this.labelFirstStage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFirstStage.Location = new System.Drawing.Point(3, 0);
            this.labelFirstStage.Name = "labelFirstStage";
            this.labelFirstStage.Size = new System.Drawing.Size(105, 16);
            this.labelFirstStage.TabIndex = 0;
            this.labelFirstStage.Text = "Первая стадия";
            // 
            // check_refl_Bind
            // 
            this.check_refl_Bind.AutoSize = true;
            this.check_refl_Bind.Checked = global::SeisWide_Surfer.Properties.Settings.Default.refl_bind;
            this.check_refl_Bind.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SeisWide_Surfer.Properties.Settings.Default, "refl_bind", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.check_refl_Bind.Location = new System.Drawing.Point(3, 28);
            this.check_refl_Bind.Name = "check_refl_Bind";
            this.check_refl_Bind.Size = new System.Drawing.Size(137, 17);
            this.check_refl_Bind.TabIndex = 1;
            this.check_refl_Bind.Text = "Произвести привязку";
            this.check_refl_Bind.UseVisualStyleBackColor = true;
            this.check_refl_Bind.CheckedChanged += new System.EventHandler(this.check_refl_Bind_CheckedChanged);
            // 
            // check_refl_Square
            // 
            this.check_refl_Square.AutoSize = true;
            this.check_refl_Square.Location = new System.Drawing.Point(3, 54);
            this.check_refl_Square.Name = "check_refl_Square";
            this.check_refl_Square.Size = new System.Drawing.Size(209, 17);
            this.check_refl_Square.TabIndex = 2;
            this.check_refl_Square.Text = "Рассчитать квадратичные удаления";
            this.check_refl_Square.UseVisualStyleBackColor = true;
            this.check_refl_Square.CheckedChanged += new System.EventHandler(this.check_refl_Square_CheckedChanged);
            // 
            // button_refl_FirstStage
            // 
            this.button_refl_FirstStage.Location = new System.Drawing.Point(3, 97);
            this.button_refl_FirstStage.Name = "button_refl_FirstStage";
            this.button_refl_FirstStage.Size = new System.Drawing.Size(119, 37);
            this.button_refl_FirstStage.TabIndex = 15;
            this.button_refl_FirstStage.Text = "Посчитать";
            this.button_refl_FirstStage.UseVisualStyleBackColor = true;
            this.button_refl_FirstStage.Click += new System.EventHandler(this.button_refl_FirstStage_Click);
            // 
            // label_refl_Wave
            // 
            this.label_refl_Wave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_refl_Wave.AutoSize = true;
            this.label_refl_Wave.Location = new System.Drawing.Point(397, 12);
            this.label_refl_Wave.Name = "label_refl_Wave";
            this.label_refl_Wave.Size = new System.Drawing.Size(140, 13);
            this.label_refl_Wave.TabIndex = 13;
            this.label_refl_Wave.Text = "Номер отраженной волны";
            // 
            // textBox_refl_Wave
            // 
            this.textBox_refl_Wave.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SeisWide_Surfer.Properties.Settings.Default, "ReflWave", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_refl_Wave.Location = new System.Drawing.Point(397, 28);
            this.textBox_refl_Wave.Name = "textBox_refl_Wave";
            this.textBox_refl_Wave.Size = new System.Drawing.Size(100, 20);
            this.textBox_refl_Wave.TabIndex = 14;
            this.textBox_refl_Wave.Text = global::SeisWide_Surfer.Properties.Settings.Default.ReflWave;
            // 
            // panel_refl_SecondStage
            // 
            this.panel_refl_SecondStage.BackColor = System.Drawing.SystemColors.Menu;
            this.panel_refl_SecondStage.ColumnCount = 2;
            this.panel_refl_SecondStage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.panel_refl_SecondStage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.panel_refl_SecondStage.Controls.Add(this.label_refl_Contours, 1, 0);
            this.panel_refl_SecondStage.Controls.Add(this.labelSecondStage, 0, 0);
            this.panel_refl_SecondStage.Controls.Add(this.button_refl_SecondStage, 0, 5);
            this.panel_refl_SecondStage.Controls.Add(this.textBox_refl_Contours, 1, 1);
            this.panel_refl_SecondStage.Controls.Add(this.check_refl_SoundCenters, 0, 3);
            this.panel_refl_SecondStage.Controls.Add(this.check_refl_Pyramid, 0, 2);
            this.panel_refl_SecondStage.Controls.Add(this.check_refl_MeanVelocity, 0, 1);
            this.panel_refl_SecondStage.Controls.Add(this.button_refl_SelectContours, 1, 2);
            this.panel_refl_SecondStage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_refl_SecondStage.Location = new System.Drawing.Point(0, 143);
            this.panel_refl_SecondStage.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel_refl_SecondStage.Name = "panel_refl_SecondStage";
            this.panel_refl_SecondStage.RowCount = 6;
            this.panel_refl_SecondStage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.panel_refl_SecondStage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_refl_SecondStage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_refl_SecondStage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_refl_SecondStage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel_refl_SecondStage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel_refl_SecondStage.Size = new System.Drawing.Size(717, 167);
            this.panel_refl_SecondStage.TabIndex = 1;
            // 
            // label_refl_Contours
            // 
            this.label_refl_Contours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_refl_Contours.AutoSize = true;
            this.label_refl_Contours.Location = new System.Drawing.Point(397, 12);
            this.label_refl_Contours.Name = "label_refl_Contours";
            this.label_refl_Contours.Size = new System.Drawing.Size(148, 13);
            this.label_refl_Contours.TabIndex = 14;
            this.label_refl_Contours.Text = "Путь к файлу с изолиниями";
            // 
            // labelSecondStage
            // 
            this.labelSecondStage.AutoSize = true;
            this.labelSecondStage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSecondStage.Location = new System.Drawing.Point(3, 0);
            this.labelSecondStage.Name = "labelSecondStage";
            this.labelSecondStage.Size = new System.Drawing.Size(103, 16);
            this.labelSecondStage.TabIndex = 0;
            this.labelSecondStage.Text = "Вторая стадия";
            // 
            // button_refl_SecondStage
            // 
            this.button_refl_SecondStage.Location = new System.Drawing.Point(3, 120);
            this.button_refl_SecondStage.Name = "button_refl_SecondStage";
            this.button_refl_SecondStage.Size = new System.Drawing.Size(119, 37);
            this.button_refl_SecondStage.TabIndex = 4;
            this.button_refl_SecondStage.Text = "Обработать изолинии";
            this.button_refl_SecondStage.UseVisualStyleBackColor = true;
            this.button_refl_SecondStage.Click += new System.EventHandler(this.button_refl_SecondStage_Click);
            // 
            // textBox_refl_Contours
            // 
            this.textBox_refl_Contours.AllowDrop = true;
            this.textBox_refl_Contours.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SeisWide_Surfer.Properties.Settings.Default, "path_contours", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox_refl_Contours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_refl_Contours.Location = new System.Drawing.Point(397, 28);
            this.textBox_refl_Contours.Name = "textBox_refl_Contours";
            this.textBox_refl_Contours.Size = new System.Drawing.Size(317, 20);
            this.textBox_refl_Contours.TabIndex = 1;
            this.textBox_refl_Contours.Text = global::SeisWide_Surfer.Properties.Settings.Default.path_contours;
            // 
            // check_refl_SoundCenters
            // 
            this.check_refl_SoundCenters.AutoSize = true;
            this.check_refl_SoundCenters.Location = new System.Drawing.Point(3, 77);
            this.check_refl_SoundCenters.Name = "check_refl_SoundCenters";
            this.check_refl_SoundCenters.Size = new System.Drawing.Size(158, 17);
            this.check_refl_SoundCenters.TabIndex = 3;
            this.check_refl_SoundCenters.Text = "На центрах зондирования";
            this.check_refl_SoundCenters.UseVisualStyleBackColor = true;
            this.check_refl_SoundCenters.Visible = false;
            this.check_refl_SoundCenters.CheckedChanged += new System.EventHandler(this.check_refl_SoundCenters_CheckedChanged);
            // 
            // check_refl_Pyramid
            // 
            this.check_refl_Pyramid.AutoSize = true;
            this.check_refl_Pyramid.Location = new System.Drawing.Point(3, 54);
            this.check_refl_Pyramid.Name = "check_refl_Pyramid";
            this.check_refl_Pyramid.Size = new System.Drawing.Size(232, 17);
            this.check_refl_Pyramid.TabIndex = 2;
            this.check_refl_Pyramid.Text = "Вывести \"пирамиду\" удалений и времён";
            this.check_refl_Pyramid.UseVisualStyleBackColor = true;
            this.check_refl_Pyramid.CheckedChanged += new System.EventHandler(this.check_refl_Pyramid_CheckedChanged);
            // 
            // check_refl_MeanVelocity
            // 
            this.check_refl_MeanVelocity.AutoSize = true;
            this.check_refl_MeanVelocity.Checked = global::SeisWide_Surfer.Properties.Settings.Default.refl_MeanVelocity;
            this.check_refl_MeanVelocity.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SeisWide_Surfer.Properties.Settings.Default, "refl_MeanVelocity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.check_refl_MeanVelocity.Location = new System.Drawing.Point(3, 28);
            this.check_refl_MeanVelocity.Name = "check_refl_MeanVelocity";
            this.check_refl_MeanVelocity.Size = new System.Drawing.Size(233, 17);
            this.check_refl_MeanVelocity.TabIndex = 2;
            this.check_refl_MeanVelocity.Text = "Рассчитать средние скорости и глубины";
            this.check_refl_MeanVelocity.UseVisualStyleBackColor = true;
            this.check_refl_MeanVelocity.CheckedChanged += new System.EventHandler(this.check_refl_MeanVelocity_CheckedChanged);
            // 
            // button_refl_SelectContours
            // 
            this.button_refl_SelectContours.Location = new System.Drawing.Point(397, 54);
            this.button_refl_SelectContours.Name = "button_refl_SelectContours";
            this.panel_refl_SecondStage.SetRowSpan(this.button_refl_SelectContours, 2);
            this.button_refl_SelectContours.Size = new System.Drawing.Size(75, 25);
            this.button_refl_SelectContours.TabIndex = 15;
            this.button_refl_SelectContours.Text = "Выбрать...";
            this.button_refl_SelectContours.UseVisualStyleBackColor = true;
            this.button_refl_SelectContours.Click += new System.EventHandler(this.button_refl_SelectContours_Click);
            // 
            // log
            // 
            this.log.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelMain.SetColumnSpan(this.log, 4);
            this.log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.log.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.log.Location = new System.Drawing.Point(3, 386);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log.Size = new System.Drawing.Size(731, 150);
            this.log.TabIndex = 19;
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // AnotherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(747, 549);
            this.Controls.Add(this.panelMain);
            this.Name = "AnotherForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Построение поля времен";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AnotherForm_FormClosing);
            this.Load += new System.EventHandler(this.AnotherForm_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelWorkspace.ResumeLayout(false);
            this.panelWorkspace.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabFirstEntry.ResumeLayout(false);
            this.panel_fe.ResumeLayout(false);
            this.panel_fe_Checks.ResumeLayout(false);
            this.panel_fe_Checks.PerformLayout();
            this.panel_fe_Aux.ResumeLayout(false);
            this.panel_fe_Aux.PerformLayout();
            this.tabReflected.ResumeLayout(false);
            this.panel_refl.ResumeLayout(false);
            this.panel_refl_FirstStage.ResumeLayout(false);
            this.panel_refl_FirstStage.PerformLayout();
            this.panel_refl_SecondStage.ResumeLayout(false);
            this.panel_refl_SecondStage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel panelMain;
        private System.Windows.Forms.Button buttonSetWorkspace;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.TableLayoutPanel panelWorkspace;
        public System.Windows.Forms.Label pathToFolder;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabFirstEntry;
        private System.Windows.Forms.TabPage tabReflected;
        private System.Windows.Forms.TextBox log;
        private System.Windows.Forms.TableLayoutPanel panel_fe;
        private System.Windows.Forms.Button button_fe_Process;
        private System.Windows.Forms.TableLayoutPanel panel_fe_Checks;
        private System.Windows.Forms.CheckBox check_fe_Bind;
        private System.Windows.Forms.CheckBox check_fe_Project;
        private System.Windows.Forms.CheckBox check_fe_SplitHodographs;
        private System.Windows.Forms.CheckBox check_fe_Interpolate;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TableLayoutPanel panel_fe_Aux;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox deltaTextBox;
        private System.Windows.Forms.Button buttonShowParameters;
        private System.Windows.Forms.TableLayoutPanel panel_refl;
        private System.Windows.Forms.TableLayoutPanel panel_refl_FirstStage;
        private System.Windows.Forms.Label labelFirstStage;
        private System.Windows.Forms.CheckBox check_refl_Bind;
        private System.Windows.Forms.CheckBox check_refl_Square;
        private System.Windows.Forms.TextBox textBox_refl_Wave;
        private System.Windows.Forms.Label label_refl_Wave;
        private System.Windows.Forms.Button button_refl_FirstStage;
        private System.Windows.Forms.TableLayoutPanel panel_refl_SecondStage;
        private System.Windows.Forms.Label label_refl_Contours;
        private System.Windows.Forms.Label labelSecondStage;
        private System.Windows.Forms.Button button_refl_SecondStage;
        private System.Windows.Forms.TextBox textBox_refl_Contours;
        private System.Windows.Forms.Button button_refl_SelectContours;
        private System.Windows.Forms.CheckBox check_refl_SoundCenters;
        private System.Windows.Forms.CheckBox check_refl_Pyramid;
        private System.Windows.Forms.CheckBox check_refl_MeanVelocity;
        private System.Windows.Forms.Button button_fe_BindWaves;
        private System.Windows.Forms.Button button_fe_BindUncertainy;
    }
}