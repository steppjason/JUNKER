using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    [SerializeField] bool firstWave = true;

    [SerializeField] ScoreBoard scoreBoard;

    public Random rnd = new Random();

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do{
            yield return StartCoroutine(SpawnAllWaves());
        }
        while(looping);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnAllWaves(){
        if(scoreBoard.IsGameStart()){
            if(firstWave){
                int i = Random.Range(1, waveConfigs.Count - 1);
                var currentWave = waveConfigs[i];
                firstWave = false;
                yield return new WaitForSeconds(6);
                yield return SpawnAllEnemies(waveConfigs[i]);
            } else {
                int i = Random.Range(0, waveConfigs.Count - 1);
                var currentWave = waveConfigs[i];
                yield return new WaitForSeconds(waveConfigs[i].GetTimeBeforeWave());
                yield return SpawnAllEnemies(waveConfigs[i]);

            }
        }

        // for(int i = startingWave; i < waveConfigs.Count; i++){
        //     var currentWave = waveConfigs[i];
        //     yield return new WaitForSeconds(waveConfigs[i].GetTimeBeforeWave());
        //     yield return SpawnAllEnemies(waveConfigs[i]);
        // }
    }

    private IEnumerator SpawnAllEnemies(WaveConfig waveConfig){
        
        for(int i = 0; i < waveConfig.GetNumberOfEnemies(); i++){
            
            PathCreator pathCreator = waveConfig.GetPath();
            
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
                pathCreator.path.GetPoint(0),
                Quaternion.identity);
    
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
        
    }
}
