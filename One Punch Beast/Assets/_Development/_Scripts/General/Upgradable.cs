using System.Collections;
using UnityEngine;

public class Upgradable : MonoBehaviour, IInteract
{
    [SerializeField] string targetTag;
    IInteractable interactable;

    CanvasManager canvas;

    private void Start()
    {
        canvas = CanvasManager.Instance;
    }

    public void Interact(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            interactable = other.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                canvas.ShowUpgrades();
            }
        }
    }

    public void StopTimeInteraction()
    {
        canvas.HideUpgrades();
    }

    public IEnumerator TimeToInteract(float timer, Collider other)
    {
        yield return null;
    }
}
