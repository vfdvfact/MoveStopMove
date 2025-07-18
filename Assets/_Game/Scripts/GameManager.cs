using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState State;
    private void Start()
    {
        State = GameState.MainMenu;
    }
}
