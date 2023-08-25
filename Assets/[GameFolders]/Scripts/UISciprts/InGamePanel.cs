using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class InGamePanel : PanelBase
{
    public TextMeshProUGUI levelText;
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(ShowPanel);
        LevelManager.Instance.OnLevelFinish.AddListener(HidePanel);
        OrderManager.OnOrderCompleted.AddListener(SetLevelText);

    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(ShowPanel);
        LevelManager.Instance.OnLevelFinish.RemoveListener(HidePanel);
        OrderManager.OnOrderCompleted.RemoveListener(SetLevelText);

    }
    private void SetLevelText()
    {
        Debug.Log("test1");
        Vector3 currentScale = levelText.gameObject.transform.localScale;
        levelText.gameObject.transform.DOPunchScale(Vector3.one*0.1f,1);
        levelText.text = "Day "+LevelManager.Instance.currentDay.ToString()+"!";
    }
}
