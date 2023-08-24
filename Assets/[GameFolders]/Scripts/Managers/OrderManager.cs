using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrderManager : Singleton<OrderManager>
{
    public List<Sprite> ProductImages;
    private int currentOrderIndex;
    [SerializeField]
    public List<OrderList> orderLists = new List<OrderList>();
    [HideInInspector]
    public OrderPanel orderPanel;

    public int orderAddWorth=100;
    public static UnityEvent OnOrderCompleted = new UnityEvent();

    public List<OrderList.Order> GetOrderInfo()
    {
        return orderLists[currentOrderIndex].orders;
    }
    private void OnEnable()
    {
        OnOrderCompleted.AddListener(OrderCompleted);
    }
    private void OnDisable()
    {
        OnOrderCompleted.RemoveListener(OrderCompleted);
    }
    private void OrderCompleted()
    {

        currentOrderIndex++;
    }
    public void IsOrdered(ProductController product)
    {
        bool isOrdered=false;
        for (int i = 0; i < orderPanel.currentOrders.Count; i++)
        {
            if (orderPanel.currentOrders[i].ProductType == product.productType)
            {
                if (orderPanel.currentOrders[i].ColorType == product.colorType)
                {
                    isOrdered = true;
                    product.ProductWorth += orderAddWorth;
                    product.MoveUI(orderPanel.GetOrderPos(product.productType, product.colorType));
                }
            }
        }
        if(!isOrdered)
            product.BecomeFloadingUI();
        
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