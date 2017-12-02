using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


	public GameObject Balloon;       


	private Vector3 Distance_CB;  

	// Use this for initialization
	void Start () {
		Distance_CB = transform.position - Balloon.transform.position;
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		transform.position = Balloon.transform.position + Distance_CB;
	}

	       

}

