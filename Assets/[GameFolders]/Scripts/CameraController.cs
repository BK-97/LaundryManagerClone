using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    public GameObject colorScene;
    public GameObject sewScene;
    public static UnityEvent OnSewOpen = new UnityEvent();
    public static UnityEvent OnColorOpen = new UnityEvent();

    private void OnEnable()
    {
        OnSewOpen.AddListener(OpenSewScene);
        OnColorOpen.AddListener(OpenColorScene);
    }
    private void OnDisable()
    {
        OnSewOpen.RemoveListener(OpenSewScene);
        OnColorOpen.RemoveListener(OpenColorScene);
    }

    private void OpenColorScene()
    {
        colorScene.SetActive(true);
        sewScene.SetActive(false);

    }
    private void OpenSewScene()
    {
        colorScene.SetActive(false);
        sewScene.SetActive(true);
    }
}
