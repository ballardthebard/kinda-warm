using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;
    [SerializeField] private float movementThreshold = 0.2f;
    [SerializeField] private float slowMotionScale;
    [SerializeField] private float headWeight;
    [SerializeField] private float handWeight;

    private float headMagnitude;
    private float leftHandMagnitude;
    private float rightHandMagnitude;

    private Vector3 headLastPos;
    private Vector3 leftHandLastPos;
    private Vector3 rightHandLastPos;

    private void Update()
    {
        headMagnitude = (head.position - headLastPos).magnitude * headWeight * 1000;
        headLastPos = head.position;

        leftHandMagnitude = (leftHand.position - leftHandLastPos).magnitude * handWeight * 1000;
        leftHandLastPos = leftHand.position;

        rightHandMagnitude = (rightHand.position - rightHandLastPos).magnitude * handWeight * 1000;
        rightHandLastPos = rightHand.position;

        float playerMovement = (headMagnitude + leftHandMagnitude + rightHandMagnitude) * Time.unscaledDeltaTime;
        playerMovement = Mathf.Clamp01(playerMovement);

        Time.timeScale = playerMovement;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}