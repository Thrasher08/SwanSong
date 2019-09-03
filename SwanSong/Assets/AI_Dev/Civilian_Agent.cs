using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian_Agent : MonoBehaviour
{
    public enum States
    {
        IDLE = 0,
        FOLLOW = 1,
        FLEE = 2
    }

    public States currentState;

    private void Start()
    {

        currentState = States.IDLE;

    }

    private void Update()
    {

        switch (currentState)
        {
            case States.IDLE:
                //IDLE
                Idle_Update();
                break;
            case States.FOLLOW:
                //FOLLOW
                Follow_Update();
                break;
            case States.FLEE:
                //FLEE
                Flee_Update();
                break;
            default:
                break;
        }

    }

    private void Idle_Update()
    {

    }

    private void Follow_Update()
    {

    }

    private void Flee_Update()
    {

    }
}
