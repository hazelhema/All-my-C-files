using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This script is going to define all the movement
public class TacticsMove : MonoBehaviour
{   
    List<Tile> selectableTiles = new List<Tile>(); // tiles that are shown during player's round SHOULD only exist in player round

    GameObject[] tiles;  // an array of all the tiles as gameobject
    public int move = 5; //move 5 tiles a turn
    public float moveSpeed = 2; // how fast the unit will walk across the tiles

    Stack<Tile>path = new Stack<Tile>(); //stack is able to do a reverse order path calculation
    Tile currentTile; // tracks the player's tile
    public float jumpHeight = 2; //they can jump /drop 2 tiles
    Vector3 velocity = new Vector3(); // how fast player moves
    Vector3 heading = new Vector3() ; //the direction they are heading
    float halfHeight= 0; // because height starts at center point , to the center of player ( that's where the player will be standing on the tile)

    

    protected void Init(){
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        halfHeight = GetComponent<Collider>().bounds.extents.y;   // get the value from the capsule collider height , and * scale of the transform of the gameobject ( in other words with respect to the scale of the object)
    }
    public void GetCurrentTile(){

    }

   // public Tile GetTargetTile(GameObject target){

    //}
}
