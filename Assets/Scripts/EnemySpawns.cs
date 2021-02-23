using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawns : MonoBehaviour
{
    [SerializeField] List<Waves> waveConfig;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }

    private IEnumerator SpawnAllWaves()
    {
        while (looping)
        {
            for (int waveIndex = 0; waveIndex < waveConfig.Count; waveIndex++)
            {
                Waves currentWave = waveConfig[waveIndex];
                yield return StartCoroutine(SpawnEnemy(currentWave));
            }
        }
    }

    private IEnumerator SpawnEnemy(Waves waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfSpawns(); enemyCount++)
        {
        Instantiate(
            waveConfig.GetEnemyPrefab(),
            waveConfig.GetWaypoints()[0].position,
            Quaternion.identity
            );
        yield return new WaitForSeconds(waveConfig.GetTimeOfEachSpawn());
        }
    }

}
