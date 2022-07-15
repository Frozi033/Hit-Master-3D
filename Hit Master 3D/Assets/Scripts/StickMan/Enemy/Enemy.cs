using System;
using UnityEngine;

[RequireComponent(typeof(RagDollControls))]
public class Enemy : MonoBehaviour
{
    private RagDollControls _ragDoll;

    public static Action EnemyDead;
    private void Start()
    {
        _ragDoll = GetComponent<RagDollControls>();
    }

    public void Dead()
    {
        _ragDoll.SetRagdoll(true);
        EnemyDead?.Invoke();
    }
}
