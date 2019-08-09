using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ChangeTracking
{

    /// <summary>
    /// A wrapper over any class to make it Restorable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Memento<T>
    {
        #region Private

        /// <summary>
        /// 
        /// </summary>
        Dictionary<PropertyInfo, object> storedProperties =
                   new Dictionary<PropertyInfo, object>();

        #endregion

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
