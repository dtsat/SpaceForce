using UnityEngine;
using System.Collections;

public class HandBehaviour : MonoBehaviour {


	public GameObject playerExplosion;
	private GameController tracker;
	private GameObject waveTrack;
	private PlayerControl player;
	private AudioSource[] sounds;
	private SpriteRenderer colour;
	private Sprite currentHand;
	public Sprite fist;
	private Collider2D coll;
	private Color current;
	private int handLife;
	private bool alive;
	public GameObject handExplosion;
	public GameObject shot;
	public Transform gun;
	private GameObject bossUnit;
	private BossFight bossScript;

	public float speed;
	public float offset, modifier;
	public float direction;

	private float attackspeed;

	void Start()
	{
		waveTrack = GameObject.Find ("GameController");
		tracker = waveTrack.GetComponent<GameController> ();
		attackspeed = tracker.handSpeed;
		bossUnit = GameObject.FindGameObjectWithTag ("BossObject");
		bossScript = bossUnit.GetComponent<BossFight> ();
		sounds = GetComponents<AudioSource> ();
		colour = GetComponentInChildren<SpriteRenderer> ();
		coll = GetComponent<Collider2D> ();
		current = colour.color;
		handLife = 5;
		alive = true;
		currentHand = colour.sprite;
		StartCoroutine (attackPattern ());
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Boundary") || other.CompareTag("PowerUp") || other.CompareTag("EnemyShot") || other.CompareTag("EnemyA") || other.CompareTag("EnemyB") || other.CompareTag("Boss"))
			return;	

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
				sounds[0].Play ();
		}
		else
			Destroy(other.gameObject);

		sounds [1].Play ();
		StartCoroutine (colourChange ());
		handLife--;
		handDie ();
	}

	IEnumerator colourChange()
	{
		colour.color = new Color (1.0f, 0.0f, 0.0f, 1.0f);
		yield return new WaitForSeconds (0.15f);
		colour.color = current;
	} 

	void shooting()
	{
		Instantiate(shot, gun.position, gun.rotation);
	}
		
	IEnumerator attackPattern()
	{
		while (alive && tracker.getGameAlive()) 
		{
			yield return new WaitForSeconds (attackspeed);
				sounds [3].Play ();
				yield return new WaitForSeconds (1.0f);
				for (int i = 0; i < 8; i++) {
				if (!alive || !tracker.getGameAlive())
						break;
					shooting ();
					yield return new WaitForSeconds (0.20f);
				}
				yield return new WaitForSeconds (1.0f);
			}
	}
		
	void handDie()
	{
		if (handLife == 0) 
		{
			alive = false;
			Instantiate (handExplosion, transform.position, transform.rotation);
			sounds [2].Play ();
			Destroy (gameObject, 3);
			if(gameObject.CompareTag("Left"))
				bossScript.setDead(1);
			else if (gameObject.CompareTag("Right"))
				bossScript.setDead(2);
			colour.enabled = false;
			coll.enabled = false;
		}
	}
		
	public bool getStatus()
	{
		return alive;
	}

	void Update () 
	{
		offset = direction * Mathf.Sin (modifier);
		modifier += 0.025f;
	}

	void FixedUpdate()
	{
		transform.Translate (new Vector3 (offset, 0.0f, 0.0f) * speed * Time.deltaTime);
	}
}
