using UnityEngine;
using System.Collections;

public class WaveDestroyed : MonoBehaviour {

	private int killedShips;
	public GameObject powerUp;
	private GameObject waveTrack;
	private GameController tracker;

	void Start () 
	{
		killedShips = 5;
		waveTrack = GameObject.Find ("GameController");
		tracker = waveTrack.GetComponent<GameController> ();
	}

	public void killShip()
	{
		killedShips--;
	}
		
	void Update () 
	{
		if (killedShips == 0) 
		{
			Instantiate (powerUp, new Vector3 (0.0f, 1.0f, 0.0f), transform.rotation);
			Destroy (gameObject);
			tracker.setWaveDead ();
		}

		if (transform.childCount == 0) 
		{
			tracker.setWaveDead ();
			Destroy (gameObject);
		}

	}
}
