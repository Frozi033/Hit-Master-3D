using UnityEngine;

[RequireComponent(typeof(Pool))]
public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _barrel;

    [SerializeField] private float _fireRate;
    [SerializeField] private float _bulletForce = 10f;
    
    private float _curTimeOut;

    private Pool _pool;


    private void Start()
    {
        _pool = GetComponent<Pool>();
    }

    private void Update()
    {
        _curTimeOut += Time.deltaTime;
    }
    public void Shoot()
    {
        if (_curTimeOut > _fireRate)
        {
            _curTimeOut = 0;
            
            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        Vector3 SpawnPoint = _barrel.position;

        Quaternion SpawnRot = _barrel.rotation;

        PoolObject Fire = _pool.GetFreeElement(SpawnPoint, SpawnRot);

        Rigidbody BulReg = Fire.GetComponent<Rigidbody>();
        BulReg.velocity = Vector3.zero;
        BulReg.AddForce(BulReg.transform.forward * _bulletForce, ForceMode.Impulse);
    }
    
}