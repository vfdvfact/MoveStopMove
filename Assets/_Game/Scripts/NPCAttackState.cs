using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttackState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.LookAt(t.CalcuBangDirection(t.currentGun.transform.position, t.target.transform.position));
        t.SetAnim("isAttack");
        t.aniWaiting = true;
    }

    public void OnExecute(Character t)
    {
        if (t.currentGun.isCooledDown && t.aniWaiting)
        {
            //Debug.Log("Bang");
            t.TriggerFireInTime();
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
