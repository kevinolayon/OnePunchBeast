using UnityEngine;

public class Puncheable : MonoBehaviour, IPunchable
{
    [SerializeField] Rigidbody rb;
    [SerializeField] ParticleSystem hitVFX;
    Animator anim;
    bool canPunch = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public bool CanPunch()
    {
        return canPunch;
    }

    public void Punched(Vector3 force, Vector3 position)
    {
        canPunch = false;
        anim.enabled = false;
        hitVFX.gameObject.SetActive(true);
        rb.AddForceAtPosition(force, position, ForceMode.Impulse);
    }
}
