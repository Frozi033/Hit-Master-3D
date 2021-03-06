using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private Camera _myCam;

    [SerializeField] private GameObject _tapPosWorld;
    [SerializeField] private List<GameObject> movePoints = new List<GameObject>();

    [SerializeField] private Gun _currentGun;

    [SerializeField] private State _startState;
    [SerializeField] private State _runState;
    [SerializeField] private State _attackState;
    
    private Animator _myAnimator;
    
    private NavMeshAgent _myNavMeshAgent;

    private Rigidbody _rb;
    
    private int _index;

    private float _speed;
    
    private static readonly int Speed = Animator.StringToHash("Speed");

    [Header("Actual State")]
    public State CurrentState;

    private void Start()
    {
        SetState(_startState);

        EnemiesCount.AllEnemiesDown += AllEnemiesDown;
        
        _myAnimator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _myNavMeshAgent = GetComponent<NavMeshAgent>();
        
        _index = 0;
    }

    private void Update()
    {
        CurrentState.Do();
    }
    
    public void SetState(State state)
    {
        CurrentState = Instantiate(state);
        CurrentState.Player = this;
        CurrentState.Init();
    }

    public void RunToPoint()
    {
        movePoints[_index].SetActive(false);
        
        _myNavMeshAgent.SetDestination(movePoints[_index].transform.position);
        
        _speed = (_myNavMeshAgent.destination - gameObject.transform.position).magnitude;
        SetAnimSpeed(_speed);
    }

    public bool DistanceToDestination()
    {
        if ((_myNavMeshAgent.destination - gameObject.transform.position).magnitude == 0)
        {
            _speed = 0;
            SetAnimSpeed(_speed);
            
            SetState(_attackState);
            
            _index++;

            return true;
        }

        return false;
    }

    public void Aim()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _myCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                _tapPosWorld.transform.position = hit.point;

                var direction = (hit.point - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = lookRotation;

                _currentGun.Shoot();
            }
        }
    }
    
    private void SetAnimSpeed(float speed)
    {
        _myAnimator.SetFloat(Speed, speed);
    }

    private void AllEnemiesDown()
    {
        SetState(_runState);
    }
}

