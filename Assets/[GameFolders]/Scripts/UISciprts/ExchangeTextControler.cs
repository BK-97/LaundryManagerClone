using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class ExchangeTextControler : MonoBehaviour
{
    public TextMeshProUGUI cashText;

    private void OnEnable()
    {
        ExchangeManager.Instance.OnCurrencyChange.AddListener(SetText);
    }
    private void OnDisable()
    {
        ExchangeManager.Instance.OnCurrencyChange.RemoveListener(SetText);
    }
    private void SetText(Dictionary<CurrencyType, int> currencyDictionary)
    {
        cashText.text = currencyDictionary[CurrencyType.Cash].ToString();
        Vector3 currentScale = transform.localScale;
        transform.DOPunchScale(currentScale * 1.01f, 0.2f);
    }
}
