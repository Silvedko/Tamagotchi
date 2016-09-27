using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Tamagochi.Constants;

namespace Tamagochi.V2
{
	public class CellController : MonoBehaviour 
	{
		public Button button;
		public Slider slider;
		public CharTimer timer;

		public delegate void CellControllerDelegate ();
		public event CellControllerDelegate OnValueChanged;

		public float CharacteristicValue
		{
			get { return characteristicValue; }
			set
			{
				characteristicValue = value;

				timer.StartTimer ();
				timer.ResetTimer ();

				// Min characteristic value
				if (characteristicValue <= GameConstants.minCharacteristicSliderValue) 
				{
					characteristicValue = GameConstants.minCharacteristicSliderValue;
					timer.StopTimer ();
				}

				// Max characteristic value
				if (characteristicValue >= GameConstants.maxCharacteristicSliderValue) 
				{
					characteristicValue = GameConstants.maxCharacteristicSliderValue;
					SetInteractiveButton (false);
				}
				else
					SetInteractiveButton (true);

				slider.value = characteristicValue;

				if(OnValueChanged != null)
					OnValueChanged ();
			}
		}
		private float characteristicValue = GameConstants.startSliderValue;

		public void OnButtonClicked ()
		{
			ChangeCharacteristicValue (GameConstants.defaultAdditionalValue);
		}
			
		public void ChangeCharacteristicValue (int value)
		{
			CharacteristicValue += value;
		}

		public void SetInteractiveButton (bool isInteractive)
		{
			button.interactable = isInteractive;
		}


		public void SetDefaultValues ()
		{
			SetInteractiveButton (true);
			CharacteristicValue = GameConstants.startSliderValue;
			timer.StartTimer ();
		}


		void Start () 
		{
			SetSliderValues ();
			timer.OnTimeIsOverEvent += delegate { ChangeCharacteristicValue ( -GameConstants.defaultReduceValue );};
			timer.StartTimer ();
		}
			


		void SetSliderValues ()
		{
			slider.minValue = GameConstants.minCharacteristicSliderValue;
			slider.maxValue = GameConstants.maxCharacteristicSliderValue;
			slider.value = CharacteristicValue;
		}

		void OnDestroy ()
		{
			timer.OnTimeIsOverEvent -= delegate { ChangeCharacteristicValue ( -GameConstants.defaultReduceValue );};
		}
	}
}