using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewMachine : MonoBehaviour, ISelectable, IProcessor
{
    #region Params

    IProduct currentProduct;
    [SerializeField]
    private bool _isSelected;
    [SerializeField]
    private bool _isLocked;
    [SerializeField]
    private bool _isEmpty;

    private bool onProcess;
    public Canvas LockCanvas;

    public EnumTypes.ProcessorTypes processorType;
    public Transform productTransform;
    public int unlockLevel;
    public int unlockCost;
    public float processPerSeconds;
    private float currentNeedProcess;
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
        ProcessStart();
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
    public void GetProduct(IProduct product)
    {
        currentProduct = product;
        currentNeedProcess = currentProduct.GetProductData().processNeed;
    }
    public Transform GetProductPlace()
    {
        return productTransform;
    }
    public void ProcessStart()
    {
        onProcess = true;
        ProcessUpdate();
    }

    public void ProcessUpdate()
    {
        currentNeedProcess -= processPerSeconds * Time.deltaTime;
        if (currentNeedProcess <= 0)
            ProcessEnd();
    }

    public void ProcessEnd()
    {
        onProcess = false;
        currentProduct.ProcessEnd();
        currentProduct = null;
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
