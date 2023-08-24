using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent OpenWinPanel = new UnityEvent();
    public static UnityEvent OpenRestartPanel = new UnityEvent();
    public static UnityEvent OnChangeModel = new UnityEvent();
    public static UnityEvent OnLevelDataChange = new UnityEvent();
    public static UnityEvent OnPlayerDataChange = new UnityEvent();
    public static UnityEvent OnProductChangeBand = new UnityEvent();
    public static IntVector3Event OnExcangeInstantiate = new IntVector3Event();
    
}
public class BoolEvent : UnityEvent<bool> { }
public class IntEvent : UnityEvent<int> { }
public class IntVector3Event : UnityEvent<Vector3,int> { }
public class FloatEvent : UnityEvent<float> { }
public class GameObjectEvent : UnityEvent<GameObject> { }
