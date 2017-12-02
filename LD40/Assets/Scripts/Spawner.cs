using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public float spawnTime;
	public GameObject target;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}


	void Spawn()
	{
        float height = transform.lossyScale.y;

        float top = transform.position.y + height / 2;
        float bottom = transform.position.y - height / 2;

        Vector3 spawnPlace = new Vector3(transform.position.x, Random.Range(bottom,top));
		GameObject instance = Instantiate(target, spawnPlace, Quaternion.identity);
        //new Vector3(Balloon.transform.position.x+2*RightEdge,Placement)
    }

}
