using UnityEngine;
using System.Collections;

public class SendShips : MonoBehaviour {

	public GameObject[] ships;

	void Start () 
	{
		StartCoroutine(SendWave ());
	}

	IEnumerator SendWave()
	{
		for(int i = 0; i < 5; i++)
		{
			Instantiate (ships [Random.Range (0, 3)], new Vector3(-3.2f, 4.5f, 0.0f), ships[0].transform.rotation);
			yield return new WaitForSeconds (0.75f);
		}
	}
}