using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class AnimateHand : MonoBehaviour
{
    [SerializeField] private InputActionProperty pinchAnimation;
    [SerializeField] private InputActionProperty fistAnimation;
    [SerializeField] private List<Transform> fingers;
    [SerializeField] private bool isRight;

    private Animator animator;
    private Vector3 defaultLocalPosition;
    private Quaternion defaultLocalRotation;

    void Start()
    {
        animator = GetComponent<Animator>();
        defaultLocalPosition = transform.localPosition;
        defaultLocalRotation = transform.localRotation;
    }

    void Update()
    {
        // Get trigger and grip values
        float triggerValue = pinchAnimation.action.ReadValue<float>();
        float gripValue = fistAnimation.action.ReadValue<float>();

        // Animate blendTree
        animator.SetFloat("Trigger", triggerValue);
        animator.SetFloat("Grip", gripValue);
    }

    public void SetPose(SelectEnterEventArgs args)
    {
        animator.enabled = false;
        Grabbable grabbable = args.interactable.GetComponent<Grabbable>();
        HandPose handPose = null;

        // Choose the appropriate hand pose
        if (isRight)
            if (isRight)
                handPose = grabbable.rightHandPose;
            else
                handPose = grabbable.leftHandPose;

        // Set hand global position and rotation to enable the correct rotation of the fingers
        transform.position += handPose.position;
        transform.rotation = handPose.rotation;

        // Set fingers rotations
        for (int i = 0; i < fingers.Count; i++)
        {
            fingers[i].rotation = handPose.fingerRotations[i];
        }

        // Correct hand local position and rotation
        transform.localPosition = handPose.position;
        transform.localRotation = handPose.rotation;
    }

    public void ReleasePose()
    {
        // Reset hand pose back to default
        animator.enabled = true;
        transform.localPosition = defaultLocalPosition;
        transform.localRotation = defaultLocalRotation;
    }
}
