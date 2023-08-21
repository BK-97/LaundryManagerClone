using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ProductHolder : MonoBehaviour,ISelectable
{
    #region Params
    public List<GameObject> productObjects;
    [HideInInspector]
    public GameObject currentProduct;
    public Image CircleImage;
    private bool _isSelected;
    [HideInInspector]
    public BandController bandController;
    #endregion
    public void SetInfo(EnumTypes.ProductTypes productType)
    {
        for (int i = 0; i < productObjects.Count; i++)
        {
            productObjects[i].SetActive(false);
        }
        for (int i = 0; i < productObjects.Count; i++)
        {
            switch (productType)
            {
                case EnumTypes.ProductTypes.Rope:
                    currentProduct=productObjects[0];
                    break;
                case EnumTypes.ProductTypes.Socks:
                    currentProduct = productObjects[1];
                    break;
                case EnumTypes.ProductTypes.Bra:
                    currentProduct = productObjects[2];
                    break;
                case EnumTypes.ProductTypes.Short:
                    currentProduct = productObjects[3];
                    break;
                default:
                    break;
            }
            currentProduct.SetActive(true);
        }
    }
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
        Fade();
        isSelected = false;
    }
    #endregion
    #region MyMethods
    public void ReadyOnBand()
    {
        CircleImage.enabled = true;
    }

    private void BrightColor()
    {
        CircleImage.color = Color.green;
    }
    private void Fade()
    {
        CircleImage.color = Color.white;
    }
    private void ShakeEffect()
    {
        transform.DOPunchScale(Vector3.one*0.1f,0.3f);
    }
    public void Demolish()
    {
        Fade();
        CircleImage.gameObject.transform.DOScale(Vector3.zero,0.2f).OnComplete(DemolishEnd);
    }
    public void DemolishEnd()
    {
        bandController.RemoveHolder(gameObject);
        PoolingSystem.Instance.DestroyAPS(gameObject);
    }
    public void MovePoint(Vector3 targetPoint)
    {
        transform.DOMove(targetPoint,0.2f);
    }
    #endregion
}
 