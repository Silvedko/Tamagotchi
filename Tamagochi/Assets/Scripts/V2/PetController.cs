using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Tamagochi.Constants;

namespace Tamagochi.V2
{
	public class PetController : MonoBehaviour 
	{
		public CellController hungryCell;
		public CellController sleepCell;
		public CellController healthyCell;
		public CellController cleanCell;

		public Image currentImage;

		public Sprite sadSprite;
		public Sprite happySprite;
		public Sprite deadSprite;

		public Slider hapinessSlider;

		public float HappinessValue
		{
			get { return happiness; }
			set 
			{
				happiness = value;

				SetAvatar ();
				hapinessSlider.value = happiness;
			}
		}
		private float happiness = 0;

		public void SetDefaultValues ()
		{
			hungryCell.SetDefaultValues ();
			sleepCell.SetDefaultValues ();
			healthyCell.SetDefaultValues ();
			cleanCell.SetDefaultValues ();
			SetAvatar ();
		}

		void Start () 
		{
			hapinessSlider.minValue = GameConstants.minCharacteristicSliderValue;
			hapinessSlider.maxValue = GameConstants.maxCharacteristicSliderValue;

			SubscribeOnButtons ();
			CalculateHappiness ();
		}


		void BlockButtons (bool needToBlock)
		{
			hungryCell.SetInteractiveButton (!needToBlock);
			sleepCell.SetInteractiveButton (!needToBlock);
			cleanCell.SetInteractiveButton (!needToBlock);
			healthyCell.SetInteractiveButton (!needToBlock);
		}

		void OnChangeValueHandler ()
		{
			CalculateHappiness ();
		}

		void CalculateHappiness ()
		{
			HappinessValue = HappinessCalculator.Calculate (hungryCell.CharacteristicValue, 
															sleepCell.CharacteristicValue, 
															healthyCell.CharacteristicValue,
															cleanCell.CharacteristicValue);	
		}

		void SetAvatar ()
		{
			if (happiness > GameConstants.happyTimePetValue)
				currentImage.sprite = happySprite;
			else if (happiness < GameConstants.happyTimePetValue && happiness > GameConstants.sadnessTimePetValue)
				currentImage.sprite = sadSprite;
			else if (happiness <= 0) 
			{
				currentImage.sprite = deadSprite;
				BlockButtons (true);
			}
		}

		void SubscribeOnButtons ()
		{
			hungryCell.OnValueChanged += OnChangeValueHandler;
			sleepCell.OnValueChanged += OnChangeValueHandler;
			healthyCell.OnValueChanged += OnChangeValueHandler;
			cleanCell.OnValueChanged += OnChangeValueHandler;
		}

		void UnSubscribeFromButtons ()
		{
			hungryCell.OnValueChanged -= OnChangeValueHandler;
			sleepCell.OnValueChanged -= OnChangeValueHandler;
			healthyCell.OnValueChanged -= OnChangeValueHandler;
			cleanCell.OnValueChanged -= OnChangeValueHandler;
		}

		void OnDestroy ()
		{
			UnSubscribeFromButtons ();
		}

	}
}
