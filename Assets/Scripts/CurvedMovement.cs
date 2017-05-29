using UnityEngine;
using System.Collections;

public class CurvedMovement : MonoBehaviour {

	public float speed;
	public float offset, modifier;
	public float direction;

	void Start() 
	{
		speed = GameObject.Find ("GameController").GetComponent<GameController> ().movement;
		GetComponent<Rigidbody2D> ().velocity = transform.up * speed;
		offset = 0;
		modifier = 0;
	}


	void Update () 
	{
		offset = direction * Mathf.Sin (modifier);
		modifier += 0.025f;
	}

	void FixedUpdate()
	{
		transform.Translate (new Vector3 (offset, 0.0f, 0.0f) * Time.deltaTime);
	}
}
