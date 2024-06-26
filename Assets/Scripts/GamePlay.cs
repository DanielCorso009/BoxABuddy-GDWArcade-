using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    // Start is called before the first frame update
    private HPS1 healthscript1;
    private HPS2 healthscript2;
    private SB1 staminascript1;
    private SB2 staminascript2;
    public GameObject player1;
    public GameObject player2;

    public GameObject hBar1;
    public GameObject hBar2;
    public GameObject sBar1;
    public GameObject sBar2;

    public GameObject[] fans = new GameObject[16];
    
    public Cheer[] cheers =new Cheer[16];
    public Text thisText;

    private PlayerController playerscript1;
    private PlayerController playerscript2;

    private int health1;
    private int health2;
    private float stam1;
    private float stam2;

    public int score;

    void Start()
    {
            Cursor.visible = false;

            playerscript1 = player1.GetComponent<PlayerController>();
            playerscript2 = player2.GetComponent<PlayerController>();

            //print(playerscript1.health + "\n" + playerscript2.stamina);
            healthscript2 = hBar2.GetComponent<HPS2>();
            health2 = playerscript2.health;
            healthscript2.SetMaxHealth(health2);
            healthscript1 = hBar1.GetComponent<HPS1>();
            health1 =playerscript1.health;
            healthscript2.SetMaxHealth(health1);

            staminascript1 = sBar1.GetComponent<SB1>();
            stam1 =playerscript1.stamina;
            staminascript1.SetMaxStamina(stam1);
            staminascript2 = sBar2.GetComponent<SB2>();
            stam2 =playerscript2.stamina;
            staminascript1.SetMaxStamina(stam2);


            thisText.text = playerscript2.wins +" | "+ playerscript1.wins;

            for(int i = 0; i < fans.Length; i++){
                
                cheers[i] = fans[i].GetComponent<Cheer>();
            }


    }
    public void HealthCheck(){
        health1 =playerscript1.health;
        health2 =playerscript2.health;
        healthscript1.SetHealth(health1);
        healthscript2.SetHealth(health2);
        //updates winner
        if(health1 <=0){
            playerscript1.knockOut();
            playerscript2.Winner();
            playerscript2.wins++;
            for(int i = cheers.Length/2; i < cheers.Length; i++){
                cheers[i].speed = 5;
            }

            StartCoroutine(ResetGame());
        }else if(health2 <=0){
            playerscript2.knockOut();
            playerscript1.Winner();
            playerscript1.wins++;
            for(int i = 0; i < cheers.Length/2; i++){
                cheers[i].speed = 5;
            }
            StartCoroutine(ResetGame());
        }
    }
    public void StamCheck(){
        stam1 =playerscript1.stamina;
        stam2 =playerscript2.stamina;
//updates stamina
        staminascript1.SetStamina(stam1);
        staminascript2.SetStamina(stam2);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Escape)) { 
	        Application.Quit();
        }

    }

    
    IEnumerator ResetGame(){
        yield return new WaitForSeconds(2);
        playerscript1.health = 150;
        playerscript2.health = 150;
        thisText.text = playerscript2.wins +" | "+ playerscript1.wins;
            for(int i = 0; i < cheers.Length; i++){
                cheers[i].speed = 0.5f;
            }
        playerscript1.BackToIdle();
        playerscript2.BackToIdle();
        HealthCheck();

    }
}
