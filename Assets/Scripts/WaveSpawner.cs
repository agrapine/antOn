using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
  private readonly float _minEnemyDistance = 0.3f;
  private float _countdown = 5f;

  private int _spawners;
  private int _waveIndex;

  public Transform EnemyPrefab;
  public float PauseTime = 5f;
  public Transform SpawnPoint;
  public Text UiCountdown;

  private void Update()
  {
    UpdateCountdown();
    
    if (_countdown < 0f)
      _countdown = 0;

    if (Math.Abs(_countdown) < Mathf.Epsilon)
    {
      _countdown = PauseTime;
      StartCoroutine(SpawnWave());
    }
    else
    {
      _countdown -= Time.deltaTime;
    }
  }

  private void UpdateCountdown()
  {
    UiCountdown.text = Mathf.Floor(_countdown).ToString("F0");
  }

  private IEnumerator SpawnWave()
  {
    _spawners++;
    _waveIndex++;
    var maxEnemies = (int) (PauseTime / _minEnemyDistance);

    var numOfEnemies = Math.Min(_waveIndex, maxEnemies);
    for (var i = 0; i < numOfEnemies; i++)
    {
      SpawnEnemy();
      yield return new WaitForSeconds(_minEnemyDistance);
    }

    _spawners--;
    yield return new WaitForSeconds(Mathf.Epsilon);
  }

  private void SpawnEnemy()
  {
    Instantiate(EnemyPrefab, SpawnPoint.position, SpawnPoint.rotation);
  }
}