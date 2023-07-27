using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAttacks : MonoBehaviour
{
    [SerializeField] private float attackRate = 0.1f;
    [SerializeField] private float nextAttackTime = 0f;
    // Start is called before the first frame update
    private Animator anim;
    private void Awake()
    {
        
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SlashUp();
                nextAttackTime = Time.time + 1f /attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                anim.SetTrigger("attack");
                Punch();
                nextAttackTime = Time.time + 1f / attackRate;
            }

        }
        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
        {
            SlashDown();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            SwipeUp();
        }

    }

    private void SlashUp()
    {
        anim.SetTrigger("attack");
    }

    private void SwipeUp()
    {
        anim.SetTrigger("swipe");
    }

    private void SlashDown()
    {
        anim.SetTrigger("slashd");
    }

    private void Punch()
    {
        anim.SetTrigger("punch");
    }
}

