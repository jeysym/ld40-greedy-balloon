using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Balloon
    public GameObject balloon;
    public float chaseH = 0.1f;
    public float chaseV = 0.0f;

    // Wall of death
	public GameObject wall;       

    private Camera cameraComponent;
    private float cameraHalfwidth = 0.0f;

	// Use this for initialization
	void Start () {
        cameraComponent = GetComponent<Camera>();
        cameraHalfwidth = cameraComponent.aspect * cameraComponent.orthographicSize;

        InvokeRepeating("SpawnBird", birdSpawnTime, birdSpawnTime);
    }

    public GameObject[] mountainRanges;
    private float mountainRangeStart = 40.0f;
    private float mountainRangeWidth = 100.0f;
    private float xSpawnThreshold = -60.0f;

    public GameObject[] cloudFormations;
    public float cloudFormationsHeight;

	public GameObject goldBar;

    void trySpawnMountainsAndCloudsAndGoldBar()
    {
        float x = transform.position.x;
        if (x > xSpawnThreshold)
        {
            float spawnX = xSpawnThreshold + mountainRangeWidth;
            Vector3 spawnPlace = new Vector3(spawnX, 0.0f, 0.0f);
            Vector3 cloudFormSpawnPlace = new Vector3(spawnX, cloudFormationsHeight, 0.0f);

            int i = (int)Random.Range(0.0f, (float)mountainRanges.Length);
            int j = (int)Random.Range(0.0f, (float)cloudFormations.Length);

            Instantiate(mountainRanges[i], spawnPlace, Quaternion.identity);
            Instantiate(cloudFormations[j], cloudFormSpawnPlace, Quaternion.identity);

			float y = Random.Range(birdsLow,birdsHigh+1.0f);
			float x1 = spawnX + Random.Range(0.0f, mountainRangeWidth);
			float x2 = spawnX + Random.Range(0.0f, mountainRangeWidth);
			float x3 = spawnX + Random.Range(0.0f, mountainRangeWidth);
			Vector3 goldBarSpawnPlace1 = new Vector3 (x1, y, 0.0f);
			Vector3 goldBarSpawnPlace2 = new Vector3 (x2, y, 0.0f);
			Vector3 goldBarSpawnPlace3 = new Vector3 (x3, y, 0.0f);
			Instantiate (goldBar, goldBarSpawnPlace1, Quaternion.identity);
			Instantiate (goldBar, goldBarSpawnPlace2, Quaternion.identity);
			Instantiate (goldBar, goldBarSpawnPlace3, Quaternion.identity);

            xSpawnThreshold += mountainRangeWidth;
        }
    }

    public GameObject bird;
    public float birdsLow;
    public float birdsHigh;
    public float birdSpawnTime;

    void SpawnBird()
    {
        float y = Random.Range(birdsLow, birdsHigh + 1.0f);
        Vector3 spawnPosition = new Vector3(transform.position.x + cameraHalfwidth + 5, y, 0.0f);
        Instantiate(bird, spawnPosition, Quaternion.identity);
    }

    void Update()
    {
        wall.transform.position = transform.position - new Vector3(cameraHalfwidth, 0.0f);
        trySpawnMountainsAndCloudsAndGoldBar();
    }

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
        Vector3 balloonPosition = balloon.transform.position;

        // Handle X axis
        float xBalloon = balloonPosition.x;
        float xCameraLeft = transform.position.x - cameraHalfwidth;
        float xDelta = xBalloon - xCameraLeft;
        float xThreshold = (chaseH * 2 * cameraHalfwidth) + xCameraLeft;

        if (xBalloon > xThreshold)
        {
            transform.position += new Vector3(xBalloon - xThreshold, 0.0f);
        }

        // Handle Y axis
        float yBalloon = balloonPosition.y;
        float yCameraTop = transform.position.y + cameraComponent.orthographicSize;
        float yCameraBottom = transform.position.y - cameraComponent.orthographicSize;

        float yTopThreshold = yCameraTop - (chaseV * 2 * cameraComponent.orthographicSize);
        float yBottomThreshold = yCameraBottom + (chaseV * 2 * cameraComponent.orthographicSize);

        if (yBalloon > yTopThreshold)
        {
            transform.position += new Vector3(0.0f, yBalloon - yTopThreshold);
        }
        else if (yBalloon < yBottomThreshold)
        {
            transform.position += new Vector3(0.0f, yBalloon - yBottomThreshold);
        }
    }

	       

}

