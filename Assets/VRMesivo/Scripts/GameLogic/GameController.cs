using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GameController : NetworkBehaviour
{
    [Networked] public bool GameContinue { get; private set; } = false;
    [Networked] public bool GameTimeStarts { get; private set; } = false;

    public void StartTraining()
    {      
        GameContinue = true;
    }

    public void EndGame()
    {
        GameContinue = false;
    }

    public void StartGameTime() 
    { 
        GameTimeStarts = true;
    }
}
