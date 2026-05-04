using DataBaseProvaider;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using RentalDBModels.Models.Interface;
using System.Data;
using System.Reflection;

namespace RentalDBModels.Models.Abstract
{
    public abstract class BaseDependeceModel : IDependency
    {
        public int Id { get; init; }

        public virtual async Task<IDependency> Insert<TModel>() where TModel : IDependency, new()
        {
            DataRow dataRow = await DBProvider.Insert<TModel>(ModelToDictionary(), this.GetType().GetProperties().Select(i => i.Name).ToArray());

            if (dataRow != null)
            {
                return dataRow.RowToObject<TModel>();
            }

            return null;
        }

        public async static IAsyncEnumerable<IDependency> InsertRange<TModel>(IEnumerable<TModel> dependencies) where TModel : BaseDependeceModel, new()
        {
            foreach (TModel dependency in dependencies)
            {
                yield return await dependency.Insert<TModel>();
            }
        }

        public virtual async Task Delete<TModel>() where TModel : IDependency, new()
        {
            IEnumerable<ConditionsParametr> conditions = ModelToDictionary().Select(i => new ConditionsParametr(i.Key, ConditionalOperators.Equal, LogicOperators.And, i.Value));

            await DBProvider.Delete<TModel>(conditions);
        }

        public async static Task DeleteRange<TModel>(IEnumerable<TModel> dependencies) where TModel : BaseDependeceModel, new()
        {
            IEnumerable<ConditionsParametr> conditions = 
                dependencies.SelectMany(d =>
                    d.ModelToDictionary().Select((i, index) =>
                        new ConditionsParametr(
                            i.Key,
                            ConditionalOperators.Equal,
                            index == 0 ? LogicOperators.And : LogicOperators.Or,
                            i.Value
                        )
                    )
                ); 

            await DBProvider.Delete<TModel>(conditions);
        }

        protected Dictionary<string, object> ModelToDictionary()
        {
            Dictionary<string, object> parametrs = new();
            PropertyInfo[] propertiesBase = typeof(BaseDependeceModel).GetProperties();

            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                if (!propertiesBase.Any(i => i.Name == property.Name))
                {
                    parametrs.Add(property.Name, property.GetValue(this));
                }
            }

            return parametrs;
        }

        public abstract TDependencyModel Initialize<TDependencyModel>(int dependencyId, int forignId) where TDependencyModel : BaseDependeceModel;
    }
}
