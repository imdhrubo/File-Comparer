using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileComparerCore.Models;
using FileComparerCore.Utilities;

namespace FileComparer.UI
{
    public partial class ExcelComparerForm : Form
    {
        #region Fields
        private static CompareExcelResult compareExcelResult;
        private static List<ColumnGroup> inputFileUniqueColumnList;
        private static List<Tuple<string, string>> uniqueKeyColumnSelectedList = new List<Tuple<string, string>>();
        private static List<Tuple<string, string>> columnSelectedList = new List<Tuple<string, string>>();
        private static List<Tuple<string, string>> groupColumnsSelectedList = new List<Tuple<string, string>>();
        private static List<Tuple<string, string>> distributionColumnsSelectedList = new List<Tuple<string, string>>();
        private static int nextGroupRowSetting = 0;
        private static int nextItemIndexRowSetting = 0;
        private static int nextGroupColumnSetting = 0;
        private static int nextItemIndexColumnSetting = 0;
        private static int nextGroupGroupSetting = 0;
        private static int nextItemIndexGroupSetting = 0;
        private static int nextGroupDistributionSetting = 0;
        private static int nextItemIndexDistributionSetting = 0;
        #endregion
        public ExcelComparerForm()
        {
            InitializeComponent();
            rowSettingsTab.Parent = null;
            columnSettingsTab.Parent = null;
            groupSettingsTab.Parent = null;
            distributionSettingsTab.Parent = null;
            rowSettingsTab.Enabled = false;
            columnSettingsTab.Enabled = false;
            groupSettingsTab.Enabled = false;
            distributionSettingsTab.Enabled = false;
        }

        private async void standardFileBrowse_Click(object sender, EventArgs e)
        {
            var result = standardFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                standardFilePath.Text = standardFile.FileName;
                reportFolderPath.Text = Constants.DefaultReportFolderPath;
                columnCheck.Enabled = true;
                columnCheck.Checked = true;
                groupCheck.Enabled = true;
                distributionCheck.Enabled = true;
                clearStandard.Enabled = true;
                if (File.Exists(compareFile.FileName))
                {
                    columnCheck.Enabled = true;
                    columnCheck.Checked = true;
                    rowCheck.Enabled = true;
                    rowCheckWithSelectedColumn.Enabled = true;
                    await PopulateUniqueColumnsList(standardFile.FileName, compareFile.FileName);
                }
            }
        }

