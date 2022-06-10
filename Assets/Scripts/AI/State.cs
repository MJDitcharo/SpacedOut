using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public EnemyMovement movement;
    [SerializeField] public Animator animator;

    public abstract State RunCurrentState();
}
