using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour {

    public void QuitApplication()
    {
        Debug.Log("Application quits now...");
        Application.Quit();
    }
}
