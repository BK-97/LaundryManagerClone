using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class InGamePanel : PanelBase
{
    public TextMeshPro levelText;
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(ShowPanel);
        LevelManager.Instance.OnLevelFinish.AddListener(HidePanel);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(ShowPanel);
        LevelManager.Instance.OnLevelFinish.RemoveListener(HidePanel);
    }
    private void SetLevelText()
    {
        levelText.gameObject.transform.DOPunchScale(levelText.gameObject.transform.localScale*1.01f,1);
        levelText.text = "Day "+LevelManager.Instance.currentDay.ToString()+"!";
    }
}
