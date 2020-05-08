using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is an enum of the various possible weapon types 
/// It also includes "Shield" type to allow a shield power-up 
/// </summary>

public enum WeaponType
{
    none, //default/ no weapen 
    blaster, //simple blaser
    spread, //two shots 
    phaser, //shots that move in waves 
    missile, //homing missiles 
    laser, //damage over time 
    shield //raise shieldlevel
}

/// <summary>
/// The weaponDefinition class allows you to set the properties of a sepcific weapon in the inspector. 
/// The main class has an array of weaponDefinitions that makes this possible 
/// </summary>
/// 
[System.Serializable]
public class WeaponDefinition
{
    public WeaponType type = WeaponType.none;
    public string letter; //letter to show on the power up 
    public Color color = Color.white; //color of collar and power up 
    public GameObject projectilePrefab; //prefab for projectiles 
    public Color projectileColor = Color.white;
    public float damageOnHit = 0; //amount of damage caused 
    public float continousDamage = 0; //damage per second 
    public float delayBetweenShots = 0;
    public float velocity = 20; //speed of projectiles 

}
public class Weapon : MonoBehaviour
{
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
