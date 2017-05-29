using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossLifeBar : MonoBehaviour {

	private HeadBehaviour healthTrack;
	private Text bossText;
	private Image[] lifeBar;
	private AudioSource beep;
	private int health;

	void Start () 
	{
		bossText = GetComponentInChildren<Text>();
		lifeBar = GetComponentsInChildren<Image> ();
		bossFightOff ();
		beep = GetComponent<AudioSource> ();
	}
	
	public IEnumerator bossFightStart()
	{
		health = 9;
		healthTrack = GameObject.FindGameObjectWithTag ("Boss").GetComponent<HeadBehaviour> ();
		bossText.enabled = true;
		for(int i = 0; i < 10; i++)
		{
			lifeBar[i].enabled = true;
			beep.Play ();
			yield return new WaitForSeconds(0.1f);
		}
			
	}

	public void getHit()
	{
		if(health >= 0)
			lifeBar[health--].enabled = false;
	}

	public void bossFightOff()
	{
		bossText.enabled = false;
		for (int i = 0; i < 10; i++)
			lifeBar [i].enabled = false;
	}
}
