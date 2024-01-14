using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Drag : MonoBehaviour, IInteract
{
    [SerializeField] string targetTag;
    [SerializeField] float timeToDrag = 1;
    [SerializeField] Transform stack;
    List<Transform> PileList = new();
    List<Transform> DragList = new();

    Coroutine stop;
    bool dragging;
    IInteractable interactable;

    private void Awake()
    {
        foreach (Transform pile in stack)
        {
            PileList.Add(pile);
        }
    }

    public void Interact(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            interactable = other.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                if (interactable.CanInteract())
                    stop = StartCoroutine(TimeToInteract(timeToDrag, other));
            }
        }
    }

    public IEnumerator TimeToInteract(float timer, Collider other)
    {
        yield return new WaitForSeconds(timer);

        dragging = true;
        interactable.Interacting();

        // Set the bodie in the pile stack
        other.transform.parent.SetParent(PileList[0]);
        other.transform.parent.DOLocalMove(Vector3.zero, 1);
        other.transform.parent.DOLocalRotate(Vector3.zero, 1, RotateMode.Fast);
        other.transform.DOLocalMove(Vector3.zero, 1);
        other.transform.DOLocalRotate(Vector3.zero, 1, RotateMode.Fast);
        DragList.Add(other.transform);
    }

    public void StopTimeInteraction()
    {
        if (!dragging)
            StopCoroutine(stop);
    }
}
