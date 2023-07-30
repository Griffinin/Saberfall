using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checks : MonoBehaviour
{
    public ContactFilter2D castFilter;
    private float groundDistance = 0.08f;
    Rigidbody2D rb; // Player
    [SerializeField] BoxCollider2D head;
    [SerializeField] BoxCollider2D foot1;
    [SerializeField] BoxCollider2D foot2;
    [SerializeField] BoxCollider2D body;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    Animator anim;

    
    [SerializeField] private bool _isGrounded;
    public bool grounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            anim.SetBool("grounded", value);

        }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = foot1.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0 || foot2.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }
}
