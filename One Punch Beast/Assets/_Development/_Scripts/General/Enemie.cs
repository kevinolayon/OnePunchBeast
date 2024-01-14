using UnityEngine;

public class Enemie : MonoBehaviour
{
    bool isPunchable = true;
    //bool isDraggable; = false;

    IPunchable punchable;

    private void Awake()
    {
        punchable = GetComponent<IPunchable>();
    }

    public bool IsPunchable()
    {
        return isPunchable;
    }

    public void BePunched()
    {
        //punchable.Punched(
    }
}
