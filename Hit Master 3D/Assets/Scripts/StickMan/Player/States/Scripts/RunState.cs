using UnityEngine;

[CreateAssetMenu]
public class RunState : State
{
    [SerializeField] private State _attackState;
    public override void Init()
    {
        Player.RunToPoint();
    }

    public override void Do()
    {
        IsFinished = Player.DistanceToDestination();
        if (IsFinished)
        {
            Player.SetState(_attackState);
        }
    }
}
