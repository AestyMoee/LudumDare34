using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public Image handle;

    public float speed;

    public bool addRail = true;
    public bool addDivide = true;
    public bool addTown = false;
    public bool addHole = false;

    public bool isRotate = false;
    public bool turnLeft = false;
    public bool turnRight = false;

    public Vector3 newRotation;
    public Vector3 initRotation;
    public Vector3 initRotationPosition;

    public GameObject CameraTop;

    public AudioSource sonnet;

    public int nbWagon = 0;

    GameObject lastWagon;
    List<GameObject> lstWagon;

    // Use this for initialization
    void Start () {
        lastWagon = gameObject;
        lstWagon = new List<GameObject>();
	}

    public void AddWagon(TownBehaviour.Nom from)
    {
        //regarde où est le dernier wagon et accroche le suivant
        GameObject wagon = Resources.Load("Prefabs/wagon", typeof(GameObject)) as GameObject;
        
        GameObject newWagon;

        if (lastWagon == gameObject)
            newWagon = Instantiate(wagon,lastWagon.transform.position + lastWagon.transform.forward * -8f, Quaternion.Euler(wagon.transform.rotation.eulerAngles + transform.rotation.eulerAngles)) as GameObject;
        else
            newWagon = Instantiate(wagon, lastWagon.transform.position + lastWagon.transform.up * 9f, Quaternion.Euler(wagon.transform.rotation.eulerAngles + transform.rotation.eulerAngles)) as GameObject;

        lastWagon = newWagon;
        lstWagon.Add(lastWagon);

        sonnet.Play();
        nbWagon++;
        speed += 5;


        Vector3 newPositionCamera = (transform.position - lastWagon.transform.position)/2;
        CameraTop.transform.position = new Vector3(transform.position.x - newPositionCamera.x,CameraTop.transform.position.y + 3, transform.position.z - newPositionCamera.z);

    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(lastWagon.transform.position + " " + transform.position);
        //transform.Rotate
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("quit");
            Application.Quit();
        }

        if(isRotate)
        {
            //Debug.Log(initRotationPosition + " " + transform.position + " = " + Vector3.Distance(initRotationPosition, transform.position));

            transform.rotation = Quaternion.Euler(Vector3.Lerp(initRotation, newRotation, Vector3.Distance(initRotationPosition, transform.position) / 7.3f));

            if (Vector3.Distance(initRotationPosition, transform.position) / 7.3f > 1)
                isRotate = false;
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if(Input.GetAxis("Horizontal")>0.2f)
        {
            handle.transform.rotation = Quaternion.Euler(0, 0, -15);
            //rotation du manche plus active bool right
            turnRight = true;
            turnLeft = false;
        }
        else if (Input.GetAxis("Horizontal") < -0.2f)
        {
            handle.transform.rotation = Quaternion.Euler(0, 0, 15);
            //rotation du manche plus active bool left
            turnLeft = true;
            turnRight = false;
        }
        else
        {
            handle.transform.rotation = Quaternion.Euler(0, 0, 0);
            turnRight = false;
            turnLeft = false;
        }
        
	}
}
