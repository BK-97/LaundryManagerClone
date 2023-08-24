using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class ExchangeTextControler : MonoBehaviour
{
    public TextMeshProUGUI cashText;
    public Image cashIcon;
    private void OnEnable()
    {
        ExchangeManager.Instance.OnCurrencyChange.AddListener(SetText);
        EventManager.OnExcangeInstantiate.AddListener(ExchangeCreate);

    }
    private void OnDisable()
    {
        ExchangeManager.Instance.OnCurrencyChange.RemoveListener(SetText);
        EventManager.OnExcangeInstantiate.RemoveListener(ExchangeCreate);

    }
    private void SetText(Dictionary<CurrencyType, int> currencyDictionary)
    {
        cashText.text = currencyDictionary[CurrencyType.Cash].ToString();
        Vector3 currentScale = transform.localScale;
        transform.DOPunchScale(currentScale * 1.01f, 0.2f);
    }
    public Image moverExchange;
    private void ExchangeCreate(Vector3 worldPos, int exchangeWorth)
    {
        Vector3 rectPos = Camera.main.WorldToScreenPoint(worldPos);
        rectPos.z = 0;
        moverExchange.rectTransform.position = rectPos;
        moverExchange.enabled = true;
        moverExchange.GetComponent<CashPile>().SetInfo(exchangeWorth,cashIcon);
    }
}
