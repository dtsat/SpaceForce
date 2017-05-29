using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerControl : MonoBehaviour {

	public float speed, fireRate;
	private float nextFire;
	public Boundary boundary;

	public GameObject shot;
	public Transform[] gun;

	public int gunType;
	private SpriteRenderer colour;
	private Color current;

	private AudioSource downgrade;
	public GameObject playerExplosion;
	private GameController tracker;

	void Start() 
	{
		gunType = 1;
		colour = GetComponent<SpriteRenderer> ();
		current = colour.color;
		downgrade = GetComponent<AudioSource> ();
		tracker = GameObject.Find ("GameController").GetComponent<GameController> ();
	}

	public void upgradeGun()
	{
		if (gunType < 4)
			gunType++;
	}

	public IEnumerator degradeGun()
	{
		gunType--;
		colour.color = new Color (1.0f, 0.0f, 0.0f, 1.0f);
		yield return new WaitForSeconds (0.25f);
		colour.color = current;
	}

	public int getGun()
	{
		return gunType;
	}

	 
	void Update()
	{
		if (gunType == 1) 
		{
			if (Input.GetKeyDown (KeyCode.Space) && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				GameObject clone = Instantiate (shot, gun[0].position, gun[0].rotation) as GameObject;
			}
		}
		if (gunType == 2) 
		{
			if (Input.GetKeyDown (KeyCode.Space) && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				GameObject clone = Instantiate (shot, gun[1].position, gun[1].rotation) as GameObject;
				GameObject clone2 = Instantiate (shot, gun[2].position, gun[2].rotation) as GameObject;
			}
		}
		if (gunType == 3) 
		{
			if (Input.GetKeyDown (KeyCode.Space) && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				GameObject clone = Instantiate (shot, gun[0].position, gun[0].rotation) as GameObject;
				GameObject clone2 = Instantiate (shot, gun[3].position, gun[3].rotation) as GameObject;
				GameObject clone3 = Instantiate (shot, gun[4].position, gun[4].rotation) as GameObject;
			}
		}
		if (gunType == 4) 
		{
			if (Input.GetKey (KeyCode.Space) && Time.time > nextFire) {
				nextFire = Time.time + fireRate - 0.25f;
				GameObject clone = Instantiate (shot, gun[0].position, gun[0].rotation) as GameObject;
				GameObject clone2 = Instantiate (shot, gun[3].position, gun[3].rotation) as GameObject;
				GameObject clone3 = Instantiate (shot, gun[4].position, gun[4].rotation) as GameObject;
			}
		}
	}

	void FixedUpdate() 
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		transform.Translate (new Vector3(moveHorizontal, moveVertical, 0.0f) * speed * Time.deltaTime);

		transform.position = new Vector3
		(
			Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax),
			0.0f
		);

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("PlayerShot"))
		{
			HellShots counter = other.GetComponent<HellShots> ();
			if (counter.getCount() > 0) {
				StartCoroutine (degradeGun ());
				if (getGun () == 0) {
					tracker.setGameAlive ();
					Instantiate (playerExplosion, transform.position, transform.rotation);
					Destroy (other.gameObject);
					Destroy (gameObject, 2);
					SpriteRenderer[] rend = GetComponentsInChildren<SpriteRenderer> ();
					Collider2D col = GetComponent<CircleCollider2D> ();
					colour.enabled = false;
					foreach (SpriteRenderer i in rend)
						i.enabled = false;
					col.enabled = false;
				} else
					downgrade.Play ();
			}
		}
	}
			
}
