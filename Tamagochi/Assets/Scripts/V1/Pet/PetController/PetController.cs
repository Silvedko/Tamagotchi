using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tamagochi.Constants;
using UnityEngine.UI;

namespace Tamagochi.V1
{
	public class PetController : MonoBehaviour 
	{
		public delegate void PetControllerDelegate ();
		public event PetControllerDelegate OnHappinessEmpty;

		[HideInInspector]
		public Image currentPetSprite;

		// Sprites for pet
		public Sprite petDeadSprite;
		public Sprite petHappySprite;
		public Sprite petSadSprite;


		// This field calculate in class
		public Happiness happinessChar;

		/// <summary>
		/// The objects of characteristics.
		/// </summary>
		public List <AbstractCharacteristic> characteristics;

	
		/// <summary>
		/// Sets the default characteristics. Method for GameController to restarting game.
		/// </summary>
		/// <param name="fields">Fields.</param>
		public void SetDefaultCharacteristics ()
		{
			foreach (var ch in characteristics) 
			{
				ch.SetDefaultValues ();
			}
		}

		/// <summary>
		/// Blocks the buttons via GameController when pet is dead.
		/// </summary>
		public void BlockButtons ()
		{
			foreach (var ch in characteristics)
			{
				ch.EnableAccessToAddButton (false);
			}
		}


		void Awake ()
		{
			currentPetSprite = GetComponent <Image> ();
		}

		void Start () 
		{
			SubscribeOnCharacteristicEvents ();	
			CalculateHappiness ();
		}


		void SubscribeOnCharacteristicEvents ()
		{
			foreach (var ch in characteristics) 
			{
				ch.OnCharacteristicChange += delegate {OnCharacteristicsWasChangedHandler ();};
			}
		}

		void UnsubscribeFromCharacteristicEvents ()
		{
			foreach (var ch in characteristics) 
			{
				ch.OnCharacteristicChange -= delegate {OnCharacteristicsWasChangedHandler ();};
			}
		}
			
		/// <summary>
		/// Calculate hapiness value when some characteristics value was changed.
		/// </summary>
		void OnCharacteristicsWasChangedHandler ()
		{
			CalculateHappiness ();
		}

			
		void CalculateHappiness ()
		{
			var value = HappinessCalculator.Calculate (characteristics.ToArray());
			
			happinessChar.SetCharacteristicValue (value);

			SetPetsAvatar (value);
		}


		/// <summary>
		/// Method sets the pets avatar depending on happiness value.
		/// </summary>
		/// <param name="happinessValue">Happiness value.</param>
		void SetPetsAvatar (float happinessValue)
		{
			if (happinessValue > GameConstants.happyTimePetValue)
				currentPetSprite.sprite = petHappySprite;
			
			if (happinessValue <= GameConstants.happyTimePetValue && happinessValue > 0)
				currentPetSprite.sprite = petSadSprite;

			if (happinessValue <= 0) 
			{
				currentPetSprite.sprite = petDeadSprite;

				if(OnHappinessEmpty != null)
					OnHappinessEmpty ();
			}
		}

						
		void OnDestroy ()
		{
			UnsubscribeFromCharacteristicEvents ();
		}			
	}
}