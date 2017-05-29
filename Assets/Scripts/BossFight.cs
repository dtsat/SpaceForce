using UnityEngine;
using System.Collections;

public class BossFight : MonoBehaviour {

	public GameObject head, left, right;
	private AudioSource[] sounds;
	public Transform headPos, leftPos, rightPos;
	private bool leftAlive, rightAlive, headAlive;
	private HeadBehaviour headScript;

	void Start () 
	{
		sounds = GetComponents<AudioSource> ();
		headScript = GetComponent<HeadBehaviour> ();
		leftAlive = true;
		rightAlive = true;
		headAlive = true;
		StartCoroutine (bossSpawn ());
	}
	
	IEnumerator bossSpawn()
	{
		yield return new WaitForSeconds (2f);
		Instantiate (head, headPos.position, headPos.rotation);
		sounds [1].Play ();
		yield return new WaitForSeconds (0.5f);
		Instantiate (left, leftPos.position, leftPos.rotation);
		sounds [1].Play ();
		yield return new WaitForSeconds (0.5f);
		Instantiate (right, rightPos.position, rightPos.rotation);
		sounds [1].Play ();
		yield return new WaitForSeconds (0.5f);
		sounds [0].Play ();
	}

	public void setDead(int i)
	{
		if (i == 1)
			leftAlive = false;
		else if (i == 2)
			rightAlive = false;
		else
			headAlive = false;
	}

	IEnumerator vulnerable()
	{
		GameObject tracker = GameObject.FindGameObjectWithTag ("Boss");
		leftAlive = true;
		rightAlive = true;
		tracker.GetComponent<HeadBehaviour> ().setVulnerable ();
		yield return new WaitForSeconds (3f);
		if(headAlive)
			tracker.GetComponent<HeadBehaviour> ().setReady ();
		yield return new WaitForSeconds (0.5f);
		if(headAlive)
			Instantiate (left, leftPos.position, leftPos.rotation);
		sounds [1].Play ();
		yield return new WaitForSeconds (0.5f);
		if(headAlive)
			Instantiate (right, rightPos.position, rightPos.rotation);
		sounds [1].Play ();
		yield return new WaitForSeconds (0.5f);
	}
		

	void Update()
	{
		if (!leftAlive && !rightAlive) 
		{
			StartCoroutine (vulnerable ());
		}
		if (!headAlive) 
		{
			sounds [0].Stop ();
			GameObject.Find ("GameController").GetComponent<GameController> ().setBossDead ();
			Destroy (gameObject);
		}
	}

}
