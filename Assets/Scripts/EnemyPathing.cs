using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> wayPoints;
    int wayPointIndex = 0;
    float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {   
        moveSpeed = waveConfig.GetMoveSpeed();
        wayPoints = waveConfig.GetWayPoints();
        transform.position = wayPoints[wayPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    public void SetWaveConfig(WaveConfig waveConfig){
        this.waveConfig = waveConfig;
    }

    private void MoveEnemy(){
        
        if(wayPointIndex <= wayPoints.Count - 1){
            var targetPosition = wayPoints[wayPointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            Debug.Log(wayPointIndex);
            Debug.Log(wayPoints.Count);

            Debug.Log(transform.position);
            Debug.Log(targetPosition);

            if(transform.position == targetPosition){
                wayPointIndex++;
            }
        } else {
            Destroy (gameObject);
        }
    }
}
