using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    
    public bool current = false; // aka ; is the player standing on the tile?
    public bool target = false; // target = where the npc will move to
    public bool selectable = false; //tiles that is clicked
    public bool walkable = false; // walking tile ( to prevent players from going over deep water)
    
    //BFS Requirement
    public bool visited = false; //tiles had been processed

    public Tile parent = null; // we want to know the parent of the tile , quickly allow us to identify the pathway (in short : its just a pathway identifier way of building a path for you by telling each cube you step on is a parent and considered a pathway) 
    public int distance = 0; // not part of bfs , calculates each tile from the start tile , limit npc and player range to move , this for bfs to prevent mapping the entire gameworld to be moveable




    public List<Tile>  adjacencyList = new List<Tile>();//when doing graph theory , we do adjacencylist or adjacencymatrix , matrix is a 2x2
    // lets identify the neighbour tiles (tiles next to the current tile) , no diagonal movement for now
    void Start()
    {
        
        
    }

    void Update()
    { // contains all information for the tile that we need for the graph theory and bfs (breadth for search) and 
      // A* algorithm for the npc
      // --------------------------------------------------------------------------------------------
      //color changing
      if(current){
        GetComponent<Renderer>().material.color = Color.magenta;
      }
      else if(target) {//npc will decide when to move is called the target
        GetComponent<Renderer>().material.color = Color.green;}
    
    else if (selectable){
        GetComponent<Renderer>().material.color = Color.red;}
    else{
        GetComponent<Renderer>().material.color = Color.white;
    } }

    public void Reset(){
        adjacencyList.Clear();
        current = false; 
        target = false; 
        selectable = false; 
        walkable = false; 
        visited = false; 
        parent = null; 
        distance = 0; 
    }
    public void FindNeighbors(float jumpHeight){  //learn how to build a function to find neighbouring objects
                            //jumpHeight : how many tiles can they jump up or down
        Reset();
        CheckTile(Vector3.forward,jumpHeight);
        CheckTile(-Vector3.forward,jumpHeight);
        CheckTile(Vector3.right,jumpHeight);
        CheckTile(-Vector3.right,jumpHeight);

    }
    public void CheckTile(Vector3 direction, float jumpHeight){// learn technical side onhow to check objects around an object
        //lets implement jumpheight
        //lets detect the tiles nearby with collider
        Vector3 halfExtends = new Vector3(0.25f,(1+ jumpHeight)/2.0f,0.25f); //needed for the 2nd part of the arguement in overlap box
        // a tile is 1,1,1 but 0.25f is enough , but y is wrong cuz we need to know if there are any reachable tiles 
        // a tile is 1 high + jump height divided by 2 (0.5) <--- range for jumping 
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtends); //Physics.OverlapBox returns an array of colliders. also hover mouse in arguement to learn more details on what to fill in
        foreach(Collider item in colliders) { //*read up on this
            //now lets determine if tile is a tile or not
        Tile tile = item.GetComponent<Tile>(); 
        if(tile != null && tile.walkable){ // means null means its not a tile and we dont care about it and we just let it go , applies to non walkable tiles too make sure != work first then the && will work
            RaycastHit hit;
            if (!Physics.Raycast(tile.transform.position , Vector3.up, out hit ,1)){//raycast start from tile position , direction upwards , gonna output a hit variable , and only looking for a distance of 1 
              // looks to see if theres something there  , if yes , dont add it , if no , add to adjency list
                adjacencyList.Add(tile);
        }
        
        }
    } 
}   
}

