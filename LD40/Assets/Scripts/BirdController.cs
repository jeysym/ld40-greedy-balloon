using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {

	public float speed;
	public GameObject Wall;

	private bool living;

	// Use this for initialization
	void Start () {
		living = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (living) {
			transform.position += new Vector3 (-Time.deltaTime * speed, 0.0f);
			if ((transform.position.x - Wall.transform.position.x) < -5 && (transform.position.z != -1)) {
				Destroy (this.gameObject);
			}
		} else {
			transform.position += new Vector3 (0.0f, -Time.deltaTime * speed);
			if(transform.position.y <-11){
				Destroy (this.gameObject);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{

		if ((other.gameObject.tag == "Player")||(other.gameObject.tag == "GoldenBar"))
		{
			living = false;
			PolygonCollider2D BirdCollider = GetComponent<PolygonCollider2D> ();
			BirdCollider.enabled = false;
			transform.Rotate (0.0f, 0.0f, 90.0f);
		}
	}
}
