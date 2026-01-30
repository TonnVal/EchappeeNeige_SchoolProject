using System;
using UnityEngine;

public static class GameEventService
{
    public static Action<bool> OnCountdownState;
    public static Action<float> OnCountdownTick;
    public static Action<bool> OnGameState;
    public static Action<bool> OnGameOver;

    public static Action OnCollision;
    public static Action<Material> OnChunkChangeColor;

    public static Action<float> OnScoreIncrease;
    public static Action<int> OnScoreMultiplicatorUpdated;

    public static Action<float> OnSnowFloodUpdated;
    public static Action<float> OnSpeedUpdated;


}
