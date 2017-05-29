using UnityEngine;
using System.Collections;

public class DestroyByBoundaryHell : MonoBehaviour {

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
			Destroy (other.gameObject);
		}
		if (other.CompareTag ("EnemyShot") || other.CompareTag ("PlayerShot")) 
		{
			HellShots counter = other.GetComponent<HellShots> ();
			counter.incCount ();
			if (other.CompareTag ("PlayerShot")) {
				if (counter.getCount () == 3) {
					Destroy (other.gameObject);
				}
			} else {
				if (counter.getCount () == 2) {
					Destroy (other.gameObject);
				}
			}
		}


			

		if (other.transform.position.x >= 3.2 || other.transform.position.x <= -3.2)
			other.transform.position = new Vector3 (-other.transform.position.x, -other.transform.position.y, other.transform.position.z);
		else
			other.transform.position = new Vector3 (other.transform.position.x, -other.transform.position.y, other.transform.position.z);
	}
}
