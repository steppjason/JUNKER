using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[CreateAssetMenu(menuName = "Wave Config")]
public class WaveConfig : ScriptableObject
{
   [SerializeField] GameObject enemyPrefab;
   //[SerializeField] GameObject pathPrefab;
   [SerializeField] PathCreator pathCreator;

   [SerializeField] float timeBetweenSpawns = 0.5f;
   [SerializeField] float spawnRandomFactor = 0f;
   [SerializeField] int numberOfEnemies = 10;
   [SerializeField] float moveSpeed = 2f;
   [SerializeField] float timeBeforeWave = 1f;

   public GameObject GetEnemyPrefab(){ return enemyPrefab; }

   /* public List<Transform> GetWayPoints(){ 
        var wayPoints = new List<Transform>();

        foreach(Transform child in pathPrefab.transform){
            wayPoints.Add(child);
        }

        return wayPoints; 
    }    */

   public float GetTimeBetweenSpawns(){ return timeBetweenSpawns; }
   public float GetTimeBeforeWave(){ return timeBeforeWave; }
   public float GetSpawnRandomFactor(){ return spawnRandomFactor; }
   public int GetNumberOfEnemies(){ return numberOfEnemies; }
   public float GetMoveSpeed(){ return moveSpeed; }
   public PathCreator GetPath(){ return pathCreator; }
}
