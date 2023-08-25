using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
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
        OrderManager.OnGetNewOrder.AddListener(SetOrders);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(SetOrders);
        LevelManager.Instance.OnLevelFinish.RemoveListener(HidePanel);
        OrderManager.OnGetNewOrder.RemoveListener(SetOrders);

    }
    public void SetOrders()
    {
        ShowPanel();
        OrderManager.Instance.orderPanel = this;
        currentOrders = OrderManager.Instance.GetOrderInfo();
        for (int i = 0; i < currentOrders.Count; i++)
        {
            for (int k = 0; k < currentOrders[i].Amount; k++)
            {
                var go = Instantiate(productIcon, transform);
                OrderIcons.Add(go.GetComponent<OrderIconController>());
                ImageSet(currentOrders[i].ProductType, currentOrders[i].ColorType);
            }
        }
    }
    private void ImageSet(EnumTypes.ProductTypes proType, EnumTypes.ColorTypes colorType)
    {
        OrderIcons[currentIndex].SetInfo(proType, colorType);
        currentIndex++;
    }
    public Vector3 GetOrderPos(EnumTypes.ProductTypes proType, EnumTypes.ColorTypes colorType)
    {
        for (int i = 0; i < OrderIcons.Count; i++)
        {

            if (OrderIcons[i].proType == proType && OrderIcons[i].colorType == colorType)
            {
                Vector3 offSet = new Vector3(0, 0, -Camera.main.transform.position.z * 2);
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(OrderIcons[i].orderIcon.transform.position + offSet);
                currentOrderIndex = i;
                return worldPos;
            }
        }
        return Vector3.zero;
    }
    private int currentOrderIndex;
    public void OrderArrived()
    {
        var go = OrderIcons[currentOrderIndex];
        go.Arrived();

        StartCoroutine(WaitForCheck());
    }
    IEnumerator WaitForCheck()
    {
        yield return new WaitForSeconds(0.75f);
        OrderIcons = OrderIcons.Where(item => item != null).ToList();
        CheckOrderList();
    }
    public void CheckOrderList()
    {
        if (OrderIcons.Count == 0)
        {
            currentIndex = 0;
            OrderManager.OnOrderCompleted.Invoke();

        }
    }
}
