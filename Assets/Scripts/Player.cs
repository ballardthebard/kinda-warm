using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    [SerializeField] private Color playerColor = Color.black;
    public void TurnWeapon(SelectEnterEventArgs args)
    {
        Transform weaponTransform = args.interactableObject.transform;

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

        weaponTransform.SetParent(args.interactorObject.transform);
    }
}
