using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class ObjectSlider : MonoBehaviour
{   
    public float slicedObjectInitialVelocity = 100f; //adds force after you cut them instead of them flying at your face 
    public Material slicedMaterial; // when cut , the inside of the fruit should have some color
    public Transform startSlicingPoint; // start of the sword
    public Transform endSlicingPoint; // end of the sword
    public LayerMask sliceableLayer; //adds a new layer
    public VelocityEstimator velocityEstimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() //use raycast here in case to always check is something is colliding or not
    {
        RaycastHit hit; //Raycast is a data type wow
        Vector3 slicingDirection = endSlicingPoint.position - startSlicingPoint.position;// this will create a vector from the start to end point 
        bool hasHit = Physics.Raycast(startSlicingPoint.position,slicingDirection,out hit, slicingDirection.magnitude, sliceableLayer ); //check if anything
        // has been hit or not during the start ,
        //1st parameter : first starting point of the raycast
        //2nd parameter : direction of raycast(Vector3 slicingDirection = endSlicingPoint.position - startSlicingPoint.position;)
        //3rd parameter : output hit ( and stores all the info to hit)
        //4th parameter : length that would can compute with the slicing direction
        //5th parameter : Layer of the sliceable object that we can hit

        // USE EZYSLICE TO EASILY CUT OBJECTS [using EzySlice ]
        
        if(hasHit){ //when we hit an object 
            if(hit.transform.gameObject.layer==10)
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            else
            Slice(hit.transform.gameObject, hit.point,velocityEstimator.GetVelocityEstimate()); // we can slice the  object , last parameter needs a rigidbody but lucky 
            //you can use the VelocityEstimator.cs script to help make this part easier

        }
    
        
    }
    void Slice(GameObject target,Vector3 planePosition,Vector3 slicerVelocity){ 
    //target = who we are cutting , 
    //planePosition = from where is he cutting from , 
    //licerVelocity = direction of cut ,
    //you need these 3 to perform a cut
        Debug.Log("Slice!");
        Vector3 slicingDirection = endSlicingPoint.position - startSlicingPoint.position;
        Vector3 planeNormal = Vector3.Cross(slicerVelocity, slicingDirection); // how to get upwards/normal direction from a slice
        //parameter 1 : variable that has the part where the katana hits downwards
        //paremeter 2 : variable that has the part of the start and end of the sword where it points forward
       // Vector3 planeNormal = Vector3.Cross(slicerVelocity);
        SlicedHull hull = target.Slice(planePosition,planeNormal,slicedMaterial); //to slice an object . Store it into a variable. put the slicedmat in there too
        //plane can be defined by 2 elemts , a point and upward , we can define all of the plane that way
        //first parameter : plane posiiton (dot)
        //second parameter : The Normal .If katana hits the plane downwords , the starting and end point of the katana , 
        //the katana sharp tip points forward , from those two we can find the cross vector pointing upwards
        if(hull != null){ // if hull exists
            DisplayScore.score++;
             GameObject upperHull  = hull.CreateUpperHull(target, slicedMaterial);// we can compute the two slicing objects
             GameObject lowerHull = hull.CreateLowerHull(target, slicedMaterial); // the two slicing objects will be called lower and upper
            CreateSlicedComponent(upperHull);
            CreateSlicedComponent(lowerHull);
            Destroy(target);//Destroy the initial game object
        }
    }
    void CreateSlicedComponent(GameObject slicedHull){ // components that we want on this game object
        Rigidbody rb = slicedHull.AddComponent<Rigidbody>(); // has gravity
        MeshCollider collider = slicedHull.AddComponent<MeshCollider>(); // needs to be convex because a rigidbody
        //always wants a convex mesh collider
        collider.convex = true;
        rb.AddExplosionForce(slicedObjectInitialVelocity,slicedHull.transform.position,1);
        //first parameter : values of the explosion
        //second parameter : who do we explode
        //third parameter : radius

        Destroy(slicedHull,4);
        
    }
}
