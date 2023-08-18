using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductHolder : MonoBehaviour,ISelectable
{
    #region Params
    public GameObject productObject;
    public Image CircleImage;
    private bool _isSelected;
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
        Debug.Log("Holder Selected");

        BrightColor();
        ShakeEffect();
        isSelected = true;
    }
    public void Deselected()
    {
        if (!isSelected)
            return;
        Debug.Log("Holder DeSelected");

        isSelected = false;
    }
    #endregion
    #region MyMethods
    private void BrightColor()
    {

    }
    private void Fade()
    {

    }
    private void ShakeEffect()
    {

    }
    #endregion
}
 