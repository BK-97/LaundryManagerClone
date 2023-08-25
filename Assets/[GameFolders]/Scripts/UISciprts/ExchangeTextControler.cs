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
    private Sequence punchSequence;
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
        string formattedCashString = ExchangeManager.Instance.FormatNumber(currencyDictionary[CurrencyType.Cash]);
        cashText.text = formattedCashString;
        Vector3 currentScale = transform.localScale;
        if (punchSequence != null && punchSequence.IsPlaying())
        {
            punchSequence.Complete();
            transform.localScale = Vector3.one;
            punchSequence.Append(transform.DOPunchScale(Vector3.one * 0.1f, 0.09f));
        }
        else
        {
            punchSequence.Append(transform.DOPunchScale(Vector3.one * 0.1f, 0.09f));
        }
       
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

        int numberOfPiles = cashes.Count;
        int pileExchangeAmount = currentExchange / numberOfPiles;
        for (int i = 0; i < numberOfPiles; i++)
        {
            if (i == numberOfPiles - 1 && currentExchange % numberOfPiles != 0)
            {
                pileExchangeAmount += currentExchange % numberOfPiles;
            }

            cashes[i].GetComponent<CashPile>().SetInfo(pileExchangeAmount, cashIcon);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
}
