using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(SpawnAllWaves());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnAllWaves(){
        for(int i = startingWave; i < waveConfigs.Count; i++){
            var currentWave = waveConfigs[i];
            yield return new WaitForSeconds(waveConfigs[i].GetTimeBeforeWave());
            yield return SpawnAllEnemies(waveConfigs[i]);
        }
    }

    private IEnumerator SpawnAllEnemies(WaveConfig waveConfig){
        
        for(int i = 0; i < waveConfig.GetNumberOfEnemies(); i++){
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
                waveConfig.GetWayPoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
        
    }
}
