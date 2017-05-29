using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

	public float speed;

	void Start() 
	{
		speed = GameObject.Find ("GameController").GetComponent<GameController> ().movement;
		GetComponent<Rigidbody2D> ().velocity = transform.up * speed;
	}
}
