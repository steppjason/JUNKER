using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ScoreBoard : MonoBehaviour
{
    //[SerializeField] TextMesh score;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI gameStartText;
    [SerializeField] int gameScore = 0;
    [SerializeField] int textCount = 2;
    [SerializeField] string[] displayText;
    
    

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreBoard();
        StartCoroutine(UpdateGameText());
        //gameStartText.text = "Test";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UpdateGameText(){
        for(int i = 0; i < textCount; i++){
            gameStartText.text = displayText[i];
            yield return new WaitForSeconds(2); 
        }
        gameStartText.text = "";
        yield return new WaitForSeconds(2);
    }

    public void AddScore(int points){
        gameScore += points;
        UpdateScoreBoard();
    }

    public void UpdateScoreBoard(){
        score.text = String.Format("{0:n0}",gameScore);
    }
}
