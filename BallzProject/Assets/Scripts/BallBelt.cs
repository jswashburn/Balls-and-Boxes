using System;
using System.Collections.Generic;
using UnityEngine;

public class BallBelt : MonoBehaviour
{
    [SerializeField] List<Ball> balls;
    [SerializeField] int _capacity;

    static Queue<Ball> _inactive;

    void Awake()
    {
        _inactive = new Queue<Ball>(_capacity);
    }

    public static void RestoreBall()
    {
        if (_inactive.Count > 0)
        {
            Ball restoredBall;
            restoredBall = _inactive.Dequeue();
            restoredBall.gameObject.SetActive(true);
            restoredBall.Reset();
        }
    }

    public static void EnqueueDeadBall(Ball ball)
    {
        _inactive.Enqueue(ball);
    }
}
