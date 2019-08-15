using Documents.Adapter;
using Documents.Excel.Worksheets;
using Documents.Exceptions;
using Documents.Extensions;
using Documents.Metadata;
using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Documents.Excel
{
    public abstract class ExcelDocumentBase<T>
        where T: class, new()
    {
        DocumentAdapter<T> DocumentAdapter;

        public ExcelDocumentBase(DocumentAdapter<T> adapter)
        {
            DocumentAdapter = adapter;
            
        }
        
        public int GetColumnLength()
        {

            int columnCounter = 0;
            foreach (ColumnMetadata column in DocumentAdapter.Columns)
            {
                
                if (column.ColumnType == ColumnType.Normal)
                {
                    
                    columnCounter++;
                }
                else if (column.ColumnType == ColumnType.MultiColumn)
                {
                    MultiColumnMetadata multiColumn = column as MultiColumnMetadata;
                    columnCounter += multiColumn.Headers.Count;
                }
            }
            return columnCounter;
        }

        protected abstract string GetErrorText(object obj, PropertyInfo propertyInfo);

        public List<T> Parse(Workbook template)
        {
            List<T> objects = new List<T>();
            bool hasNewRow = true;
            int rowCounter = 2;
            Worksheet worksheet = template.Worksheets[0];
            int colLength = GetColumnLength();
            while (hasNewRow)
            {
                int columnCounter = 0;
                T obj = new T();
                bool isEmpty = true;
                for (int i = 0; i < colLength; i++)
                {
                    if (worksheet[rowCounter, i].Value.IsEmpty == false)
                    {
                        isEmpty = false;
                        break;
                    }
                }
                if (isEmpty) break;

                try
                {
                    foreach (ColumnMetadata column in DocumentAdapter.Columns)
                    {
                        if (column.ColumnType == ColumnType.Normal)
                        {
                            int currentColumn = columnCounter++;
                            CellValue value = worksheet[rowCounter, currentColumn].Value;

                            column.PropertyInfo.SetValue(obj, column.CellEditor.Parse(value, column.PropertyInfo.PropertyType));
                            
                            string error = GetErrorText(obj, column.PropertyInfo);
                            if (!String.IsNullOrEmpty(error))
                                throw new ExcelParseException(currentColumn, rowCounter, error);

                            if(column.PropertyInfo2 != null)
                            {
                                column.PropertyInfo2.SetValue(obj, column.CellEditor.Parse(value, column.PropertyInfo2.PropertyType));
                                error = GetErrorText(obj, column.PropertyInfo2);
                                if (!String.IsNullOrEmpty(error))
                                    throw new ExcelParseException(currentColumn, rowCounter, error);
                            }
                        }
                        else if (column.ColumnType == ColumnType.MultiColumn)
                        {
                            MultiColumnMetadata multiColumn = column as MultiColumnMetadata;
                            IDictionary<string, string> dict = new Dictionary<string, string>();
                            for (int i = 0; i < multiColumn.Headers.Count; i++)
                            {
                                CellValue value = worksheet[rowCounter, columnCounter++].Value;
                                
                                dict.Add(multiColumn.Headers[i], column.CellEditor.Parse<string>(value));
                            }
                            if(multiColumn.PropertyInfo.PropertyType == typeof(IDictionary<string, string>))
                                multiColumn.PropertyInfo.SetValue(obj, dict);
                            else
                            {
                                var dictObj = multiColumn.PropertyInfo.GetValue(obj);
                                if (dictObj != null)
                                    DictionaryExtensions.SetDictionary((IDictionary<string, string>)multiColumn.PropertyInfo.GetValue(obj), dict);
                                else
                                {
                                    object newObj = Activator.CreateInstance(multiColumn.PropertyInfo.PropertyType);
                                    DictionaryExtensions.SetDictionary((IDictionary<string, string>)newObj, dict);
                                    multiColumn.PropertyInfo.SetValue(obj, newObj);
                                }
                            }

                            string error = GetErrorText(obj, column.PropertyInfo);
                            if (!String.IsNullOrEmpty(error))
                                throw new ExcelParseException(columnCounter, rowCounter, error);
                        }
                    }
                }
                catch(ExcelParseException e)
                {
                    throw e;
                }
                catch
                {
                    throw new ExcelParseException(columnCounter, rowCounter);
                }
                rowCounter++;
                objects.Add(obj);
                
            }
            return objects;
        }

        public Workbook GetTemplate()
        {
            Workbook workbook = new Workbook();
            workbook.CreateNewDocument();
            ListWorksheet listWorksheet = new ListWorksheet(workbook.Worksheets.Add());
            
            var worksheet = workbook.Worksheets[0];
            worksheet.Rows.Insert(0, 2);

            int columnCounter = 0;
            foreach(ColumnMetadata column in DocumentAdapter.Columns)
            {
                column.CellEditor.Init(worksheet, listWorksheet, worksheet.Range.FromLTRB(columnCounter, 2, columnCounter, 100));
                
                if (column.ColumnType == ColumnType.Normal)
                {
                    worksheet[0, columnCounter].SetValue(column.Name);
                    worksheet[0, columnCounter].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                    worksheet[0, columnCounter].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                    worksheet[0, columnCounter].AutoFitColumns();
                    worksheet.MergeCells(worksheet.Range.FromLTRB(columnCounter, 0, columnCounter, 1));
                    columnCounter++;
                }
                else if(column.ColumnType == ColumnType.MultiColumn)
                {
                    MultiColumnMetadata multiColumn = column as MultiColumnMetadata;
                    worksheet[0, columnCounter].SetValue(column.Name);
                    worksheet[0, columnCounter].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                    worksheet[0, columnCounter].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                    worksheet[0, columnCounter].AutoFitColumns();

                    worksheet.MergeCells(worksheet.Range.FromLTRB(columnCounter, 0, columnCounter + multiColumn.Headers.Count-1, 0));
                    for(int i = 0; i < multiColumn.Headers.Count; i++)
                    {
                        worksheet[1, columnCounter].SetValue(multiColumn.Headers[i]);
                        worksheet[1, columnCounter].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                        worksheet[1, columnCounter].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                        worksheet[1, columnCounter].AutoFitColumns();
                        columnCounter++;
                    }
                }
            }
            return workbook;
        }
    }
}
