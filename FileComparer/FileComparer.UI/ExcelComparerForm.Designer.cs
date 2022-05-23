namespace FileComparer.UI
{
    partial class ExcelComparerForm
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
            this.standardFile = new System.Windows.Forms.OpenFileDialog();
            this.compareFile = new System.Windows.Forms.OpenFileDialog();
            this.reportFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.basicTab = new System.Windows.Forms.TabPage();
            this.basicSettingsPanel = new System.Windows.Forms.Panel();
            this.clearCompare = new System.Windows.Forms.Button();
            this.clearStandard = new System.Windows.Forms.Button();
            this.reportFolderBrowse = new System.Windows.Forms.Button();
            this.compareFileBrowse = new System.Windows.Forms.Button();
            this.standardFileBrowse = new System.Windows.Forms.Button();
            this.reportFolderPath = new System.Windows.Forms.TextBox();
            this.compareFilePath = new System.Windows.Forms.TextBox();
            this.standardFilePath = new System.Windows.Forms.TextBox();
            this.reportFileLabel = new System.Windows.Forms.Label();
            this.compareFileLabel = new System.Windows.Forms.Label();
            this.standardFileLabel = new System.Windows.Forms.Label();
            this.rowSettingsTab = new System.Windows.Forms.TabPage();
            this.rowSettingsPanel = new System.Windows.Forms.Panel();
            this.nextForRowSettings = new System.Windows.Forms.Button();
            this.waitRowSettings = new System.Windows.Forms.Label();
            this.uniqueKeyColumns = new System.Windows.Forms.ListView();
            this.searchUniqueKeyLabel = new System.Windows.Forms.Label();
            this.searchKeyColumnLabel = new System.Windows.Forms.Label();
            this.selectedItems = new System.Windows.Forms.TextBox();
            this.uniqueKeyColumnsSearch = new System.Windows.Forms.TextBox();
            this.uniqueKeyColumnLabel = new System.Windows.Forms.Label();
            this.columnSettingsTab = new System.Windows.Forms.TabPage();
            this.columnSettingsPanel = new System.Windows.Forms.Panel();
            this.nextForColumnSetting = new System.Windows.Forms.Button();
            this.selectedColumns = new System.Windows.Forms.ListView();
            this.waitColumnSettings = new System.Windows.Forms.Label();
            this.searchSelectedColumnLabel = new System.Windows.Forms.Label();
            this.selectedColumnsLabel = new System.Windows.Forms.Label();
            this.selectedItemsColumns = new System.Windows.Forms.TextBox();
            this.columnItemsSelectedSearch = new System.Windows.Forms.TextBox();
            this.columnsLabel = new System.Windows.Forms.Label();
            this.groupSettingsTab = new System.Windows.Forms.TabPage();
            this.groupSettingsPanel = new System.Windows.Forms.Panel();
            this.nextForGroupSetting = new System.Windows.Forms.Button();
            this.groupColumns = new System.Windows.Forms.ListView();
            this.groupColumnsSearchLabel = new System.Windows.Forms.Label();
            this.waitGroupSettings = new System.Windows.Forms.Label();
            this.selectedGroupColumnsLabel = new System.Windows.Forms.Label();
            this.selectedGroupColumns = new System.Windows.Forms.TextBox();
            this.groupColumnsSearch = new System.Windows.Forms.TextBox();
            this.groupColumnsLabel = new System.Windows.Forms.Label();
            this.distributionSettingsTab = new System.Windows.Forms.TabPage();
            this.distributionSettingsPanel = new System.Windows.Forms.Panel();
            this.nextForDistributionSetting = new System.Windows.Forms.Button();
            this.distributionColumns = new System.Windows.Forms.ListView();
            this.distributionColumnsSearchLabel = new System.Windows.Forms.Label();
            this.waitDistributionSettings = new System.Windows.Forms.Label();
            this.selectedDistributionColumnsLabel = new System.Windows.Forms.Label();
            this.selectedDistributionColumns = new System.Windows.Forms.TextBox();
            this.distributionColumnsSearch = new System.Windows.Forms.TextBox();
            this.distributionColumnsLabel = new System.Windows.Forms.Label();
            this.rowCheckWithSelectedColumn = new System.Windows.Forms.CheckBox();
            this.rowCheck = new System.Windows.Forms.CheckBox();
            this.columnCheck = new System.Windows.Forms.CheckBox();
            this.configurationLabel = new System.Windows.Forms.Label();
            this.generateReport = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.spinner = new System.Windows.Forms.PictureBox();
            this.groupCheck = new System.Windows.Forms.CheckBox();
            this.timerRowSettings = new System.Windows.Forms.Timer(this.components);
            this.timerColumnSettings = new System.Windows.Forms.Timer(this.components);
            this.timerGroupSettings = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.distributionCheck = new System.Windows.Forms.CheckBox();
            this.timerDistributionSettings = new System.Windows.Forms.Timer(this.components);
            this.tabControl.SuspendLayout();
            this.basicTab.SuspendLayout();
            this.basicSettingsPanel.SuspendLayout();
            this.rowSettingsTab.SuspendLayout();
            this.rowSettingsPanel.SuspendLayout();
            this.columnSettingsTab.SuspendLayout();
            this.columnSettingsPanel.SuspendLayout();
            this.groupSettingsTab.SuspendLayout();
            this.groupSettingsPanel.SuspendLayout();
            this.distributionSettingsTab.SuspendLayout();
            this.distributionSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinner)).BeginInit();
            this.SuspendLayout();
            // 
            // standardFile
            // 
            this.standardFile.FileName = "standardFile";
            // 
            // compareFile
            // 
            this.compareFile.FileName = "compareFile";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.basicTab);
            this.tabControl.Controls.Add(this.rowSettingsTab);
            this.tabControl.Controls.Add(this.columnSettingsTab);
            this.tabControl.Controls.Add(this.groupSettingsTab);
            this.tabControl.Controls.Add(this.distributionSettingsTab);
            this.tabControl.Location = new System.Drawing.Point(33, 13);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(823, 350);
            this.tabControl.TabIndex = 13;
            // 
            // basicTab
            // 
            this.basicTab.Controls.Add(this.basicSettingsPanel);
            this.basicTab.Location = new System.Drawing.Point(4, 22);
            this.basicTab.Name = "basicTab";
            this.basicTab.Padding = new System.Windows.Forms.Padding(3);
            this.basicTab.Size = new System.Drawing.Size(815, 324);
            this.basicTab.TabIndex = 0;
            this.basicTab.Text = "Basic Settings";
            this.basicTab.UseVisualStyleBackColor = true;
            // 
            // basicSettingsPanel
            // 
            this.basicSettingsPanel.Controls.Add(this.clearCompare);
            this.basicSettingsPanel.Controls.Add(this.clearStandard);
            this.basicSettingsPanel.Controls.Add(this.reportFolderBrowse);
            this.basicSettingsPanel.Controls.Add(this.compareFileBrowse);
            this.basicSettingsPanel.Controls.Add(this.standardFileBrowse);
            this.basicSettingsPanel.Controls.Add(this.reportFolderPath);
            this.basicSettingsPanel.Controls.Add(this.compareFilePath);
            this.basicSettingsPanel.Controls.Add(this.standardFilePath);
            this.basicSettingsPanel.Controls.Add(this.reportFileLabel);
            this.basicSettingsPanel.Controls.Add(this.compareFileLabel);
            this.basicSettingsPanel.Controls.Add(this.standardFileLabel);
            this.basicSettingsPanel.Location = new System.Drawing.Point(64, 62);
            this.basicSettingsPanel.Name = "basicSettingsPanel";
            this.basicSettingsPanel.Size = new System.Drawing.Size(645, 210);
            this.basicSettingsPanel.TabIndex = 12;
            // 
            // clearCompare
            // 
            this.clearCompare.Enabled = false;
            this.clearCompare.Location = new System.Drawing.Point(496, 82);
            this.clearCompare.Name = "clearCompare";
            this.clearCompare.Size = new System.Drawing.Size(75, 23);
            this.clearCompare.TabIndex = 13;
            this.clearCompare.Text = "Clear";
            this.clearCompare.UseVisualStyleBackColor = true;
            this.clearCompare.Click += new System.EventHandler(this.clearCompare_Click);
            // 
            // clearStandard
            // 
            this.clearStandard.Enabled = false;
            this.clearStandard.Location = new System.Drawing.Point(496, 25);
            this.clearStandard.Name = "clearStandard";
            this.clearStandard.Size = new System.Drawing.Size(75, 23);
            this.clearStandard.TabIndex = 12;
            this.clearStandard.Text = "Clear";
            this.clearStandard.UseVisualStyleBackColor = true;
            this.clearStandard.Click += new System.EventHandler(this.clearStandard_Click);
            // 
            // reportFolderBrowse
            // 
            this.reportFolderBrowse.Location = new System.Drawing.Point(399, 140);
            this.reportFolderBrowse.Name = "reportFolderBrowse";
            this.reportFolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.reportFolderBrowse.TabIndex = 11;
            this.reportFolderBrowse.Text = "Browse";
            this.reportFolderBrowse.UseVisualStyleBackColor = true;
            this.reportFolderBrowse.Click += new System.EventHandler(this.reportFolderBrowse_Click);
            // 
            // compareFileBrowse
            // 
            this.compareFileBrowse.Location = new System.Drawing.Point(399, 82);
            this.compareFileBrowse.Name = "compareFileBrowse";
            this.compareFileBrowse.Size = new System.Drawing.Size(75, 23);
            this.compareFileBrowse.TabIndex = 10;
            this.compareFileBrowse.Text = "Browse";
            this.compareFileBrowse.UseVisualStyleBackColor = true;
            this.compareFileBrowse.Click += new System.EventHandler(this.compareFileBrowse_Click);
            // 
            // standardFileBrowse
            // 
            this.standardFileBrowse.Location = new System.Drawing.Point(399, 25);
            this.standardFileBrowse.Name = "standardFileBrowse";
            this.standardFileBrowse.Size = new System.Drawing.Size(75, 23);
            this.standardFileBrowse.TabIndex = 9;
            this.standardFileBrowse.Text = "Browse";
            this.standardFileBrowse.UseVisualStyleBackColor = true;
            this.standardFileBrowse.Click += new System.EventHandler(this.standardFileBrowse_Click);
            // 
            // reportFolderPath
            // 
            this.reportFolderPath.Location = new System.Drawing.Point(129, 140);
            this.reportFolderPath.Name = "reportFolderPath";
            this.reportFolderPath.ReadOnly = true;
            this.reportFolderPath.Size = new System.Drawing.Size(242, 20);
            this.reportFolderPath.TabIndex = 8;
            // 
            // compareFilePath
            // 
            this.compareFilePath.Location = new System.Drawing.Point(129, 82);
            this.compareFilePath.Name = "compareFilePath";
            this.compareFilePath.ReadOnly = true;
            this.compareFilePath.Size = new System.Drawing.Size(242, 20);
            this.compareFilePath.TabIndex = 7;
            // 
            // standardFilePath
            // 
            this.standardFilePath.Location = new System.Drawing.Point(129, 25);
            this.standardFilePath.Name = "standardFilePath";
            this.standardFilePath.ReadOnly = true;
            this.standardFilePath.Size = new System.Drawing.Size(242, 20);
            this.standardFilePath.TabIndex = 6;
            // 
            // reportFileLabel
            // 
            this.reportFileLabel.AutoSize = true;
            this.reportFileLabel.Location = new System.Drawing.Point(23, 143);
            this.reportFileLabel.Name = "reportFileLabel";
            this.reportFileLabel.Size = new System.Drawing.Size(86, 13);
            this.reportFileLabel.TabIndex = 2;
            this.reportFileLabel.Text = "Report File Path:";
            // 
            // compareFileLabel
            // 
            this.compareFileLabel.AutoSize = true;
            this.compareFileLabel.Location = new System.Drawing.Point(23, 85);
            this.compareFileLabel.Name = "compareFileLabel";
            this.compareFileLabel.Size = new System.Drawing.Size(71, 13);
            this.compareFileLabel.TabIndex = 1;
            this.compareFileLabel.Text = "Compare File:";
            // 
            // standardFileLabel
            // 
            this.standardFileLabel.AutoSize = true;
            this.standardFileLabel.Location = new System.Drawing.Point(23, 28);
            this.standardFileLabel.Name = "standardFileLabel";
            this.standardFileLabel.Size = new System.Drawing.Size(72, 13);
            this.standardFileLabel.TabIndex = 0;
            this.standardFileLabel.Text = "Standard File:";
            // 
            // rowSettingsTab
            // 
            this.rowSettingsTab.Controls.Add(this.rowSettingsPanel);
            this.rowSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.rowSettingsTab.Name = "rowSettingsTab";
            this.rowSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.rowSettingsTab.Size = new System.Drawing.Size(815, 324);
            this.rowSettingsTab.TabIndex = 1;
            this.rowSettingsTab.Text = "Row Settings";
            this.rowSettingsTab.UseVisualStyleBackColor = true;
            // 
            // rowSettingsPanel
            // 
            this.rowSettingsPanel.Controls.Add(this.nextForRowSettings);
            this.rowSettingsPanel.Controls.Add(this.waitRowSettings);
            this.rowSettingsPanel.Controls.Add(this.uniqueKeyColumns);
            this.rowSettingsPanel.Controls.Add(this.searchUniqueKeyLabel);
            this.rowSettingsPanel.Controls.Add(this.searchKeyColumnLabel);
            this.rowSettingsPanel.Controls.Add(this.selectedItems);
            this.rowSettingsPanel.Controls.Add(this.uniqueKeyColumnsSearch);
            this.rowSettingsPanel.Controls.Add(this.uniqueKeyColumnLabel);
            this.rowSettingsPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.rowSettingsPanel.Location = new System.Drawing.Point(36, 6);
            this.rowSettingsPanel.Name = "rowSettingsPanel";
            this.rowSettingsPanel.Size = new System.Drawing.Size(744, 299);
            this.rowSettingsPanel.TabIndex = 0;
            // 
            // nextForRowSettings
            // 
            this.nextForRowSettings.Location = new System.Drawing.Point(622, 81);
            this.nextForRowSettings.Name = "nextForRowSettings";
            this.nextForRowSettings.Size = new System.Drawing.Size(51, 20);
            this.nextForRowSettings.TabIndex = 8;
            this.nextForRowSettings.Text = "Next";
            this.toolTip.SetToolTip(this.nextForRowSettings, "Click on this to navigate to the next sheet");
            this.nextForRowSettings.UseVisualStyleBackColor = true;
            this.nextForRowSettings.Click += new System.EventHandler(this.nextForRowSettings_Click);
            // 
            // waitRowSettings
            // 
            this.waitRowSettings.AutoSize = true;
            this.waitRowSettings.BackColor = System.Drawing.Color.Transparent;
            this.waitRowSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.waitRowSettings.ForeColor = System.Drawing.Color.Red;
            this.waitRowSettings.Location = new System.Drawing.Point(30, 179);
            this.waitRowSettings.Name = "waitRowSettings";
            this.waitRowSettings.Size = new System.Drawing.Size(126, 90);
            this.waitRowSettings.TabIndex = 7;
            this.waitRowSettings.Text = "Please wait \r\nwhile the \r\ncolumns are \r\nbeing populated\r\n\r\n";
            this.waitRowSettings.Visible = false;
            // 
            // uniqueKeyColumns
            // 
            this.uniqueKeyColumns.CheckBoxes = true;
            this.uniqueKeyColumns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.uniqueKeyColumns.Location = new System.Drawing.Point(172, 116);
            this.uniqueKeyColumns.Name = "uniqueKeyColumns";
            this.uniqueKeyColumns.Size = new System.Drawing.Size(501, 167);
            this.uniqueKeyColumns.TabIndex = 6;
            this.uniqueKeyColumns.UseCompatibleStateImageBehavior = false;
            this.uniqueKeyColumns.View = System.Windows.Forms.View.Details;
            this.uniqueKeyColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.uniqueKeyColumns_ItemCheck);
            this.uniqueKeyColumns.MouseClick += new System.Windows.Forms.MouseEventHandler(this.uniqueKeyColumns_MouseClick);
            // 
            // searchUniqueKeyLabel
            // 
            this.searchUniqueKeyLabel.AutoSize = true;
            this.searchUniqueKeyLabel.Location = new System.Drawing.Point(31, 81);
            this.searchUniqueKeyLabel.Name = "searchUniqueKeyLabel";
            this.searchUniqueKeyLabel.Size = new System.Drawing.Size(44, 13);
            this.searchUniqueKeyLabel.TabIndex = 5;
            this.searchUniqueKeyLabel.Text = "Search:";
            // 
            // searchKeyColumnLabel
            // 
            this.searchKeyColumnLabel.AutoSize = true;
            this.searchKeyColumnLabel.Location = new System.Drawing.Point(31, 22);
            this.searchKeyColumnLabel.Name = "searchKeyColumnLabel";
            this.searchKeyColumnLabel.Size = new System.Drawing.Size(116, 13);
            this.searchKeyColumnLabel.TabIndex = 4;
            this.searchKeyColumnLabel.Text = "Selected Key Columns:";
            // 
            // selectedItems
            // 
            this.selectedItems.Location = new System.Drawing.Point(172, 18);
            this.selectedItems.Multiline = true;
            this.selectedItems.Name = "selectedItems";
            this.selectedItems.ReadOnly = true;
            this.selectedItems.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.selectedItems.Size = new System.Drawing.Size(501, 48);
            this.selectedItems.TabIndex = 3;
            // 
            // uniqueKeyColumnsSearch
            // 
            this.uniqueKeyColumnsSearch.Location = new System.Drawing.Point(172, 81);
            this.uniqueKeyColumnsSearch.Name = "uniqueKeyColumnsSearch";
            this.uniqueKeyColumnsSearch.Size = new System.Drawing.Size(433, 20);
            this.uniqueKeyColumnsSearch.TabIndex = 2;
            this.uniqueKeyColumnsSearch.TextChanged += new System.EventHandler(this.uniqueKeyColumnsSearch_TextChanged);
            // 
            // uniqueKeyColumnLabel
            // 
            this.uniqueKeyColumnLabel.AutoSize = true;
            this.uniqueKeyColumnLabel.Location = new System.Drawing.Point(31, 118);
            this.uniqueKeyColumnLabel.Name = "uniqueKeyColumnLabel";
            this.uniqueKeyColumnLabel.Size = new System.Drawing.Size(108, 13);
            this.uniqueKeyColumnLabel.TabIndex = 0;
            this.uniqueKeyColumnLabel.Text = "Unique Key Columns:";
            // 
            // columnSettingsTab
            // 
            this.columnSettingsTab.Controls.Add(this.columnSettingsPanel);
            this.columnSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.columnSettingsTab.Name = "columnSettingsTab";
            this.columnSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.columnSettingsTab.Size = new System.Drawing.Size(815, 324);
            this.columnSettingsTab.TabIndex = 2;
            this.columnSettingsTab.Text = "Column Settings";
            this.columnSettingsTab.UseVisualStyleBackColor = true;
            // 
            // columnSettingsPanel
            // 
            this.columnSettingsPanel.Controls.Add(this.nextForColumnSetting);
            this.columnSettingsPanel.Controls.Add(this.selectedColumns);
            this.columnSettingsPanel.Controls.Add(this.waitColumnSettings);
            this.columnSettingsPanel.Controls.Add(this.searchSelectedColumnLabel);
            this.columnSettingsPanel.Controls.Add(this.selectedColumnsLabel);
            this.columnSettingsPanel.Controls.Add(this.selectedItemsColumns);
            this.columnSettingsPanel.Controls.Add(this.columnItemsSelectedSearch);
            this.columnSettingsPanel.Controls.Add(this.columnsLabel);
            this.columnSettingsPanel.Location = new System.Drawing.Point(36, 6);
            this.columnSettingsPanel.Name = "columnSettingsPanel";
            this.columnSettingsPanel.Size = new System.Drawing.Size(744, 299);
            this.columnSettingsPanel.TabIndex = 1;
            // 
            // nextForColumnSetting
            // 
            this.nextForColumnSetting.Location = new System.Drawing.Point(622, 81);
            this.nextForColumnSetting.Name = "nextForColumnSetting";
            this.nextForColumnSetting.Size = new System.Drawing.Size(51, 20);
            this.nextForColumnSetting.TabIndex = 9;
            this.nextForColumnSetting.Text = "Next";
            this.toolTip.SetToolTip(this.nextForColumnSetting, "Click on this to navigate to the next sheet");
            this.nextForColumnSetting.UseVisualStyleBackColor = true;
            this.nextForColumnSetting.Click += new System.EventHandler(this.nextForColumnSetting_Click);
            // 
            // selectedColumns
            // 
            this.selectedColumns.CheckBoxes = true;
            this.selectedColumns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.selectedColumns.Location = new System.Drawing.Point(172, 116);
            this.selectedColumns.Name = "selectedColumns";
            this.selectedColumns.Size = new System.Drawing.Size(501, 167);
            this.selectedColumns.TabIndex = 6;
            this.selectedColumns.UseCompatibleStateImageBehavior = false;
            this.selectedColumns.View = System.Windows.Forms.View.Details;
            this.selectedColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.selectedColumns_ItemCheck);
            this.selectedColumns.MouseClick += new System.Windows.Forms.MouseEventHandler(this.selectedColumns_MouseClick);
            // 
            // waitColumnSettings
            // 
            this.waitColumnSettings.AutoSize = true;
            this.waitColumnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.waitColumnSettings.ForeColor = System.Drawing.Color.Red;
            this.waitColumnSettings.Location = new System.Drawing.Point(30, 179);
            this.waitColumnSettings.Name = "waitColumnSettings";
            this.waitColumnSettings.Size = new System.Drawing.Size(126, 72);
            this.waitColumnSettings.TabIndex = 7;
            this.waitColumnSettings.Text = "Please wait \r\nwhile the\r\ncolumns are \r\nbeing populated\r\n";
            this.waitColumnSettings.Visible = false;
            // 
            // searchSelectedColumnLabel
            // 
            this.searchSelectedColumnLabel.AutoSize = true;
            this.searchSelectedColumnLabel.Location = new System.Drawing.Point(31, 81);
            this.searchSelectedColumnLabel.Name = "searchSelectedColumnLabel";
            this.searchSelectedColumnLabel.Size = new System.Drawing.Size(44, 13);
            this.searchSelectedColumnLabel.TabIndex = 5;
            this.searchSelectedColumnLabel.Text = "Search:";
            // 
            // selectedColumnsLabel
            // 
            this.selectedColumnsLabel.AutoSize = true;
            this.selectedColumnsLabel.Location = new System.Drawing.Point(31, 22);
            this.selectedColumnsLabel.Name = "selectedColumnsLabel";
            this.selectedColumnsLabel.Size = new System.Drawing.Size(95, 13);
            this.selectedColumnsLabel.TabIndex = 4;
            this.selectedColumnsLabel.Text = "Selected Columns:";
            // 
            // selectedItemsColumns
            // 
            this.selectedItemsColumns.Location = new System.Drawing.Point(172, 18);
            this.selectedItemsColumns.Multiline = true;
            this.selectedItemsColumns.Name = "selectedItemsColumns";
            this.selectedItemsColumns.ReadOnly = true;
            this.selectedItemsColumns.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.selectedItemsColumns.Size = new System.Drawing.Size(501, 48);
            this.selectedItemsColumns.TabIndex = 3;
            // 
            // columnItemsSelectedSearch
            // 
            this.columnItemsSelectedSearch.Location = new System.Drawing.Point(172, 81);
            this.columnItemsSelectedSearch.Name = "columnItemsSelectedSearch";
            this.columnItemsSelectedSearch.Size = new System.Drawing.Size(433, 20);
            this.columnItemsSelectedSearch.TabIndex = 2;
            this.columnItemsSelectedSearch.TextChanged += new System.EventHandler(this.columnItemsSelectedSearch_TextChanged);
            // 
            // columnsLabel
            // 
            this.columnsLabel.AutoSize = true;
            this.columnsLabel.Location = new System.Drawing.Point(31, 118);
            this.columnsLabel.Name = "columnsLabel";
            this.columnsLabel.Size = new System.Drawing.Size(50, 13);
            this.columnsLabel.TabIndex = 0;
            this.columnsLabel.Text = "Columns:";
            // 
            // groupSettingsTab
            // 
            this.groupSettingsTab.Controls.Add(this.groupSettingsPanel);
            this.groupSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.groupSettingsTab.Name = "groupSettingsTab";
            this.groupSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.groupSettingsTab.Size = new System.Drawing.Size(815, 324);
            this.groupSettingsTab.TabIndex = 3;
            this.groupSettingsTab.Text = "Group Settings";
            this.groupSettingsTab.UseVisualStyleBackColor = true;
            // 
            // groupSettingsPanel
            // 
            this.groupSettingsPanel.Controls.Add(this.nextForGroupSetting);
            this.groupSettingsPanel.Controls.Add(this.groupColumns);
            this.groupSettingsPanel.Controls.Add(this.groupColumnsSearchLabel);
            this.groupSettingsPanel.Controls.Add(this.waitGroupSettings);
            this.groupSettingsPanel.Controls.Add(this.selectedGroupColumnsLabel);
            this.groupSettingsPanel.Controls.Add(this.selectedGroupColumns);
            this.groupSettingsPanel.Controls.Add(this.groupColumnsSearch);
            this.groupSettingsPanel.Controls.Add(this.groupColumnsLabel);
            this.groupSettingsPanel.Location = new System.Drawing.Point(36, 6);
            this.groupSettingsPanel.Name = "groupSettingsPanel";
            this.groupSettingsPanel.Size = new System.Drawing.Size(744, 299);
            this.groupSettingsPanel.TabIndex = 2;
            // 
            // nextForGroupSetting
            // 
            this.nextForGroupSetting.Location = new System.Drawing.Point(622, 81);
            this.nextForGroupSetting.Name = "nextForGroupSetting";
            this.nextForGroupSetting.Size = new System.Drawing.Size(51, 20);
            this.nextForGroupSetting.TabIndex = 9;
            this.nextForGroupSetting.Text = "Next";
            this.toolTip.SetToolTip(this.nextForGroupSetting, "Click on this to navigate to the next sheet");
            this.nextForGroupSetting.UseVisualStyleBackColor = true;
            this.nextForGroupSetting.Click += new System.EventHandler(this.nextForGroupSetting_Click);
            // 
            // groupColumns
            // 
            this.groupColumns.CheckBoxes = true;
            this.groupColumns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.groupColumns.Location = new System.Drawing.Point(172, 116);
            this.groupColumns.Name = "groupColumns";
            this.groupColumns.Size = new System.Drawing.Size(501, 167);
            this.groupColumns.TabIndex = 6;
            this.groupColumns.UseCompatibleStateImageBehavior = false;
            this.groupColumns.View = System.Windows.Forms.View.Details;
            this.groupColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.groupColumns_ItemCheck);
            this.groupColumns.MouseClick += new System.Windows.Forms.MouseEventHandler(this.groupColumns_MouseClick);
            // 
            // groupColumnsSearchLabel
            // 
            this.groupColumnsSearchLabel.AutoSize = true;
            this.groupColumnsSearchLabel.Location = new System.Drawing.Point(31, 81);
            this.groupColumnsSearchLabel.Name = "groupColumnsSearchLabel";
            this.groupColumnsSearchLabel.Size = new System.Drawing.Size(44, 13);
            this.groupColumnsSearchLabel.TabIndex = 5;
            this.groupColumnsSearchLabel.Text = "Search:";
            // 
            // waitGroupSettings
            // 
            this.waitGroupSettings.AutoSize = true;
            this.waitGroupSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.waitGroupSettings.ForeColor = System.Drawing.Color.Red;
            this.waitGroupSettings.Location = new System.Drawing.Point(30, 179);
            this.waitGroupSettings.Name = "waitGroupSettings";
            this.waitGroupSettings.Size = new System.Drawing.Size(126, 72);
            this.waitGroupSettings.TabIndex = 8;
            this.waitGroupSettings.Text = "Please wait \r\nwhile the\r\ncolumns are \r\nbeing populated\r\n";
            this.waitGroupSettings.Visible = false;
            // 
            // selectedGroupColumnsLabel
            // 
            this.selectedGroupColumnsLabel.AutoSize = true;
            this.selectedGroupColumnsLabel.Location = new System.Drawing.Point(31, 22);
            this.selectedGroupColumnsLabel.Name = "selectedGroupColumnsLabel";
            this.selectedGroupColumnsLabel.Size = new System.Drawing.Size(127, 13);
            this.selectedGroupColumnsLabel.TabIndex = 4;
            this.selectedGroupColumnsLabel.Text = "Selected Group Columns:";
            // 
            // selectedGroupColumns
            // 
            this.selectedGroupColumns.Location = new System.Drawing.Point(172, 18);
            this.selectedGroupColumns.Multiline = true;
            this.selectedGroupColumns.Name = "selectedGroupColumns";
            this.selectedGroupColumns.ReadOnly = true;
            this.selectedGroupColumns.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.selectedGroupColumns.Size = new System.Drawing.Size(501, 48);
            this.selectedGroupColumns.TabIndex = 3;
            // 
            // groupColumnsSearch
            // 
            this.groupColumnsSearch.Location = new System.Drawing.Point(172, 81);
            this.groupColumnsSearch.Name = "groupColumnsSearch";
            this.groupColumnsSearch.Size = new System.Drawing.Size(433, 20);
            this.groupColumnsSearch.TabIndex = 2;
            this.groupColumnsSearch.TextChanged += new System.EventHandler(this.groupColumnsSearch_TextChanged);
            // 
            // groupColumnsLabel
            // 
            this.groupColumnsLabel.AutoSize = true;
            this.groupColumnsLabel.Location = new System.Drawing.Point(31, 118);
            this.groupColumnsLabel.Name = "groupColumnsLabel";
            this.groupColumnsLabel.Size = new System.Drawing.Size(50, 13);
            this.groupColumnsLabel.TabIndex = 0;
            this.groupColumnsLabel.Text = "Columns:";
            // 
            // distributionSettingsTab
            // 
            this.distributionSettingsTab.Controls.Add(this.distributionSettingsPanel);
            this.distributionSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.distributionSettingsTab.Name = "distributionSettingsTab";
            this.distributionSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.distributionSettingsTab.Size = new System.Drawing.Size(815, 324);
            this.distributionSettingsTab.TabIndex = 4;
            this.distributionSettingsTab.Text = "Distribution Settings";
            this.distributionSettingsTab.UseVisualStyleBackColor = true;
            // 
            // distributionSettingsPanel
            // 
            this.distributionSettingsPanel.Controls.Add(this.nextForDistributionSetting);
            this.distributionSettingsPanel.Controls.Add(this.distributionColumns);
            this.distributionSettingsPanel.Controls.Add(this.distributionColumnsSearchLabel);
            this.distributionSettingsPanel.Controls.Add(this.waitDistributionSettings);
            this.distributionSettingsPanel.Controls.Add(this.selectedDistributionColumnsLabel);
            this.distributionSettingsPanel.Controls.Add(this.selectedDistributionColumns);
            this.distributionSettingsPanel.Controls.Add(this.distributionColumnsSearch);
            this.distributionSettingsPanel.Controls.Add(this.distributionColumnsLabel);
            this.distributionSettingsPanel.Location = new System.Drawing.Point(36, 6);
            this.distributionSettingsPanel.Name = "distributionSettingsPanel";
            this.distributionSettingsPanel.Size = new System.Drawing.Size(744, 299);
            this.distributionSettingsPanel.TabIndex = 3;
            // 
            // nextForDistributionSetting
            // 
            this.nextForDistributionSetting.Location = new System.Drawing.Point(622, 81);
            this.nextForDistributionSetting.Name = "nextForDistributionSetting";
            this.nextForDistributionSetting.Size = new System.Drawing.Size(51, 20);
            this.nextForDistributionSetting.TabIndex = 9;
            this.nextForDistributionSetting.Text = "Next";
            this.toolTip.SetToolTip(this.nextForDistributionSetting, "Click on this to navigate to the next sheet");
            this.nextForDistributionSetting.UseVisualStyleBackColor = true;
            this.nextForDistributionSetting.Click += new System.EventHandler(this.nextForDistributionSetting_Click);
            // 
            // distributionColumns
            // 
            this.distributionColumns.CheckBoxes = true;
            this.distributionColumns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.distributionColumns.Location = new System.Drawing.Point(172, 116);
            this.distributionColumns.Name = "distributionColumns";
            this.distributionColumns.Size = new System.Drawing.Size(501, 167);
            this.distributionColumns.TabIndex = 6;
            this.distributionColumns.UseCompatibleStateImageBehavior = false;
            this.distributionColumns.View = System.Windows.Forms.View.Details;
            this.distributionColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.distributionColumns_ItemCheck);
            this.distributionColumns.MouseClick += new System.Windows.Forms.MouseEventHandler(this.distributionColumns_MouseClick);
            // 
            // distributionColumnsSearchLabel
            // 
            this.distributionColumnsSearchLabel.AutoSize = true;
            this.distributionColumnsSearchLabel.Location = new System.Drawing.Point(31, 81);
            this.distributionColumnsSearchLabel.Name = "distributionColumnsSearchLabel";
            this.distributionColumnsSearchLabel.Size = new System.Drawing.Size(44, 13);
            this.distributionColumnsSearchLabel.TabIndex = 5;
            this.distributionColumnsSearchLabel.Text = "Search:";
            // 
            // waitDistributionSettings
            // 
            this.waitDistributionSettings.AutoSize = true;
            this.waitDistributionSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.waitDistributionSettings.ForeColor = System.Drawing.Color.Red;
            this.waitDistributionSettings.Location = new System.Drawing.Point(30, 179);
            this.waitDistributionSettings.Name = "waitDistributionSettings";
            this.waitDistributionSettings.Size = new System.Drawing.Size(126, 72);
            this.waitDistributionSettings.TabIndex = 8;
            this.waitDistributionSettings.Text = "Please wait \r\nwhile the\r\ncolumns are \r\nbeing populated\r\n";
            this.waitDistributionSettings.Visible = false;
            // 
            // selectedDistributionColumnsLabel
            // 
            this.selectedDistributionColumnsLabel.AutoSize = true;
            this.selectedDistributionColumnsLabel.Location = new System.Drawing.Point(31, 22);
            this.selectedDistributionColumnsLabel.Name = "selectedDistributionColumnsLabel";
            this.selectedDistributionColumnsLabel.Size = new System.Drawing.Size(95, 13);
            this.selectedDistributionColumnsLabel.TabIndex = 4;
            this.selectedDistributionColumnsLabel.Text = "Selected Columns:";
            // 
            // selectedDistributionColumns
            // 
            this.selectedDistributionColumns.Location = new System.Drawing.Point(172, 18);
            this.selectedDistributionColumns.Multiline = true;
            this.selectedDistributionColumns.Name = "selectedDistributionColumns";
            this.selectedDistributionColumns.ReadOnly = true;
            this.selectedDistributionColumns.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.selectedDistributionColumns.Size = new System.Drawing.Size(501, 48);
            this.selectedDistributionColumns.TabIndex = 3;
            // 
            // distributionColumnsSearch
            // 
            this.distributionColumnsSearch.Location = new System.Drawing.Point(172, 81);
            this.distributionColumnsSearch.Name = "distributionColumnsSearch";
            this.distributionColumnsSearch.Size = new System.Drawing.Size(433, 20);
            this.distributionColumnsSearch.TabIndex = 2;
            this.distributionColumnsSearch.TextChanged += new System.EventHandler(this.distributionColumnsSearch_TextChanged);
            // 
            // distributionColumnsLabel
            // 
            this.distributionColumnsLabel.AutoSize = true;
            this.distributionColumnsLabel.Location = new System.Drawing.Point(31, 118);
            this.distributionColumnsLabel.Name = "distributionColumnsLabel";
            this.distributionColumnsLabel.Size = new System.Drawing.Size(50, 13);
            this.distributionColumnsLabel.TabIndex = 0;
            this.distributionColumnsLabel.Text = "Columns:";
            // 
            // rowCheckWithSelectedColumn
            // 
            this.rowCheckWithSelectedColumn.AutoSize = true;
            this.rowCheckWithSelectedColumn.BackColor = System.Drawing.Color.Transparent;
            this.rowCheckWithSelectedColumn.Enabled = false;
            this.rowCheckWithSelectedColumn.Location = new System.Drawing.Point(345, 376);
            this.rowCheckWithSelectedColumn.Name = "rowCheckWithSelectedColumn";
            this.rowCheckWithSelectedColumn.Size = new System.Drawing.Size(190, 17);
            this.rowCheckWithSelectedColumn.TabIndex = 12;
            this.rowCheckWithSelectedColumn.Text = "Row Check With Selected Column";
            this.rowCheckWithSelectedColumn.UseVisualStyleBackColor = false;
            this.rowCheckWithSelectedColumn.CheckedChanged += new System.EventHandler(this.rowCheckWithSelectedColumn_CheckedChanged);
            // 
            // rowCheck
            // 
            this.rowCheck.AutoSize = true;
            this.rowCheck.BackColor = System.Drawing.Color.Transparent;
            this.rowCheck.Enabled = false;
            this.rowCheck.Location = new System.Drawing.Point(257, 376);
            this.rowCheck.Name = "rowCheck";
            this.rowCheck.Size = new System.Drawing.Size(82, 17);
            this.rowCheck.TabIndex = 12;
            this.rowCheck.Text = "Row Check";
            this.rowCheck.UseVisualStyleBackColor = false;
            this.rowCheck.CheckedChanged += new System.EventHandler(this.rowCheck_CheckedChanged);
            // 
            // columnCheck
            // 
            this.columnCheck.AutoSize = true;
            this.columnCheck.BackColor = System.Drawing.Color.Transparent;
            this.columnCheck.Enabled = false;
            this.columnCheck.Location = new System.Drawing.Point(156, 376);
            this.columnCheck.Name = "columnCheck";
            this.columnCheck.Size = new System.Drawing.Size(95, 17);
            this.columnCheck.TabIndex = 4;
            this.columnCheck.Text = "Column Check";
            this.columnCheck.UseVisualStyleBackColor = false;
            // 
            // configurationLabel
            // 
            this.configurationLabel.AutoSize = true;
            this.configurationLabel.Location = new System.Drawing.Point(78, 376);
            this.configurationLabel.Name = "configurationLabel";
            this.configurationLabel.Size = new System.Drawing.Size(72, 13);
            this.configurationLabel.TabIndex = 3;
            this.configurationLabel.Text = "Configuration:";
            // 
            // generateReport
            // 
            this.generateReport.Location = new System.Drawing.Point(81, 414);
            this.generateReport.Name = "generateReport";
            this.generateReport.Size = new System.Drawing.Size(123, 23);
            this.generateReport.TabIndex = 5;
            this.generateReport.Text = "Generate Report";
            this.generateReport.UseVisualStyleBackColor = true;
            this.generateReport.Click += new System.EventHandler(this.generateReport_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // spinner
            // 
            this.spinner.BackColor = System.Drawing.Color.Transparent;
            this.spinner.Image = global::FileComparer.UI.Properties.Resources.cloading;
            this.spinner.Location = new System.Drawing.Point(296, 414);
            this.spinner.Name = "spinner";
            this.spinner.Size = new System.Drawing.Size(95, 23);
            this.spinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.spinner.TabIndex = 14;
            this.spinner.TabStop = false;
            this.spinner.Visible = false;
            // 
            // groupCheck
            // 
            this.groupCheck.AutoSize = true;
            this.groupCheck.BackColor = System.Drawing.Color.Transparent;
            this.groupCheck.Enabled = false;
            this.groupCheck.Location = new System.Drawing.Point(541, 376);
            this.groupCheck.Name = "groupCheck";
            this.groupCheck.Size = new System.Drawing.Size(89, 17);
            this.groupCheck.TabIndex = 15;
            this.groupCheck.Text = "Group Check";
            this.groupCheck.UseVisualStyleBackColor = false;
            this.groupCheck.CheckedChanged += new System.EventHandler(this.groupCheck_CheckedChanged);
            this.groupCheck.CheckStateChanged += new System.EventHandler(this.groupCheck_CheckStateChanged);
            // 
            // timerRowSettings
            // 
            this.timerRowSettings.Interval = 250;
            this.timerRowSettings.Tick += new System.EventHandler(this.timerRowSettings_Tick);
            // 
            // timerColumnSettings
            // 
            this.timerColumnSettings.Interval = 250;
            this.timerColumnSettings.Tick += new System.EventHandler(this.timerColumnSettings_Tick);
            // 
            // timerGroupSettings
            // 
            this.timerGroupSettings.Interval = 250;
            this.timerGroupSettings.Tick += new System.EventHandler(this.timeGroupSettings_Tick);
            // 
            // distributionCheck
            // 
            this.distributionCheck.AutoSize = true;
            this.distributionCheck.BackColor = System.Drawing.Color.Transparent;
            this.distributionCheck.Enabled = false;
            this.distributionCheck.Location = new System.Drawing.Point(636, 376);
            this.distributionCheck.Name = "distributionCheck";
            this.distributionCheck.Size = new System.Drawing.Size(112, 17);
            this.distributionCheck.TabIndex = 16;
            this.distributionCheck.Text = "Distribution Check";
            this.distributionCheck.UseVisualStyleBackColor = false;
            this.distributionCheck.CheckedChanged += new System.EventHandler(this.distributionCheck_CheckedChanged);
            this.distributionCheck.CheckStateChanged += new System.EventHandler(this.distributionCheck_CheckStateChanged);
            // 
            // timerDistributionSettings
            // 
            this.timerDistributionSettings.Interval = 250;
            this.timerDistributionSettings.Tick += new System.EventHandler(this.timerDistributionSettings_Tick);
            // 
            // ExcelComparerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 449);
            this.Controls.Add(this.spinner);
            this.Controls.Add(this.rowCheck);
            this.Controls.Add(this.rowCheckWithSelectedColumn);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.generateReport);
            this.Controls.Add(this.configurationLabel);
            this.Controls.Add(this.columnCheck);
            this.Controls.Add(this.groupCheck);
            this.Controls.Add(this.distributionCheck);
            this.Name = "ExcelComparerForm";
            this.Text = "Excel Comparer";
            this.tabControl.ResumeLayout(false);
            this.basicTab.ResumeLayout(false);
            this.basicSettingsPanel.ResumeLayout(false);
            this.basicSettingsPanel.PerformLayout();
            this.rowSettingsTab.ResumeLayout(false);
            this.rowSettingsPanel.ResumeLayout(false);
            this.rowSettingsPanel.PerformLayout();
            this.columnSettingsTab.ResumeLayout(false);
            this.columnSettingsPanel.ResumeLayout(false);
            this.columnSettingsPanel.PerformLayout();
            this.groupSettingsTab.ResumeLayout(false);
            this.groupSettingsPanel.ResumeLayout(false);
            this.groupSettingsPanel.PerformLayout();
            this.distributionSettingsTab.ResumeLayout(false);
            this.distributionSettingsPanel.ResumeLayout(false);
            this.distributionSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog standardFile;
        private System.Windows.Forms.OpenFileDialog compareFile;
        private System.Windows.Forms.FolderBrowserDialog reportFolder;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage rowSettingsTab;
        private System.Windows.Forms.TabPage basicTab;
        private System.Windows.Forms.Label standardFileLabel;
        private System.Windows.Forms.Label compareFileLabel;
        private System.Windows.Forms.Label reportFileLabel;
        private System.Windows.Forms.Label configurationLabel;
        private System.Windows.Forms.CheckBox columnCheck;
        private System.Windows.Forms.Button generateReport;
        private System.Windows.Forms.TextBox standardFilePath;
        private System.Windows.Forms.TextBox compareFilePath;
        private System.Windows.Forms.TextBox reportFolderPath;
        private System.Windows.Forms.Button standardFileBrowse;
        private System.Windows.Forms.Button compareFileBrowse;
        private System.Windows.Forms.Button reportFolderBrowse;
        private System.Windows.Forms.Panel basicSettingsPanel;
        private System.Windows.Forms.CheckBox rowCheck;
        private System.Windows.Forms.Panel rowSettingsPanel;
        private System.Windows.Forms.Label uniqueKeyColumnLabel;
        private System.Windows.Forms.TextBox uniqueKeyColumnsSearch;
        private System.Windows.Forms.TextBox selectedItems;
        private System.Windows.Forms.Label searchUniqueKeyLabel;
        private System.Windows.Forms.Label searchKeyColumnLabel;
        private System.Windows.Forms.CheckBox rowCheckWithSelectedColumn;
        private System.Windows.Forms.TabPage columnSettingsTab;
        private System.Windows.Forms.Panel columnSettingsPanel;
        private System.Windows.Forms.Label searchSelectedColumnLabel;
        private System.Windows.Forms.Label selectedColumnsLabel;
        private System.Windows.Forms.TextBox selectedItemsColumns;
        private System.Windows.Forms.TextBox columnItemsSelectedSearch;
        private System.Windows.Forms.Label columnsLabel;
        private System.Windows.Forms.ListView uniqueKeyColumns;
        private System.Windows.Forms.ListView selectedColumns;
        private System.Windows.Forms.PictureBox spinner;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.TabPage groupSettingsTab;
        private System.Windows.Forms.Panel groupSettingsPanel;
        private System.Windows.Forms.ListView groupColumns;
        private System.Windows.Forms.Label groupColumnsSearchLabel;
        private System.Windows.Forms.Label selectedGroupColumnsLabel;
        private System.Windows.Forms.TextBox selectedGroupColumns;
        private System.Windows.Forms.TextBox groupColumnsSearch;
        private System.Windows.Forms.Label groupColumnsLabel;
        private System.Windows.Forms.CheckBox groupCheck;
        private System.Windows.Forms.Label waitRowSettings;
        private System.Windows.Forms.Label waitColumnSettings;
        private System.Windows.Forms.Label waitGroupSettings;
        private System.Windows.Forms.Timer timerRowSettings;
        private System.Windows.Forms.Timer timerColumnSettings;
        private System.Windows.Forms.Timer timerGroupSettings;
        private System.Windows.Forms.Button nextForRowSettings;
        private System.Windows.Forms.Button nextForColumnSetting;
        private System.Windows.Forms.Button nextForGroupSetting;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabPage distributionSettingsTab;
        private System.Windows.Forms.Panel distributionSettingsPanel;
        private System.Windows.Forms.Button nextForDistributionSetting;
        private System.Windows.Forms.ListView distributionColumns;
        private System.Windows.Forms.Label distributionColumnsSearchLabel;
        private System.Windows.Forms.Label waitDistributionSettings;
        private System.Windows.Forms.Label selectedDistributionColumnsLabel;
        private System.Windows.Forms.TextBox selectedDistributionColumns;
        private System.Windows.Forms.TextBox distributionColumnsSearch;
        private System.Windows.Forms.Label distributionColumnsLabel;
        private System.Windows.Forms.CheckBox distributionCheck;
        private System.Windows.Forms.Timer timerDistributionSettings;
        private System.Windows.Forms.Button clearStandard;
        private System.Windows.Forms.Button clearCompare;
    }
}

