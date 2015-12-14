using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoleBehaviour : MonoBehaviour {

    PlayerController player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter()
    {
        Canvas canvasfin = GameObject.FindGameObjectWithTag("CanvasFin").GetComponent<Canvas>();
        canvasfin.GetComponentInChildren<Text>().text = "Score : "+player.nbWagon;
        canvasfin.enabled = true;
        
        //affiche un menu
        //stop le temps et la musique
        Time.timeScale = 0f;
        
    }
}