        private async void compareFileBrowse_Click(object sender, EventArgs e)
        {
            var result = compareFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                compareFilePath.Text = compareFile.FileName;
                clearCompare.Enabled = true;
                if (File.Exists(standardFile.FileName))
                {
                    columnCheck.Enabled = true;
                    columnCheck.Checked = true;
                    rowCheck.Enabled = true;
                    rowCheckWithSelectedColumn.Enabled = true;
                    groupColumnsSearch.Clear();
                    distributionColumnsSearch.Clear();
                    selectedGroupColumns.Clear();
                    selectedDistributionColumns.Clear();
                    groupCheck.CheckState = CheckState.Unchecked;
                    distributionCheck.CheckState = CheckState.Unchecked;

                    await PopulateUniqueColumnsList(standardFile.FileName, compareFile.FileName);
                }
            }
        }

        private void reportFolderBrowse_Click(object sender, EventArgs e)
        {
            var result = reportFolder.ShowDialog();
            if (result == DialogResult.OK)
            {
                reportFolderPath.Text = reportFolder.SelectedPath;
            }
        }

        private void generateReport_Click(object sender, EventArgs e)
        {
            Logger.Log("generateReport_Click() start");
            if (!AreFileFolderPathsValid())
            {
                MessageBox.Show(Constants.InvalidFileMessage);
                return;
            }
            Directory.CreateDirectory(reportFolderPath.Text);
            if (!AreSelectionsValid())
            {
                MessageBox.Show(Constants.SelectionInvalidMessage);
                return;
            }
            if (!AreUniqueColumnsValid())
            {
                MessageBox.Show(Constants.UniqueKeyInvalidMessage);
                return;
            }
            if (!AreSelectedColumnsValid())
            {
                MessageBox.Show(Constants.UniqueKeyAndSelectedColumnInvalidMessage);
                return;
            }
            if (!AreUniqueAndSelectedColumnsUnique())
            {
                MessageBox.Show(Constants.UniqueKeyAndSelectedColumnNotUniqueMessage);
                return;
            }
            if (!AreGroupColumnsValid())
            {
                MessageBox.Show(Constants.GroupColumnsInvalidMessage);
                return;
            }
            if (!AreDistributionColumnsValid())
            {
                MessageBox.Show(Constants.DistributionColumnsInvalidMessage);
                return;
            }
            spinner.Visible = true;
            spinner.Dock = DockStyle.Fill;
            backgroundWorker.RunWorkerAsync();
            Logger.Log("generateReport_Click() end");
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (!string.IsNullOrEmpty(compareFilePath.Text))
            {
                compareExcelResult = new FileComparerCore.FileComparer().CompareExcel(GetConfig(), standardFilePath.Text,
                    compareFilePath.Text, reportFolderPath.Text);
            }
            else
            {
                compareExcelResult = new FileComparerCore.FileComparer().AnalyzeSingleFile(GetConfig(), standardFilePath.Text,
                   reportFolderPath.Text);
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            spinner.Visible = false;
            if (compareExcelResult.IsSuccess)
            {
                var answer = MessageBox.Show(Constants.SuccessMessage);
                if (answer == DialogResult.OK)
                {
                    OpenFile(compareExcelResult.ReportFilePath);
                }
            }
            else
            {
                MessageBox.Show(Constants.ErrorMessage);
            }
        }

        private bool AreFileFolderPathsValid()
        {
            if (!string.IsNullOrEmpty(compareFilePath.Text))
            {
                return File.Exists(standardFilePath.Text) &&
                    File.Exists(compareFilePath.Text) && Directory.Exists(reportFolderPath.Text);
            }
            return File.Exists(standardFilePath.Text) && Directory.Exists(reportFolderPath.Text);
        }

        private bool AreSelectionsValid()
        {
            return columnCheck.Checked || rowCheck.Checked || rowCheckWithSelectedColumn.Checked || groupCheck.Checked ||
                distributionCheck.Checked;
        }

        private bool AreUniqueColumnsValid()
        {
            if (rowCheck.Checked && !uniqueKeyColumnSelectedList.Any())
            {
                return false;
            }
            return true;
        }

        private bool AreSelectedColumnsValid()
        {
            if (rowCheckWithSelectedColumn.Checked)
            {
                var uniqueSheets = GetGroupNames(uniqueKeyColumnSelectedList);
                var selectedSheets = GetGroupNames(columnSelectedList);
                return uniqueSheets.All(selectedSheets.Contains) && uniqueSheets.Count == selectedSheets.Count;
            }
            return true;
        }

        private bool AreGroupColumnsValid()
        {
            if (groupCheck.Checked && !groupColumnsSelectedList.Any())
            {
                return false;
            }
            return true;
        }

        private bool AreDistributionColumnsValid()
        {
            if (distributionCheck.Checked && !distributionColumnsSelectedList.Any())
            {
                return false;
            }
            return true;
        }

        private bool AreUniqueAndSelectedColumnsUnique()
        {
            return columnSelectedList.Intersect(uniqueKeyColumnSelectedList).Count() == 0;
        }

        private Config GetConfig()
        {
            var config = new Config();
            if (columnCheck.Checked)
            {
                config.CheckColumns = true;
            }
            if (rowCheck.Checked)
            {
                config.CheckRows = true;
                config.UniqueKeys = GetSelectedColumns(uniqueKeyColumnSelectedList);
            }
            if (rowCheckWithSelectedColumn.Checked)
            {
                config.CheckOnSelectedColumns = true;
                config.UniqueKeys = GetSelectedColumns(uniqueKeyColumnSelectedList);
                config.SelectedColumns = GetSelectedColumns(columnSelectedList);
            }
            if (groupCheck.Checked)
            {
                config.CheckGroups = true;
                config.SelectedGroups = GetSelectedColumns(groupColumnsSelectedList);
            }
            if (distributionCheck.Checked)
            {
                config.CheckDistribution = true;
                config.SelectedDistributions = GetSelectedColumns(distributionColumnsSelectedList);
            }
            return config;
        }

        private List<ColumnGroup> GetSelectedColumns(List<Tuple<string, string>> selectedColumnList)
        {
            var groupNames = GetGroupNames(selectedColumnList);
            var columnGroups = new List<ColumnGroup>();
            foreach (var groupName in groupNames)
            {
                var columnGroup = new ColumnGroup();
                var foundColumns = selectedColumnList.Where(x => x.Item1 == groupName).Select(x => x.Item2).ToList();
                columnGroup.Sheet = groupName;
                columnGroup.Columns = foundColumns;
                columnGroups.Add(columnGroup);
            }
            return columnGroups;
        }

        private void rowCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (rowCheck.Checked)
            {
                rowSettingsTab.Parent = tabControl;
            }
            else if (!rowCheckWithSelectedColumn.Checked)
            {
                rowSettingsTab.Parent = null;
            }
        }

        private async Task PopulateUniqueColumnsList(string standardFilePath, string compareFilePath = null)
        {
            Logger.Log("PopulateUniqueColumnsList() start");
            uniqueKeyColumns.Clear();
            uniqueKeyColumnSelectedList.Clear();
            selectedColumns.Clear();
            columnSelectedList.Clear();
            selectedItemsColumns.Clear();
            selectedItems.Clear();
            groupColumns.Clear();
            groupColumnsSelectedList.Clear();
            distributionColumns.Clear();
            distributionColumnsSelectedList.Clear();
            waitRowSettings.Visible = true;
            waitColumnSettings.Visible = true;
            waitGroupSettings.Visible = true;
            waitDistributionSettings.Visible = true;

            uniqueKeyColumns.Columns.Add(new ColumnHeader() { Width = uniqueKeyColumns.Width });
            uniqueKeyColumns.HeaderStyle = ColumnHeaderStyle.None;
            uniqueKeyColumns.Dock = DockStyle.None;
            selectedColumns.Columns.Add(new ColumnHeader() { Width = selectedColumns.Width });
            selectedColumns.HeaderStyle = ColumnHeaderStyle.None;
            selectedColumns.Dock = DockStyle.None;
            groupColumns.Columns.Add(new ColumnHeader() { Width = groupColumns.Width });
            groupColumns.HeaderStyle = ColumnHeaderStyle.None;
            groupColumns.Dock = DockStyle.None;
            distributionColumns.Columns.Add(new ColumnHeader() { Width = distributionColumns.Width });
            distributionColumns.HeaderStyle = ColumnHeaderStyle.None;
            distributionColumns.Dock = DockStyle.None;

            List<ColumnGroup> columnGroups = new List<ColumnGroup>();
            if (!string.IsNullOrEmpty(compareFilePath))
            {
                columnGroups = await new ExcelHelper().GetColumnNames(standardFilePath, compareFilePath);
            }
            else
            {
                columnGroups = await new ExcelHelper().GetColumnNames(standardFilePath);
            }
            inputFileUniqueColumnList = columnGroups;
            foreach (var columnGroup in columnGroups)
            {
                // Adds a new group that has a left-aligned header
                ListViewGroup groupUnique = new ListViewGroup(columnGroup.Sheet, HorizontalAlignment.Left);
                ListViewGroup groupSelectedColumn = new ListViewGroup(columnGroup.Sheet, HorizontalAlignment.Left);
                ListViewGroup groupGroupColumn = new ListViewGroup(columnGroup.Sheet, HorizontalAlignment.Left);
                ListViewGroup groupDistributionColumn = new ListViewGroup(columnGroup.Sheet, HorizontalAlignment.Left);
                foreach (var commonColumn in columnGroup.Columns)
                {
                    uniqueKeyColumns.Items.Add(new ListViewItem(commonColumn, groupUnique));
                    selectedColumns.Items.Add(new ListViewItem(commonColumn, groupSelectedColumn));
                    groupColumns.Items.Add(new ListViewItem(commonColumn, groupGroupColumn));
                    distributionColumns.Items.Add(new ListViewItem(commonColumn, groupDistributionColumn));
                }
                uniqueKeyColumns.Groups.Add(groupUnique);
                selectedColumns.Groups.Add(groupSelectedColumn);
                groupColumns.Groups.Add(groupGroupColumn);
                distributionColumns.Groups.Add(groupDistributionColumn);
            }
            waitRowSettings.Visible = false;
            waitColumnSettings.Visible = false;
            waitGroupSettings.Visible = false;
            waitDistributionSettings.Visible = false;
            rowSettingsTab.Enabled = true;
            columnSettingsTab.Enabled = true;
            groupSettingsTab.Enabled = true;
            distributionSettingsTab.Enabled = true;
            Logger.Log("PopulateUniqueColumnsList() end");
        }

        private void uniqueKeyColumnsSearch_TextChanged(object sender, EventArgs e)
        {
            InitializeTimer(timerRowSettings);
        }

        private void AddSearchedItems(List<ColumnGroup> inputFileUniqueColumnList, ListView listView, string searchText)
        {
            listView.Clear();
            listView.Groups.Clear();
            nextGroupRowSetting = 0;
            nextItemIndexRowSetting = 0;
            nextGroupColumnSetting = 0;
            nextItemIndexColumnSetting = 0;
            nextGroupGroupSetting = 0;
            nextItemIndexGroupSetting = 0;

            listView.Columns.Add(new ColumnHeader() { Width = listView.Width });
            listView.HeaderStyle = ColumnHeaderStyle.None;
            listView.Dock = DockStyle.None;
            foreach (var item in inputFileUniqueColumnList)
            {
                ListViewGroup group = new ListViewGroup(item.Sheet, HorizontalAlignment.Left);
                foreach (var column in item.Columns)
                {
                    if (item.Sheet.IndexOf(searchText, StringComparison.InvariantCultureIgnoreCase) >= 0
                        || column.IndexOf(searchText, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        listView.Items.Add(new ListViewItem(column, group));
                        listView.Groups.Add(group);
                    }
                }
            }
        }

        private string GetSelectedValuesTextRepresentation(List<Tuple<string, string>> selectedList)
        {
            var groupNames = GetGroupNames(selectedList);
            string selectedColumns = "";
            foreach (var groupName in groupNames)
            {
                var foundTuples = selectedList.Where(x => x.Item1 == groupName).Select(x => x.Item2).ToList();
                var result = string.IsNullOrEmpty(selectedColumns) ? selectedColumns += groupName + ": " : selectedColumns += " " + groupName + ": ";
                selectedColumns += string.Join(", ", foundTuples);
            }
            return selectedColumns;
        }

        private List<string> GetGroupNames(List<Tuple<string, string>> itemListTuples)
        {
            return itemListTuples.Select(x => x.Item1).Distinct().ToList();
        }

        private void uniqueKeyColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            uniqueKeyColumnSelectedList = AddCheckedItems(e, uniqueKeyColumns, uniqueKeyColumnSelectedList);
            selectedItems.Text = GetSelectedValuesTextRepresentation(uniqueKeyColumnSelectedList);
        }

        private void OpenFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                Process.Start(filePath);
            }
        }

        private void rowCheckWithSelectedColumn_CheckedChanged(object sender, EventArgs e)
        {
            if (rowCheckWithSelectedColumn.Checked)
            {
                rowSettingsTab.Parent = tabControl;
                columnSettingsTab.Parent = tabControl;
            }
            else
            {
                columnSettingsTab.Parent = null;
                if (!rowCheck.Checked)
                {
                    rowSettingsTab.Parent = null;
                }
            }
        }

        private void selectedColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            columnSelectedList = AddCheckedItems(e, selectedColumns, columnSelectedList);
            selectedItemsColumns.Text = GetSelectedValuesTextRepresentation(columnSelectedList);
        }

        private List<Tuple<string, string>> AddCheckedItems(ItemCheckEventArgs e, ListView listView, List<Tuple<string, string>> selectedList)
        {
            foreach (var item in listView.CheckedItems)
            {
                selectedList.Add(new Tuple<string, string>(((ListViewItem)item).Group.Header, ((ListViewItem)item).Text));
            }

            if (e.NewValue == CheckState.Checked)
            {

                selectedList.Add(new Tuple<string, string>(listView.Items[e.Index].Group.Header, listView.Items[e.Index].Text));
            }
            else
            {
                selectedList.RemoveAll(x => x.Item1 == listView.Items[e.Index].Group.Header && x.Item2 == listView.Items[e.Index].Text);
            }
            selectedList = selectedList.Distinct().ToList();
            selectedList.Sort(new CustomColumnNameComparer(inputFileUniqueColumnList)); // sorts the selected list according to the sequence of input sheets
            return selectedList;
        }

        private void columnItemsSelectedSearch_TextChanged(object sender, EventArgs e)
        {
            InitializeTimer(timerColumnSettings);
        }

        private void GenerateCheckStates(ListView listView, List<Tuple<string, string>> selectedList)
        {
            for (int i = 0; i < listView.Items.Count; i++)
            {
                foreach (var selectedColumn in selectedList.ToList())
                {
                    if (selectedColumn.Item2.Contains(listView.Items[i].Text) && selectedColumn.Item1 == listView.Items[i].Group.Header)
                    {
                        listView.Items[i].Checked = true;
                    }
                }
            }
        }

        private void groupCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (groupCheck.Checked)
            {
                groupSettingsTab.Parent = tabControl;
            }
            else
            {
                groupSettingsTab.Parent = null;
            }
        }

        private void groupColumnsSearch_TextChanged(object sender, EventArgs e)
        {
            InitializeTimer(timerGroupSettings);
        }

        private void InitializeTimer(Timer timer)
        {
            timer.Stop();  // clears the previous timer
            timer.Start(); // starts a new timer
        }

        private void groupColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            groupColumnsSelectedList = AddCheckedItems(e, groupColumns, groupColumnsSelectedList);
            selectedGroupColumns.Text = GetSelectedValuesTextRepresentation(groupColumnsSelectedList);
        }

        private void timerColumnSettings_Tick(object sender, EventArgs e)
        {
            // stops the timer after 250 ms & starts searching
            timerColumnSettings.Stop();
            AddSearchedItems(inputFileUniqueColumnList, selectedColumns, columnItemsSelectedSearch.Text.Trim());
            GenerateCheckStates(selectedColumns, columnSelectedList);
        }

        private void timeGroupSettings_Tick(object sender, EventArgs e)
        {
            // stops the timer after 250 ms & starts searching
            timerGroupSettings.Stop();
            AddSearchedItems(inputFileUniqueColumnList, groupColumns, groupColumnsSearch.Text.Trim());
            GenerateCheckStates(groupColumns, groupColumnsSelectedList);
        }

        private void timerRowSettings_Tick(object sender, EventArgs e)
        {
            // stops the timer after 250 ms & starts searching
            timerRowSettings.Stop();
            AddSearchedItems(inputFileUniqueColumnList, uniqueKeyColumns, uniqueKeyColumnsSearch.Text.Trim());
            GenerateCheckStates(uniqueKeyColumns, uniqueKeyColumnSelectedList);
        }

        private void uniqueKeyColumns_MouseClick(object sender, MouseEventArgs e)
        {
            CheckItemOnLabelClick(uniqueKeyColumns, e);
        }

        private void selectedColumns_MouseClick(object sender, MouseEventArgs e)
        {
            CheckItemOnLabelClick(selectedColumns, e);
        }

        private void groupColumns_MouseClick(object sender, MouseEventArgs e)
        {
            CheckItemOnLabelClick(groupColumns, e);
        }

        private void CheckItemOnLabelClick(ListView listView, MouseEventArgs e)
        {
            var clickLocation = listView.HitTest(e.Location);

            if (clickLocation.Location == ListViewHitTestLocations.Label)
            {
                clickLocation.Item.Checked = !clickLocation.Item.Checked;
            }
        }

        /// <summary>
        /// Gets the next sheet to navigate
        /// </summary>
        /// <param name="nextGroup"></param>
        /// <param name="listView"></param>
        /// <returns></returns>
        private int GetNextGroup(int nextGroup, ListView listView)
        {
            if (nextGroup < listView.Groups.Count - 1)
            {
                nextGroup++;
            }
            else nextGroup = 0;
            return nextGroup;
        }

        /// <summary>
        /// Sets the next group on view and returns next item index
        /// </summary>
        /// <param name="nextGroup"></param>
        /// <param name="nextItemIndex"></param>
        /// <param name="listView"></param>
        /// <returns></returns>
        private int SetVisibleGroup(int nextGroup, int nextItemIndex, ListView listView)
        {
            if (nextGroup == 0)
            {
                nextItemIndex = 0;
            }
            else
            {
                nextItemIndex += listView.Groups[nextGroup - 1].Items.Count;
            }
            listView.EnsureVisible(nextItemIndex);
            return nextItemIndex;
        }

        /// <summary>
        /// Event handler for navigating to the next sheets on row settings tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextForRowSettings_Click(object sender, EventArgs e)
        {
            nextGroupRowSetting = GetNextGroup(nextGroupRowSetting, uniqueKeyColumns);
            nextItemIndexRowSetting = SetVisibleGroup(nextGroupRowSetting, nextItemIndexRowSetting, uniqueKeyColumns);
        }

        /// <summary>
        /// Event handler for navigating to the next sheets on column settings tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextForColumnSetting_Click(object sender, EventArgs e)
        {
            nextGroupColumnSetting = GetNextGroup(nextGroupColumnSetting, selectedColumns);
            nextItemIndexColumnSetting = SetVisibleGroup(nextGroupColumnSetting, nextItemIndexColumnSetting, selectedColumns);
        }

        /// <summary>
        /// Event handler for navigating to the next sheets on group settings tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextForGroupSetting_Click(object sender, EventArgs e)
        {
            nextGroupGroupSetting = GetNextGroup(nextGroupGroupSetting, groupColumns);
            nextItemIndexGroupSetting = SetVisibleGroup(nextGroupGroupSetting, nextItemIndexGroupSetting, groupColumns);
        }

        /// <summary>
        /// Populates the column list when group check configuration is selected and only the standard file is provided
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void groupCheck_CheckStateChanged(object sender, EventArgs e)
        {
            if (groupCheck.CheckState == CheckState.Checked && distributionCheck.CheckState == CheckState.Unchecked
                && string.IsNullOrEmpty(compareFilePath.Text))
            {
                await PopulateUniqueColumnsList(standardFile.FileName);
            }
        }

        /// <summary>
        /// Populates the column list when distribution check configuration is selected and only the standard file is provided
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void distributionCheck_CheckStateChanged(object sender, EventArgs e)
        {
            if (distributionCheck.CheckState == CheckState.Checked && groupCheck.CheckState == CheckState.Unchecked
                && string.IsNullOrEmpty(compareFilePath.Text))
            {
                await PopulateUniqueColumnsList(standardFile.FileName);
            }
        }

        private void distributionCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (distributionCheck.Checked)
            {
                distributionSettingsTab.Parent = tabControl;
            }
            else
            {
                distributionSettingsTab.Parent = null;
            }
        }

        private void distributionColumnsSearch_TextChanged(object sender, EventArgs e)
        {
            InitializeTimer(timerDistributionSettings);
        }

        private void timerDistributionSettings_Tick(object sender, EventArgs e)
        {
            // stops the timer after 250 ms & starts searching
            timerDistributionSettings.Stop();
            AddSearchedItems(inputFileUniqueColumnList, distributionColumns, distributionColumnsSearch.Text.Trim());
            GenerateCheckStates(distributionColumns, distributionColumnsSelectedList);
        }

        private void nextForDistributionSetting_Click(object sender, EventArgs e)
        {
            nextGroupDistributionSetting = GetNextGroup(nextGroupDistributionSetting, distributionColumns);
            nextItemIndexDistributionSetting = SetVisibleGroup(nextGroupDistributionSetting, nextItemIndexDistributionSetting,
                distributionColumns);
        }

        private void distributionColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            distributionColumnsSelectedList = AddCheckedItems(e, distributionColumns, distributionColumnsSelectedList);
            selectedDistributionColumns.Text = GetSelectedValuesTextRepresentation(distributionColumnsSelectedList);
        }

        private void distributionColumns_MouseClick(object sender, MouseEventArgs e)
        {
            CheckItemOnLabelClick(distributionColumns, e);
        }

        /// <summary>
        /// Clears the standard file text field and resets to default configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearStandard_Click(object sender, EventArgs e)
        {
            standardFilePath.Clear();
            standardFile.Reset();
            reportFolderPath.Clear();
            ResetConfiguration(false);
            groupCheck.Enabled = false;
            distributionCheck.Enabled = false;
            clearStandard.Enabled = false;
        }

        /// <summary>
        /// Clears the compare file text field and resets to default configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearCompare_Click(object sender, EventArgs e)
        {
            compareFilePath.Clear();
            compareFile.Reset();
            ResetConfiguration(true);
            clearCompare.Enabled = false;
        }

        /// <summary>
        /// Resets default configuration on clear button click
        /// </summary>
        private void ResetConfiguration(bool isCompare)
        {
            if (!isCompare)
            {
                columnCheck.CheckState = CheckState.Unchecked;
                columnCheck.Enabled = false;
            }
            rowCheck.CheckState = CheckState.Unchecked;
            rowCheck.Enabled = false;
            rowCheckWithSelectedColumn.CheckState = CheckState.Unchecked;
            rowCheckWithSelectedColumn.Enabled = false;
            groupCheck.CheckState = CheckState.Unchecked;
            distributionCheck.CheckState = CheckState.Unchecked;
            uniqueKeyColumns.Clear();
            uniqueKeyColumns.Groups.Clear();
            selectedColumns.Clear();
            selectedColumns.Groups.Clear();
            groupColumns.Clear();
            groupColumns.Groups.Clear();
            distributionColumns.Clear();
            distributionColumns.Groups.Clear();
            selectedItems.Clear();
            selectedItemsColumns.Clear();
            selectedGroupColumns.Clear();
            selectedDistributionColumns.Clear();
            uniqueKeyColumnsSearch.Clear();
            columnItemsSelectedSearch.Clear();
            groupColumnsSearch.Clear();
            distributionColumnsSearch.Clear();
        }
    }
}
