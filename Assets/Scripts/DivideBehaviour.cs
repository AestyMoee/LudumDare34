using UnityEngine;
using System.Collections;

public class DivideBehaviour : MonoBehaviour {

    PlayerController player;
    public bool isActive = false;

    public GameObject AddTownForward;
    public GameObject AddTownLeftRight;

    public GameObject changementDeVoie;

    public bool buildForward;
    public bool createHole = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update () {
        if (isActive)
        {
            bool visibles = false;
            Renderer[] renderers = GetComponentsInChildren<Renderer>();

            for (int i=0;i< renderers.Length;i++)
            {
                if (renderers[i].isVisible)
                {
                    visibles = true;
                    break;
                }
            }

            if(!visibles)
                Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            isActive = true;
    }
}
