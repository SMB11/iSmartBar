using Documents.Editors;
using Documents.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Documents.Adapter
{
    public class DocumentAdapter<T>
        where T: class
    {

        private List<ColumnMetadata> columns = new List<ColumnMetadata>();

        public ReadOnlyCollection<ColumnMetadata> Columns
        {
            get
            {
                return columns.AsReadOnly();
            }
        }

        public bool HasMultiColumn { get; private set; }

        public DocumentAdapter<T> Column<TProperty>(Expression<Func<T, TProperty>> expression, string name, CellEditor cellEditor = null)
        {
            PropertyInfo propertyInfo = GetPropertyInfo(expression);
            NormalColumnMetadata colinfo = new NormalColumnMetadata(propertyInfo, name);
            if (cellEditor != null) colinfo.CellEditor = cellEditor;
            columns.Add(colinfo);
            return this;
        }

        public DocumentAdapter<T> Column<TProperty1, TProperty2>(Expression<Func<T, TProperty1>> expression, Expression<Func<T, TProperty2>> expression2, string name, CellEditor cellEditor = null)
        {
            PropertyInfo propertyInfo1 = GetPropertyInfo(expression);
            PropertyInfo propertyInfo2 = GetPropertyInfo(expression2);
            NormalColumnMetadata colinfo = new NormalColumnMetadata(propertyInfo1, propertyInfo2, name);
            if (cellEditor != null) colinfo.CellEditor = cellEditor;
            columns.Add(colinfo);
            return this;
        }

        public DocumentAdapter<T> MultiColumn<TProperty>(Expression<Func<T, TProperty>> expression, string name, List<string> headers, CellEditor cellEditor = null)
        {
            PropertyInfo propertyInfo = GetPropertyInfo(expression);
            Type propType = propertyInfo.PropertyType;
            bool isDict = typeof(IDictionary<string, string>).IsAssignableFrom(propType);
            if (!isDict) throw new ArgumentException(String.Format("Property Type {0} is not supported for MultiColumn at this time.", propType.ToString()));

            MultiColumnMetadata colinfo = new MultiColumnMetadata(propertyInfo, name, headers);
            if (cellEditor != null) colinfo.CellEditor = cellEditor;
            columns.Add(colinfo);
            HasMultiColumn = true;
            return this;
        }

        private static PropertyInfo GetPropertyInfo<TSource, TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda.ToString()));

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda.ToString()));

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));

            return propInfo;
        }
    }
}
