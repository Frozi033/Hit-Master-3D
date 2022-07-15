using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PoolObject))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _timeToLife;
    [SerializeField] private float _bulletDamage = 25f;
    
    private PoolObject _poolObject;

    private void Start()
    {
        _poolObject = GetComponent<PoolObject>();
    }

    private void OnEnable()
    {
        StartCoroutine(Destroy());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            _poolObject.ReturnToPool();

            health.TakeDamage(_bulletDamage);
        }
        else
        {
            _poolObject.ReturnToPool();
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_timeToLife);
        _poolObject.ReturnToPool();
    }
}
