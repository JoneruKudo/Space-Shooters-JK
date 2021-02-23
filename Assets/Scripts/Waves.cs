using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class Waves : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] int numberOfSpawns = 5;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeOfEachSpawn = 0.3f;
    [SerializeField] float spawnRandomFactor = 0.3f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public List<Transform> GetWaypoints() 
    {
        var waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waypoints.Add(child);
        }
        return waypoints; 
    }
    public int GetNumberOfSpawns() { return numberOfSpawns; }
    public float GetMoveSpeed() { return moveSpeed; }
    public float GetTimeOfEachSpawn() { return timeOfEachSpawn; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }

}
