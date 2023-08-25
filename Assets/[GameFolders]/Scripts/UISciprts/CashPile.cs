using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CashPile : MonoBehaviour
{
    int worth;
    Image targetImage;
    public void SetInfo(int cashWorth,Image target)
    {
        worth = cashWorth;
        targetImage = target;
        MoveUI();
    }
    private void MoveUI()
    {
        GetComponent<Image>().rectTransform.DOMove(targetImage.rectTransform.position, 1).OnComplete(CashSell);

    }
    private void CashSell()
    {
        ExchangeManager.Instance.AddCurrency(CurrencyType.Cash, worth);
        GetComponent<Image>().enabled = false;
    }
}
