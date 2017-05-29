using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
	public float time;

	void Start()
	{
		Destroy (gameObject, time);
	}
}
