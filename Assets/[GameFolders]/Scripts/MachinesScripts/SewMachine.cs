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


    [Header("SewMachine Params")]
    public Transform needle;
    public Transform ropeRoll;

    private Tween _needleTween;
    private Tween _ropeRollTween;
    private GameObject processingProduct;

    [Header("Selectable Params")]
    [SerializeField]
    private bool _isSelected;
    [SerializeField]
    private bool _isLocked;

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
    public void GetProduct(EnumTypes.ProductTypes proType, EnumTypes.ColorTypes colorType)
    {
        ProcessStart();
    }
    public Transform GetProductPlace()
    {
        return productTransform;
    }
    public void ProcessStart()
    {
        _needleTween = needle.DOMoveY(needle.transform.position.y - 0.03f, 0.1f).SetLoops(-1, LoopType.Yoyo);
        ropeRoll.gameObject.SetActive(true);
        ropeRoll.GetComponent<SewRope>().StartWorking(processTime);
        _ropeRollTween = ropeRoll.DOLocalRotate(360 * Vector3.up, 4, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        _onProcess = true;
        
        Vector3 instantiatePos = produceSpot.position;
        Quaternion instantiateRot = produceSpot.rotation;
        processingProduct = PoolingSystem.Instance.InstantiateAPS("ProductHolder", instantiatePos, instantiateRot);
        ProductHolder productHolder = processingProduct.GetComponent<ProductHolder>();
        productHolder.SetInfo(ProductionType,EnumTypes.ColorTypes.None, addWorth);
        productHolder.currentProduct.GetComponent<IFakeProduct>().StartUnDissolve(processTime);
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
        _onProcess = false;
        processingProduct.GetComponent<ProductHolder>().currentProduct.GetComponent<IProduct>().MoveNextProcess();
        StartCoroutine(IconColorCO());

        stars.Play();
        clouds.Play();
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
        if (LevelManager.Instance.currentDay >= unlockLevel)
        {

            if (ExchangeManager.Instance.GetCurrency(CurrencyType.Cash) >= unlockCost)
            {
                ProductIconHolder.gameObject.SetActive(true);
                ProductIcon.sprite = OrderManager.Instance.GetProductSprite(ProductionType);
                ExchangeManager.Instance.UseCurrency(CurrencyType.Cash, unlockCost);
                //unlock fx
                LockImage.gameObject.SetActive(false);
                IsLocked = false;
            }
        }

    }
    #endregion
    #region MyMethods
    private void Start()
    {
        OnProcess = false;

        if (!IsLocked)
        {
            ProcessorUnlock();
        }
        else
        {
            ProductIconHolder.gameObject.SetActive(false);
            if (unlockLevel > LevelManager.Instance.currentDay)
                lockText.text = "Level "+unlockLevel.ToString();
            else
                lockText.text = unlockCost.ToString();

        }
        
        ropeRoll.gameObject.SetActive(false);

    }
    private void Update()
    {
        if (_onProcess)
        {
            ProcessUpdate();
        }
    }


    #endregion
}
