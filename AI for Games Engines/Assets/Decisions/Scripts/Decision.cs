using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : MonoBehaviour
{

    public int age;

    //Needs stats
    public int thirst;
    public int hunger;
    public int toilet;
    public int money;

    //Ride Stats
    public int excitement;
    public int fear;
    public int nausia;
    public int fun;

    //What it will do next
    public string next;


    void Start()
    {
        //Generates the stats for the currently spawned peep.
        GeneratePeep();
    }

    void Update()
    {
        //Runs the decision state machine to decide what to do next.
        Decisions();
        //Updates the needs on a random choice to make them go down
        Needs();

        //test movement for collision with buildings and needs before Paul's ai movement has been added.
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //gameObject.transform.Translate(moveHorizontal * 0.05f, moveVertical * 0.25f, 0.0f);
    }

    //generates the stats randomly for each peep
    void GeneratePeep()
    {
        age = Random.Range(10, 100);
        thirst = Random.Range(10, 100);
        hunger = Random.Range(10, 100);
        toilet = Random.Range(10, 100);
        excitement = Random.Range(10, 100);
        fear = Random.Range(10, 100);
        money = Random.Range(10, 30) * age;
    }

    void Decisions()
    {
        //Needs decisions tree
        if (money >= 30)
        {
            if (hunger < 30 || thirst < 30 || toilet < 30)
            {
                if (toilet <= 30)
                {
                    next = "Toilet";
                }
                if (thirst < toilet)
                {
                    next = "Drink";
                }
                if (hunger < thirst && hunger < toilet)
                {
                    next = "Eat";
                }
            }
            else
            {
                //if needs are fufilled will decide what ride it wants to ride based on the stats
                if (excitement > 50 && age > 18)
                {
                    if (fear > 50)
                    {
                        next = "Fast";
                    }
                    else
                    {
                        next = "Medium";
                    }
                }
                if (excitement < 50 && fear > 50)
                {
                    next = "Scary";
                }
                if (excitement < 50 && fear < 50)
                {
                    next = "Slow";
                }
            }
        }
        //will use atm if has no money
        else next = "ATM";
    }

    //Functions to be called by buildings to increase stats
    public void Drink()
    {
        thirst = 100;
    }
    public void Eat()
    {
        hunger = 100;
    }
    public void Toilet()
    {
        toilet = 100;
    }

    //randomly ticks needs down to give the appearance of the ai becoming more hungry over time etc
    void Needs()
    {
        int rando = Random.Range(1, 1000);
        if (rando == 500)
        {
            hunger = hunger - 2;
            thirst = thirst - 2;
            toilet = toilet - 2;
        }
    }

    //When collides with buildings with tag will take money and increase stats.
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Drink")
        {
            thirst = 100;
            money -= 10;
            print("drink");
        }
        if (col.gameObject.tag == "Food")
        {
            hunger = 100;
            money -= 20;
            print("food");
        }
        if (col.gameObject.tag == "Toilet")
        {
            toilet = 100;
            print("toilet");
        }
        if (col.gameObject.tag == "ATM")
        {
            money += Random.Range(100, 500);
            print("cash cash");
        }
    }
}
