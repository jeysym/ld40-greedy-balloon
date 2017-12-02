﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWhenLeft : MonoBehaviour {

    public GameObject wallOfDeath;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.z == -1)
            return;

        float deltaX = transform.position.x - wallOfDeath.transform.position.x;
        if (deltaX < -80)
        {
            Destroy(gameObject);
        }
	}
}
