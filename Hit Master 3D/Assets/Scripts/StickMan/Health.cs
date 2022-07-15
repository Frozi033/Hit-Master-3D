using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHP;

    private float _currentHp;

    public static Action UIChangedHPHandle;
    private void Awake()
    {
        _currentHp = _maxHP;
    }

    public float health
    {
        get => _currentHp;
        set => _currentHp = value;
    }

    public void TakeDamage(float damage)
    {
        _currentHp -= damage;
        
        HealthChanged();
    }
    
    public float ConvertHP()
    {
        return _currentHp / _maxHP;
    }
    
    private void HealthChanged()
    {
        _currentHp = Mathf.Clamp(_currentHp, 0f, _maxHP);
        
        UIChangedHPHandle?.Invoke();
    }
}
