using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour {

	private Rigidbody2D rb;

	public float hSpeed = 1.0f;
	public float vSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		float hAxis = Input.GetAxis("Horizontal");
		bool spaceDown = Input.GetKey(KeyCode.Space);

		if (spaceDown)
		{
			rb.AddForce(new Vector2(0.0f, 1.0f) * vSpeed);
		}
		rb.AddForce(new Vector2(1.0f, 0.0f) * hAxis * hSpeed);
	}
}
