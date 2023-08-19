using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class TapToStartPanel : PanelBase
{
    public Image handImage;
    public TextMeshProUGUI startText;
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(HidePanel);
        SceneController.Instance.OnSceneLoaded.AddListener(ShowPanel);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(HidePanel);
        SceneController.Instance.OnSceneLoaded.RemoveListener(ShowPanel);
    }
    private void Start()
    {
        handImage.gameObject.transform.DOScale(Vector3.one*1.1f,1).SetLoops(-1,LoopType.Yoyo);
        startText.gameObject.transform.DOScale(Vector3.one*1.1f,1).SetLoops(-1,LoopType.Yoyo);
    }
}
