using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CurrencyType //Must be the same name as in PlayerPrefKeys
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
    private void Start()
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.CurrentCash, 300);
        currencyDictionary[CurrencyType.Cash] = PlayerPrefs.GetInt(PlayerPrefKeys.CurrentCash, 300); //For Test
    }
    public void SetUIPos(Vector3 pos)
    {
        UIPos = pos;
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
}
public class DictonaryEvent : UnityEvent<Dictionary<CurrencyType, int>> { }