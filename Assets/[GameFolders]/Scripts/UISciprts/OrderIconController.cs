using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderIconController : MonoBehaviour
{
    public Image orderIcon;
    public Image backGround;
    [HideInInspector]
    public EnumTypes.ProductTypes proType;
    [HideInInspector]
    public EnumTypes.ColorTypes colorType;
    public void SetInfo(EnumTypes.ProductTypes proType, EnumTypes.ColorTypes colorType)
    {
        this.proType = proType;
        this.colorType = colorType;
        orderIcon.sprite = OrderManager.Instance.GetProductSprite(proType);
        orderIcon.color = ColorManager.Instance.GetColorCode(colorType);
    }
    public Vector3 GetPos()
    {
        return transform.position;
    }
    public void Arrived()
    {
        backGround.color = Color.green;
        Invoke("Demolish", 0.5f);
    }
    void Demolish()
    {
        gameObject.SetActive(false);
        //deðiþecek
    }
}
