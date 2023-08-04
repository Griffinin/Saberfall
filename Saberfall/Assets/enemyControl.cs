//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class enemyControl : MonoBehaviour
//{
//    public GameObject pointA;
//    public GameObject pointB;
//    private Rigidbody2D rigidBody;
//    private Animator animator;
//    private Transform currentPt;
//    private float speed;

//    // Start is called before the first frame update
//    void Start()
//    {
//        rigidBody = GetComponent<Rigidbody2D>();
//        animator = GetComponent<Animator>();
//        currentPt = pointB.transform;
//        animator.SetBool("isMoving", true);
        
//    }

//    // Update is called once per frame
//    void Update()
//    {

//        Vector2 pt = currentPt.position - transform.position; //distance between player and pt to patrol

//        if (currentPt == pointB.transform)
//            rigidBody.velocity = new Vector2(speed, 0);
//        else
//            rigidBody.velocity = new Vector2(-speed, 0);
        
//    }
//}
