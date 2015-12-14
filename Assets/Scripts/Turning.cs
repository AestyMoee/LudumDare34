using UnityEngine;
using System.Collections;

public class Turning : MonoBehaviour {

    PlayerController player;
    public bool turnLeft;

    bool isTurningLeft;
    bool isTurningRight;


    DivideBehaviour divide;
	// Use this for initialization
	void Start () {
        divide = transform.parent.parent.gameObject.GetComponent<DivideBehaviour>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (divide.buildForward)
            {
                if (divide.createHole)
                    divide.AddTownForward.GetComponent<AddTown>().createHole = true;
                else
                    divide.AddTownForward.GetComponent<AddTown>().createVille = true;
            }
            else
            {
                if (divide.createHole)
                    divide.AddTownLeftRight.GetComponent<AddTown>().createHole = true;
                else
                    divide.AddTownLeftRight.GetComponent<AddTown>().createVille = true;
            }

            player.addRail = true;

            if (turnLeft && player.turnLeft)
            {
                player.isRotate = true;
                player.initRotation = player.transform.rotation.eulerAngles;
                player.initRotationPosition = player.transform.position;
                player.newRotation = new Vector3(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y - 35, player.transform.rotation.eulerAngles.z);
                isTurningLeft = true;
                //oriente le train et préviens les wagons

            }
            else if (!turnLeft && player.turnRight)
            {
                player.isRotate = true;
                player.initRotation = player.transform.rotation.eulerAngles;
                player.initRotationPosition = player.transform.position;
                player.newRotation = new Vector3(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y + 35, player.transform.rotation.eulerAngles.z);
                isTurningRight = true;

            }

        }
        else if(other.tag == "Wagon")
        {
            WagonBehaviour wagon = other.GetComponent<WagonBehaviour>();
            if (isTurningLeft)
            {
                wagon.isRotate = true;
                wagon.initRotation = wagon.transform.rotation.eulerAngles;
                wagon.initRotationPosition = wagon.transform.position;
                wagon.newRotation = new Vector3(wagon.transform.rotation.eulerAngles.x, wagon.transform.rotation.eulerAngles.y - 35, wagon.transform.rotation.eulerAngles.z);
            }
            else if(isTurningRight)
            {
                wagon.isRotate = true;
                wagon.initRotation = wagon.transform.rotation.eulerAngles;
                wagon.initRotationPosition = wagon.transform.position;
                wagon.newRotation = new Vector3(wagon.transform.rotation.eulerAngles.x, wagon.transform.rotation.eulerAngles.y + 35, wagon.transform.rotation.eulerAngles.z);
            }
        }
        //Destroy(divide.changementDeVoie);
    }
}
