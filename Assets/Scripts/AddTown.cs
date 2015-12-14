using UnityEngine;
using System.Collections;

public class AddTown : MonoBehaviour {

    PlayerController player;
    public bool createVille = false;
    public bool createHole = false;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (createVille)
                player.addTown = true;
            if (createHole)
                player.addHole = true;
            transform.parent.GetComponent<DivideBehaviour>().isActive = true;
            player.addDivide = true;
        }
    }
}
