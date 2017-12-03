using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BalloonController : MonoBehaviour {

    public Transform fireParticles;
    private ParticleSystem fireSystem;
    bool burnOn = false;

    public Text hpCountText;
    public Text goldBarCountText;

    public Transform explosionEffect;

    public Transform fallingObjectSpawnLocation;
    public Transform fallingObject;
    public float fallingObjectThrow;
    public float fallingObjectTorque;

	public int health;
	public int goldBars;
    public float goldBarMass; 
	public float hSpeed;
	public float vSpeed;
	public float velocityImpact;
	public int birdImpact;

    private bool goldBarThrown = false;
    private Rigidbody2D rb;
	private PolygonCollider2D pcol;
	private Rigidbody2D[] rbs;
	private PolygonCollider2D[] pcols;

	private bool dead;


    // Use this for initialization
    void Start () {
        fireSystem = fireParticles.GetComponent<ParticleSystem>();

		rb = gameObject.GetComponent<Rigidbody2D>();
		pcol = gameObject.GetComponent<PolygonCollider2D> ();
		rbs = gameObject.GetComponentsInChildren<Rigidbody2D> ();
		pcols = gameObject.GetComponentsInChildren<PolygonCollider2D>();
		rb.mass = 1.0f;
		dead = false;
	}


	void FixedUpdate () {
		if (!dead) 
		{
			hpCountText.text = "" + health;
			goldBarCountText.text = "" + goldBars;

			float hAxis = Input.GetAxis("Horizontal");
			float burn = Input.GetAxis("Burn");
			float throwGoldBar = Input.GetAxis("Throw");

			float maxVForce = vSpeed;
			float maxHForce = hSpeed * hAxis;

			if (burn > 0)
			{
                if (burnOn == false)
                {
                    fireSystem.Play();
                    burnOn = true;
                }

				rb.AddForce(new Vector2(0.0f, 1.0f) * maxVForce);
			}
            else
            {
                if (burnOn == true)
                {
                    fireSystem.Stop();
                    burnOn = false;
                }
            }

			rb.AddForce(new Vector2(1.0f, 0.0f) * maxHForce);

			if (throwGoldBar > 0 && goldBarThrown == false)
			{
				goldBarThrown = true;
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

			if (throwGoldBar == 0.0f)
				goldBarThrown = false;
			
			if (health <= 0)
			{
				Instantiate(explosionEffect, transform.position, Quaternion.identity);
				fireSystem.Stop();
				Death();
			}

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
			
			float velocity = other.relativeVelocity.magnitude;
			float f;
			if (velocity < 1.6f) {
				f = 0.3f * velocity;
			} else if (velocity < 3.5f) {
				f = 0.7f * velocity;
			} else if (velocity < 5.0f) {
				f = 1.0f * velocity;
			} else if (velocity < 10.0f) {
				f = 1.5f * velocity;
			} else {
				f = 2.0f * velocity;
			}

			health -= (int)(velocityImpact*f);
        }
        else if (other.gameObject.tag == "Bird")
        {
            health -= birdImpact;
        }
    }

	void Death()
	{
		Destroy(rb);
		Destroy (pcol);

		foreach (var item in pcols) {
			item.enabled = true;			
		}
		foreach (var item in rbs) {
			item.simulated = true;
		}

		dead = true;

		StartCoroutine(Reset());




	}

	IEnumerator Reset()
	{
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
		
}
