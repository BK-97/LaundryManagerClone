using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    public GameObject colorCamera;
    public GameObject sewCamera;
    public static UnityEvent OnSewOpen = new UnityEvent();
    public static UnityEvent OnColorOpen = new UnityEvent();
    private SelectController selectController;

    private void OnEnable()
    {
        selectController = GetComponent<SelectController>();

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
        selectController.ResetSelect();
        colorCamera.SetActive(true);
        colorCamera.tag = "MainCamera";
        sewCamera.SetActive(false);
        sewCamera.tag = "SecondCamera";

    }
    private void OpenSewScene()
    {
        selectController.ResetSelect();
        sewCamera.SetActive(true);
        sewCamera.tag = "MainCamera";
        colorCamera.SetActive(false);
        colorCamera.tag = "SecondCamera";
    }
}
