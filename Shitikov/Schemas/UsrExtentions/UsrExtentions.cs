using System.Collections.Generic;
using Terrasoft.Common;
using Terrasoft.Core.Entities;

namespace Terrasoft.Configuration
{
    /// <summary>
    /// Методы расширения для объектов
    /// </summary>
    public static class Extentions
	{
		/// <summary>
		/// Проверка на существование дублей записи по заданным колонкам и значениям.
		/// </summary>
		/// <param name="entity">Запись для которой осуществляется проверка</param>
		/// <param name="values">Словарь значений (название колонки - значение)</param>
		/// <param name="logicalOperation">Логическая операция для искомых значений (And - поиск полного соответствия, OR - соответствие хотя бы по одному признаку).</param>
		/// <returns>true - дубли есть, false - дублей нет</returns>
		public static bool HasDoubles(this Entity entity, Dictionary<string, object> values, LogicalOperationStrict logicalOperation = LogicalOperationStrict.And)
        {
			return GlobalHelper.IsEntityHasDoubles(entity.UserConnection, entity.SchemaName, values, logicalOperation);
        }
	}
}
