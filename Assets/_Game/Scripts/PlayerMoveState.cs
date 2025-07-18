using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.SetAnim("isRun");
    }

    public void OnExecute(Character t)
    {
        if (t.GetInputForMove())
        {
            t.Move();
        }
        else if (t.currentGun.isCooledDown && t.GetFilteredCollider() != null)
        {
            t.ChangeState(new PlayerAttackState());
        }
        else 
        {
            t.ChangeState(new PlayerIdleState());
        }
    }

    public void OnExit(Character t)
    {
        
    }
}
