using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour {

	private Text[] canvasTexts;
	public float delay;
	private AudioSource[] sounds;

	void Start () 
	{
		canvasTexts = GetComponentsInChildren<Text> ();
		sounds = GetComponents<AudioSource> ();
	}
	
	public void UpdateScore(string s)
	{
		canvasTexts [0].text = s;
	}

	public void ShowWave()
	{
		canvasTexts [1].text = "WAVE CLEARED!";
		sounds [1].Play ();
	}

	public void HideWave()
	{
		canvasTexts [1].text = "";
	}

	public void ShowPower()
	{
		canvasTexts [2].text = "POWER UP AVAILABLE!";
	}

	public void HidePower()
	{
		canvasTexts [2].text = "";
	}

	public void ShowMulti()
	{
		canvasTexts [3].text = "BONUS\nMULTIPLIER\nINCREASED!";
		sounds [0].Play ();
	}

	public void HideMulti()
	{
		canvasTexts [3].text = "";
	}

	public void showGameOver(string s)
	{
		canvasTexts [4].text = s;
		sounds [2].Play ();
	}

	public void showBossWin(string s)
	{
		canvasTexts [5].text = s;
	}

	public void winjingle()
	{
		sounds [3].Play ();
	}

}
