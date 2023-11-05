using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CurrencyType
{
    Coin,
    Cash
}

public class ExchangeManager : Singleton<ExchangeManager>
{
    private Dictionary<CurrencyType, int> currencyDictionary;
    public DictonaryEvent OnCurrencyChange = new DictonaryEvent();
    [HideInInspector]
    public UnityEvent OnCurrencyAdded = new UnityEvent();
    [HideInInspector]
    public Vector3 UIPos;
    public ExchangeManager()
    {
        currencyDictionary = new Dictionary<CurrencyType, int>();
    }
    public void SetUIPos(Vector3 pos)
    {
        UIPos = pos;
    }
    private void Start()
    {
        currencyDictionary[CurrencyType.Cash] = PlayerPrefs.GetInt(PlayerPrefKeys.CurrentCash, 100);
        OnCurrencyChange.Invoke(currencyDictionary);
    }
    private void OnEnable()
    {
        SceneController.Instance.OnSceneLoaded.AddListener(()=>OnCurrencyChange.Invoke(currencyDictionary));
    }
    private void OnDisable()
    {
        SceneController.Instance.OnSceneLoaded.RemoveListener(()=>OnCurrencyChange.Invoke(currencyDictionary));
    }
    #region CurrencyMethods
    public bool UseCurrency(CurrencyType currencyType, int amount)
    {
        if (currencyDictionary.ContainsKey(currencyType))
        {
            if (currencyDictionary[currencyType] >= amount)
            {
                currencyDictionary[currencyType] -= amount;
                string exchangeName = currencyType.ToString();
                PlayerPrefs.SetInt(exchangeName, currencyDictionary[currencyType]);
                OnCurrencyChange.Invoke(currencyDictionary);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void AddCurrency(CurrencyType currencyType, int amount)
    {
        if (currencyDictionary.ContainsKey(currencyType))
        {
            currencyDictionary[currencyType] += amount;
            string exchangeName = currencyType.ToString();
            PlayerPrefs.SetInt(exchangeName, currencyDictionary[currencyType]);
            OnCurrencyChange.Invoke(currencyDictionary);
            OnCurrencyAdded.Invoke();
        }
    }

    public int GetCurrency(CurrencyType currencyType)
    {
        if (currencyDictionary.ContainsKey(currencyType))
        {
            return currencyDictionary[currencyType];
        }
        else
        {
            return 0;
        }
    }
    #endregion
    #region Helpers
    public string FormatNumber(int value)
    {
        if (value >= 1000 && value < 1000000)
        {
            return (value / 1000f).ToString("F1") + "K";
        }
        else if (value >= 1000000)
        {
            return (value / 1000000f).ToString("F1") + "M";
        }
        else
        {
            return value.ToString();
        }
    }
    public int GetProductValueByType(EnumTypes.ProductTypes productType)
    {
        int cachedValue = 0;
        if (productType == EnumTypes.ProductTypes.Rope)
            cachedValue = 0;
        else if(productType == EnumTypes.ProductTypes.Socks)
            cachedValue = 50;
        else if (productType == EnumTypes.ProductTypes.Bra)
            cachedValue = 100;
        else if (productType == EnumTypes.ProductTypes.Short)
            cachedValue = 200;
        else if (productType == EnumTypes.ProductTypes.Tong)
            cachedValue = 300;
        return cachedValue;
    }
    public int GetProductValueByColor(EnumTypes.ColorTypes colorType)
    {
        int cachedValue = 0;

        if (colorType == EnumTypes.ColorTypes.None)
            cachedValue = 0;
        else if (colorType == EnumTypes.ColorTypes.Turquoise)
            cachedValue = 100;
        else if (colorType == EnumTypes.ColorTypes.Blue)
            cachedValue = 120;
        else if (colorType == EnumTypes.ColorTypes.Green)
            cachedValue = 200;
        else if (colorType == EnumTypes.ColorTypes.Red)
            cachedValue = 300;

        return cachedValue;
    }
    #endregion
}
public class DictonaryEvent : UnityEvent<Dictionary<CurrencyType, int>> { }