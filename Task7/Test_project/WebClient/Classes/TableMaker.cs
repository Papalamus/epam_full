using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace WebClient.Classes
{
    public class TableMaker
    {

        public static Table MakeTable(List<object> toDisplay,Table tbl)
        {
            //PlaceHolder1.Controls.Clear();

            // Fetch the number of Rows and Columns for the table 
            // using the properties
            //int tblRows = Rows;
            //int tblCols = Columns;
            // Create a Table and set its properties 
            //Table tbl = new Table();
            // Add the table to the placeholder control
            //PlaceHolder1.Controls.Add(tbl);
            // Now iterate through the table and add your controls 

            tbl.CellSpacing = 10;
            tbl.CellPadding = 10;
            Type displayedObjecType = toDisplay[0].GetType();
            tbl.Rows.Add(CreateHeader(displayedObjecType));

            foreach (var o in toDisplay)
            {
                TableRow tr = CreateRow(o);
                tbl.Rows.Add(tr);

            // This parameter helps determine in the LoadViewState event,
            // whether to recreate the dynamic controls or not

            //ViewState["dynamictable"] = true;
            }
            return tbl;

        }

        private static TableRow CreateHeader(Type type)
        {
            TableRow result = new TableRow();

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var fieldInfo in fields)
            {
                TableCell cell = new TableCell();
                cell.Text = fieldInfo.Name;
                result.Cells.Add(cell);
            }

            var properties =
                type.GetProperties(BindingFlags.Public  | BindingFlags.Instance);

            foreach (var propertyInfo in properties)
            {
                TableCell cell = new TableCell();
                cell.Text = propertyInfo.Name;
                result.Cells.Add(cell);
            }
            return result;
        }

        private static TableRow CreateRow(object value)
        {
            TableRow result = new TableRow();
            Type type = value.GetType();
            var fields = type.GetFields(BindingFlags.Public  | BindingFlags.Instance);

            foreach (var fieldInfo in fields)
            {
                if (fieldInfo.FieldType.IsArray)
                {
                    //tODO добавить ссылку знать бы как
                    TableCell newCell = new TableCell();
                    object val = "";
                    result.Cells.Add(CreateCell(val.ToString()));

                }
                else
                {
                    TableCell newCell = new TableCell();

                    object val = fieldInfo.GetValue(value) ?? "";
                    result.Cells.Add(CreateCell(val.ToString()));
                }
            }

            var properties =
                type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.PropertyType.IsArray)
                {
                    //tODO добавить ссылку знать бы как
                    TableCell newCell = new TableCell();
                    object val = "";
                    result.Cells.Add(CreateCell(val.ToString()));

                }
                else
                {
                    TableCell newCell = new TableCell();
                    object val = propertyInfo.GetValue(value)?? "";
                    result.Cells.Add(CreateCell(val.ToString()));
                }
            }
            //TableCell proba = new TableCell();
            //proba.Text = "Peka";
            //result.Cells.Add(proba);
            return result;
        }

        private static TableCell CreateCell(string text)
        {
            TableCell newCell = new TableCell();

            newCell.Text = text;
            newCell.HorizontalAlign = HorizontalAlign.Center;

            return newCell;
        }


    }
}