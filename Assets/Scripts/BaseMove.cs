using UnityEngine;
using System.Collections;

public class BaseMove : MonoBehaviour {

	void Update()
	{
		if (transform.position.y <= -8.0f) 
		{
			Destroy (gameObject);
		}
	}

}
