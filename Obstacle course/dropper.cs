using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class dropper : MonoBehaviour
{
    public Rigidbody rb;
    MeshRenderer mr;

    // Start is called before the ;first frame update
    void Start()
    {
        
        mr = GetComponent<MeshRenderer>();
        rb.useGravity = false;
        mr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 3){
            mr.enabled = true;
            rb.useGravity = true;


        }
    }

}
