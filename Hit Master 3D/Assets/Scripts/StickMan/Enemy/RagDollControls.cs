using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider))]
public class RagDollControls : MonoBehaviour
{
    private Animator _animator;

   private BoxCollider _myCollider;
    
    private Rigidbody[] _rigidbodies;
    
    private Collider[] _colliders;

    public bool on;
    private void Start()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _colliders = GetComponentsInChildren<Collider>();
        _animator = GetComponent<Animator>();
        _myCollider = GetComponent<BoxCollider>();
        
        SetRagdoll(false);
    }

    private void Update()
    {
        if (on)
        {
            SetRagdoll(true);
            on = false;
        }
    }

    public void SetRagdoll(bool enabled)
    {
        _animator.enabled = !enabled;
        
        SetRigidBodies(enabled, _rigidbodies);
        SetColliders(enabled, _colliders);

        _myCollider.enabled = !enabled;
    }

    private void SetRigidBodies(bool enebled, Rigidbody[] rigidbodies)
    {
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = !enebled;
            rigidbody.useGravity = enebled;
        }
    }

    private void SetColliders(bool enebled, Collider[] colliders)
    {
        foreach (var collider in colliders)
        {
            collider.isTrigger = !enebled;
        }
    }
}
