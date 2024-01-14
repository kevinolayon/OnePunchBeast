using UnityEngine;

public class Puncheable : MonoBehaviour, IPunchable
{
    [SerializeField] Rigidbody rb;
    [SerializeField] ParticleSystem hitVFX;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Punched(Vector3 force, Vector3 position)
    {
        anim.enabled = false;
        hitVFX.transform.position = position;
        hitVFX.gameObject.SetActive(true);
        rb.AddForceAtPosition(force, position, ForceMode.Impulse);
    }
}
