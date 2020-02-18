using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : MonoBehaviour
{

    public int age;

    public int thirst;
    public int hunger;
    public int toilet;

    public int excitement;
    public int fear;
    public int nausia;

    public string next;


    // Start is called before the first frame update
    void Start()
    {
        GeneratePeep();
    }

    // Update is called once per frame
    void Update()
    {
        Decisions();
        Needs();
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        gameObject.transform.Translate(moveHorizontal * 0.05f, moveVertical * 0.25f, 0.0f);
    }

    void GeneratePeep()
    {
        age = Random.Range(10, 100);
        thirst = Random.Range(10, 100);
        hunger = Random.Range(10, 100);
        toilet = Random.Range(10, 100);
        excitement = Random.Range(10, 100);
        fear = Random.Range(10, 100);
    }

    void Decisions()
    {
        //Needs
        if(hunger < 30 || thirst < 30 || toilet < 30)
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
        } else
        {
            if(excitement > 50 && age > 18)
            {
                if(fear > 50)
                {
                    next = "Fast";
                }
                else
                {
                    next = "Medium";
                }
            }
            if(excitement < 50 && fear > 50)
            {
                next = "Scary";
            }
            if(excitement < 50 && fear < 50)
            {
                next = "Slow";
            }
        }
    }

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
}
