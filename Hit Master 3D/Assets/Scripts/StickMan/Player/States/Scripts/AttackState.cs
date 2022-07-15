using UnityEngine;

[CreateAssetMenu]
public class AttackState : State
{
    [SerializeField] private State _runState;
    public override void Init()
    {
        
    }

    public override void Do()
    {
        if (IsFinished)
        {
            Player.SetState(_runState);
        }
        Aiming();
    }

    void Aiming()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Player.Aim();
        }
    }
    
}
