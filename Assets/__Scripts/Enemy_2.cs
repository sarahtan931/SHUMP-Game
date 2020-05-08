using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    [Header("Set in Inspector: Enemy_2")]
    //determines how much the since wave will affect movement 
    public float sinEccentricity = .6f;
    public float lifeTime = 10;

    [Header("Set Dynamically: Enemy_2")]
    //enemy _2 uses a sin wave to modify a 2-point linear interpolation 
    public Vector3 p0;
    public Vector3 p1;
    public float birthTime;

    private void Start()
    {
        p0 = Vector3.zero;
        p0.x = -bndCheck.camWidth - bndCheck.radius;
        p1.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

        //possibly swap sides 
        if(Random.value > .5f)
        {
            //setting the .x of each point to its negative will move it to the other side of the screen 
            p0.x *= -1;
            p1.x *= -1;
        }

        //set the birtTime to the current time 
        birthTime = Time.time;
    }

    public override void Move()
    {
        //curves work based on a u value between 0 & 1 
        float u = (Time.time - birthTime) / lifeTime;

        //if u>1 then it has been longer than lifeTime since birthTime 
        if (u > 1)
        {
            //this enemy_2 has finishes its life 
            Destroy(this.gameObject);
            return;
        }

        //adjust u by adding a u cruve based on a sine wave 
        u = u + sinEccentricity * (Mathf.Sin(u * Mathf.PI * 2));

        //interpolate the two linear interpolation points 
        pos = (1 - u) * p0 + u * p1;
    }

}
