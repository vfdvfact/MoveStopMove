using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : IState<Character>
{
    public void OnEnter(Character t)
    {
        //Debug.Log("NPCIdle");
        t.SetAnim("isIdle");
    }
    public void OnExecute(Character t)
    {
        if (t.currentGun.isCooledDown&&t.GetFilteredCollider() != null&&t.CanSeeTarget())
        {
            t.ChangeState(new NPCAttackState());
        }
        else if (t.IsTargetStillInRange())
        {
            return;
        }
        else if (t.isFindedRandomDestination())
        {
            t.ChangeState(new NPCMoveState());
        }
    }

    public void OnExit(Character t)
    {
        
    }
}
