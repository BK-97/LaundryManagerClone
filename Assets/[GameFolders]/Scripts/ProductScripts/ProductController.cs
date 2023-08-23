using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ProductController : MonoBehaviour,IProduct
{
    #region Params
    private ProductHolder holder;
    public ProductHolder Holder { get { return (holder == null) ? holder = GetComponentInParent<ProductHolder>() : holder; } }
    [HideInInspector]
    public EnumTypes.ProductTypes productType;
    [HideInInspector]
    public EnumTypes.ColorTypes colorType;
    private int _worth;
    public int ProductWorth { get => _worth; set => _worth = value; }
    #endregion
    #region IProductMethods
    public void SetInfo(EnumTypes.ProductTypes proType, EnumTypes.ColorTypes colorType, int addWorth)
    {
        ProductWorth += addWorth;
        productType = proType;
        this.colorType = colorType;
    }
    public void MoveProcess(IProcessor processor)
    {
        Transform targetTransform = processor.GetProductPlace();
        Holder.CircleImageScale();
        transform.DOMove(targetTransform.position, 1).SetEase(Ease.Linear).OnComplete(() => MoveProcessEnd(processor));
    }
    public void MoveProcessEnd(IProcessor processor)
    {
        Holder.OutBand();
        Holder.bandController.RemoveHolder(Holder.gameObject);
        processor.GetProduct(Holder);
    }
    public void MoveNextProcess()
    {
        Vector3 targetPos = GameManager.Instance.NextSceneUIPos;
        transform.DORotate(Vector3.zero,0.5f);
        transform.DOMove(targetPos, 1).SetEase(Ease.Linear).OnComplete(ProductNextScene);
    }
    public void Sell()
    {
        OrderManager.Instance.IsOrdered(this);
    }
    #endregion
    #region MyMethods

    public void BecomeFloadingUI()
    {
        ExchangeManager.Instance.AddCurrency(CurrencyType.Cash, ProductWorth); //bu kýsým FloadingUI da olacak
        PoolingSystem.Instance.DestroyAPS(Holder.gameObject);
    }
    public void MoveUI(Vector3 UIPos)
    {
        transform.DOMove(UIPos,1).OnComplete(UICheck);
    }
    private void UICheck()
    {
        OrderManager.Instance.orderPanel.OrderArrived();
        BecomeFloadingUI();
    }
    private void ProductNextScene()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        Holder.bandController.nextBand.AddHolder(Holder.gameObject);
        EventManager.OnProductChangeBand.Invoke();

    }

    #endregion
}
