using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.SetAnim("isDie");
        t.isDead = true;
    }

    public void OnExecute(Character t)
    {
        t.WaitFailUI();
    }

    public void OnExit(Character t)
    {
        
    }
}
