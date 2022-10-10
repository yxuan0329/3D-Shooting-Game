using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // graffiti
    public Text uiText;

    //states
    enum State { NotStarted, PlayingG1, PlayingG2, GameOver, WonGame1, WonGame2 }

    // current state
    State currState;

    // Enemy Manager
    EnemyManager enemyManager;

    int totalEnemyNum = 17; // total number of enemy

    public bool slow = false; // whether the speed is slower or not

    public bool wonGame1 = false; 

    public GameObject winnerSound1;
    public GameObject winnerSound2;
    public GameObject loserSound;

    // Start is called before the first frame update
    void Start()
    {
        // start as not playing
        currState = State.NotStarted;

        // refresh UI
        RefreshUI();

        // find the enemy manager
        enemyManager = GameObject.FindObjectOfType<EnemyManager>();


        // log error if it wasn't found
        if (enemyManager == null)
        {
            Debug.LogError("there needs to be an EnemyManager in the scene");
        }
    }

    void RefreshUI()
    {
        // act according to the state
        switch(currState)
        {
            case State.NotStarted:
                uiText.text = "Shoot here to begin";
                break;

            case State.PlayingG1:
                uiText.text = "Gummies shooted: " + (totalEnemyNum - enemyManager.numEnemies);
                break;
            case State.PlayingG2:
                uiText.text = "Defeat big gummies after shooting 5 times!";
                break;

            case State.GameOver:
                uiText.text = "Game Over! \nShoot to restart";
                GameObject newloserSound = Instantiate(loserSound);
                break;

            case State.WonGame1:
                wonGame1 = true;
                uiText.text = "YOU WON! \nShoot to start stage 2";
                GameObject newWinnerSound1 = Instantiate(winnerSound1);
                break;
            case State.WonGame2:
                uiText.text = "YOU WON! \nShoot to restart";
                GameObject newWinnerSound2 = Instantiate(winnerSound2);
                wonGame1 = false;
                break;
        }  
    }

    public void InitGame()
    {
        //don't initiate the game if the game is already running!
        if (currState == State.PlayingG1 || currState == State.PlayingG2) return;

        // set the state
        if (wonGame1 == false)
        {
            currState = State.PlayingG1;
            enemyManager.CreateEnemyWave();
        }
        else // wonGame1 == true
        {
            currState = State.PlayingG2;
            enemyManager.CreateBigGummy();
        }

        // show text on the graffiti
        RefreshUI();
    }

    // game over
    public void GameOver()
    {
        // do nothing if we were already on game over
        if (currState == State.GameOver) return;

        // set the state to game over
        currState = State.GameOver;

        // show text on the graffiti
        RefreshUI();

        // remove all enemies
        enemyManager.KillAll();
    }

    // In stage 1, checks whether we've won, and if we did win, refresh UI
    public void HandleEnemyDead()
    {
        if (currState != State.PlayingG1) return;

        RefreshUI();

        // have we won the game?
        if(enemyManager.numEnemies <= 0)
        {
            // set the state of the game
            currState = State.WonGame1;
            wonGame1 = true;

            // show text on the graffiti
            RefreshUI();

            // remove all enemies
            enemyManager.KillAll();
        }
    }

    // In stage 2, checks whether we've won, and if we did win, refresh UI
    public void HandleBigGummyDead()
    {
        if (currState != State.PlayingG2) return;

        RefreshUI();

        // have we won the game?
        if (enemyManager.numBigGummy <= 0)
        {
            // set the state of the game
            currState = State.WonGame2;

            // show text on the graffiti
            RefreshUI();

            // remove all enemies
            enemyManager.KillAll();
        }
    }
}
