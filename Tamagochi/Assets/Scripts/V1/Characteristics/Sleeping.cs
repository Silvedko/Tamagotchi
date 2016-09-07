using UnityEngine;
using System.Collections;
using Tamagochi.Constants;

namespace Tamagochi.V1
{
	public class Sleeping : AbstractCharacteristic 
	{
		protected override void SetCharacteristicDefaultValue ()
		{
			CharacteristicValue = GameConstants.startSleepingValue;
		}
	}
}