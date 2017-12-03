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
    }

    public GameObject[] mountainRanges;
    private float mountainRangeStart = 40.0f;
    private float mountainRangeWidth = 100.0f;
    private float xSpawnThreshold = -60.0f;

    void trySpawnMountain()
    {
        float x = transform.position.x;
        if (x > xSpawnThreshold)
        {
            float spawnX = xSpawnThreshold + mountainRangeWidth;
            Vector3 spawnPlace = new Vector3(spawnX, 0.0f, 0.0f);

            int i = (int)Random.Range(0.0f, (float)mountainRanges.Length);

            Instantiate(mountainRanges[i], spawnPlace, Quaternion.identity);
            xSpawnThreshold += mountainRangeWidth;
        }
    }

    void Update()
    {
        wall.transform.position = transform.position - new Vector3(cameraHalfwidth, 0.0f);
        trySpawnMountain();
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

