using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class RawMaterialSpawner : MonoBehaviour
{
    private BandController bandController;
    public BandController BandController { get { return (bandController == null) ? bandController = GetComponent<BandController>() : bandController; } }
    public static IntEvent OnBuyResources = new IntEvent();
    private void OnEnable()
    {
        OnBuyResources.AddListener(Spawn);
    }
    private void OnDisable()
    {
        OnBuyResources.RemoveListener(Spawn);
    }
    public void Spawn(int resourcesCost)
    {
        if (BandController.IsBandFull())
            return;

        var go = PoolingSystem.Instance.InstantiateAPS("ProductHolder");
        go.GetComponent<ProductHolder>().SetInfo(EnumTypes.ProductTypes.Rope, EnumTypes.ColorTypes.None, 0);
        BandController.AddHolder(go);
        go.GetComponent<ProductHolder>().OnBand();
        ExchangeManager.Instance.UseCurrency(CurrencyType.Cash, resourcesCost);

    }
}
