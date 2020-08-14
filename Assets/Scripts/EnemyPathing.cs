using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> wayPoints;
    int wayPointIndex = 0;
    float moveSpeed;
    float movementThisFrame;

    PathCreator pathCreator;
    EndOfPathInstruction end;

    // Start is called before the first frame update
    void Start()
    {   
        moveSpeed = waveConfig.GetMoveSpeed();
        //wayPoints = waveConfig.GetWayPoints();
        pathCreator = waveConfig.GetPath();
        transform.position = pathCreator.path.GetPoint(0);
        end = EndOfPathInstruction.Stop;
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
        
        movementThisFrame += moveSpeed * Time.deltaTime;
        
        transform.position = pathCreator.path.GetPointAtDistance(movementThisFrame, end);

        
        
        if(pathCreator.path.GetClosestTimeOnPath(transform.position) == 1){
            Destroy(gameObject);
        }


        /*if(wayPointIndex <= wayPoints.Count - 1){
            var targetPosition = wayPoints[wayPointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if(transform.position == targetPosition){
                wayPointIndex++;
            }
        } else {
            Destroy (gameObject);
        }*/

    }
}
