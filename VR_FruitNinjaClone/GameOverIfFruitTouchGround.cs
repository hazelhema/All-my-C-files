using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverIfFruitTouchGround : MonoBehaviour
{   public int maxNumberOfError = 3;
    private int currentNumberOfError = 0;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.layer == 8){// coresponds to the our layer sliceable
            currentNumberOfError++;
        }
            if(currentNumberOfError == maxNumberOfError){
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);

            //if you dont destroy the game object now , the same gameobject will repeat multiple times
            //so
            Destroy(collision.gameObject);    
            }
        }
}
        

