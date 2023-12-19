
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHand : MonoBehaviour
{
    [SerializeField] private InputActionProperty pinchAnimation;
    [SerializeField] private InputActionProperty fistAnimation;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float triggerValue = pinchAnimation.action.ReadValue<float>();
        float gripValue = fistAnimation.action.ReadValue<float>();

        animator.SetFloat("Trigger", triggerValue);
        animator.SetFloat("Grip", gripValue);

    }
}
