using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


	public GameObject Wall;       


	private Vector3 Distance_CW;  

	// Use this for initialization
	void Start () {
		Distance_CW = transform.position - Wall.transform.position;
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		transform.position = Wall.transform.position + Distance_CW;
	}

	       

}

