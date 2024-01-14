using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    [SerializeField] Rigidbody[] rbList;
    Rigidbody rb;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die(Vector3 pos)
    {
        anim.enabled = false;
        rb.AddForce(pos + Vector3.up * 50, ForceMode.Impulse);
        //foreach (Rigidbody rb in rbList)
        //{
        //    rb.AddForce(pos + Vector3.up * 50, ForceMode.Impulse);
        //}                
    }    
}
