using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductData", menuName = "Datas/Product Data")]
public class ProductData : ScriptableObject
{
    public EnumTypes.ProductTypes ProductType;
    public EnumTypes.ColorTypes ColorType;
    public int Price;
    public float processNeed;

}
