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
    public override void ShowPanel()
    {
        base.ShowPanel();
        SetLevelText();
    }
    private void SetLevelText()
    {
        Vector3 currentScale = levelText.gameObject.transform.localScale;
        levelText.gameObject.transform.DOPunchScale(Vector3.one*0.1f,1);
        levelText.text = "Day "+ PlayerPrefs.GetInt(PlayerPrefKeys.CurrentDay, 1).ToString()+"!";
    }
}
