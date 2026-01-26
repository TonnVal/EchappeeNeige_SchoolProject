using System;
using UnityEngine;

public static class GameEventService
{
    [Header("Timer Events")]
    public static Action<bool> OnTimerGreenUpdated;
    public static Action<bool> OnTimerBlueUpdated;
    public static Action<bool> OnTimerRedUpdated;
    public static Action<bool> OnTimerBlackUpdated;
    [Header("Timer Data")]
    public static Action<float> HandleTimer;
}
