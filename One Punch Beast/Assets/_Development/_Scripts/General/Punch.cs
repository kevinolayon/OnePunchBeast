using UnityEngine;

public class Punch : MonoBehaviour, IPunch
{
    [SerializeField] float force = 10f;
    [SerializeField] string targetTag;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Punching(Collider other)
    {
        if (other.CompareTag(targetTag))
        {            
            Puncheable enemie = other.GetComponentInParent<Puncheable>();

            if (enemie != null)
            {
                if (!enemie.CanPunch()) return;
                anim.SetTrigger("punching");

                // Get force and direction
                Vector3 newDir = other.transform.position - transform.position;
                newDir.y = 1;
                Vector3 newForce = force * newDir.normalized;

                // Get position
                Vector3 newPos = other.ClosestPoint(transform.position);

                enemie.Punched(newForce, newPos);
            }
        }
    }
}
