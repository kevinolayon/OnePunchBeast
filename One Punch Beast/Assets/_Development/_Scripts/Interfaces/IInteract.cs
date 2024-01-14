using System.Collections;
using UnityEngine;

public interface IInteract
{
    IEnumerator TimeToInteract(float timer, Collider other);
    void Interact(Collider other);

    void StopTimeInteraction();
}
