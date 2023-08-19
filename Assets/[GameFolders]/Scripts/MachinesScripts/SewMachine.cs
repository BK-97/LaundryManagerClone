using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewMachine : MonoBehaviour, ISelectable, IProcessor
{
    #region Params
    [Header("Processor Info")]
    public EnumTypes.ProcessorTypes ProcessorType;
    public EnumTypes.ProductTypes ProductionType;
    public int processTime;
    public Transform produceSpot;
    public Transform productTransform;

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
        Debug.Log("GetProduct");

        ProcessStart();
    }
    public Transform GetProductPlace()
    {
        return productTransform;
    }
    public void ProcessStart()
    {
        Debug.Log("ProcessStart");

        onProcess = true;
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
        onProcess = false;
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
