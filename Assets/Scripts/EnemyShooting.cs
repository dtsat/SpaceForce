using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {

	private float delay;
	private bool hasShot;
	public GameObject shot;
	public Transform gun;


	void Start () 
	{
		delay = Random.Range (0.75f, 5.0f);
		hasShot = false;
		StartCoroutine (Shoot ());
	}

	IEnumerator Shoot()
	{
		if (!hasShot) 
		{
			yield return new WaitForSeconds (delay);
			Instantiate (shot, gun.position, gun.rotation);
		}
		hasShot = true;
	}
}
