using eShop.BusinessEntities.Base;
using LinqToDB.Mapping;

namespace BusinessEntities
{
    public abstract class IDEntityBase<T> : IIdEntityBase<T>
    {
        public T ID { get;set; }
    }
}