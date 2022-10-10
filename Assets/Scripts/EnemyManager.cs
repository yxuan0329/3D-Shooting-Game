using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    // number of enemies on z
    public int numZ;

    // number of enemies on x
    public int numX;

    // number of layers
    public int numY;
    
    // separation
    public float separation;

    // enemy and coin prefab
    public GameObject enemyPrefab;
    public GameObject coinPrefab;
    public GameObject BigGummyPrefab;

    // number of enemies
    public int numEnemies;

    // in stage 2, the number of big Gummy
    public int numBigGummy;
    
    public void CreateEnemyWave()
    {
        // calculate number of enemies
        numEnemies = numZ * numX * numY - 1;

        Vector3 startPos = transform.position;

        int randX = Random.Range(0, numX);
        int randY = Random.Range(0, numY);
        int randZ = Random.Range(0, numZ);

        for (int k = 0; k < numY; k++)
        {
            for (int j = 0; j < numX; j++)
            {
                for (int i = 0; i < numZ; i++)
                {
                    if (i == randZ && j == randX && k == randY)
                    {
                        // generate coin
                        GameObject newcoin = Instantiate(coinPrefab);

                        // set enemy position
                        newcoin.transform.position = new Vector3(startPos.x + separation * randX, startPos.y + separation * randY, startPos.z + separation * randZ);
                    }
                    else
                    {
                        // spawn enemy
                        GameObject newEnemy = Instantiate(enemyPrefab);

                        // set enemy position
                        newEnemy.transform.position = new Vector3(startPos.x + separation * j, startPos.y + separation * k, startPos.z + separation * i);
                    }
                    
                }
            }
        }          
    }

    public void CreateBigGummy()
    {
        numBigGummy = 3;
        // calculate HP of Big Gummy
        

        Vector3 startPos = transform.position;

        int randX = Random.Range(0, numX);
        int randY = Random.Range(0, numY);
        int randZ = Random.Range(0, numZ);

        

        // set position
        for (int i = 0; i<numBigGummy; i++)
        {
            GameObject newBigGummy = Instantiate(BigGummyPrefab);
            newBigGummy.transform.position = new Vector3(startPos.x + separation * randX, startPos.y + separation * randY, startPos.z + separation * i * 2);
        }
        
    }

    public void KillAll()
    {
        //find all the enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //iterate through these enemies and destroy them
        for(int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }

        //find all the coins
        GameObject[] coinObject = GameObject.FindGameObjectsWithTag("coin");
        //iterate through these enemies and destroy them
        for (int i = 0; i < coinObject.Length; i++)
        {
            Destroy(coinObject[i]);
        }

        //find all the big gummy
        GameObject[] BigGummy = GameObject.FindGameObjectsWithTag("BigGummy");
        //iterate through these enemies and destroy them
        for (int i = 0; i < BigGummy.Length; i++)
        {
            Destroy(BigGummy[i]);
        }
    }
}
