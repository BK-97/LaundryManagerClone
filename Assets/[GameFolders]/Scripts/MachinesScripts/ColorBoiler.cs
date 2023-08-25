using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class ColorBoiler : MonoBehaviour, ISelectable,IProcessor
{
    #region Params
    [Header("Processor Info")]
    public EnumTypes.ProcessorTypes ProcessorType;
    public EnumTypes.ColorTypes ColorType;
    public int processTime;
    public Transform produceSpot;
    public Transform productTransform;
    public int addWorth;
    public int unlockLevel;
    public int unlockCost;

    [Header("ColorBoiler Params")]
    private ProductHolder processingProduct;
    public MeshRenderer boilColorMesh;
    public Slider timerSlider;

    [Header("Serializefields")]
    [SerializeField]
    private bool _isSelected;
    [SerializeField]
    private bool _isLocked;

    private bool _onProcess;
    private float elapsedTime;

    [Header("UI")]
    public Image LockImage;
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
        timerSlider.gameObject.SetActive(true);
        timerSlider.maxValue = processTime;
        timerSlider.value = 0;

        processingProduct.gameObject.transform.position = produceSpot.position;
        processingProduct.gameObject.transform.rotation = produceSpot.rotation;
        processingProduct.currentProduct.gameObject.transform.localPosition = Vector3.zero;
        processingProduct.currentProduct.gameObject.transform.localRotation = Quaternion.identity;

        Color newColor = ColorManager.Instance.GetColorCode(ColorType);
        processingProduct.currentProduct.GetComponent<ModelController>().StartColorChange(processTime, newColor,produceSpot);
        
        OnProcess = true;

    }
    public void ProcessEnd()
    {
        elapsedTime = 0.0f;
        _onProcess = false;
        StartCoroutine(WaitForSendingProduct());
        processingProduct.SetInfo(processingProduct.currentProduct.GetComponent<ProductController>().productType, ColorType, addWorth);
        timerSlider.gameObject.SetActive(false);
        timerSlider.value = 0;
        stars.Play();
        clouds.Play();
    }
    IEnumerator WaitForSendingProduct()
    {
        yield return new WaitForSeconds(1.5f);
        processingProduct.GetComponent<ProductHolder>().currentProduct.GetComponent<IProduct>().Sell();
    }

    public void ProcessorUnlock()
    {
        if (PlayerPrefs.GetInt(PlayerPrefKeys.CurrentDay, 1) >= unlockLevel)
        {
            if (ExchangeManager.Instance.GetCurrency(CurrencyType.Cash) >= unlockCost)
            {
                ExchangeManager.Instance.UseCurrency(CurrencyType.Cash, unlockCost);

                if (LevelManager.Instance.IsLevelStarted)
                    particleRain.Play();

                LockImage.gameObject.SetActive(false);
                IsLocked = false;
            }
        }
    }
    public void ProcessUpdate()
    {
        elapsedTime += Time.deltaTime;
        timerSlider.value = elapsedTime;
        if (elapsedTime >= processTime)
        {
            ProcessEnd();
        }
    }
    #endregion
    #region MonoBehaviourMethods
    private void Start()
    {
        OnProcess = false;
        Color liquidColor = ColorManager.Instance.GetColorCode(ColorType);
        boilColorMesh.materials[1].color = liquidColor;
        if (!IsLocked)
        {
            ProcessorUnlock();
        }
        else
        {
            UpdateLockText();
        }
    }
    private void Update()
    {
        if (OnProcess)
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
