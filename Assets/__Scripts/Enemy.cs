using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f; //speed in m/s
    public float fireRate = .3f; //seconds/shot
    public float health = 10;
    public int score = 100; //points earned for destroying this 

    protected BoundsCheck bndCheck;

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    //property, method that acts like a field 
    public Vector3 pos {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

   void Update()
    {
        Move();

        if (bndCheck != null && bndCheck.offDown)
        {
            //we are off the bottom so destroy this game object
            Destroy(gameObject);
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    private void OnCollisionEnter(Collision coll)
    {
        GameObject otherGo = coll.gameObject;
        if(otherGo.tag == "ProjectileHero")
        {
            Destroy(otherGo); //destroy the projectile 
            Destroy(gameObject); //destroy this enemy game object 
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGo.name);
        }
    }
}
