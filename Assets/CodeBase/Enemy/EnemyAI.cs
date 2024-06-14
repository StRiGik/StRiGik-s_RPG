using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SpaceAdventure.Utils;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private State _startingState;
    [SerializeField] private float _roamingDistanceMax = 7f;
    [SerializeField] private float _roamingDistanceMin = 3f;
    [SerializeField] private float _roamingTimerMax = 2f;

    private NavMeshAgent _navMeshAgent;
    private State _currentState;
    private float _roamingTime;
    private Vector3 _roamingPosition;
    private Vector3 _startPosition;

    private enum State
    {
        Roaming
    }
    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _currentState = _startingState;
    }

    private void Update()
    {
        switch(_currentState)
        {
            default:
            case State.Roaming:
                _roamingTime -= Time.deltaTime;
                if(_roamingTime <= 0)
                {
                    Roaming();
                    _roamingTime = _roamingTimerMax;
                }
                break;
        }
    }

    private void Roaming()
    {
        _startPosition = transform.position;
        _roamingPosition = GetRoamingPosition();
        _navMeshAgent.SetDestination(_roamingPosition);
    }

    private Vector3 GetRoamingPosition()
    {
        return _startPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(_roamingDistanceMin, _roamingDistanceMax);
    }

    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if(sourcePosition.x > targetPosition.x) { transform.rotation = Quaternion.Euler(0, -180, 0); }
        else { transform.rotation = Quaternion.Euler(0, 0, 0); }
    }
}
 