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
    // Start is called before the first frame update
    private bool _hasTarget = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
       // gun = GetComponent<SpriteRenderer>();
        gun = transform.Find("Gun").GetComponent<SpriteRenderer>();
        //gun = GetComponent<SpriteRenderer>();
        uh = GetComponent<UnitHealth>();
    }
    public bool HasTarget { get { return _hasTarget; }private set
        {
            _hasTarget = value;
            anim.SetBool("hasTarget", value);
        }
     }



    // Update is called once per frame
    private void Update()
    {
        HasTarget = zone.detectedCollider.Count > 0;
        if (!uh.IsAlive)
        {
            gun.enabled = true;
        }

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
