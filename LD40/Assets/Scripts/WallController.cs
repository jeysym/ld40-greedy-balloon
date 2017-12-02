using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

	public GameObject Balloon;       
	public float FixedDistance;
 

	// Use this for initialization
	void Start () {
		transform.position = Balloon.transform.position - new Vector3 (FixedDistance, 0.0f);
	}

	void FixedUpdate () 
	{
		float Distance_WB = transform.position.x - Balloon.transform.position.x;
		Vector3 Change = new Vector3 (Distance_WB, 0.0f);
		if (Change.magnitude >= FixedDistance) {
			transform.position = Balloon.transform.position - new Vector3 (FixedDistance, 0.0f);
		} 
		else 
		{
			transform.position = new Vector3 (transform.position.x, Balloon.transform.position.y);
		}
	}
		
}
