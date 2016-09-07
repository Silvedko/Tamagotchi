using UnityEngine;
using System.Collections;
using Tamagochi.Constants;

namespace Tamagochi.V1
{
	public class Hunger : AbstractCharacteristic
	{
		protected override void SetCharacteristicDefaultValue ()
		{
			CharacteristicValue = GameConstants.startHungerValue;
		}
	}
}