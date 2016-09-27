using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Tamagochi.Constants;

public class CharTimer : MonoBehaviour 
{
	public delegate void TimerDelegate ();
	public event TimerDelegate OnTimeIsOverEvent;

	private bool stopTimer = true;

	Text timerText;
	float endTime;

	bool stopUpdate = false;

	/// <summary>
	/// Values for String.Join for correct displaying text
	/// </summary>
	string[] values = new string[2];

	public void StopTimer ()
	{
		stopTimer = true;
		endTime = 0;
	}

	public void StartTimer ()
	{
		stopTimer = false;
		endTime = GameConstants.timeToReduceValues;
	}

	public void ResetTimer ()
	{
		stopUpdate = false;
		endTime = GameConstants.timeToReduceValues; 
	}

	void Awake ()
	{
		InitTimer ();
	}

	void InitTimer () 
	{
		endTime = Time.time + GameConstants.timeToReduceValues;
		timerText = GetComponent <Text> ();


		values [0] = 0 + ((int)endTime / 60).ToString ();
		values [1] = ((int)endTime % 60).ToString ();
		timerText.text = String.Join(":", values);
	}
	
	void Update () 
	{
		if(!stopUpdate && !stopTimer)
		{
			endTime -= Time.deltaTime;

			if (endTime < 0) 
			{
				endTime = 0;
				stopUpdate = true;

				if (OnTimeIsOverEvent != null)
					OnTimeIsOverEvent ();
			}

			values [0] = SetCorrectNumbers((int)endTime / 60);
			values [1] = SetCorrectNumbers((int)endTime % 60);

			timerText.text = String.Join (":", values);
		}
	}

	string SetCorrectNumbers (int number)
	{
		string num;
		if (number < 10 && number >= 0)
			num = 0 + number.ToString ();
		else
			num = number.ToString ();
		
		return num;
	}
}
