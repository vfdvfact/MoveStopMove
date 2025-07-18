using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.SetAnim("isIdle");
    }

    public void OnExecute(Character t)
    {
        if (t.GetInputForMove())
        {
            t.ChangeState(new PlayerMoveState());
        }
        else if (t.currentGun.isCooledDown && t.GetFilteredCollider() != null)
        {
            t.ChangeState(new PlayerAttackState());
        }
    }

    public void OnExit(Character t)
    {
        
    }
}
