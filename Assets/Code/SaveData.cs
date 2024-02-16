using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SaveData : MonoBehaviour
{
    public Vector3 vec3;



    public void Save()
    {
        PlayerPrefs.SetFloat("PlayerX",Player.instance.transform.position.x);
    }
}
