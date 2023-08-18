using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PanelBase : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        HidePanel();
    }

    public virtual void ShowPanel()
    {
        if (canvasGroup != null)
        {
            canvasGroup.interactable = true;
            canvasGroup.alpha = 1;
        }
    }

    public virtual void HidePanel()
    {
        if (canvasGroup != null)
        {
            canvasGroup.interactable = false;
            canvasGroup.alpha = 0;
        }
    }
}