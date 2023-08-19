using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProductUI : MonoBehaviour
{
    public Image productImage;
    public void SetInfo(Image productimage,Color productColor)
    {
        productImage = productimage;
        productimage.color = productColor;
    }
    public void OrderCompleted()
    {

    }
    public void OrderCanceled()
    {

    }
}
