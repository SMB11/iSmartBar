using Facade.Repository;
using iSmartBar.Repositories.LinqToDB;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace iSmartBar.Repositories.Base
{
    /// <summary>
    /// Base class for all repositories that 
    /// implements Insert, Update, Remove and Select operations for Linq2DB
    /// inteneded to be instanciated for every request.
    /// </summary>
    /// <typeparam name="T">The entity class/DB model that this repository works with</typeparam>
    /// <typeparam name="R">The repository class that inherits RepositoryBase. Example usage: public class ProductRepository&lt;ProductModel, ProductRepository&gt;</typeparam>
    public abstract class CompositeRepositoryBase<T, K1, K2, R> : ICompositeRepository<T, K1, K2>
        where T : class
        where R : class
    {
        // a list that keeps track of all LoadWith requests
        List<Expression<Func<T, object>>> LoadWithList = new List<Expression<Func<T, object>>>();
        // the offset to be used for the next select
        int _offset;
        // the limit to be used for the next select
        int _limit;
        // the order by predicate to be used for the next select
        Func<T, object> _orderBy;
        // specifies ascending or desending select
        bool _ascending;

        /// <summary>
        /// Forces the next Select to laod entities with the selected nested / foreign entity
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public R LoadWith(Expression<Func<T, object>> exp)
        {
            LoadWithList.Add(exp);
            return this as R;
        }


        /// <summary>
        /// Sets the offset for the next select.
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public R Offset(int offset)
        {
            _offset = offset;
            return this as R;
        }

        /// <summary>
        /// Sets the limit for the next select.
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public R Limit(int limit)
        {
            _limit = limit;
            return this as R;
        }

        private Func<T, object> GetOrderByExpression(string sortColumn)
        {
            Func<T, object> orderByExpr = null;
            if (!String.IsNullOrEmpty(sortColumn))
            {
                Type sponsorResultType = typeof(T);

                if (sponsorResultType.GetProperties().Any(prop => prop.Name == sortColumn))
                {
                    System.Reflection.PropertyInfo pinfo = sponsorResultType.GetProperty(sortColumn);
                    orderByExpr = (data => pinfo.GetValue(data, null));
                }
            }
            return orderByExpr;
        }

        public R OrderBy(string orderBy, bool ascending = true)
        {
            _ascending = ascending;
            _orderBy = GetOrderByExpression(orderBy);
            return this as R;
        }

        private ITable<T> GetTable(ISmartBarDB context)
        {
            ITable<T> table = TableExpression.Compile()(context);
            foreach (var exp in LoadWithList)
            {
                table = table.LoadWith(exp);
            }
            return table;
        }


        /// <summary>
        /// Inserts the specified range into the database
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public long InsertRange(List<T> list)
        {
            using (ISmartBarDB context = new ISmartBarDB())
            {
                return context.BulkCopy(list).RowsCopied;
            }
        }

        /// <summary>
        /// Inserts the specified range into the database asynchronously.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<long> InsertRangeAsync(List<T> list, CancellationToken token = new CancellationToken())
        {
            return Task.Run(() => InsertRange(list));
        }

        /// <summary>
        /// Executes the select operation with the current state.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        internal IEnumerable<T> ExecuteSelect(Func<ITable<T>, IEnumerable<T>> func, ISmartBarDB context)
        {
            IEnumerable<T> res = func(GetTable(context));


            if (_orderBy != null)
            {
                if (_ascending)
                    res = res.OrderBy(_orderBy.Clone() as Func<T, object>);
                else
                    res = res.OrderByDescending(_orderBy.Clone() as Func<T, object>);
            }
            if (_offset != 0)
                res = res.Skip(_offset);
            _offset = 0;

            if (_limit != 0)
                res = res.Take(_limit);
            _limit = 0;

            _orderBy = null;
            LoadWithList.Clear();
            return res;
        }

        /// <summary>
        /// Executes the select operation with the current state asynchronously.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        internal async Task<IEnumerable<T>> ExecuteSelectAsync(Func<ITable<T>, IEnumerable<T>> func, ISmartBarDB context)
        {
            return await Task.Run(() => ExecuteSelect(func, context));
        }

        /// <summary>
        /// Should be implemeneted in inhereted class to specifiy the db table for Linq2DB DataContext
        /// </summary>
        internal abstract Expression<Func<ISmartBarDB, ITable<T>>> TableExpression { get; }
       

        /// <summary>
        /// Insert the object into the table
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual void Insert(T obj)
        {
            using (ISmartBarDB context = new ISmartBarDB())
            {
                context.Insert(obj);
            }
        }

        /// <summary>
        /// Insert the object into the table asynchronously.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task InsertAsync(T obj, CancellationToken token = new CancellationToken())
        {
            using (ISmartBarDB context = new ISmartBarDB())
            {
                await context.InsertWithIdentityAsync(obj);
            }
        }

        /// <summary>
        /// Updates the object in the table.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual int Update(T obj)
        {
            using (ISmartBarDB context = new ISmartBarDB())
            {
                return context.Update(obj);
            }
        }

        /// <summary>
        /// Updates the object in the table asynchronously.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(T obj, CancellationToken token = new CancellationToken())
        {
            using (ISmartBarDB context = new ISmartBarDB())
            {
                return await context.UpdateAsync(obj);
            }
        }

        /// <summary>
        /// Removes the object from the table.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual int Remove(T obj)
        {
            using (ISmartBarDB context = new ISmartBarDB())
            {
                return context.Delete(obj);
            }
        }

        /// <summary>
        /// Removes the object from the table asynchronously.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<int> RemoveAsync(T obj, CancellationToken token = new CancellationToken())
        {
            using (ISmartBarDB context = new ISmartBarDB())
            {
                return await context.DeleteAsync(obj);
            }
        }

        /// <summary>
        /// Get the entity with the specified id.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract T FindByID(K1 key1, K2 key2);

        /// <summary>
        /// Get the entity with the specified id asynchrnously.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public abstract Task<T> FindByIDAsync(K1 key1, K2 key2, CancellationToken token = new CancellationToken());

        /// <summary>
        /// Get all entites from the table.
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetAll( )
        {
            using (ISmartBarDB context = new ISmartBarDB())
            {
                return ExecuteSelect(t => t, context).ToList();
            }
        }

        /// <summary>
        /// Get all entites from the table asynchrnously.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetAllAsync( CancellationToken token = new CancellationToken())
        {
            using (ISmartBarDB context = new ISmartBarDB())
            {
                return (await ExecuteSelectAsync(t => t, context)).ToList();
            }
        }

        /// <summary>
        /// Find by predicate.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual List<T> Find(Func<T, bool> where)
        {
            using (ISmartBarDB context = new ISmartBarDB())
            {
                return ExecuteSelect(t => t.Where(where), context).ToList();
            }
        }

        /// <summary>
        /// Find by predicate asynchronously.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(Func<T, bool> where, CancellationToken token = new CancellationToken())
        {
            using (ISmartBarDB context = new ISmartBarDB())
            {
                return (await ExecuteSelectAsync(t => t.Where(where), context)).ToList();
            }
        }

    }
}
