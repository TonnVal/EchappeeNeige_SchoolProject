using System;
using UnityEngine;

public static class GameEventService
{
    public static Action<bool> OnGameState;

    public static Action OnCollision;

    public static Action<float> OnScoreIncrease;
    public static Action<float> OnSnowFloodUpdated;
}
