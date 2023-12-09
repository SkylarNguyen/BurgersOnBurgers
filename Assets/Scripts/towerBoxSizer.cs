using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class towerBoxSizer : MonoBehaviour
{

    //The plan: because it has caused me several sleepless and sickening hours of making no progress in having
    //colliders apply to the ingredients in the stack and the falling ingredients
    //I had the brilliant plan to try and resize the plate collider as if it's adding the children and merging together.
    //hopefully it works
    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        ResizeCollider();
    }

    void ResizeCollider()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

        // Calculate bounds including children
        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }

        float maxHeight = bounds.size.y; // Initialize the maximum height to the parent's height

        // Check the maximum height of the stack including children
        foreach (Transform child in transform)
        {
            Renderer childRenderer = child.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                maxHeight += childRenderer.bounds.size.y;
            }
        }

        // Adjust the collider size to cover the bounds and total stack height
        boxCollider.center = bounds.center - transform.position;
        boxCollider.size = new Vector3(bounds.size.x, maxHeight, bounds.size.z);
    }

    // Whenever a child is added or removed, resize the collider
    void OnTransformChildrenChanged()
    {
        ResizeCollider();
    }
}
