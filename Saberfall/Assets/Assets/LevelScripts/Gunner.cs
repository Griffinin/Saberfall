using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private DetectionZone zone;
    [SerializeField] private SpriteRenderer gun;
    private UnitHealth uh;
    [SerializeField] private GameObject playerLoc;
    private bool flipping = false;
    private bool _hasTarget = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        gun = transform.Find("Gun").GetComponent<SpriteRenderer>();
        uh = GetComponent<UnitHealth>();
    }
    //function to check to set the hasTarget variable transitioning the gunner to the shooting phase
    public bool HasTarget { get { return _hasTarget; }private set
        {
            _hasTarget = value;
            anim.SetBool("hasTarget", value);
        }
     }



    // Update is called once per frame
    private void Update()
    {
        //relies on the detection zone to find target
        HasTarget = zone.detectedCollider.Count > 0;
        if (!uh.IsAlive)
        {
            //enable a gun sprite if the enemy dies
            gun.enabled = true;
        }
        //logic for flipping the gunner
        if (uh.IsAlive)
        {
            if (playerLoc.transform.position.x > (transform.position.x + 4f) && !flipping)
            {
                flip();

            }
            else if (playerLoc.transform.position.x < (transform.position.x-4f) && flipping)
            {
                flip();

            }
        }
    }
    private void flip()
    {
        Vector3 currScale = transform.localScale;
        currScale.x *= -1;
        transform.localScale = currScale;
        flipping = !flipping;
    }
}
