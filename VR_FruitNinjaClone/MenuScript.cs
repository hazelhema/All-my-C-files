using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // need this
public class MenuScript // not going to be attached to any game object hence there is no Monobehavior
{
    [MenuItem("Tools/Assign Tile Material")] //*ask
    public void AssignTileMaterial(){
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        Material material = Resources.Load<Material>("Tile"); //delcare the material variable
        foreach (GameObject t in tiles){ //loop through every single one

        }    }
    
}
