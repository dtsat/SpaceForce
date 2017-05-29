using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] wave1, wave2, wave3;
	public GameObject boss;
	public Vector3 spawnValues;
	public float startWait, waveWait;
	public bool waveDead, bossDead;

	public int killedShips, lostShips;
	public GameObject powerUp;
	private GameObject waveTrack;

	private int gameScore, multiplier;

	private GameObject hudTrack;
	private UIScript guiFunctions;

	private bool gameAlive, gameOverDisplayed;

	private AudioSource music;

	private BackgroundEffect bg;

	public int scoreA, scoreB, bossScore;
	public float movement, headSpeed, handSpeed;


	void Start()
	{
		movement = 1.5f;
		headSpeed = 6.0f;
		handSpeed = 2.0f;
		music = GetComponent<AudioSource> ();
		bg = GameObject.Find ("Background").GetComponent<BackgroundEffect> ();
		StartCoroutine(SpawnWaves ());
		gameScore = 0;
		multiplier = 1;
		killedShips = 5;
		lostShips = 5;
		hudTrack = GameObject.Find ("HUD");
		guiFunctions = hudTrack.GetComponent<UIScript> ();
		gameAlive = true;
		gameOverDisplayed = false;
	}

	public int getScore()
	{
		return gameScore;
	}

	public void addScore(int n)
	{
		gameScore += n*multiplier;
		guiFunctions.UpdateScore("SCORE - " + gameScore.ToString() + "\nMULTI - " + multiplier.ToString() + "X");
	}

	public void setWaveDead()
	{
		waveDead = true;
	}

	public void setBossDead()
	{
		bossDead = true;
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);

		while (gameAlive) 
		{
			yield return new WaitForSeconds (1.0f);
			for (int i = 0; i < 3; i++) 
			{
				if (gameAlive) {
					int number = Random.Range (0, 2);
					if (number == 0) {
						int waves = Random.Range (2, 5);
						killedShips = 5 * waves;
						lostShips = killedShips;
						for (int j = 0; j < waves; j++) {
							Vector3 spawnPosition = new Vector3 (Random.Range (-1.65f, 1.65f), spawnValues.y, spawnValues.z);
							Instantiate (wave1 [Random.Range (0, 4)], spawnPosition, wave1 [0].transform.rotation);
							yield return new WaitForSeconds (0.35f);
							Instantiate (wave1 [Random.Range (0, 4)], new Vector3 (spawnPosition.x + 0.75f, spawnPosition.y, spawnPosition.z), wave1 [0].transform.rotation);
							Instantiate (wave1 [Random.Range (0, 4)], new Vector3 (spawnPosition.x - 0.75f, spawnPosition.y, spawnPosition.z), wave1 [0].transform.rotation);
							yield return new WaitForSeconds (0.35f);
							Instantiate (wave1 [Random.Range (0, 4)], new Vector3 (spawnPosition.x + 1.50f, spawnPosition.y, spawnPosition.z), wave1 [0].transform.rotation);
							Instantiate (wave1 [Random.Range (0, 4)], new Vector3 (spawnPosition.x - 1.50f, spawnPosition.y, spawnPosition.z), wave1 [0].transform.rotation);
							yield return new WaitForSeconds (0.75f);
						}
					} else if (number == 1) {
						int waves = Random.Range (2, 5);
						killedShips = 5 * waves;
						lostShips = killedShips;
						for (int j = 0; j < waves; j++) {
							int type = Random.Range (0, 2);
							if (type == 0) {
								for (int k = 0; k < 5; k++) {
									Instantiate (wave2 [Random.Range (0, 4)], new Vector3 (-3.2f, 6.0f, 0.0f), wave2 [0].transform.rotation);
									yield return new WaitForSeconds (0.35f);
								}
							} else {
								for (int k = 0; k < 5; k++) {
									Instantiate (wave3 [Random.Range (0, 4)], new Vector3 (3.2f, 6.0f, 0.0f), wave3 [0].transform.rotation);
									yield return new WaitForSeconds (0.35f);
								}
							}
							yield return new WaitForSeconds (0.75f);
						}
					}
					waveDead = false;
					while (!waveDead)
						yield return null;
					yield return new WaitForSeconds (6.0f);
				} 
				else
					break;
			}
			killedShips = 5;
			lostShips = 5;
			music.Stop();
			StartCoroutine(bg.fadeIn ());
			yield return new WaitForSeconds (9.0f);
			Instantiate(boss, boss.transform.position, boss.transform.rotation);
			bossDead = false;
			while (!bossDead)
				yield return null;
			yield return new WaitForSeconds (5.0f);
			guiFunctions.winjingle ();
			guiFunctions.showBossWin ("BOSS DEFEATED!");
			yield return new WaitForSeconds (0.75f);
			guiFunctions.showBossWin ("BOSS DEFEATED!\n\nWELL DONE!");
			yield return new WaitForSeconds (0.75f);
			guiFunctions.showBossWin ("BOSS DEFEATED!\n\nWELL DONE!\n\nSCORE BONUS\n" + (bossScore*multiplier).ToString());
			addScore (bossScore * multiplier);
			StartCoroutine(bg.fadeOut ());
			yield return new WaitForSeconds (6.0f);
			guiFunctions.showBossWin ("");
			movement += 0.75f;
			headSpeed -= 0.25f;
			handSpeed -= 0.1f;
			music.Play ();
		}
	}

	public void killShip(int i)
	{
		killedShips--;
		if (i == 1)
			addScore (scoreA);
		else if (i == 2)
			addScore (scoreB);
	}

	public void loseShip()
	{
		lostShips--;
	}

	public void resestMulti()
	{
		multiplier = 1;
	}

	IEnumerator guiWave()
	{
		guiFunctions.ShowWave ();
		yield return new WaitForSeconds(2.0f);
		guiFunctions.ShowPower ();
		Instantiate (powerUp, new Vector3 (0.0f, 1.0f, 0.0f), transform.rotation);
		yield return new WaitForSeconds(2.0f);
		guiFunctions.HidePower ();
		guiFunctions.HideWave ();
	}

	IEnumerator guiWaveMulti()
	{
		if (gameAlive) 
		{	
			guiFunctions.ShowWave ();
			yield return new WaitForSeconds (2.0f);
			guiFunctions.ShowPower ();
			Instantiate (powerUp, new Vector3 (0.0f, 1.0f, 0.0f), transform.rotation);
			yield return new WaitForSeconds (2.0f);
			guiFunctions.ShowMulti ();
			guiFunctions.UpdateScore ("SCORE - " + gameScore.ToString () + "\nMULTI - " + multiplier.ToString () + "X");
			yield return new WaitForSeconds (2.0f);
			guiFunctions.HideMulti ();
			guiFunctions.HidePower ();
			guiFunctions.HideWave ();
		}
	}
		
	public void setGameAlive()
	{
		gameAlive = false;
	}

	public bool getGameAlive()
	{
		return gameAlive;
	}

	IEnumerator gameOver()
	{
		yield return new WaitForSeconds (3.0f);
		guiFunctions.showGameOver ("GAME\nOVER\n\nSCORE\n" + gameScore);
		yield return new WaitForSeconds (5.0f);
		SceneManager.LoadScene ("TitleMenu");
	}
		

	void Update () 
	{
		if (killedShips == 0) 
		{
			killedShips = 5;
			lostShips = 1;
			multiplier++;
			if (!gameOverDisplayed)
				StartCoroutine (guiWaveMulti ());
			setWaveDead ();
			return;
		}
		if (lostShips == 0) 
		{
			lostShips = 5;
			if (!gameOverDisplayed)
				StartCoroutine (guiWave ());
			setWaveDead ();
			return;
		}
		if (!gameAlive && !gameOverDisplayed) 
		{
			gameOverDisplayed = true;
			StartCoroutine (gameOver ());
		}
	}
}
