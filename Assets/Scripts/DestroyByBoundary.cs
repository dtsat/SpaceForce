using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	private GameController tracker;
	private GameObject waveTrack;

	void Start()
	{
		waveTrack = GameObject.Find ("GameController");
		tracker = waveTrack.GetComponent<GameController> ();
	}
		
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag ("EnemyA") || other.CompareTag ("EnemyB")) 
		{
			tracker.loseShip ();
			tracker.resestMulti ();
		}
		Destroy (other.gameObject);
	}
}
