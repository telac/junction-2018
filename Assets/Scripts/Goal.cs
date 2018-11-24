using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private bool _lightGoal;
    private bool _darkGoal;

    public void Awake()
    {
        _lightGoal = false;
        _darkGoal = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "LightBall" && !_lightGoal)
        {
            _lightGoal = true;
        }

        if (other.tag == "DarkBall" && !_darkGoal)
        {
            _darkGoal = true;
        }

        if (_lightGoal && _darkGoal) {
            GameManager.Instance.EndLevel();
        }
    }
}
