 namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Entities.Events;

	/// <summary>
	/// Обработчик событийного слоя для объекта "Продукт".
	/// </summary>
	[EntityEventListener(SchemaName = "Product")]
	public class UsrProductEventListener : BaseEntityEventListener
	{
		#region Fields: private
		/// <summary>
		/// Сущность.
		/// </summary>
		private Product product { get; set; }

		#endregion

		#region Methods: public

		/// <summary>
		/// Обработчик перед сохранением.
		/// </summary>
		public override void OnSaving(object sender, EntityBeforeEventArgs e)
		{
			base.OnSaving(sender, e);

			product = (Product)sender;

			CheckForDoubles(e);
		}

		/// <summary>
		/// Проверка на дубли.
		/// </summary>
		private void CheckForDoubles(EntityBeforeEventArgs e)
		{
			if (product.HasDoubles(
				new Dictionary<string, object>()
                {
					{"Name", product.Name }
				}))
			{
				e.IsCanceled = true;

				throw new Exception(LocalizableStringHelper.GetValue(product.UserConnection, "UsrProductEventListener", "ProductHasDoubleErrorMessage"));
			}
		}

		#endregion
	}
}