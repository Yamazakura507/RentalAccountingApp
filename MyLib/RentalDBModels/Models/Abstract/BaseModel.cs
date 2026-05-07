using DataBaseProvaider;
using DataBaseProvaider.Attributes;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using RentalDBModels.Models.Interface;
using System.Data;
using System.Reflection;

namespace RentalDBModels.Models.Abstract
{
    public abstract class BaseModel : IModel
    {
        public int Id { get; init; }
        public bool Flag { get; set; }

        public IModel Clone()
        {
            return (IModel)this.MemberwiseClone();
        }

        public IEnumerable<DependencyAttribute> GetDependencies<TModel>() where TModel : IModel
        {
            return typeof(TModel).GetCustomAttributes(typeof(DependencyAttribute), false)
                            .Cast<DependencyAttribute>();
        }

        public virtual async Task Delete()
        {
            await this.GetType().InvokeMethodByType([new[] { new ConditionsParametr(nameof(this.Id), ConditionalOperators.Equal, this.Id) }], nameof(DBProvider.Delete))
                .ContinueWith((task) =>
                {
                    if (!task.IsFaulted)
                    {
                        this.Flag = !this.Flag;
                    }
                });
            
        }

        public virtual async Task Delete<TModel>() where TModel : IModel
        {
            await DBProvider.Delete<TModel>([new ConditionsParametr(nameof(this.Id), ConditionalOperators.Equal, this.Id)])
            .ContinueWith((task) =>
            {
                if (!task.IsFaulted)
                {
                    this.Flag = !this.Flag;
                }
            });
        }

        public virtual async Task<IModel> Insert()
        {
            string[] returning = this.GetType().GetProperties()
                                                .Where(p => !p.IsDefined(typeof(SkipPropertyAttribute), inherit: true))
                                                .Select(i => i.Name).ToArray();

            DataRow row = await this.GetType().GetResultByType<DataRow>(
                                            [
                                                ModelToDictionary(),
                                                returning
                                            ], nameof(DBProvider.Insert));

            if (row != null)
            {
                return (IModel)Convert.ChangeType(await this.GetType().GetResultByType<object>([row], nameof(Converter.RowToObject), typeof(Converter)), this.GetType());
            }

            return null;
        }

        public virtual async Task<TModel> Insert<TModel>() where TModel : IModel, new()
        {
            string[] returning = this.GetType().GetProperties()
                                                .Where(p => !p.IsDefined(typeof(SkipPropertyAttribute), inherit: true))
                                                .Select(i => i.Name).ToArray();

            DataRow dataRow = await DBProvider.Insert<TModel>(ModelToDictionary(), returning);

            if (dataRow != null)
            {
                return dataRow.RowToObject<TModel>();
            }

            return default(TModel);
        }

        public virtual async Task<bool> InsertDependency<TDependencyModel>(IModel model, IEnumerable<int> idDependecies) where TDependencyModel : BaseDependeceModel, new()
        {
            bool result = false;

            if (model is not null && idDependecies is not null && idDependecies.Count() > 0)
            {
                result = true;
                ConstructorInfo constructor = typeof(TDependencyModel).GetConstructor(new[] { typeof(int), typeof(int) });

                IEnumerable<TDependencyModel> dependensies = idDependecies.Select(i => new TDependencyModel().Initialize<TDependencyModel>(i, model.Id));
                IAsyncEnumerable<IDependency> newDependencies = BaseDependeceModel.InsertRange(dependensies);

                await foreach (IDependency dependency in newDependencies)
                {
                    if (dependency is null) result = false;
                }
            }

            return result;
        }

        public virtual async Task<bool> RemoveDependency<TDependencyModel>(IModel model, IEnumerable<int> idDependecies) where TDependencyModel : BaseDependeceModel, new()
        {
            bool result = false;

            if (model is not null && idDependecies is not null && idDependecies.Count() > 0)
            {
                result = true;
                ConstructorInfo constructor = typeof(TDependencyModel).GetConstructor(new[] { typeof(int), typeof(int) });

                IEnumerable<TDependencyModel> dependensies = idDependecies.Select(i => new TDependencyModel().Initialize<TDependencyModel>(i, model.Id));

                await BaseDependeceModel.DeleteRange(dependensies).ContinueWith(task => result = !task.IsFaulted);
            }

            return result;
        }

        public virtual async Task<IModel> Update(IModel oldModel = null)
        {
            Dictionary<string, object> parametrUpdate = ModelToDictionary(oldModel);

            if (parametrUpdate.Count == 0) goto Exit;

            DataRow row = await this.GetType().GetResultByType<DataRow>(
                                            [
                                                parametrUpdate,
                                                new [] { new ConditionsParametr(nameof(this.Id), ConditionalOperators.Equal, this.Id) },
                                                this.GetType().GetProperties()
                                                    .Where(p => !p.IsDefined(typeof(SkipPropertyAttribute), inherit: true))
                                                    .Select(i => i.Name).ToArray()
                                            ], nameof(DBProvider.Update));

            if (row != null)
            {
                return (IModel)Convert.ChangeType(await this.GetType().GetResultByType<object>([row], nameof(Converter.RowToObject), typeof(Converter)), this.GetType());
            }

            Exit:
            return oldModel ?? this;
        }

        public virtual async Task<TModel> Update<TModel>(TModel oldModel = null) where TModel : class, IModel, new()
        {
            Dictionary<string, object> parametrUpdate = ModelToDictionary(oldModel);

            if (parametrUpdate.Count == 0) goto Exit;

            DataRow dataRow = await DBProvider.Update<TModel>(
                                        parametrUpdate,
                                        [new ConditionsParametr(nameof(this.Id), ConditionalOperators.Equal, this.Id)],
                                        this.GetType().GetProperties()
                                            .Where(p => !p.IsDefined(typeof(SkipPropertyAttribute), inherit: true))
                                            .Select(i => i.Name).ToArray());

            if (dataRow != null)
            {
                return dataRow.RowToObject<TModel>();
            }

            Exit:
            return oldModel ?? (this as TModel);
        }

        protected Dictionary<string, object> ModelToDictionary(IModel oldModel = null)
        {
            Dictionary<string, object> parametrs = new ();
            PropertyInfo[] propertiesBase = typeof(BaseModel).GetProperties();

            foreach (PropertyInfo property in this.GetType().GetProperties().Where(p => !p.IsDefined(typeof(SkipPropertyAttribute), inherit: true)))
            {
                if (!propertiesBase.Any(i => i.Name == property.Name) && 
                    (oldModel is null || !Equals(property.GetValue(this), property.GetValue(oldModel))))
                {
                    parametrs.Add(property.Name, property.GetValue(this));
                }
            }

            return parametrs;
        }
    }
}
