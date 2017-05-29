using UnityEngine;
using System.Collections;

public class StarScript : MonoBehaviour {


	private ParticleSystem stars;
	private Vector3 current;

	void Start () 
	{
		stars = GetComponentInChildren<ParticleSystem> ();
		current = transform.position;
	}
	
	public IEnumerator speedUp()
	{
		for (int i = 0; i < 10; i++) 
		{
			stars.gravityModifier += 0.5f;
			yield return new WaitForSeconds (0.25f);
		}
		for (int i = 0; i < 10; i++) 
		{
			stars.gravityModifier -= 0.5f;
			yield return new WaitForSeconds (0.25f);
		}
		stars.startSpeed = 0;
	}

	public void resetSpeed()
	{
		stars.startSpeed = 2;
	}

}
