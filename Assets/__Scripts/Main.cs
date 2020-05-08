using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S; //a singleton for main

    [Header("Set In Inspector")]
    public GameObject[] prefabEnemies; //array of enemy prefabs 
    public float enemySpawnPerSecond = .5f; //# enemies/second
    public float enemyDefaultPadding = 1.5f; //padding for position
    public WeaponDefinition[] weaponDefinitions;

    private BoundsCheck bndCheck;

    void Awake()
    {
        S = this;
        //Set bndCheck to reference the bounds check componenet on this game object 
        bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    public void SpawnEnemy()
    {
        //pick a random enemy prefab to instiate 
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        //position the enemy above the screen with a random x pos
        float enemyPadding = enemyDefaultPadding;
        if(go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        //set the inital position for the spawned enemy 
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;

        //invoke spawnEnemy() again
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    public void DelayedRestart(float delay)
    {
        //invoke the restart() method in delay seconds 
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        //reload _Scene_0 to restart the game 
        SceneManager.LoadScene("_Scene_0");
    }
    
}
