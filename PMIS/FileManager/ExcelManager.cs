﻿using DocumentFormat.OpenXml.Packaging;
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

namespace WSM.WindowsServices.FileManager
{
    public class ExcelManager : FileInterface
    {

        /*
            //using dialog box to open the excel file and giving the extension as .xlsx to open the excel files
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xlsx";
            openfile.Filter = "(.xlsx)|*.xlsx";

            ///show open file dialog box 
            bool? result = openfile.ShowDialog();
            ///process open file dialog box results
            if (result == true)
            {
               //copying the path of the excel file to a textbox named txtFilePath
               string txtFilePath = openfile.FileName;
               //Add reference then select Microsoft .Office.Interop.Excel

            }
        */

        [TestMethod]
        public static object Read(string fileName)
        {
            try
            {
                // ایجاد یک رشته اتصال برای باز کردن فایل اکسل
                // استفاده از پارامتر HDR برای نادیده گرفتن سطر اول فایل اکسل که عناوین را نشان می‌دهد
                var connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={fileName};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";

                var worksheetString = "";
                // به دست آوردن نام اولین شیت
                using (var pck = new ExcelPackage(new FileInfo(fileName)))
                {
                    var workbook = pck.Workbook;
                    var worksheet = workbook.Worksheets[0];
                    worksheetString = worksheet.Name;
                }

                // ایجاد یک شیء از کلاس DataTable برای نگهداری داده‌های خوانده شده از فایل اکسل
                var dataTable = new DataTable();

                // ایجاد یک شیء از کلاس OleDbConnection برای اتصال به فایل اکسل
                using (var connection = new OleDbConnection(connectionString))
                {
                    // باز کردن اتصال
                    connection.Open();

                    // ایجاد یک شیء از کلاس OleDbCommand برای اجرای یک پرس و جو بر روی فایل اکسل
                    // انتخاب همه سطرها و ستون‌های کاربرگ اول فایل اکسل
                    using (var command = new OleDbCommand("SELECT * FROM [" + worksheetString + "$]", connection))
                    {
                        // ایجاد یک شیء از کلاس OleDbDataAdapter برای پر کردن یک جدول داده با نتیجه پرس و جو
                        using (var adapter = new OleDbDataAdapter(command))
                        {
                            // پر کردن جدول داده با استفاده از شیء OleDbDataAdapter
                            adapter.Fill(dataTable);
                        }
                    }

                    // بستن اتصال
                    connection.Close();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public static bool Write(string fileName, object fileContent)
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
                    foreach (DataTable dataTable in ((DataSet)fileContent).Tables)
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
        public static bool Write(string fileName, List<DataGridView> fileContent)
        {
            try
            {
                return Write(fileName, ConvertDataGridViewListToDataSet(fileContent));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public static DataSet ConvertDataGridViewListToDataSet(List<DataGridView> dgvList)
        {
            try
            {
                DataSet dataSet = new DataSet();
                foreach (DataGridView dgv in dgvList)
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
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}