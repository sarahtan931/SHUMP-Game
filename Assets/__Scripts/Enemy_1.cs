using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enemy_1 extends the enemy class
public class Enemy_1 : Enemy
{
    [Header("Set in Inspector: Enemy_1")]
    //# seconds for a full sine wave 
    public float waveFrequency = 2;
    //sine wave width in meters 
    public float waveWidth = 4; 
    public float waveRoty = 45;

    private float x0; //initial x value of pos
    private float birthTime;

    //start works well becasue its not used by the enemy superclass
    private void Start()
    {
        //set x0 to the initial x position of Enemy_1
        x0 = pos.x;

        birthTime = Time.time;
    }

    //override the move function on enemy 
    public override void Move()
    {
        
            //becasue pos is a property you cant diretly set pos.x
            //so get the pos as an editable vector3
            Vector3 tempPos = pos;
            //theta adjusts based on time 
            float age = Time.time - birthTime;
            float theta = Mathf.PI * 2 * age / waveFrequency;
            float sin = Mathf.Sin(theta);
            tempPos.x = x0 + waveWidth * sin;
            pos = tempPos;

            //rotate a bit about y 
            Vector3 rot = new Vector3(0, sin * waveRoty, 0);
            this.transform.rotation = Quaternion.Euler(rot);

            //base.move() still handles the movement down in y
            base.Move();

           
        }
    }
