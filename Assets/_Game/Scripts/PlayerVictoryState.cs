using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVictoryState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.SetAnim("isWin");
    }

    public void OnExecute(Character t)
    {
        
    }

    public void OnExit(Character t)
    {
        
    }
}
