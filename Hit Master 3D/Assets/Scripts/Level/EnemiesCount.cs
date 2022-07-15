using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemiesCount : MonoBehaviour
{
    [SerializeField] private List<Stage> _stage = new List<Stage>();

    private int _enemiesLeft;
    private int id;

    public static Action AllEnemiesDown;

    private void Start()
    {
        id = 0;
        Enemy.EnemyDead += EnemyMinus;

        _enemiesLeft = _stage[id].Count;
    }

    private void OnDisable()
    {
        Enemy.EnemyDead -= EnemyMinus;
    }

    private void NextStage()
    {
        if (id == _stage.Count - 1)
        {
             SceneManager.LoadScene(0);
        }
        else
        {
            _enemiesLeft = _stage[id++].Count;
        }
    }

    private void EnemyMinus()
    {
        _enemiesLeft--;
        
        if (_enemiesLeft <= 0)
        {
            NextStage();
            
            AllEnemiesDown?.Invoke();
        }
    }
}
