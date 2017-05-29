using UnityEngine;
using System.Collections;

public class HeadBehaviour : MonoBehaviour {

	public GameObject playerExplosion;
	private GameController tracker;
	private GameObject waveTrack;
	private PlayerControl player;
	private AudioSource[] sounds;
	private SpriteRenderer colour;
	private Sprite currentHead;
	public Sprite hurt;
	private Collider2D coll;
	private Color current;
	private int headLife;
	private bool alive;
	private bool handsAlive;
	public GameObject headExplosion;
	public GameObject shot;
	public Transform gun1, gun2;
	public Transform[] explos;
	private BossLifeBar lifebar;

	public float speed;
	public float offset, modifier;
	public float direction;

	private float attackspeed;

	void Start()
	{
		waveTrack = GameObject.Find ("GameController");
		tracker = waveTrack.GetComponent<GameController> ();
		attackspeed = tracker.headSpeed;
		sounds = GetComponents<AudioSource> ();
		colour = GetComponentInChildren<SpriteRenderer> ();
		current = colour.color;
		headLife = 10;
		alive = true;
		handsAlive = true;
		currentHead = colour.sprite;
		lifebar = GameObject.FindGameObjectWithTag ("BossLife").GetComponent<BossLifeBar> ();
		StartCoroutine(lifebar.bossFightStart ());
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

		if (!handsAlive) {
			sounds [1].Play ();
			StartCoroutine (colourChange ());
			lifebar.getHit ();
			headLife--;
			StartCoroutine(headDie ());
		} else
			sounds [2].Play ();
	}

	IEnumerator colourChange()
	{
		colour.color = new Color (1.0f, 0.0f, 0.0f, 1.0f);
		yield return new WaitForSeconds (0.15f);
		colour.color = current;
	} 

	IEnumerator shooting()
	{
		Instantiate(shot, gun1.position, gun1.rotation);
		yield return new WaitForSeconds (0.15f);
		Instantiate(shot, gun2.position, gun2.rotation);
	}

	IEnumerator attackPattern()
	{
		while (alive && tracker.getGameAlive()) 
		{
			yield return new WaitForSeconds (attackspeed);
			sounds [3].Play ();
			yield return new WaitForSeconds (1.0f);
			for (int i = 0; i < 15; i++) {
				if (!alive || !handsAlive || !tracker.getGameAlive())
					break;
				StartCoroutine(shooting ());
				yield return new WaitForSeconds (0.1f);
			}
			yield return new WaitForSeconds (1.0f);
		}
	}

	public void setVulnerable()
	{
		colour.sprite = hurt;
		handsAlive = false;
	}

	public void setReady()
	{
		colour.sprite = currentHead;
		handsAlive = true;
	}

	IEnumerator headDie()
	{
		if (headLife == 0) 
		{
			GameObject.FindGameObjectWithTag ("BossObject").GetComponent<BossFight>().setDead(3);
			alive = false;
			foreach (Transform e in explos) {
				Instantiate (headExplosion, e.position, e.rotation);
				yield return new WaitForSeconds (0.35f);
			}
			sounds [4].Play ();
			lifebar.bossFightOff ();
			Destroy (gameObject, 3);
			colour.enabled = false;
		}
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

