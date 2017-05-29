using UnityEngine;
using System.Collections;

public class BackgroundEffect : MonoBehaviour {


	private SpriteRenderer bg; 
	private AudioSource[] sounds;
	private StarScript stars;

	void Start () 
	{
		bg = GetComponentInChildren<SpriteRenderer> ();
		stars = GameObject.Find ("StarField").GetComponent<StarScript> ();
		sounds = GetComponents<AudioSource> ();
	}
	
	public IEnumerator fadeIn()
	{
		sounds [0].Play ();
		StartCoroutine(stars.speedUp ());
		yield return new WaitForSeconds(5f);
		float alpha = 0.0f;
		sounds [1].Play ();
		for(int i = 0; i <= 40; i++)
		{
			bg.color = new Color(1.0f, 1.0f, 1.0f, alpha);
			alpha += 0.025f;
			yield return new WaitForSeconds(0.1f);
		}

	}

	public IEnumerator fadeOut()
	{
		float alpha = 1.0f;
		for(int i = 0; i <= 20; i++)
		{
			bg.color = new Color(1.0f, 1.0f, 1.0f, alpha);
			alpha -= 0.05f;
			yield return new WaitForSeconds(0.1f);
		}
		stars.resetSpeed ();
	}



}
