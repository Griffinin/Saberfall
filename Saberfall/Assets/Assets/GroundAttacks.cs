using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAttacks : MonoBehaviour
{
    [SerializeField] private float attackRate = 0.1f;
    [SerializeField] private float nextAttackTime = 0f;
    Checks check;
    private bool airAttack = false;
    private bool canThrow;
    ProjectileLauncher projectileLauncher;
    // Start is called before the first frame update
    private Animator anim;
    private void Awake()
    {
        check = GetComponent<Checks>();
        anim = GetComponent<Animator>();
        projectileLauncher = GetComponent<ProjectileLauncher>();

    }

    // Update is called once per frame
    private void Update()
    {
        inventoryCheck();
        if (Time.time >= nextAttackTime && check.grounded)
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
            else if(Input.GetKeyDown(KeyCode.R) && canThrow)
            {
                anim.SetTrigger("rangedAttack");

            }

        }

        if (!check.grounded && !check.jumpAttack)
        {
            if (Input.GetKeyDown(KeyCode.R) && !airAttack && canThrow) { SlashUp(); airAttack = true; }
        }


        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
        {
            SlashDown();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            SwipeUp();
        }

        if (check.grounded)
        {
            airAttack = false;
        }

    }


    private void SlashUp()
    {
        anim.SetTrigger("attack");
        anim.ResetTrigger("swipe");
        anim.ResetTrigger("slashd");
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



    private void inventoryCheck()
    {
        int ind = projectileLauncher.getIndex();
        if(ind == 0)
        {
            canThrow = MenuController.hasItem(GameObject.Find("ItemList/Sword"));
        }
        else if (ind == 1)
        {
            canThrow = MenuController.hasItem(GameObject.Find("ItemList/Knife"));
        }
        else if (ind == 2)
        {
            canThrow = MenuController.hasItem(GameObject.Find("ItemList/Sword2"));
        }
    }
}

