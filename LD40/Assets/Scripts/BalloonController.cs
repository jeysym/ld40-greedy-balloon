using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonController : MonoBehaviour {

    public Text hpCountText;
    public Text goldBarCountText;

    public Transform explosionEffect;

    public Transform fallingObjectSpawnLocation;
    public Transform fallingObject;
    public float fallingObjectThrow;
    public float fallingObjectTorque;

    public int health = 100;
    public int goldBars = 0;

    public float goldBarMass; 
	public float hSpeed;
	public float vSpeed;

    private bool xWasDown = false;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody2D>();
		rb.mass = 1.0f;
	}

	// Update is called once per frame
	void Update () {
        hpCountText.text = "" + health;
        //goldBarCountText.text = "" + goldBars;
		goldBarCountText.text = "" + rb.mass.ToString();
		float hAxis = Input.GetAxis("Horizontal");
		float burn = Input.GetAxis("Burn");
        bool xDown = Input.GetKey(KeyCode.X);

        float maxVForce = vSpeed;
        float maxHForce = hSpeed * hAxis;

		if (burn > 0)
		{
			rb.AddForce(new Vector2(0.0f, 1.0f) * maxVForce);
		}
		rb.AddForce(new Vector2(1.0f, 0.0f) * maxHForce);

        if (xDown && xWasDown == false)
        {
            xWasDown = true;
            if (goldBars > 0)
            {
                goldBars--;
				rb.mass -= goldBarMass;
                Transform fallingInstance = Instantiate(fallingObject, fallingObjectSpawnLocation.position, Quaternion.identity);
                Rigidbody2D fRB = fallingInstance.GetComponent<Rigidbody2D>();
                if (fRB != null)
                {
                    fRB.AddForce(new Vector2(-fallingObjectThrow, 0.0f));
                    fRB.AddTorque(fallingObjectTorque);
                }
            }
        }
        
        if (xDown == false)
            xWasDown = false;

        if (health == 0)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            health--;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GoldenBar"))
        {
            other.gameObject.SetActive(false);
            goldBars++;
			rb.mass += goldBarMass;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "GoldenBar")
        {
            other.gameObject.SetActive(false);
            goldBars++;
			rb.mass += goldBarMass;
        }
        else if (other.gameObject.tag == "Mountain")
        {
            health -= 20;
        }
        else if (other.gameObject.tag == "Bird")
        {
            health -= 10;
        }
    }
}
