using UnityEngine;
using System.Collections;

public class WagonBehaviour : MonoBehaviour {

    PlayerController player;

    public Vector3 newRotation;
    public Vector3 initRotation;
    public Vector3 initRotationPosition;
    public bool isRotate;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * Time.deltaTime * -player.speed);

        if (isRotate)
        {
            transform.rotation = Quaternion.Euler(Vector3.Lerp(initRotation, newRotation, Vector3.Distance(initRotationPosition, transform.position) / 7.8f));

            if (Vector3.Distance(initRotationPosition, transform.position) / 7.8f > 1)
                isRotate = false;
        }
    }
}
