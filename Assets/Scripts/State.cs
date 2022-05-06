using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public EnemyMovement movement;

    public abstract State RunCurrentState();
}
