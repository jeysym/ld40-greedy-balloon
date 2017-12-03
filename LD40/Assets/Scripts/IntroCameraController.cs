using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum IntroCamState
{
    WAITING, FADING, FOLLOWING
};

public class IntroCameraController : MonoBehaviour {

    public GameObject mainCamera;
    public float panOutSpeed;
    public float chaseSpeed;
    public float fadeSpeed;

    public CanvasGroup statusBarGroup;
    public CanvasGroup splashScreenGroup;

    private IntroCamState state = IntroCamState.WAITING;
    private Camera cameraComponent;
    private Camera mainCameraComponent;

    private bool cameraFollowStarted = false;

	// Use this for initialization
	void Start () {
        cameraComponent = GetComponent<Camera>();
        mainCameraComponent = mainCamera.GetComponent<Camera>();
	}

    void SwitchToMainCamera()
    {
        cameraComponent.enabled = false;
        mainCameraComponent.enabled = true;
        Destroy(this);
    }

    // Update is called once per frame
    void Update() {
        switch (state)
        {
            case IntroCamState.WAITING:
                if (Input.GetAxis("Burn") > 0.0f)
                    state = IntroCamState.FADING;
                break;
            case IntroCamState.FADING:
                doFading();
                break;
            case IntroCamState.FOLLOWING:
                doFollowing();
                break;
        }
    }

    private float fadeValue = 0.0f;
    void doFading()
    {
        fadeValue += (fadeSpeed * Time.deltaTime);
        if (fadeValue >= 1.0f)
        {
            state = IntroCamState.FOLLOWING;
            statusBarGroup.alpha = 1.0f;
            splashScreenGroup.alpha = 0.0f;
        }
        else
        {
            statusBarGroup.alpha = fadeValue;
            splashScreenGroup.alpha = (1 - fadeValue);
        }
    }

    void doFollowing() { 
        if (cameraFollowStarted || Input.GetAxis("Burn") > 0.0f)
        {
            Vector3 toMainCamera = Vector3.Normalize(mainCamera.transform.position - transform.position);
            float distanceToMainCamera = Vector3.Magnitude(mainCamera.transform.position - transform.position);

            bool sizeOK = cameraComponent.orthographicSize >= mainCameraComponent.orthographicSize;
            bool positionOK = distanceToMainCamera < 5.0f;

            if (sizeOK && positionOK)
            {
                SwitchToMainCamera();
            }
            else
            {
                if (!sizeOK) 
                    cameraComponent.orthographicSize += (Time.deltaTime * panOutSpeed);

                if (!positionOK) 
                    transform.position += (chaseSpeed * Time.deltaTime * (mainCamera.transform.position - transform.position));
                    

                cameraFollowStarted = true;
            }
        }
	}
}
