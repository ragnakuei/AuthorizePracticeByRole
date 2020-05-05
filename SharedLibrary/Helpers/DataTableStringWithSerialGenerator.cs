using System;
using System.Collections.Generic;
using System.Data;

namespace SharedLibrary.Helpers
{
    /// <summary>
    /// DataTable 只有單一欄位 string
    /// </summary>
    public class DataTableStringWithSerialGenerator
    {
        private readonly DataTable                _result;
        
        public           DataTable                Result => _result;

        private int _serialNo = 1;

        public DataTableStringWithSerialGenerator(string columnName, bool isNullableType = true)
        {
            _result = new DataTable();

            var column = new DataColumn();
            column.ColumnName  = columnName;
            column.AllowDBNull = isNullableType;
            column.DataType    = typeof(string);
            _result.Columns.Add(column);
            
            var serialColumn = new DataColumn();
            serialColumn.AutoIncrement = true;
            serialColumn.AutoIncrementSeed = 1;
            serialColumn.AutoIncrementStep = 1;
            serialColumn.ColumnName  = "Serial";
            serialColumn.AllowDBNull = false;
            serialColumn.DataType    = typeof(long);
            _result.Columns.Add(serialColumn);
        }

        public void Add(string rowData)
        {
            var row = _result.NewRow();
            row[0] = rowData;
            _result.Rows.Add(row);
        }

        public void AddRange(IEnumerable<string> rowData)
        {
            foreach (var row in rowData)
            {
                Add(row);
            }
        }
    }
}