using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePanel : PanelBase
{
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
}
