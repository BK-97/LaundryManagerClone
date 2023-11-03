using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    private Button button;
    public int resourceCost;
    private CanvasGroup canvasGroup;
    private void Start()
    {
        button = GetComponent<Button>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;

    }
    private void OnEnable()
    {
        GameManager.Instance.OnDeskChange.AddListener(DeskChanged);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnDeskChange.RemoveListener(DeskChanged);
    }
    private void DeskChanged()
    {
        if (GameManager.Instance.GetCurrentDesk() == GameManager.WorkDesks.ColorDesk)
        {
            canvasGroup.alpha = 0;
        }
        else
        {
            canvasGroup.alpha = 1;
        }

    }
    private void Update()
    {
        if (ExchangeManager.Instance.GetCurrency(CurrencyType.Cash) < resourceCost)
        {
            button.interactable = false;
        }
        else
            button.interactable = true;
    }
    public void BuyResources()
    {
        RawMaterialSpawner.OnBuyResources.Invoke(resourceCost);
    }
}
