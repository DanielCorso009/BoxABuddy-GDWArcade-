using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isPlayer2 = false;
    //-------------------------{W,A,S,D,Z,X,C,SPACE,V,B}
    //-------------------------{UP_ARROW,DOWN_ARROW}
    public string[] controls = {"","","","","","","","",""};

    // Start is called before the first frame update
    void Start()
    {
        print(controls.Length);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
