using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HandPose", menuName = "ScriptableObjects/HandPose", order = 1)]
public class HandPose : ScriptableObject
{
    public Vector3 position;
    public Quaternion rotation;

    public List<Quaternion> fingerRotations;
}