using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class SceneChangerButton : MonoBehaviour
{
    public Button nextSceneButton;
    private Vector3 offSet;
    public TextMeshProUGUI buttonText;
    private void SetUIPos()
    {
        offSet = new Vector3(0, 0, -Camera.main.transform.position.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(nextSceneButton.transform.position + offSet);
        GameManager.Instance.NextSceneUIPos = worldPos;
    }
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(SetUIPos);
        EventManager.OnProductChangeBand.AddListener(ImagePump);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(SetUIPos);
        EventManager.OnProductChangeBand.RemoveListener(ImagePump);
    }
    private void ImagePump()
    {
        Vector3 currentScale = nextSceneButton.transform.localScale;
        nextSceneButton.transform.DOPunchScale(currentScale * 0.4f, 0.2f);
    }
    private void ChangeScene()
    {
        GameManager.Instance.DeskChange();

        if (GameManager.Instance.GetCurrentDesk()==GameManager.WorkDesks.SewDesk)
        {
            buttonText.text = "SEW";
        }
        else
        {
            buttonText.text = "PAINT";
        }
    }
}
