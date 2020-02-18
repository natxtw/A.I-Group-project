using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drinks : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Peep")
        {
            print("drink");
        }
    }
}
