using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoveState : IState<Character>
{
    public void OnEnter(Character t)
    {
        //Debug.Log("NPCMove");
        t.ResumeRun();
        t.SetAnim("isRun");
        t.Move();

    }

    public void OnExecute(Character t)
    {
        if (t.currentGun.isCooledDown&&t.GetFilteredCollider() != null)
        {            
            t.ChangeState(new NPCAttackState());
        }
        else if (t.GetFilteredCollider() != null)
        {
            t.ChangeState(new NPCIdleState());
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
        t.PauseRun();
    }
}
