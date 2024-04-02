using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheer : MonoBehaviour
{
    public GameObject fan;
    public float yVal;
    public float speed;


    // Update is called once per frame

    private void Start(){
        yVal = transform.position.y;
    }
    public void Update()
    {
        fan.transform.position = new Vector3(transform.position.x,Mathf.PingPong(Time.time*speed+yVal, yVal+0.05f),transform.position.z); 
    }
}

