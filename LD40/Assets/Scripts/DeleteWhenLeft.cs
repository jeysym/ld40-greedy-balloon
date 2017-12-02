using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWhenLeft : MonoBehaviour {

    public GameObject wallOfDeath;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float deltaX = transform.position.x - wallOfDeath.transform.position.x;
        if (deltaX < -10)
        {
            Destroy(gameObject);
        }
	}
}
