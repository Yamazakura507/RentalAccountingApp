using WinFormsComponents.Classes.Enums;

namespace WinFormsComponents.Classes.Model
{
    /// <summary>
    /// Объект списка зависимости
    /// </summary>
    public class DependencyInfo
    {
        /// <summary>
        /// Идентификатор зависимости
        /// </summary>
        public int? IdDependency { get; set; }

        /// <summary>
        /// Статус объекта зависимости
        /// </summary>
        public DependencyStatus Status { get; set; } = DependencyStatus.Default;

        /// <summary>
        /// Конструктор объекта зависимости
        /// </summary>
        /// <param name="idDependency">Идентификатор объекта зависимости</param>
        public DependencyInfo(int? idDependency)
        {
            IdDependency = idDependency;
        }

        /// <summary>
        /// Конструктор объекта зависимости
        /// </summary>
        /// <param name="idDependency">Идентификатор объекта зависимости</param>
        /// <param name="status">Статус объекта зависимости</param>
        public DependencyInfo(int? idDependency, DependencyStatus status)
        {
            IdDependency = idDependency;
            Status = status;
        }
    }
}
