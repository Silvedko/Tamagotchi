using UnityEngine;
using System.Collections;

namespace Tamagochi.V2
{
	public class GameController : MonoSingleton <GameController> 
	{
		public PetController petController;

		public void RestartGame () 
		{
			petController.SetDefaultValues ();
		}
		
		void Start ()
		{

		}
	}
}