using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tamagochi.V1;

public class HappinessCalculator  
{
	public static float Calculate (params ICharacteristic [] parameters)
	{
		float value = 0;

		foreach (var parameter in parameters) 
		{
			value += parameter.GetCharacteristicValue ();
		}

		return value / parameters.Length;
	}


	public static float Calculate (List <ICharacteristic> list)
	{
		float value = 0;

		foreach (var parameter in list) 
		{
			value += parameter.GetCharacteristicValue ();
		}

		return value / list.Count;
	}
}
