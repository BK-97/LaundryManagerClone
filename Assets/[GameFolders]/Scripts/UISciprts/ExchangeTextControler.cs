using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class ExchangeTextControler : MonoBehaviour
{
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI levelText;
    public Image cashIcon;
    public List<Image> cashes;
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
        transform.DOPunchScale(currentScale * 0.2f, 0.2f);
    }
    public Image moverExchange;
    private void ExchangeCreate(Vector3 worldPos, int exchangeWorth)
    {
        Vector3 rectPos = Camera.main.WorldToScreenPoint(worldPos);
        rectPos.z = 0;
        for (int i = 0; i < cashes.Count; i++)
        {
            cashes[i].rectTransform.position = rectPos;
            cashes[i].enabled = true;
        }
        StartCoroutine(WaitCash(exchangeWorth));
    }
    IEnumerator WaitCash(int exchangeWorth)
    {
        int currentExchange = exchangeWorth;
        for (int i = 0; i < cashes.Count; i++)
        {
            int pileExchangeAmount=0;
            if (i != 2)
                pileExchangeAmount = exchangeWorth-(currentExchange/3);
            else
                pileExchangeAmount = currentExchange;

            Debug.Log(pileExchangeAmount);
            cashes[i].GetComponent<CashPile>().SetInfo(pileExchangeAmount, cashIcon);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
}
