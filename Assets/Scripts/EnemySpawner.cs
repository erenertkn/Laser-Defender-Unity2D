using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool looping = true;
    int startingWave = 0;
    

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemies(WaveConfig wave)
    {
        for(int i=0; i<wave.GetNumberOfEnemies(); i++)
        {
            var insObj = Instantiate(wave.GetEnemyPrefab(), wave.GetWayPoints()[0].position, Quaternion.identity);
            insObj.GetComponent<EnemyPathing>().SetWaveConfig(wave);
            yield return new WaitForSeconds(wave.GetTimeBetweenSpawns());
        }
        
    }
    private IEnumerator SpawnAllWaves()
    {
        for(int currentWave=startingWave; currentWave<waveConfigs.Count; currentWave++)
        {
            yield return SpawnEnemies(waveConfigs[currentWave]);
        }
        
    }
}
