using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : Enemy
{
    //Enemy_3 extends enemy
    //enemy_3 will move folliwing a bezier curve, which is a linear interpolation between more than 2 points  
    [Header("Set in Inspector: Enemy_3")]
    public float lifeTime = 5;

    [Header("Set Dynamically: Enemy_3")]
    public Vector3[] points;
    public float birthTime;

    //start works well becasue it is not used by the enemy superclass 
    void Start()
    {
        points = new Vector3[3]; //initialize points 

        //the start position has already been set by Main.spawnenemy 
        points[0] = pos;

        //set xmin and xmax the same way that main.spawnEnemy() does 
        float xMin = -bndCheck.camWidth + bndCheck.radius;
        float xMax = bndCheck.camWidth - bndCheck.radius;

        Vector3 v;
        //pick a random middle position in the bottom half of the screen 
        v = Vector3.zero;
        v.x = Random.Range(xMin, xMax);
        v.y = -bndCheck.camHeight * Random.Range(2.75f, 2);
        points[1] = v;

        //pick a random final position above the top of the screen 
        v = Vector3.zero;
        v.y = pos.y;
        v.x = Random.Range(xMin, xMax);
        points[2] = v;

        //set the birth time to the current time 

        birthTime = Time.time;

    }

    public override void Move()
    {
        //bezier curves work based on a u value between 0 and 1 
        float u = (Time.time - birthTime) / lifeTime;

        if (u > 1)
        {
            //this Enemy_3 has finished its life 
            Destroy(this.gameObject);
            return;
        }

        //interpolate the three bezier curve points 
        Vector3 p01, p12;
        u = u - .2f * Mathf.Sin(u * Mathf.PI * 2);
        p01 = (1 - u) * points[0] + u * points[1];
        p12 = (1 - u) * points[1] + u * points[2];
        pos = (1 - u) * p01 + u * p12;
    }
}
