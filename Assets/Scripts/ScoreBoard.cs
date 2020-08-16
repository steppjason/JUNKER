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
    [SerializeField] bool gameStart = false;
    [SerializeField] GameObject controlsSprite;
    [SerializeField] TextMeshProUGUI controlsText;
    
    public bool doOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStart && doOnce){
            UpdateScoreBoard();
            StartCoroutine(UpdateGameText());
            doOnce = false;   
        }

        if(Input.GetButtonDown("Submit") && !gameStart){
            gameStart = true;
            Destroy(controlsSprite);
            Destroy(controlsText);
        }
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

    public bool IsGameStart(){
        return gameStart;
    }
}
