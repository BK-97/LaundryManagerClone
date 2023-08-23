using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMaterialSpawner : MonoBehaviour
{
    private BandController bandController;
    public BandController BandController { get { return (bandController == null) ? bandController = GetComponent<BandController>() : bandController; } }
    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        for (int i = 0; i < 3; i++)
        {
            var go = PoolingSystem.Instance.InstantiateAPS("ProductHolder");
            go.GetComponent<ProductHolder>().SetInfo(EnumTypes.ProductTypes.Rope, EnumTypes.ColorTypes.None, 0);
            BandController.AddHolder(go);
            BandController.Holders[i].transform.position = BandController.HolderPoints[i].position;
            BandController.Holders[i].transform.rotation = BandController.HolderPoints[i].rotation;
            go.GetComponent<ProductHolder>().OnBand();
        }
    }
}
