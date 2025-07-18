using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDieState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.SetAnim("isDie");
        t.isDead = true;
    }

    public void OnExecute(Character t)
    {
        t.Die();
    }

    public void OnExit(Character t)
    {
        
    }
}
