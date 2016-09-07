using UnityEngine;
using System.Collections;
using Tamagochi.Constants;

namespace Tamagochi.V1
{
	public class Health : AbstractCharacteristic 
	{
		protected override void SetCharacteristicDefaultValue ()
		{
			CharacteristicValue = GameConstants.startHealthValue;
		}

	}
}