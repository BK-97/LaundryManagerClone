using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class SceneChangerButton : MonoBehaviour
{
    public Button button;
    private Vector3 offSet;
    public TextMeshProUGUI buttonText;
    bool isColorScene;
    private void SetUIPos()
    {
        offSet = new Vector3(0, 0, -Camera.main.transform.position.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(button.transform.position + offSet);
        GameManager.Instance.NextSceneUIPos = worldPos;
    }
    private void OnEnable()
    {
        EventManager.OnProductArriveNextSceneButton.AddListener(ImagePump);
        LevelManager.Instance.OnLevelStart.AddListener(SetUIPos);
    }
    private void OnDisable()
    {
        EventManager.OnProductArriveNextSceneButton.RemoveListener(ImagePump);
        LevelManager.Instance.OnLevelStart.RemoveListener(SetUIPos);

    }
    public void ImagePump(GameObject test)
    {
        Vector3 currentScale = button.transform.localScale;
        button.transform.DOPunchScale(currentScale * 1.01f, 0.2f);
    }
    public void ChangeScene()
    {
        if (!isColorScene)
        {
            CameraController.OnColorOpen.Invoke();
            buttonText.text = "SEW";
            isColorScene = true;
            return;
        }
        else
        {
            CameraController.OnSewOpen.Invoke();
            buttonText.text = "PAINT";
            isColorScene = false;
        }

    }
}
