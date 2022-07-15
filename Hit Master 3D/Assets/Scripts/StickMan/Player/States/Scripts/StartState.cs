using UnityEngine;

[CreateAssetMenu]
public class StartState : State
{
    [SerializeField] private State _runState;
    public override void Init()
    {
    }
    
    public override void Do()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Player.SetState(_runState);
        }
    }
}