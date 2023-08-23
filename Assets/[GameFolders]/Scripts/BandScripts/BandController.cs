using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandController : MonoBehaviour
{
    public BandController nextBand;
    private RawMaterialSpawner matSpawner;
    public List<Transform> HolderPoints;
    public List<GameObject> Holders;
    public bool colorBoil;
    private void Start()
    {
        matSpawner = GetComponent<RawMaterialSpawner>();
    }
    public void SortHolders()
    {
        for (int i = 0; i < Holders.Count; i++)
        {
            float distance = Vector3.Distance(Holders[i].transform.position, HolderPoints[i].position);
            if (distance > 0.1f)
                Holders[i].GetComponent<ProductHolder>().MovePoint(HolderPoints[i].position);
        }
    }
    public void AddHolder(GameObject newHolder)
    {
        newHolder.transform.position = HolderPoints[Holders.Count].position;
        newHolder.transform.rotation = HolderPoints[Holders.Count].rotation;
        Holders.Add(newHolder);
        newHolder.GetComponent<ProductHolder>().bandController=this;
        newHolder.GetComponent<ProductHolder>().OnBand();
    }
    public void RemoveHolder(GameObject removeHolder)
    {
        for (int i = Holders.Count - 1; i >= 0; i--)
        {
            if (removeHolder == Holders[i])
            {
                Holders.RemoveAt(i);
            }
        }

        if (Holders.Count == 0)
        {
            if (matSpawner != null)
                matSpawner.Spawn();
        }

        SortHolders();
    }
}
