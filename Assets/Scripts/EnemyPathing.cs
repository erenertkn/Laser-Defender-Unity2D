using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> wayPoints;
    int targetPoint=0;
    // Start is called before the first frame update
    void Start()
    {
        wayPoints = waveConfig.GetWayPoints();
        transform.position = wayPoints[targetPoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
    }

    private void Move()
    {
        if (targetPoint <= wayPoints.Count - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[targetPoint].transform.position, waveConfig.GetMoveSpeed() * Time.deltaTime);
            if (transform.position == wayPoints[targetPoint].transform.position)
            {
                targetPoint++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
}