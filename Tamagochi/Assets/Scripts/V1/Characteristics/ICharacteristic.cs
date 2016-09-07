using UnityEngine;
using UnityEngine.UI;

namespace Tamagochi.V1
{

	public interface ICharacteristic 
	{
		/// <summary>
		/// Reduces characteristic value.
		/// </summary>
		void ReduceValue (int value);

		/// <summary>
		/// Increases characteristic value.
		/// </summary>
		void IncreaseValue (int value);

		/// <summary>
		/// Gets the characteristic value.
		/// </summary>
		/// <returns>The characteristic value.</returns>
		float GetCharacteristicValue ();

		/// <summary>
		/// Sets the default values to characteristics.
		/// </summary>
		void SetDefaultValues ();
	}
}
