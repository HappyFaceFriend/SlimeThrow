using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Camera Shake Params")]
[System.Serializable]
public class CameraShakeParams : ScriptableObject
{
    public enum CurveType { EaseIn, Linear, Constant }
    public float Magnitude;
    public float Duration;
    public CurveType Curve;
}