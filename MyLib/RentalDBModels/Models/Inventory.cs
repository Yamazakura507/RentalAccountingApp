using DataBaseProvaider;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using RentalDBModels.Models.Abstract;
using RentalDBModels.Models.Interface;
using System.Data;

namespace RentalDBModels.Models
{
    public class Inventory : BaseLookupModel
    {
        public override async Task Delete()
        {
            await DBProvider.Delete<Materials>([new ConditionsParametr("Id", ConditionalOperators.Equal, this.Id)])
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
            DataRow dataRow = await DBProvider.Insert<Materials>(ModelToDictionary(), this.GetType().GetProperties().Select(i => i.Name).ToArray());

            if (dataRow != null)
            {
                return dataRow.RowToObject<Materials>();
            }
            
            return null;
        }

        public override async Task<IModel> Update()
        {
            DataRow dataRow = await DBProvider.Update<Materials>(
                                        ModelToDictionary(), 
                                        [new ConditionsParametr(nameof(this.Id), ConditionalOperators.Equal, this.Id)], 
                                        this.GetType().GetProperties().Select(i => i.Name).ToArray());

            if (dataRow != null)
            {
                return dataRow.RowToObject<Materials>();
            }

            return null;
        }
    }
}
