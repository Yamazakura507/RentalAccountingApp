using DataBaseProvaider;
using DataBaseProvaider.Attributes;
using DataBaseProvaider.Enums;
using DataBaseProvaider.Objects;
using RentalDBModels.Models.Interface;
using RentalDBModels.Views.Interface;
using System.Drawing;
using System.Reflection;

namespace RentalDBModels.Views.Abstract
{
    public abstract class BaseView : IView
    {
        [ViewModel(ViewHide = true)]
        public int Id { get; set; }

        [ViewModel(ViewHide = true, RemovingFlag = true)]
        public bool Flag { get; set; }

        [ViewModel(ViewHide = true)]
        public virtual Type ModelType { get; set; }

        [ViewModel(ViewHide = true, BackColor = true)]
        public virtual Color BackColor { get; set; } = Color.White;

        public async Task<IModel> GetModel() => (IModel)Convert.ChangeType(await this.ModelType.GetResultByType<object>([this.Id], nameof(DBProvider.GetModel)), this.ModelType);

        protected async Task<IEnumerable<int>> GetDependenciesId<TDependencyModel>() where TDependencyModel : IDependency, new()
        {
            string dependencyNmae = null;
            string forignName = null;
            PropertyInfo[] properties = typeof(TDependencyModel).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                DependencyModelAttribute dmAttribute = property.GetCustomAttribute<DependencyModelAttribute>();

                if (dmAttribute is not null)
                {
                    if (dmAttribute.IsForigen) forignName = property.Name;
                    if (dmAttribute.IsDependency) dependencyNmae = property.Name;
                }
            }

            return await DBProvider.GetColumnModel<int, TDependencyModel>(dependencyNmae, [new ConditionsParametr(forignName, ConditionalOperators.Equal, this.Id)]);
        }
    }
}
