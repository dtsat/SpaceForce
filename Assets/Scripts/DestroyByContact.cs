using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	private GameController tracker;
	private GameObject waveTrack;
	private PlayerControl player;
	private AudioSource downgrade;

	void Start()
	{
		waveTrack = GameObject.Find ("GameController");
		tracker = waveTrack.GetComponent<GameController> ();
		downgrade = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Boundary") || other.CompareTag("PowerUp") || other.CompareTag("EnemyShot") || other.CompareTag("EnemyA") || other.CompareTag("EnemyB") || other.CompareTag("Boss") || other.CompareTag("Right") || other.CompareTag("Left"))
			return;	
		
		Instantiate (explosion, transform.position, transform.rotation);

		if (other.CompareTag ("Player")) 
		{
			player = other.GetComponent<PlayerControl>();
			StartCoroutine(player.degradeGun ());
			if (player.getGun () == 0) 
			{
				tracker.setGameAlive ();
				Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
				Destroy(other.gameObject,2);
				SpriteRenderer[] rend = other.GetComponentsInChildren<SpriteRenderer>();
				Collider2D col = other.GetComponent<CircleCollider2D>();
				foreach(SpriteRenderer i in rend)
					i.enabled = false;
				col.enabled = false;
			}
			else
				downgrade.Play ();
		}
		else
			Destroy(other.gameObject);
		
		if(gameObject.CompareTag("EnemyA"))
		{
			tracker.killShip(1);
			tracker.loseShip ();
		}
		else if(gameObject.CompareTag("EnemyB"))
		{
			tracker.killShip(2);
			tracker.loseShip ();
		}

		Destroy (gameObject, 0.50f);
		SpriteRenderer[] rend2 = GetComponentsInChildren<SpriteRenderer>();
		Collider2D col2 = GetComponent<CircleCollider2D>();
		foreach(SpriteRenderer i in rend2)
			i.enabled = false;
		col2.enabled = false;
	}
}