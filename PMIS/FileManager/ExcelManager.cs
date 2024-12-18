using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;
using System.Data.OleDb;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml;
using System.Xml;
using System.Globalization;
using WSM.WindowsServices.FileManager.Interfaces;
using Castle.Components.DictionaryAdapter.Xml;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;
using System.Windows.Forms;

namespace WSM.WindowsServices.FileManager
{
    public class ExcelManager : FileInterface
    {
        [TestMethod]
        private static DataSet Read(string fileName)
        {
            try
            {
                DataSet dataSet = new DataSet();
                var connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={fileName};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";

                using (var connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (var pck = new ExcelPackage(new FileInfo(fileName)))
                    {
                        var workbook = pck.Workbook;
                        foreach (var worksheet in workbook.Worksheets)
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.TableName = worksheet.Name;

                            using (var command = new OleDbCommand("SELECT * FROM [" + dataTable.TableName + "$]", connection))
                            {
                                using (var adapter = new OleDbDataAdapter(command))
                                {
                                    adapter.Fill(dataTable);
                                }
                            }
                            dataSet.Tables.Add(dataTable);
                        }
                    }
                    connection.Close();
                }
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public static object Read<T>(string fileName) where T : class
        {
            try
            {
                DataSet dataSet = Read(fileName);
                if (typeof(T) == typeof(DataSet))
                {
                    return dataSet;
                }
                else if (typeof(T) == typeof(List<DataGridView>))
                {
                    return ConvertDataSetToDataGridViewList(dataSet);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public static List<DataGridView> ConvertDataSetToDataGridViewList(DataSet dataSet)
        {
            try
            {
                List<DataGridView> dgvList = new List<DataGridView>();
                foreach (DataTable dataTable in dataSet.Tables)
                {
                    DataGridView dgv = new DataGridView();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        int temp = dgv.Columns.Add(column.ColumnName, column.ColumnName);
                    }
                    foreach (DataRow row in dataTable.Rows)
                    {
                        dgv.Rows.Add(row);
                    }
                    dgvList.Add(dgv);
                }
                return dgvList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        private static bool Write(string fileName, DataSet fileContent)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                //Create a file
                var fileInfo = new FileInfo(fileName);
                if (fileInfo.Exists)
                    fileInfo.Delete();

                using (var pck = new ExcelPackage(fileInfo))
                {
                    var workbook = pck.Workbook;
                    foreach (DataTable dataTable in (fileContent).Tables)
                    {
                        var worksheet = workbook.Worksheets.Add(dataTable.TableName);
                        worksheet.View.RightToLeft = true;
                        worksheet.Cells.LoadFromDataTable(dataTable, true);
                    }
                    pck.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public static bool Write<T>(string fileName, T fileContent) where T : class
        {
            try
            {
                if (typeof(T) == typeof(DataSet))
                {
                    return Write(fileName, fileContent);
                }
                else if (typeof(T) == typeof(List<DataGridView>))
                {
                    return Write(fileName, ConvertDataGridViewListToDataSet(fileContent));
                }
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public static DataSet ConvertDataGridViewListToDataSet(object dgvList)
        {
            try
            {
                DataSet dataSet = new DataSet();
                if (dgvList.GetType() == typeof(List<DataGridView>))
                {
                    foreach (DataGridView dgv in (List<DataGridView>)dgvList)
                    {
                        if (dgv.DataSource != null)
                        {
                            DataTable dataTable = new DataTable();
                            if (dgv.DataSource.GetType().GetGenericArguments().Count() > 0)
                            {
                                string? nameSpace = dgv.DataSource.GetType().GetGenericArguments()[0].Namespace;
                                dataTable.TableName = nameSpace?.Substring(nameSpace.LastIndexOf('.') + 1);
                            }
                            foreach (DataGridViewColumn column in dgv.Columns)
                            {
                                if (column.Visible)
                                {
                                    if (column is not DataGridViewButtonColumn && !column.Name.StartsWith("Vrt"))
                                    {
                                        dataTable.Columns.Add(column.HeaderText);
                                    }
                                }
                            }
                            foreach (DataGridViewRow row in dgv.Rows)
                            {
                                DataRow dRow = dataTable.NewRow();
                                foreach (DataGridViewColumn column in dgv.Columns)
                                {
                                    if (column.Visible)
                                    {
                                        if (column is not DataGridViewButtonColumn && !column.Name.StartsWith("Vrt"))
                                        {
                                            if (column is DataGridViewComboBoxColumn)
                                            {
                                                dRow[column.HeaderText] = row.Cells[column.Name].FormattedValue;
                                            }
                                            else
                                            {
                                                dRow[column.HeaderText] = row.Cells[column.Name].Value;
                                            }
                                        }
                                    }
                                }
                                dataTable.Rows.Add(dRow);
                            }
                            dataSet.Tables.Add(dataTable);
                        }
                    }
                }
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}