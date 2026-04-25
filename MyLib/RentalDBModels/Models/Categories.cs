using DataBaseProvaider;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using RentalDBModels.Models.Abstract;
using RentalDBModels.Models.Interface;
using System.Data;

namespace RentalDBModels.Models
{
    public class Categories : BaseLookupModel
    {
        public override async Task Delete()
        {
            await DBProvider.Delete<Categories>([new ConditionsParametr("Id", ConditionalOperators.Equal, this.Id)])
            .ContinueWith((task) =>
            {
                if (!task.IsFaulted)
                {
                    this.Flag = !this.Flag;
                }
            });
        }

        public override async Task<IModel> Insert()
        {
            DataRow dataRow = await DBProvider.Insert<Categories>(ModelToDictinory(), this.GetType().GetProperties().Select(i => i.Name).ToArray());

            if (dataRow != null)
            {
                return dataRow.RowToObject<Categories>();
            }
            
            return null;
        }

        public override async Task<IModel> Update()
        {
            DataRow dataRow = await DBProvider.Update<Categories>(
                                        ModelToDictinory(), 
                                        [new ConditionsParametr(nameof(this.Id), ConditionalOperators.Equal, this.Id)], 
                                        this.GetType().GetProperties().Select(i => i.Name).ToArray());

            if (dataRow != null)
            {
                return dataRow.RowToObject<Categories>();
            }

            return null;
        }
    }
}
