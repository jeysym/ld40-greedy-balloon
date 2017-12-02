using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonController : MonoBehaviour {

    public Text hpCountText;
    public Text goldBarCountText;

    public Transform fallingObjectSpawnLocation;
    public Transform fallingObject;

    public int health = 100;
    public int goldBars = 0;

    public float goldenBarSlowdown; 
	public float hSpeed;
	public float vSpeed;

    private bool xWasDown = false;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody2D>();
        
	}

	// Update is called once per frame
	void Update () {
        hpCountText.text = "" + health;
        goldBarCountText.text = "" + goldBars;

		float hAxis = Input.GetAxis("Horizontal");
		float burn = Input.GetAxis("Burn");
        bool xDown = Input.GetKey(KeyCode.X);

        float maxVForce = vSpeed + (1 - goldenBarSlowdown) * (float)goldBars;
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
                Instantiate(fallingObject, fallingObjectSpawnLocation.position, Quaternion.identity);
            }
        }
        
        if (xDown == false)
            xWasDown = false;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GoldenBar"))
        {
            other.gameObject.SetActive(false);
            goldBars++;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "GoldenBar")
        {
            other.gameObject.SetActive(false);
            goldBars++;
        }
        else if (other.gameObject.tag == "Mountain")
        {
            health -= 20;
        }
    }
}
