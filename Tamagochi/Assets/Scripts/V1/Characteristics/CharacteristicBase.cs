using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Tamagochi.Constants;

namespace Tamagochi.V1
{
	public class CharacteristicBase : MonoBehaviour, ICharacteristic 
	{
		protected Slider slider;
		protected Button btn;
		protected CharTimer timer;

		public delegate void CharacteristicsDelegate ();
		public event CharacteristicsDelegate OnCharacteristicChange; 

		/// <summary>
		/// Gets or sets the characteristic value.
		/// </summary>
		/// <value>The characteristic value.</value>
		public float CharacteristicValue
		{
			get	{ return charValue; }
			set 
			{
				charValue = value;
				StartTimer ();
				ResetTimer ();

				// Min characteristic value
				if (charValue <= GameConstants.minCharacteristicSliderValue) 
				{
					charValue = GameConstants.minCharacteristicSliderValue;
					StopTimer ();
				}
				
				// Max characteristic value
				if (charValue > GameConstants.maxCharacteristicSliderValue) 
				{
					charValue = GameConstants.maxCharacteristicSliderValue;
					EnableAccessToAddButton (false);
				} else {
					EnableAccessToAddButton (true);
				}

				GetUISlider ().value = charValue;


				if(OnCharacteristicChange != null)
					OnCharacteristicChange ();
			}
		}
		private float charValue = 0;

		#region Implementation of interface methods
		/// <summary>
		/// Reduces characteristic value.
		/// </summary>
		public void ReduceValue (int value)
		{
			CharacteristicValue -= value;
		}

		/// <summary>
		/// Increases characteristic value.
		/// </summary>
		public void IncreaseValue (int value)
		{
			CharacteristicValue += value;
		}

		/// <summary>
		/// Gets the characteristic value.
		/// </summary>
		/// <returns>The value.</returns>
		public float GetCharacteristicValue ()
		{
			return CharacteristicValue;
		}


		/// <summary>
		/// Sets the default values to characteristics.
		/// </summary>
		public virtual void SetDefaultValues ()
		{
			SetSliderDefaultValues ();
			SetCharacteristicDefaultValue ();
			EnableAccessToAddButton (true);
			ResetTimer ();
		}
		#endregion

		/// <summary>
		/// On or off add button
		/// </summary>
		/// <param name="isAccessed">If set to <c>true</c> is accessed.</param>
		public virtual void EnableAccessToAddButton (bool isAccessed)
		{
			btn.interactable = isAccessed;
		}


		/// <summary>
		/// Gets the user interface slider.
		/// </summary>
		/// <returns>The user interface slider.</returns>
		private Slider GetUISlider ()
		{
			if (slider == null) 
			{
				slider = gameObject.GetComponentInChildren <Slider> ();
				SetSliderDefaultValues ();
			}
			return slider;
		}

		#region methods for timer
		/// <summary>
		/// Gets the timer on gameobject
		/// </summary>
		/// <returns>The timer.</returns>
		private CharTimer GetTimer ()
		{
			if (timer == null)
				timer = GetComponentInChildren <CharTimer> ();
			
			return timer;		
		}

		/// <summary>
		/// Starts the timer.
		/// </summary>
		private void StartTimer ()
		{
			if(timer != null) //times may be null in happiness object
				timer.StartTimer();
		}

		/// <summary>
		/// Stops the timer.
		/// </summary>
		private void StopTimer ()
		{
			if (timer != null)
				timer.StopTimer ();
		}

		/// <summary>
		/// Resets the timer.
		/// </summary>
		private void ResetTimer ()
		{
			if(timer != null)
				timer.ResetTimer ();
		}
		#endregion

		/// <summary>
		/// Gets the user interface button for increasing characteristic.
		/// </summary>
		/// <returns>The user interface button.</returns>
		private Button GetUIButton ()
		{
			if (btn == null)
				btn = gameObject.GetComponentInChildren <Button> ();
			
			return btn;
		}


		void Awake ()
		{
			GetUIButton ();
			GetTimer ();

			if(timer != null)
				timer.OnTimeIsOverEvent += delegate {ReduceValue (GameConstants.defaultReduceValue);};
			
			btn.onClick.AddListener (OnAddButtonCLicked);

			SetDefaultValues ();
			StartTimer ();
		}
			
		protected virtual void OnAddButtonCLicked ()
		{
			IncreaseValue (GameConstants.defaultAdditionalValue);
		}

		protected void SetSliderDefaultValues ()
		{
			GetUISlider ();

			slider.minValue = GameConstants.minCharacteristicSliderValue;
			slider.maxValue = GameConstants.maxCharacteristicSliderValue;
			slider.value = CharacteristicValue;
		}

		protected void SetCharacteristicDefaultValue ()
		{
			CharacteristicValue = GameConstants.startCharValue;
		}

		void OnDestroy ()
		{
			btn.onClick.RemoveListener (OnAddButtonCLicked);
			if(timer != null)
				timer.OnTimeIsOverEvent -= delegate {ReduceValue (GameConstants.defaultReduceValue);};
		}
	}
}
