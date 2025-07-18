using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoveState : IState<Character>
{
    public void OnEnter(Character t)
    {
        //Debug.Log("NPCMove");
        t.SetAnim("isRun");
        t.Move();
        t.ResumeRun();
    }

    public void OnExecute(Character t)
    {
        if (t.currentGun.isCooledDown&&t.GetFilteredCollider() != null && t.CanSeeTarget())
        {
            t.PauseRun();
            t.ChangeState(new NPCAttackState());
        }
        else if (t.GetInputForMove())
        {
            return;
        }
        else
        {
            t.ChangeState(new NPCIdleState());
        }
    }

    public void OnExit(Character t)
    {
        
    }
}
