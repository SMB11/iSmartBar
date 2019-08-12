using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    /// <summary>
    /// Adds features that help a class manage its disposables better
    /// </summary>
    public interface IDisposableContainer: IDisposable
    {
        /// <summary>
        /// Registers an object as a disposable that will be disposed of 
        /// when the  Dispose method of the class is called
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        T Disposable<T>(T obj) where T: IDisposable;
    }
}
