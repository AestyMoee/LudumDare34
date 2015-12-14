using UnityEngine;
using System.Collections;

public class TownBehaviour : MonoBehaviour {

    public enum Nom
    {
        Aville,
        Btown,
        Ccity,
        Hole
    }

    public Nom nom;

    PlayerController player;
    public bool isActive = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update () {
	    if(isActive && !transform.parent.gameObject.GetComponentInChildren<Renderer>().isVisible)
        {
            Destroy(transform.parent.gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.addRail = true;
            player.addDivide = true;
            isActive = true;

            player.AddWagon(nom);
        }
    }
}
