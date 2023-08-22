using System;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : Singleton<OrderManager>
{
    public List<Sprite> ProductImages;
    private int currentLevel;
    [SerializeField]
    public List<OrderList> orderLists = new List<OrderList>();

    public List<OrderList.Order> GetOrderInfo()
    {
        return orderLists[currentLevel].orders;
    }
    public Sprite GetProductSprite(EnumTypes.ProductTypes product)
    {
        int spriteIndex=0;
        switch (product)
        {
            case EnumTypes.ProductTypes.Rope:
                break;

            case EnumTypes.ProductTypes.Socks:
                spriteIndex = 0;
                break;
            case EnumTypes.ProductTypes.Bra:
                spriteIndex = 1;
                break;
            case EnumTypes.ProductTypes.Short:
                spriteIndex = 2;
                break;
            case EnumTypes.ProductTypes.Tong:
                spriteIndex = 3;
                break;
            case EnumTypes.ProductTypes.None:
                break;
            default:
                break;
        }
        Debug.Log(spriteIndex);

        return ProductImages[spriteIndex];

    }

}
#region Serializable
[Serializable]
public class OrderList
{
    [Serializable]
    public class Order
    {
        public EnumTypes.ProductTypes ProductType;
        public EnumTypes.ColorTypes ColorType;
        public int Amount;

        public Order(EnumTypes.ProductTypes productType, EnumTypes.ColorTypes colorType, int amount)
        {
            ProductType = productType;
            ColorType = colorType;
            Amount = amount;
        }
    }

    public List<Order> orders = new List<Order>();
}
#endregion