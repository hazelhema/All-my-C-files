using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // you need this
public class DisplayScore : MonoBehaviour
{
        public TextMeshPro textMeshPro;
        public static int score; // by adding static you are able to acccess this from other scripts
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = score.ToString();
        //you can put the slice object +1 point in the objectslicer script because the 
        //variables are there anyways
    }
}
