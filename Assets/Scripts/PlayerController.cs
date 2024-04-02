using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    public bool isPlayer2 = false;// used for winner declaration, no need to tie controls to it.

    //-------------------------{W,A,S,D,Z,X,C,SPACE,V,B} Player1
    //-------------------------{UP_ARROW,DOWN_ARROW,LEFT_ARROW,RIGHT_ARROW,7,8,9,4,5,6} Player2
    public string[] controls = {"","","","","","","","","",""};

    //movement////////////////////////////////////////
    public float speed = 0f;
    public Vector3 movement = new Vector3(0,0,0);
    /////////////////////////////////////////////////
    
    
    
    //stats./
    public int health = 150;
    public float dash = 1f;
    private int Damage = 0;    
    public float stamina = 100f;
    public int wins =0;
    ///////////////////////////////////////////////////
    
   //states
    private bool isAtk = false;
    private bool inBlock = false;
// component initializing
    private Animator anim;
    private LookAtConstraint look;

    public GameObject otherPlayer;
    private PlayerController otherScript;

    private GamePlay gamePlay;
     
    //public GameObject hitbox;
    void Start()
    {
       //print(controls.Length);
       anim = GetComponent<Animator>();
       otherScript = otherPlayer.GetComponent<PlayerController>();
       gamePlay = GameObject.Find("UiandEngine").GetComponent<GamePlay>();
       //hitbox.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(!anim.GetBool("depleted")){
        InputHandler();
        keepInBounds();
        //if(anim.GetCurrentAnimatorStateInfo(0).IsTag("ATK")) isAtk = true;else isAtk=false;
        print(anim.GetCurrentAnimatorStateInfo(0).IsTag("idle"));


        transform.Translate(movement*speed*dash*Time.deltaTime);
        }
        //staminaSystem
        if(stamina <= 100){
            stamina+= 0.1f;
        }
        if(stamina <=0){
            anim.SetBool("depleted", true);
            inBlock = false;
            isAtk = false;
        }else if(stamina >= 50){
            anim.SetBool("depleted", false);
        }
        gamePlay.StamCheck();
    }

    void InputHandler(){
        // Will set movement to 0 to stop movement when buttons are no longer held
        int U = 0;
        int D = 0;
        int L = 0;
        int R = 0;
        //sets movement variables to 1 when held down
        if(Input.GetKey(controls[0])){//move up
            //print(controls[0]);
            U=1;
        }
        if(Input.GetKey(controls[1])){//move down
            //print(controls[1]);
            D=1;
        }
        if(Input.GetKey(controls[2])){// move left
            //print(controls[2]);
            L=1;
        }
        if(Input.GetKey(controls[3])){// move right
           // print(controls[3]);
            R=1;
        }

        if(Input.GetKey(controls[8])){//will be to dash?
            //print(controls[8]);
            dash = 2f;
            stamina-=0.2f;
        }else dash =1;
        //////////////////////////////
        if(Input.GetKey(controls[5])){//will be to block
                //print(controls[5]);
            anim.SetBool("block", true);
            inBlock = true;
            }else{
            inBlock = false;
            anim.SetBool("block",false);
            }
        // will activate scripts according to the controls(coming soon). 
        if(!inBlock){
            if(Input.GetKeyDown(controls[4])){//will be to jab
                //print(controls[4]);
                animManager("jab");
                
            }

            else if(Input.GetKeyDown(controls[6])){//will be to cross
                //print(controls[6]);
                animManager("cross");
                
                
            }
            else if(Input.GetKeyDown(controls[7])){//will be to hook left
                //print(controls[7]);
                animManager("hook-left");


            }
            else if(Input.GetKeyDown(controls[9])){// will be to hook right
                //print(controls[9]);
                animManager("hook-right");
            }
        }
        movement.x = R-L;
        movement.z = U-D;
    }

    // will add a function to handle anims and damage scale. also  a function to 
    void animManager(string name){
        switch(name){
            case "jab":
                anim.SetTrigger(name);              
                stamina-= 2;
                Damage = 5;
                break;
            case "cross":  
                anim.SetTrigger(name);            
                stamina-= 3;
                Damage = 7;
                break;
            case "hook-left":
                anim.SetTrigger(name);
                stamina -=8;
                Damage = 10;
                break;
            case "hook-right":
                anim.SetTrigger(name);
                stamina -=8;
                Damage = 10;
                break;
            default:
                break;

        }
            //hitbox.SetActive(true);

    }
    //makes sure nothing funny happens with physics
    void keepInBounds(){
        if(transform.position.x > 0.3f){
            transform.position = new Vector3(0.3f,transform.position.y,transform.position.z);
        }else if(transform.position.x <-0.3f){
            transform.position = new Vector3(-0.3f,transform.position.y,transform.position.z);
        }
        if(transform.position.z > 0.3f){
            transform.position = new Vector3(transform.position.x,transform.position.y,0.3f);
        }else if(transform.position.z < -0.3f){
            transform.position = new Vector3(transform.position.x,transform.position.y,-0.3f);
        }
        if(transform.position.y != 0){
            transform.position = new Vector3(transform.position.x,0,transform.position.z);
        }
    }
    //battle functions
    public void knockOut(){
        anim.SetBool("knockout", true);
    }
    public void Winner(){
        anim.SetBool("winner",true);
    }
    public void BackToIdle(){
        anim.SetBool("winner",false);
        anim.SetBool("knockout", false);


    }
    private void OnTriggerEnter(Collider other){
        //print(otherScript.isAtk);
         print(other.gameObject.name);
        if(!other.gameObject.name.Equals("Hitbox Center"))
            //print(other.gameObject.name);
            if(inBlock){
                    print("?");
            stamina-=15+otherScript.Damage;
            }else{
                health-=otherScript.Damage;
                gamePlay.HealthCheck();
            }
        }
}
