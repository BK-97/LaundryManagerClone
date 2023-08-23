using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    public GameObject colorScene;
    public GameObject sewScene;
    public static UnityEvent OnSewOpen = new UnityEvent();
    public static UnityEvent OnColorOpen = new UnityEvent();

    public BandController TestBand;
    private void OnEnable()
    {
        OnSewOpen.AddListener(OpenSewScene);
        OnColorOpen.AddListener(OpenColorScene);
        EventManager.OnProductArriveNextSceneButton.AddListener(Test);
    }
    private void OnDisable()
    {
        OnSewOpen.RemoveListener(OpenSewScene);
        OnColorOpen.RemoveListener(OpenColorScene);
        EventManager.OnProductArriveNextSceneButton.RemoveListener(Test);

    }
    private void Test(GameObject test)
    {
        Debug.Log("test");
        var go = PoolingSystem.Instance.InstantiateAPS("ProductHolder", transform.position);
        go.GetComponent<ProductHolder>().SetInfo(EnumTypes.ProductTypes.Socks, EnumTypes.ColorTypes.None, 50);
        TestBand.AddHolder(go);
    }
    private void OpenColorScene()
    {
        colorScene.SetActive(true);
        sewScene.SetActive(false);

    }
    private void OpenSewScene()
    {
        colorScene.SetActive(false);
        sewScene.SetActive(true);
    }
}
