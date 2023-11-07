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
    [SerializeField]
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
        Holder.OutBand();

    }
    public void MoveProcessEnd(IProcessor processor)
    {
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
        ShowToCamera();
    }
    #endregion
    #region MyMethods
    private void ShowToCamera()
    {
        Vector3 movePos = Camera.main.transform.position+new Vector3(0,0, 1);
        Holder.gameObject.transform.DOMove(movePos,1).OnComplete(()=>StartCoroutine(WaitForGoSell()) );
        Holder.currentProduct.transform.DOLocalMove(Vector3.zero, 0.5f);
    }
    IEnumerator WaitForGoSell()
    {
        Holder.priceText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Holder.priceText.gameObject.SetActive(false);
        OrderManager.Instance.IsOrdered(this);
    }
    public void BecomeFloadingUI()
    {
        EventManager.OnExcangeInstantiate.Invoke(transform.position,ProductWorth);
        Holder.gameObject.SetActive(false);
    }
    public void MoveUI(Vector3 UIPos)
    {
        transform.DOMove(UIPos,1).OnComplete(UICheck);
    }
    private void UICheck()
    {
        BecomeFloadingUI();
        OrderManager.Instance.orderPanel.OrderArrived();
    }
    private void ProductNextScene()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        Holder.bandController.nextBand.AddHolder(Holder.gameObject);
        EventManager.OnProductChangeBand.Invoke();

    }

    #endregion
}
