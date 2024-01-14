using UnityEngine;

public class Draggable : MonoBehaviour, IInteractable
{
    [SerializeField] LayerMask layerToAvoid;
    bool canDrag = true;
    Rigidbody[] bodies;
    Collider[] colliders;    

    private void Awake()
    {
        bodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
    }

    public bool CanInteract()
    {
        return canDrag;
    }

    public void Interacting()
    {
        canDrag = false;
        bodies[0].isKinematic = true;
        foreach (Rigidbody rb in bodies)
        {
            //rb.isKinematic = true;
            rb.excludeLayers = layerToAvoid;
        }

        foreach (Collider collider in colliders)
        {
            collider.excludeLayers = layerToAvoid;
        }
    }
}
