using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // movement range
    public float rangeH = 5;
    public float rangeV = 1;
   
    // speed
    public float speed = 2;

    // direction
    int direction = 1;

    // accumulated movement
    float accMovement = 0;

     // available states
    enum State { MovingHorizontally, MovingVertically, Dead };
    
    // keep track of the current state
    State currState;

    // Game Manager
    GameManager gm;

    // Enemy Manager
    EnemyManager em;

    // slow down degree
    public float degree = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // initial state
        currState = State.MovingHorizontally;

        // game manager
        gm = GameObject.FindObjectOfType<GameManager>();
        gm.slow = false;

        // log error if it wasn't found
        if (gm == null)
        {
            Debug.LogError("there needs to be an GameManager in the scene");
        }

        // enemy manager
        em = GameObject.FindObjectOfType<EnemyManager>();

        // log error if it wasn't found
        if (em == null)
        {
            Debug.LogError("there needs to be an EnemyManager in the scene");
        }
    }

    // Update is called once per frame
    public void Update()
    {
        // nothing happens if the enemy is dead
        if (currState == State.Dead) return;

        // calculate movement  v = d / t --> d = v * t
        if (gm.slow == true)
        {
            speed = 0.6f;
            Debug.Log("update speed = 0.6");
        }

        float movement = speed * Time.deltaTime * degree;
        

        // update accumulate movement
        accMovement += movement;


        // are we moving horizontally?
        if (currState == State.MovingHorizontally)
        {
            // if yes, then transition to moving vertically
            if(accMovement >= rangeH)
            {
                // transition to moving vertically
                currState = State.MovingVertically;

                // reverse direction (for horizontal movement)
                direction *= -1;

                // reset acc movement
                accMovement = 0;
            }
            // if not, move the invader horizontally
            else
            {
                transform.position += transform.forward * movement * direction;
            }
        }
        // this is, if we are moving vertically
        else if (currState == State.MovingVertically)
        {
            // if yes, then transition to moving horizontally
            if (accMovement >= rangeV)
            {
                // transition to moving horiz
                currState = State.MovingHorizontally;

                // reset acc movement
                accMovement = 0;
            }
            // if not, move the invader vertically
            else
            {
                transform.position += Vector3.down * movement;
            }
        }
    }

    public void KillEnemy()
    {
        // nothing will happen if already dead
        if (currState == State.Dead) return;

        // set the state to dead
        currState = State.Dead;

        //[implement your own effect here]

        //[Example]
        Destroy(gameObject);
        //[End of Example]

        // decrease number of enemies
        em.numEnemies--;
        
        // check winning condition
        gm.HandleEnemyDead();
    }

    public void KillCoin()
    {
        // nothing will happen if already dead
        if (currState == State.Dead) return;

        // set the state to dead
        currState = State.Dead;

        //[implement your own effect here]

        //[Example]
        Destroy(gameObject);
        gm.slow = true; // slow down the gummy
        //[End of Example]

        // decrease number of enemies //coin does not belong to gummy
        // em.numEnemies--;

        // check winning condition
        gm.HandleEnemyDead();
    }

    /*public void KillBigGummy()
    {
        // nothing will happen if already dead
        if (currState == State.Dead) return;

        em.HPLeft--;

        // set the state to dead if count = 30
        if ( em.HPLeft == 0)
        {
            currState = State.Dead;
            Destroy(gameObject);
        }
        

        //[implement your own effect here]

        //[Example]
        
        //[End of Example]

        // decrease number of enemies
        // em.numEnemies--;

        // check winning condition
        gm.HandleBigGummyDead();
    }*/

    void OnTriggerEnter(Collider other)
    {
        // nothing will happen if already dead
        if (currState == State.Dead) return;

        //check if the enemy hit the player
        if (other.CompareTag("Player Body"))
        {
            gm.GameOver();
        }

        //check if the enemy reached the floor
        else if (other.CompareTag("Ground"))
        {
            gm.GameOver();
        }
    }
}
