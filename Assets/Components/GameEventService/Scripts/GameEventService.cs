using System;
using UnityEngine;

public static class GameEventService
{
    [Header("Main States")]
    public static Action<bool> OnGameState;

    [Header("Score")]
    public static Action<float> OnScoreIncrease;
}
