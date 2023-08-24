using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfController : MonoBehaviour
{
    public EnumTypes.ProcessorTypes shelfProcessors;
    public List<GameObject> sewMachines;
    public List<GameObject> colorMachines;
    private void Awake()
    {
        switch (shelfProcessors)
        {
            case EnumTypes.ProcessorTypes.SewMachine:
                OpenCloseObjects(true, sewMachines);
                OpenCloseObjects(false, colorMachines);
                break;
            case EnumTypes.ProcessorTypes.ColorChanger:
                OpenCloseObjects(false,sewMachines);
                OpenCloseObjects(true, colorMachines);
                break;
            case EnumTypes.ProcessorTypes.None:
                break;
            default:
                break;
        }
    }
    private void OpenCloseObjects(bool status,List<GameObject> objects)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].SetActive(status);
        }
    }
}
