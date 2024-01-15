using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour, IInteract, IDrag
{
    [SerializeField] float dragSpeed = 10;
    [SerializeField] float stopDistance = .5f;
    [SerializeField] float rotSpeed = 180;
    [SerializeField] float dragHeight = 1;
    [SerializeField] string targetTag;
    [SerializeField] float timeToDrag = 1;
    [SerializeField] int maxStack = 1;
    //[SerializeField] Transform stack;
    //List<Transform> PileList = new();
    List<Transform> DragList = new();

    Coroutine stop;

    bool dragging;

    IInteractable interactable;


    //private void Awake()
    //{
    //    foreach (Transform pile in stack)
    //    {
    //        PileList.Add(pile);
    //    }
    //}

    private void Awake()
    {
        DragList.Add(transform);
    }

    private void Update()
    {
        if (DragList.Count > 1)
        {
            for (int i = 1; i < DragList.Count; i++)
            {
                Vector3 targetPos = DragList[i - 1].position - (DragList[i - 1].forward * stopDistance);
                DragList[i].SetPositionAndRotation(Vector3.Lerp(DragList[i].position, new Vector3(targetPos.x, dragHeight, targetPos.z), Time.smoothDeltaTime * dragSpeed),
                    Quaternion.Slerp(DragList[i].rotation, Quaternion.LookRotation(targetPos - DragList[i].position), Time.smoothDeltaTime * rotSpeed));
            }
        }
    }

    public void Interact(Collider other)
    {
        dragging = false;
        if (other.CompareTag(targetTag))
        {
            interactable = other.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                if (interactable.CanInteract() && DragList.Count < maxStack + 1)
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
        //other.transform.parent.SetParent(PileList[0]);
        //other.transform.parent.DOLocalMove(Vector3.zero, 1);
        //other.transform.parent.DOLocalRotate(Vector3.zero, 1, RotateMode.Fast);
        //other.transform.DOLocalMove(Vector3.zero, 1);
        //other.transform.DOLocalRotate(Vector3.zero, 1, RotateMode.Fast);
        DragList.Add(other.transform);
        CanvasManager.Instance.UpdateStack();

        interactable = null;
    }

    public void StopTimeInteraction()
    {
        if (!dragging && stop != null)
            StopCoroutine(stop);
    }

    public void StackIncrease()
    {
        maxStack += 1;
    }

    public int Release()
    {
        int count = 0;
        if (DragList.Count > 1)
        {
            count = DragList.Count - 1;

            for (int i = count; i > 0; i--)
            {
                Transform drag = DragList[i];
                Rigidbody dragRb = drag.GetComponent<Rigidbody>();
                Collider col = drag.GetComponent<Collider>();
                dragRb.isKinematic = false;
                col.excludeLayers &= ~(1 << LayerMask.NameToLayer("Player"));
                dragRb.excludeLayers &= ~(1 << LayerMask.NameToLayer("Player"));
                DragList.RemoveAt(i);
            }
        }

        return count;
    }

    public int MaxStack()
    {
        return maxStack;
    }

    public int CurrentStack()
    {
        return DragList.Count - 1;
    }
}
