using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderPanel : PanelBase
{
    public GameObject productIcon;
    public List<OrderIconController> OrderIcons;
    public List<OrderList.Order> currentOrders;
    private int currentIndex;
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(SetOrders);
        LevelManager.Instance.OnLevelFinish.AddListener(HidePanel);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(SetOrders);
        LevelManager.Instance.OnLevelFinish.RemoveListener(HidePanel);
    }
    public void SetOrders()
    {
        ShowPanel();
        currentOrders = OrderManager.Instance.GetOrderInfo();
        for (int i = 0; i < currentOrders.Count; i++)
        {
            for (int k = 0; k < currentOrders[i].Amount; k++)
            {
                var go=Instantiate(productIcon, transform);
                OrderIcons.Add(go.GetComponent<OrderIconController>());
                ImageSet(currentOrders[i].ProductType, currentOrders[i].ColorType);
            }
        }
    }
    private void ImageSet(EnumTypes.ProductTypes proType,EnumTypes.ColorTypes colorType)
    {
        Debug.Log(OrderIcons[currentIndex].orderIcon.sprite.name);
        OrderIcons[currentIndex].orderIcon.sprite = OrderManager.Instance.GetProductSprite(proType);
        OrderIcons[currentIndex].orderIcon.color = ColorManager.Instance.GetColorCode(colorType);
       
        currentIndex++;
    }
  
}
