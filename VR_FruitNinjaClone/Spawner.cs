using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour

{   //you can use the same script from beat saber , just this time , the velocity of the fruits flying to you will be different
    public float velocityIntensity; //how strong the prefab flies to you
    public GameObject[] prefabs; //prefabs we spawn
    public Transform[] spawnPoints; // places to spawn the object
    public float spawnTimer = 2; 
    public float timer; //time that passed
    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnTimer){ // if timer passes 2 second , its time to instatiate a game object
            Transform randomPoint = spawnPoints[Random.Range(0,spawnPoints.Length)]; //random points with transform
            //above code will return random element among the spawn point array
            GameObject randomPrefab = prefabs[Random.Range(0,prefabs.Length)]; // we can do the same but for prefabs
            GameObject spawnedPrefab = Instantiate(randomPrefab,randomPoint.position,randomPoint.rotation);
            //spawn a prefab
            timer -= spawnTimer; //reset the timer
            Rigidbody rb = spawnedPrefab.GetComponent<Rigidbody>();
            rb.velocity =  randomPoint.forward*velocityIntensity; //;aunch the prefab forward with certain velocity intensity

        }
    }
}
