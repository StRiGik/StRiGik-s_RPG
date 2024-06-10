using UnityEngine;

public abstract class Folower : MonoBehaviour
{
    [SerializeField] private Transform _targetTransfotm;
    [SerializeField] private Vector3 _offcet;
    [SerializeField] private float _smoothing;

    protected void Move(float deltaTime)
    {
        if(_targetTransfotm != null)
        {
            Vector3 nextPosition = Vector3.Lerp(transform.position, _targetTransfotm.position + _offcet, deltaTime * _smoothing);
            transform.position = nextPosition;
        }
        
        
    }
}
