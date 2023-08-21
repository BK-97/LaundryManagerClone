using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
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
    public Slider timerSlider;
    private EnumTypes.ProductTypes currentProductType;

    [Header("ColorBoiler Params")]
    private GameObject processingProduct;

    [Header("Serializefields")]
    [SerializeField]
    private bool _isSelected;
    [SerializeField]
    private bool _isLocked;

    private bool _onProcess;
    private float elapsedTime;

    [Header("UI")]
    public Image LockImage;

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
        Debug.Log("Machine Selected");

        if (isSelected)
            return;
        isSelected = true;
        Deselected();
    }
    public void Deselected()
    {
        if (!isSelected)
            return;
        Debug.Log("Machine DeSelected");

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
    public void GetProduct(EnumTypes.ProductTypes productType,EnumTypes.ColorTypes colorType)
    {
        currentProductType = productType;
        ProcessStart();

    }

    public Transform GetProductPlace()
    {
        return productTransform;
    }
    public void ProcessStart()
    {
        Debug.Log("ProcessStart");
        timerSlider.gameObject.SetActive(true);
        timerSlider.maxValue = processTime;
        timerSlider.value = 0;
        Vector3 instantiatePos = produceSpot.position;
        Quaternion instantiateRot = produceSpot.rotation;
        processingProduct = PoolingSystem.Instance.InstantiateAPS("ProductHolder", instantiatePos, instantiateRot);
        ProductHolder productHolder = processingProduct.GetComponent<ProductHolder>();
        productHolder.SetInfo(currentProductType,ColorType,addWorth);
        OnProcess = true;

    }
    public void ProcessEnd()
    {
        elapsedTime = 0.0f;
        timerSlider.gameObject.SetActive(false);
        OnProcess = false;
        processingProduct.GetComponent<ProductHolder>().currentProduct.GetComponent<IProduct>().Sell();
        stars.Play();
        clouds.Play();
    }

    public void ProcessorUnlock()
    {
        LockImage.enabled = false;
        IsLocked = false;
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
        if (!IsLocked)
            ProcessorUnlock();
    }
    private void Update()
    {
        if (OnProcess)
        {
            ProcessUpdate();
        }
    }
    #endregion
}
