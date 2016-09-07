using UnityEngine;
using System.Collections;
using Tamagochi.V1;
using Tamagochi.Constants;

public class GameController : MonoSingleton <GameController> 
{
	public PetController petController;

	public void RestartGame ()
	{
		petController.SetDefaultCharacteristics ();
	}

	void Start ()
	{
		if (petController == null)
			petController = GameObject.FindObjectOfType <PetController> ();
		
		petController.OnHappinessEmpty += delegate {BlockAddButtons ();};
	}

	void BlockAddButtons ()
	{
		petController.BlockButtons ();
	}

}
