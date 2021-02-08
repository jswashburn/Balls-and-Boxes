using System;
using System.Collections.Generic;
using UnityEngine;

public class BallBelt : MonoBehaviour
{
    [SerializeField] byte _capacity;

    static Queue<Ball> _inactive;

    void OnEnable()
    {
        GameStateManager.OnDifficultyIncrease += RestoreBall;
    }

    void Awake()
    {
        _inactive = new Queue<Ball>(_capacity);
    }

    public static void RestoreBall()
    {
        if (_inactive.Count > 0)
        {
            Ball restoredBall = _inactive.Dequeue();
            restoredBall.gameObject.SetActive(true);
            restoredBall.Reset();
        }
    }

    public static void EnqueueDeadBall(Ball deadBall)
    {
        _inactive.Enqueue(deadBall);
    }
}
