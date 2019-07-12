using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Editable
{
    public class Memento<T>
    {
        Dictionary<PropertyInfo, object> storedProperties =
                   new Dictionary<PropertyInfo, object>();

        public Memento(T originator)
        {
            var propertyInfos =
                typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                       .Where(p => p.CanRead && p.CanWrite);

            foreach (var property in propertyInfos)
            {
                this.storedProperties[property] = property.GetValue(originator, null);
            }
        }

        public void Restore(T originator)
        {
            foreach (var pair in this.storedProperties)
            {
                if(typeof(IEditableObject).IsAssignableFrom(pair.Key.PropertyType) && pair.Value != null)
                {
                    IEditableObject editableObject = (IEditableObject)pair.Value;
                    editableObject?.CancelEdit();
                }
                else
                {
                    pair.Key.SetValue(originator, pair.Value, null);
                }
            }
        }
    }
}
