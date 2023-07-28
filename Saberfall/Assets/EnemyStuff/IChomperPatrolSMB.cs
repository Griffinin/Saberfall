using UnityEngine;

namespace Saberfall
{
    public interface IChomperPatrolSMB
    {
        void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
    }
}