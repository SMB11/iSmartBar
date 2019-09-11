using System.ComponentModel;
using System.Linq;
using BusinessEntities;
using BusinessEntities.Culture;
using BusinessEntities.Location;
using BusinessEntities.Products;
using BusinessEntities.Security;
using BusinessEntities.Statistics;
using Common.DataAccess;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using LinqToDB.Reflection;

namespace iSmartBar.Repositories.LinqToDB
{
    public class ISmartBarDB : DataConnection
    {
        private static MappingSchema mappingSchema;

        public ISmartBarDB() : base("Default") 
        {

            if (mappingSchema == null)
                mappingSchema = InitContextMappings(this.MappingSchema);
            FluentMappingBuilder mb = MappingSchema.GetFluentMappingBuilder();

            MappingSchema.EntityDescriptorCreatedCallback = (mappingSchema, entityDescriptor) =>
            {
                if (!entityDescriptor.TypeAccessor.Type.IsAbstract)
                {
                    if (entityDescriptor.TypeAccessor.Type.IsSubclassOf(typeof(IDEntityBase<int>)) || entityDescriptor.TypeAccessor.Type.IsSubclassOf(typeof(IDEntityBase<string>)))
                    {
                        var idCol = entityDescriptor.Columns.Where(c => c.MemberName == "ID").FirstOrDefault();
                        if (idCol.MemberName == idCol.ColumnName)
                            idCol.ColumnName = entityDescriptor.TypeAccessor.Type.Name + "ID";

                    }

                }
            };

        }

        private static MappingSchema InitContextMappings(MappingSchema ms)
        {
            ms.GetFluentMappingBuilder()
                .Entity<IDEntityBase<int>>()
                .HasPrimaryKey(x => x.ID)
                .HasIdentity(x => x.ID);

            ms.GetFluentMappingBuilder()
                .Entity<IDEntityBase<string>>()
                .HasPrimaryKey(x => x.ID);
            return ms;
           
        }
        

        public ITable<Hotel> Hotels => GetTable<Hotel>();
        public ITable<CityInfo> CityInfos => GetTable<CityInfo>();
        public ITable<City> Cities => GetTable<City>();
        public ITable<Country> Countries => GetTable<Country>();
        public ITable<CountryInfo> CountryInfos => GetTable<CountryInfo>();
        public ITable<Visit> Visits => GetTable<Visit>();
    }
}