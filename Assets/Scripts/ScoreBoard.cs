using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


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

    public bool playerDead = false;
    
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
            controlsText.text = "";
        } else if(!gameStart && Input.GetButtonDown("Cancel")){
            Application.Quit();
        }

        if(playerDead && Input.GetButtonDown("Jump")){
            SceneManager.LoadScene(0);
            
        } else if(playerDead && Input.GetButtonDown("Cancel")){
            Application.Quit();
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

    public void IsPlayerDead(bool dead){
        playerDead = dead;
    }
}
