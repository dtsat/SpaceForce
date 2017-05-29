using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuControls : MonoBehaviour {

	private AudioSource[] sounds;
	private bool top, middle, bottom;
	public float delay;

	void Start () 
	{
		sounds = GetComponents<AudioSource> ();
		top = true;
		middle = false;
		bottom = false;
		delay = 1.0f;
	}
	
	void upOne()
	{
		transform.Translate (0.0f, 0.6f, 0.0f);
		sounds [0].Play ();
	}

	void downOne()
	{
		transform.Translate (0.0f, -0.6f, 0.0f);
		sounds [0].Play ();
	}

	IEnumerator selectOption()
	{
		sounds [1].Play ();
		yield return new WaitForSeconds (delay);
		if (top)
			SceneManager.LoadScene ("MainGameNormal");
		else if (middle)
			SceneManager.LoadScene ("MainGameHellMode");
		else if (bottom)
			Application.Quit ();
			

	}

	void FixedUpdate () 
	{
		if (top) 
		{
			if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S)) {
				downOne ();
				top = false;
				middle = true;
			} else if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W))
				sounds [0].Play ();
			else if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Return))
				StartCoroutine(selectOption ());
			return;
		}
		else if (middle) 
		{
			if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S)) 
			{
				downOne ();
				middle = false;
				bottom = true;
			} 
			else if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W))
			{
				upOne ();
				middle = false;
				top = true;
			}
			else if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Return))
				StartCoroutine(selectOption ());
			return;
		}
		else if(bottom)
		{
			if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S)) 
				sounds [0].Play ();
			else if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W))
			{
				upOne ();
				bottom = false;
				middle = true;
			}
			else if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Return))
				StartCoroutine(selectOption ());
			return;
		}
	}
}
