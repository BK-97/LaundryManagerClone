using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SewMachine : MonoBehaviour, ISelectable, IProcessor
{
    #region Params
    [Header("Processor Info")]
    public EnumTypes.ProcessorTypes ProcessorType;
    public EnumTypes.ProductTypes ProductionType;
    public int processTime;
    public Transform produceSpot;
    public Transform productTransform;
    [Header("SewMachine Params")]
    public Transform needle;
    public Transform ropeRoll;
    private Tween _needleTween;
    private Tween _ropeRollTween;
    private GameObject processingProduct;
    [Header("Serializefields")]
    [SerializeField]
    private bool _isSelected;
    [SerializeField]
    private bool _isLocked;
    [SerializeField]
    private bool _isEmpty;

    private bool onProcess;
    private float elapsedTime;

    [Header("UI")]
    public Canvas LockCanvas;
    public Image ProductIcon;

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
        Debug.Log("Machine Selected");
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
    public bool IsEmpty
    {
        get => _isEmpty;
        set { _isEmpty = value; }
    }
    public void GetProduct()
    {
        ProcessStart();
    }
    public Transform GetProductPlace()
    {
        return productTransform;
    }
    public void ProcessStart()
    {
        Debug.Log("ProcessStart");
        _needleTween = needle.DOMoveY(needle.transform.position.y - 0.03f, 0.1f).SetLoops(-1, LoopType.Yoyo);
        ropeRoll.gameObject.SetActive(true);
        ropeRoll.GetComponent<SewRope>().StartWorking(processTime);
        _ropeRollTween = ropeRoll.DORotate(360 * Vector3.up, 4, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        onProcess = true;
        Vector3 instantiatePos = produceSpot.position;
        Quaternion instantiateRot = produceSpot.rotation;
        processingProduct = PoolingSystem.Instance.InstantiateAPS("ProductHolder", instantiatePos, instantiateRot);
        ProductHolder productHolder = processingProduct.GetComponent<ProductHolder>();
        productHolder.SetInfo(ProductionType);
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
        onProcess = false;
        processingProduct.GetComponent<ProductHolder>().currentProduct.GetComponent<IProduct>().MoveNextProcessPlatform();
        StartCoroutine(IconColorCO());
        stars.Play();
        clouds.Play();
    }
    IEnumerator IconColorCO()
    {
        Color cacheColor = ProductIcon.color;
        ProductIcon.color = Color.green;
        yield return new WaitForSeconds(1.5f);
        ProductIcon.color = cacheColor;
    }

    public void ProcessorUnlock()
    {
        LockCanvas.enabled = false;
        IsLocked = false;
    }
    #endregion
    #region MyMethods
    private void Start()
    {
        IsEmpty = true;
        if (!IsLocked)
            ProcessorUnlock();
    }
    private void Update()
    {
        if (onProcess)
        {
            ProcessUpdate();
        }
    }


    #endregion
}
