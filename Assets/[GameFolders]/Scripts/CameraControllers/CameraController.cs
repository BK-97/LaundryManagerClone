using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    #region Params
    public Transform colorCameraPoint;
    public Transform sewCameraPoint;
    private SelectController selectController;
    #endregion
    #region Methods
    private void OnEnable()
    {
        selectController = GetComponent<SelectController>();

        GameManager.Instance.OnDeskChange.AddListener(ChangeCamera);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnDeskChange.RemoveListener(ChangeCamera);
    }
    private void ChangeCamera()
    {
        selectController.ResetSelect();
        if (GameManager.Instance.GetCurrentDesk() == GameManager.WorkDesks.ColorDesk)
        {
            Camera.main.transform.position = colorCameraPoint.position;
        }
        else
        {
            Camera.main.transform.position = sewCameraPoint.position;
        }
    }
    #endregion
}
