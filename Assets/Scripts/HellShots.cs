using UnityEngine;
using System.Collections;

public class HellShots : MonoBehaviour {

	private int count;
	public int speed;

	void Start () 
	{
		GetComponent<Rigidbody2D> ().velocity = transform.up * speed;
		count = 0;
	}
	
	public int getCount()
	{
		return count;
	}

	public void incCount()
	{
		count++;
	}
}
