using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MollyEffect : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particle;
    void Start()
    {
        particle.Emit(100);
    }
}
