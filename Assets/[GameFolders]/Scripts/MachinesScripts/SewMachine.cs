using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class SewMachine : MonoBehaviour, ISelectable, IProcessor
{
    #region Params
    [Header("Processor Info")]
    public EnumTypes.ProcessorTypes ProcessorType;
    public EnumTypes.ProductTypes ProductionType;
    public int processTime;
    public Transform produceSpot;
    public Transform productTransform;
    public int addWorth;
    public int unlockLevel;
    public int unlockCost;
    public int prefIDIndex;
    private string cachedID;

    public List<ParticleSystem> fxs;
    [Header("SewMachine Params")]
    public Transform needle;
    public Transform ropeRoll;

    private Tween _needleTween;
    private Tween _ropeRollTween;
    [SerializeField]
    private ProductHolder processingProduct;

    [Header("Selectable Params")]
    [SerializeField]
    private bool _isSelected;

    [Header("Processor Params")]
    [SerializeField]
    private bool _isLocked;
    [SerializeField]
    private bool _hasReadyProduct;
    [SerializeField]
    private bool _onProcess;

    private float elapsedTime;

    [Header("UI")]
    public Image LockImage;
    public Image ProductIconHolder;
    public Image ProductIcon;
    public TextMeshProUGUI lockText;

    [Header("Particles")]
    public ParticleSystem stars;
    public ParticleSystem clouds;
    public ParticleSystem particleRain;

    #endregion
    #region ISelectable
    public bool isSelected
    {
        get => _isSelected;
        set { _isSelected = value; }
    }

    public void Selected()
    {
        if (isSelected)
            return;

        isSelected = true;

        Deselected();

    }
    public void Deselected()
    {
        if (!isSelected)
            return;

        isSelected = false;
    }
    #endregion
    #region IProcessor
    public bool IsLocked
    {
        get => _isLocked;
        set { _isLocked = value; }
    }
    public bool OnProcess
    {
        get => _onProcess;
        set { _onProcess = value; }
    }
    public bool HasReadyProduct
    {
        get => _hasReadyProduct;
        set { _hasReadyProduct = value; }
    }
    public void GetProduct(ProductHolder proHolder)
    {
        processingProduct = proHolder;
        ProcessStart();
    }
    public Transform GetProductPlace()
    {
        return productTransform;
    }
    public void ProcessStart()
    {
        _needleTween = needle.DOMoveY(needle.transform.position.y - 0.05f, 0.1f).SetLoops(-1, LoopType.Yoyo);
        ropeRoll.gameObject.SetActive(true);
        ropeRoll.GetComponent<SewRope>().StartWorking(processTime);
        _ropeRollTween = ropeRoll.DOLocalRotate(360 * Vector3.up, 4, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        _onProcess = true;

        processingProduct.gameObject.transform.position = produceSpot.position;
        processingProduct.gameObject.transform.rotation = produceSpot.rotation;
        processingProduct.currentProduct.gameObject.transform.localPosition = Vector3.zero;
        processingProduct.currentProduct.gameObject.transform.localRotation = Quaternion.identity;

        processingProduct.SetInfo(ProductionType, EnumTypes.ColorTypes.None, addWorth);

        processingProduct.currentProduct.GetComponent<ModelController>().StartUnDissolve(processTime);
        StartCoroutine(WaitCO());
    }
    IEnumerator WaitCO()
    {
        for (int i = 0; i < processTime-2; i++)
        {
            int _index = i;
            if (_index >= fxs.Count)
                _index = processTime%fxs.Count;
            fxs[_index].Play();
            yield return new WaitForSeconds(1);
        }
    }
    public void ProcessUpdate()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= processTime)
        {
            ProcessEnd();
        }
    }

    public void ProcessEnd()
    {
        elapsedTime = 0.0f;
        _needleTween.Kill();
        _ropeRollTween.Kill();
        ropeRoll.gameObject.SetActive(false);
        StartCoroutine(IconColorCO());
        StartCoroutine(WaitForSendingProduct());
        stars.Play();
        clouds.Play();
    }
    IEnumerator WaitForSendingProduct()
    {
        yield return new WaitForSeconds(1.5f);
        _hasReadyProduct = true;
        _onProcess = false;
    }
    public void SendProduct()
    {
        processingProduct.GetComponent<ProductHolder>().currentProduct.GetComponent<IProduct>().MoveNextProcess();
        _hasReadyProduct = false;

    }
    IEnumerator IconColorCO()
    {
        Color cacheColor = ProductIconHolder.color;
        ProductIconHolder.color = Color.green;
        yield return new WaitForSeconds(1.5f);
        ProductIconHolder.color = cacheColor;
    }
    public void ProcessorUnlock()
    {
        if (PlayerPrefs.GetInt(PlayerPrefKeys.CurrentDay, 1) >= unlockLevel)
        {
            if (!LevelManager.Instance.IsLevelStarted)
            {
                ProductIconHolder.gameObject.SetActive(true);
                ProductIcon.sprite = OrderManager.Instance.GetProductSprite(ProductionType);

                LockImage.gameObject.SetActive(false);
                IsLocked = false;
            }
            else
            {
                if (ExchangeManager.Instance.GetCurrency(CurrencyType.Cash) >= unlockCost)
                {
                    PlayerPrefs.SetInt(cachedID, 1);
                    particleRain.Play();
                    ExchangeManager.Instance.UseCurrency(CurrencyType.Cash, unlockCost);

                    ProductIconHolder.gameObject.SetActive(true);
                    ProductIcon.sprite = OrderManager.Instance.GetProductSprite(ProductionType);

                    LockImage.gameObject.SetActive(false);
                    IsLocked = false;
                }
            }
        }
    }
    #endregion
    #region MyMethods
    private void Start()
    {
        OnProcess = false;
        SetPrefID();
        if (!IsLocked)
        {
            ProcessorUnlock();
        }
        else
        {
            if (PlayerPrefs.GetInt(cachedID, 0) == 1)
                ProcessorUnlock();
            else
            {
                ProductIconHolder.gameObject.SetActive(false);
                UpdateLockText();
            }

        }

        ropeRoll.gameObject.SetActive(false);

    }
    private void SetPrefID()
    {
        switch (ProcessorType)
        {
            case EnumTypes.ProcessorTypes.SewMachine:
                cachedID = "Sew" + prefIDIndex;

                break;
            case EnumTypes.ProcessorTypes.ColorChanger:
                cachedID = "Color" + prefIDIndex;
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        if (_onProcess)
        {
            ProcessUpdate();
        }
    }
    private void UpdateLockText()
    {
        if (IsLocked)
        {
            if (unlockLevel > PlayerPrefs.GetInt(PlayerPrefKeys.CurrentDay, 1))
            {
                lockText.text = "Level " + unlockLevel.ToString();
            }
            else
            {
                lockText.text = unlockCost.ToString();
            }

        }
    }
    private void OnEnable()
    {
        OrderManager.OnGetNewOrder.AddListener(UpdateLockText);
    }
    private void OnDisable()
    {
        OrderManager.OnGetNewOrder.RemoveListener(UpdateLockText);

    }
    #endregion
}
