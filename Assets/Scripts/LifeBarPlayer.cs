using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeBarPlayer : MonoBehaviour {

	private GameObject player;
	private PlayerControl playerTrack;
	private Image[] ships;

	void Start () 
	{
		player = GameObject.Find ("PlayerShip");
		playerTrack = player.GetComponent<PlayerControl> ();
		ships = GetComponentsInChildren<Image> ();
	}

	void Update()
	{
		if (playerTrack.getGun() == 0) 
		{
			ships [0].enabled = false;
			ships [1].enabled = false;
			ships [2].enabled = false;
			ships [3].enabled = false;
		}
		if (playerTrack.getGun() == 1) 
		{
			ships [0].enabled = true;
			ships [1].enabled = false;
			ships [2].enabled = false;
			ships [3].enabled = false;
		}
		if (playerTrack.getGun() == 2) 
		{
			ships [0].enabled = true;
			ships [1].enabled = true;
			ships [2].enabled = false;
			ships [3].enabled = false;
		}
		if (playerTrack.getGun() == 3) 
		{
			ships [0].enabled = true;
			ships [1].enabled = true;
			ships [2].enabled = true;
			ships [3].enabled = false;
		}
		if (playerTrack.getGun() == 4) 
		{
			ships [0].enabled = true;
			ships [1].enabled = true;
			ships [2].enabled = true;
			ships [3].enabled = true;
		}
	}
}

