using Assets.MonoBehaviors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Saberfall
{

    public class ChomperPatrolSMB : SMB<EnemyBehaviors>
    {
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //We do this explicitly here instead of in the enemy class, that allow to handle obstacle differently according to state
            // (e.g. look at the ChomperRunToTargetSMB that stop the pursuit if there is an obstacle) 
            float dist = m_MonoBehaviour.speed;
            if (m_MonoBehaviour.CheckForObstacle(dist))
            {
                //this will inverse the move vector, and UpdateFacing will then flip the sprite & forward vector as moveVector will be in the other direction
                m_MonoBehaviour.SetHorizontalSpeed(-dist);
                m_MonoBehaviour.UpdateFacing();
            }
            else
            {
                m_MonoBehaviour.SetHorizontalSpeed(dist);
            }

            m_MonoBehaviour.ScanForPlayer();
        }
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}

    }
