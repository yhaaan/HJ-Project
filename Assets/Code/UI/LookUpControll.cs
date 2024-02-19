using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LookUpControll : MonoBehaviour
{
    public GameObject lookUpCam;
    public void UpV()
    {
        if(GameManager.instance.lookOutStatus)
            lookUpCam.GetComponent<CinemachineVirtualCamera>().Priority = 11;
    }
    public void DownV()
    {
        if(GameManager.instance.lookOutStatus)
            lookUpCam.GetComponent<CinemachineVirtualCamera>().Priority = 9;
    }
    
}
