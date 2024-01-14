using UnityEngine;

public interface IPunchable
{
    bool CanPunch();
    void Punched(Vector3 force, Vector3 position);
}
