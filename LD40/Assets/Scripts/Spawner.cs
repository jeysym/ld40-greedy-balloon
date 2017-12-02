﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public float Top;
	public float Bottom;
	public float RightEdge;
	public float SpawnTime;
	public GameObject Target;
	public GameObject Balloon;


	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", SpawnTime, SpawnTime);
	}


	void Spawn()
	{
		float Placement = Random.Range(Bottom,Top);
		Target = Instantiate(Target,new Vector3(Balloon.transform.position.x+2*RightEdge,Placement),Quaternion.identity);
	}

}
