using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingMotion : MonoBehaviour {

    public float Amplitude;
    public float Freq;

    private Vector3 originalPosition;

	// Use this for initialization
	void Start () {
        originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate()
    {
        float yOffset = Amplitude * Mathf.Sin(Time.time * Freq);
        transform.position = new Vector3(originalPosition.x, originalPosition.y + yOffset, originalPosition.z);
    }
}
