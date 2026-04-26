using DataBaseProvaider;
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

        public virtual async Task Delete()
        {
            await this.GetType().InvokeMethodByType([new ConditionsParametr(nameof(this.Id), ConditionalOperators.Equal, this.Id)], nameof(DBProvider.Delete))
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
            DataRow row = await this.GetType().GetResultByType<DataRow>(
                                            [
                                                ModelToDictionary(), 
                                                this.GetType().GetProperties().Select(i => i.Name).ToArray()
                                            ], nameof(DBProvider.Insert));

            if (row != null)
            {
                return (IModel)Convert.ChangeType(await this.GetType().GetResultByType<object>([row], nameof(Converter.RowToObject), typeof(Converter)), this.GetType());
            }

            return null;
        }

        public virtual async Task<IModel> Update()
        {
            DataRow row = await this.GetType().GetResultByType<DataRow>(
                                            [
                                                ModelToDictionary(),
                                                new ConditionsParametr(nameof(this.Id), ConditionalOperators.Equal, this.Id),
                                                this.GetType().GetProperties().Select(i => i.Name).ToArray()
                                            ], nameof(DBProvider.Update));

            if (row != null)
            {
                return (IModel)Convert.ChangeType(await this.GetType().GetResultByType<object>([row], nameof(Converter.RowToObject), typeof(Converter)), this.GetType());
            }

            return null;
        }

        protected Dictionary<string, object> ModelToDictionary()
        {
            Dictionary<string, object> parametrs = new ();
            PropertyInfo[] propertiesBase = typeof(BaseModel).GetProperties();

            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                if (!propertiesBase.Any(i => i.Name == property.Name))
                {
                    parametrs.Add(property.Name, property.GetValue(this));
                }
            }

            return parametrs;
        }
    }
}
