using DataBaseProvaider;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using RentalDBModels.Models.Interface;

namespace RentalDBModels.Models.Abstract
{
    public abstract class BaseRemovingModel : BaseModel, IRemovingModel
    {
        public bool Flag { get; set; } = true;

        public override async Task Delete()
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

        public override async Task Delete<TModel>()
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
    }
}
