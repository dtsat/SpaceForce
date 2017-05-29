using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour {

	public float speed;
	private GameObject shipTracker;
	private Transform targetPosition;

	void Start () 
	{
		shipTracker = GameObject.Find ("PlayerShip");
		if (shipTracker != null) 
		{
			targetPosition = shipTracker.GetComponent<Transform> ();
			SetPosition ();
		}
		else
			GetComponent<Rigidbody2D> ().velocity = transform.up * 2 * speed;
	}

	void SetPosition()
	{
		GetComponent<Rigidbody2D> ().velocity = (targetPosition.position - transform.position).normalized * speed;
	}
}
