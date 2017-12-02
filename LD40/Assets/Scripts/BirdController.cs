using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {

	public float speed;
	public GameObject Wall;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (-Time.deltaTime * speed, 0.0f);
		if ((transform.position.x - Wall.transform.position.x) < 0) 
		{
			Destroy (this.gameObject);
		}
	}
}
