using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	AudioSource[] sounds;
	PlayerControl player;

	void Start () 
	{
		sounds = GetComponents<AudioSource> ();
		GetComponent<Rigidbody2D> ().velocity = transform.up * -1.0f;
		sounds [0].Play ();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Boundary"))
			return;	

		if (other.CompareTag ("Player")) 
		{
			sounds [1].Play ();
			player = other.GetComponent<PlayerControl> ();
			player.upgradeGun ();
			Destroy (gameObject, 1);
			SpriteRenderer rend = GetComponentInChildren<SpriteRenderer>();
			Collider2D col = GetComponent<CircleCollider2D>();
			rend.enabled = false;
			col.enabled = false;
		}
	}


}
