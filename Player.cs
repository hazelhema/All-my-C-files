using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera thirdPersonCam;
    [SerializeField]
    CinemachineVirtualCamera firstPersonCam;
    [SerializeField]
    CinemachineVirtualCamera topDownCam;
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            

        }
    }
    }

