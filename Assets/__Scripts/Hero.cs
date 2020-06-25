using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    static public Hero S; //Singleton 

    [Header("Set in Inspector}")]
    //these field control the movement of the ship
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1; //this variale holds a reference to the last triggering gameobject

    //this varaible holds a reference to the last triggering gameobject 
    private GameObject lastTriggerGo = null;

    private void Awake()
    {
        if (S == null)
        {
            S = this; //Set the singleton
        } else {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }
    }
    private void Update()
    {
        //Pull in information from the Input class
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        //change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        //rotate the ship to make it feel more dynamic 
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        //allow the ship to fire 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TempFire();
        }
    }

    void TempFire()
    {
        GameObject projGo = Instantiate<GameObject>(projectilePrefab);
        projGo.transform.position = transform.position;
        Rigidbody rigidB = projGo.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.up * projectileSpeed;

    }
    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //print("Triggered" + go.name);

        //make sure its not the same triggering go as last time 
        if(go == lastTriggerGo){
            return;
        }
        lastTriggerGo = go;

        if(go.tag == "Enemy") { //if the shield was triggered by an enemy 
            shieldLevel--; //decrease the level of the shield by 1
            Destroy(go); //and destroy the enemy 

        }
        else
        {
            print("Triggered by non-Enemy: " + go.name);
        }
    }

    public float shieldLevel
    {
        get {
            return (_shieldLevel);
        }
        set { _shieldLevel = Mathf.Min(value, 4);
        //if the shield is going to be set to less than zero
        if (value < 0)
            {
                Destroy(this.gameObject);
                //tell Main.S to restart the game after the delay
                LoadNextScene();
              
            }
        }
    }

    public void LoadNextScene()
    {
      
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}


