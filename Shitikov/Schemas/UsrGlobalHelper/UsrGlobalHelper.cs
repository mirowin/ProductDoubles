using System;
using System.Collections.Generic;
using Terrasoft.Common;
using Terrasoft.Core;
using Terrasoft.Core.Entities;

namespace Terrasoft.Configuration
{
    /// <summary>
    /// Класс для работы с часто иcпользуемыми методами.
    /// </summary>
    public static class GlobalHelper
	{
		/// <summary>
		/// Проверка на существование записи с заданными значениями.
		/// </summary>
		/// <param name="userConnection">Соединение пользователя.</param>
		/// <param name="values">Словарь значений (название колонки - значение)</param>
		/// <param name="logicalOperation">Логическая операция для искомых значений (And - поиск полного соответствия, OR - соответствие хотя бы по одному признаку).</param>
		/// <returns>true - дубли есть, false - дублей нет.</returns>
		public static bool IsEntityHasDoubles(UserConnection userConnection, string schemaName, Dictionary<string, object> values, LogicalOperationStrict logicalOperation = LogicalOperationStrict.And)
		{
			var esq = new EntitySchemaQuery(userConnection.EntitySchemaManager, schemaName)
			{
				RowCount = 1
			};

			esq.AddColumn("Id");

			var valuesFilters = new EntitySchemaQueryFilterCollection(esq, logicalOperation);

			foreach (var item in values)
			{
				if (IsEmptyValue(item.Value))
				{
					valuesFilters.Add(esq.CreateIsNullFilter(item.Key));
				}
				else
				{
					valuesFilters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, item.Key, item.Value));
				}
			}

			esq.Filters.Add(valuesFilters);

			return esq.GetEntityCollection(userConnection).Count > 0;
		}

		/// <summary>
		/// Проверка на отсутствие значения.
		/// </summary>
		/// <param name="value">Значение.</param>
		/// <returns>Признак отсутствия.</returns>
		public static bool IsEmptyValue(Object value)
		{
			switch(value)
			{
				case DateTime date: return date == default;
				case Guid guid: return guid == default;
				case int number: return number == default;
				case double dbl: return dbl == default;
				case String str: return String.IsNullOrWhiteSpace(str) || str == default;
				default: return value == default;
			}
		}
	}
}
