#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandPoseCreator : MonoBehaviour
{
    [SerializeField] private HandPose handPose;
    [SerializeField] private Transform hand;
    [SerializeField] private List<Transform> fingers;

    private void Start()
    {
        // Update position and rotation
        handPose.position = hand.position;
        handPose.rotation = hand.rotation;

        // Update fingers rotations
        handPose.fingerRotations.Clear();
        foreach (Transform finger in fingers)
        {
            handPose.fingerRotations.Add(finger.rotation);
        }

        // Refresh the AssetDatabase to reflect changes made to assets
        AssetDatabase.Refresh();
        // Mark the 'handPose' asset as dirty (modified) so that Unity knows it needs to be saved
        EditorUtility.SetDirty(handPose);
        // Save changes made to assets
        AssetDatabase.SaveAssets();
    }
}
#endif