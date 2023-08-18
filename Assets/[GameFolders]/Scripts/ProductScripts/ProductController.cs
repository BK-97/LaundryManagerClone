using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductController : MonoBehaviour,IProduct
{
    public ProductData productData;
    #region Params
    #endregion
    #region IProductMethods
    public ProductData GetProductData()
    {
        return productData;
    }

    public void MoveProcess(IProcessor processor)
    {
        Transform targetTransform = processor.GetProductPlace();
    }

    public void ProcessEnd()
    {
        throw new System.NotImplementedException();
    }

    public void Sell()
    {
        throw new System.NotImplementedException();
    }
    #endregion
    #region MyMethods

    #endregion
}
