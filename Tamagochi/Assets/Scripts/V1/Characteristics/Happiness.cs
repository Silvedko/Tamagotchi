using UnityEngine;
using System.Collections;
using Tamagochi.Constants;

namespace Tamagochi.V1
{
	public class Happiness : AbstractCharacteristic 
	{
		public void SetCharacteristicValue (float value)
		{
			CharacteristicValue = value;
		}

		protected override void OnAddButtonCLicked ()
		{
			GameController.Instance.RestartGame ();
		}
	}
}