using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    [SerializeField] private Color playerColor = Color.black;
    [SerializeField] private float parentingDelay = 1.0f;

    private Transform weaponTransform;
    private Transform holdingHand;

    public void TurnWeapon(SelectEnterEventArgs args)
    {
        weaponTransform = args.interactableObject.transform;
        holdingHand = args.interactorObject.transform;

        // Change weapon color if it isn't players color already
        if (weaponTransform.tag != "PlayerWeapon")
        {
            Renderer[] allChildRenderers = weaponTransform.GetComponentsInChildren<Renderer>();

            foreach (Renderer childRenderer in allChildRenderers)
            {
                foreach (Material material in childRenderer.materials)
                {
                    material.color = playerColor;
                }
            }

            weaponTransform.tag = "PlayerWeapon";
        }

        // Invoke pareting with delay to fix positioning bug
        Invoke("ParentWeapon", parentingDelay);
    }

    private void ParentWeapon()
    {
        weaponTransform.SetParent(holdingHand);
    }
}
