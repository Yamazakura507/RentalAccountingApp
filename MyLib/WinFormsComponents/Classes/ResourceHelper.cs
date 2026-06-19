using System.Reflection;
using WinFormsComponents.Properties;

namespace WinFormsComponents.Classes
{
    internal static class ResourceHelper
    {
        /// <summary>
        /// Получить Bitmap из Resources по имени
        /// </summary>
        /// <param name="resourceName">Наименование ресурса</param>
        /// <returns>Bitmap объект из ресурсов</returns>
        public static Bitmap? GetBitmapResources(this string resourceName) => resourceName.GetResources<Bitmap>();

        /// <summary>
        /// Получить byte[] из Resources по имени
        /// </summary>
        /// <param name="resourceName">Наименование ресурса</param>
        /// <returns>byte[] объект из ресурсов</returns>
        public static byte[]? GetByteArrayResources(this string resourceName) => resourceName.GetResources<byte[]>();

        /// <summary>
        /// Получить из Resources по имени
        /// </summary>
        /// <param name="resourceName">Наименование ресурса</param>
        /// <returns>Jбъект из ресурсов</returns>
        public static T? GetResources<T>(this string resourceName) where T : class
        {
            try
            {
                Type resourcesType = typeof(Resources);

                PropertyInfo? property = resourcesType.GetProperty(
                    resourceName,
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic
                );

                return property?.GetValue(null) as T;
            }
            catch
            {
                return null;
            }
        }
    }
}
