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
    [SerializeField] private float fistTreshold = 0.75f;

    private Animator animator;
    private Vector3 defaultLocalPosition;
    private Quaternion defaultLocalRotation;
    private Collider collider;

    private bool isHoldingObject;

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponentInChildren<Collider>();
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

        //Enable Collider
        if (gripValue >= fistTreshold && !isHoldingObject)
        {
            collider.enabled = true;
        }
        else
        {
            collider.enabled = false;
        }
    }

    public void SetPose(SelectEnterEventArgs args)
    {
        isHoldingObject = true;
        animator.enabled = false;
        Grabbable grabbable = args.interactableObject.transform.GetComponent<Grabbable>();
        HandPose handPose = null;

        // Choose the appropriate hand pose
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
        // Reset hand back to default
        isHoldingObject = false;
        animator.enabled = true;
        transform.localPosition = defaultLocalPosition;
        transform.localRotation = defaultLocalRotation;
    }
}
