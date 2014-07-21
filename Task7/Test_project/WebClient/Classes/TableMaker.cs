using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using Test_project.Reflection;

namespace WebClient.Classes
{
    public class TableMaker
    {
        private Type _objectsType;
        private FieldInfo[] _fieldInfos;
        private PropertyInfo[] _propertyInfos;

        public TableMaker(Type objectsType)
        {
            _objectsType = objectsType;
            _fieldInfos = _objectsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            _propertyInfos = _objectsType.GetProperties(BindingFlags.Public|BindingFlags.Instance);
        }

        public TableRow CreateRow(object displayedObj, Func<MemberInfo,object, TableCell> createRegularCell,
            Func<MemberInfo,object, TableCell> createArrayCell)
        {
            TableRow result = new TableRow();
            foreach (var fieldInfo in _fieldInfos)
            {
                TableCell newCell;
                if (fieldInfo.FieldType.IsArray)
                {
                    newCell = createArrayCell(fieldInfo,displayedObj);
                }
                else
                {
                    newCell = createRegularCell(fieldInfo,displayedObj);
                }
                result.Cells.Add(newCell);
            }

            foreach (var propertyInfo in _propertyInfos)
            {
                TableCell newCell;
                if (propertyInfo.PropertyType.IsArray)
                {
                    //tODO добавить ссылку знать бы как
                    newCell = createArrayCell(propertyInfo, displayedObj);
                }
                else
                {
                    newCell = createRegularCell(propertyInfo, displayedObj);
                }
                result.Cells.Add(newCell);
            }
            return result;
        }

        public Table MakeTable(List<object> toDisplay,Table tbl)
        {
           

            tbl.CellSpacing = 10;
            tbl.CellPadding = 10;
            Type displayedObjecType = toDisplay[0].GetType();
            tbl.Rows.Add(CreateHeader(displayedObjecType));

            foreach (var o in toDisplay)
            {
                TableRow tr = CreateRow(o);
                tbl.Rows.Add(tr);

            }
            return tbl;

        }

        private  TableRow CreateHeader(Type type)
        {
            TableRow result = CreateRow(_objectsType, NameCell, NameCell);
            return result;
        }

        private TableRow CreateRow(object value)
        {
            TableRow result = CreateRow(value, ValueCell, ValueCell);
            return result;
        }

        private TableCell ValueCell(MemberInfo memberInfo,object obj )
        {
            TableCell newCell = new TableCell();
            object value = memberInfo.GetValue(obj) ?? "NULL";
            newCell.Text = value.ToString();
            newCell.HorizontalAlign = HorizontalAlign.Center;
            return newCell;
        }

        private TableCell LinkCell(MemberInfo memberInfo, object obj)
        {
            TableCell newCell = new TableCell();
            object value = memberInfo.GetValue(obj) ?? "NULL";
            newCell.Text = value.ToString();
            newCell.HorizontalAlign = HorizontalAlign.Center;
            return newCell;
        }

        private TableCell NameCell(MemberInfo memberInfo, object obj)
        {
            TableCell newCell = new TableCell();
            string text = memberInfo.Name;
            newCell.Text = text;
            newCell.HorizontalAlign = HorizontalAlign.Center;
            return newCell;
        }

        //private  TableCell CreateCell(string text)
        //{
        //    TableCell newCell = new TableCell();

        //    newCell.Text = text;
        //    newCell.HorizontalAlign = HorizontalAlign.Center;

        //    return newCell;
        //}


    }
}