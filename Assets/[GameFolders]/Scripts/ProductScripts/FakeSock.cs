using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FakeSock : MonoBehaviour, IFakeProduct
{
    float currentFloat;
    public MeshRenderer mesh;
    public void StartColorChange(float processTime)
    {
        
    }
    public void StartUnDissolve(float processTime)
    {
        Vector3 newPos = new Vector3(-0.1f,0,0.1f);
        transform.DOMove(transform.position+newPos,processTime);
        currentFloat = 0.35f;
        DOTween.To(() => currentFloat, x => currentFloat = x, 0.25f, processTime).SetEase(Ease.Linear)
            .OnUpdate(() => mesh.sharedMaterials[0].SetFloat("_Dissolve", currentFloat));
    }
}
