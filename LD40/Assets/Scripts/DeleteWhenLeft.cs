using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWhenLeft : MonoBehaviour {

    public GameObject wallOfDeath;
    public float objectWidth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.z == -1.0f)
            return;

        float deltaX = transform.position.x - wallOfDeath.transform.position.x;
        if (deltaX < -(objectWidth + 10.0f))
        {
            Destroy(gameObject);
        }
	}
}
