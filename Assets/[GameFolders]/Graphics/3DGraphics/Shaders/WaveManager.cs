using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public Material waveMaterial;
    public ComputeShader waveCompute;
    public RenderTexture NState, Nm1State, Np1State;
    public Vector2Int resolution; //has to be same with obstaclesTexture resolution
    public RenderTexture obstaclesTex;
    public Vector3 effect;
    public float dispersion = 0.98f;

    void Start()
    {
        InitilizeTexture(ref NState);
        InitilizeTexture(ref Nm1State);
        InitilizeTexture(ref Np1State);
        obstaclesTex.enableRandomWrite = true;
        waveMaterial.mainTexture = NState;
    }
    void InitilizeTexture(ref RenderTexture tex)
    {
        tex = new RenderTexture(resolution.x, resolution.y, 1, UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SNorm);
        tex.enableRandomWrite = true;
        tex.Create();
    }

    void Update()
    {
        Graphics.CopyTexture(NState, Nm1State);
        Graphics.CopyTexture(Np1State, NState);

        waveCompute.SetTexture(0, "NState", NState);
        waveCompute.SetTexture(0, "Nm1State", Nm1State);
        waveCompute.SetTexture(0, "Np1State", Np1State);

        waveCompute.SetVector("effect",effect);
        waveCompute.SetVector("resolution", new Vector2(resolution.x,resolution.y));

        waveCompute.SetFloat("dispersion", dispersion);
        waveCompute.SetTexture(0,"obstaclesTex", obstaclesTex);
        waveCompute.Dispatch(0,resolution.x/8,resolution.y/8,1);
    }
}
