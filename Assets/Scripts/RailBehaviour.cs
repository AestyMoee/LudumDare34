using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RailBehaviour : MonoBehaviour {

    public bool create = true;
    public bool isActive = false;

    //public GameObject prefabInstance;
    //public GameObject prefabInstanceLeft;
    //public GameObject prefabInstanceRight;
    //public GameObject prefabInstanceTown;

    public Sprite spriteA;
    public Sprite spriteB;
    public Sprite spriteC;
    public Sprite spriteH;

    PlayerController player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive && !GetComponentInChildren<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
	}

    TownBehaviour.Nom CreateLevel()
    {
        float r = Random.value;
        if (r < 0.25f)
        {
            //créer un trou
            return TownBehaviour.Nom.Hole;
        }
        else if (r < 0.50f)
        {
            return TownBehaviour.Nom.Aville;
        }
        else if (r < 0.75f)
        {
            return TownBehaviour.Nom.Btown;
        }
        else
        {
            return TownBehaviour.Nom.Ccity;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isActive = true;

            if(create && player.addRail)
            {
                float r = Random.value;
                GameObject go;
                
                if(r < 0.1f)
                {
                    
                    if(player.addTown || player.addHole)
                    {
                        if(player.addTown)
                        {
                            player.addTown = false;
                            player.addRail = false;

                            Debug.Log("test");

                            GameObject town = Resources.Load("Prefabs/Town", typeof(GameObject)) as GameObject;
                            go = Instantiate(town, transform.position + transform.forward * 60, town.transform.rotation * player.transform.rotation) as GameObject;
                        }
                        else
                        {
                            player.addRail = false;
                            player.addDivide = false;
                            player.addHole = false;
                            GameObject hole = Resources.Load("Prefabs/Break", typeof(GameObject)) as GameObject;
                            go = Instantiate(hole, transform.position + transform.forward * 60, hole.transform.rotation * player.transform.rotation) as GameObject;
                        }

                    }
                    else if (player.addDivide)
                    {
                        player.addRail = false;
                        player.addDivide = false;

                        bool rightDirection = false;

                        TownBehaviour.Nom next = CreateLevel();

                        if (r < 0.05f)
                        {
                            GameObject left = Resources.Load("Prefabs/Left", typeof(GameObject)) as GameObject;
                            go = Instantiate(left, transform.position + transform.forward * 54, left.transform.rotation * player.transform.rotation) as GameObject;

                            if (next == TownBehaviour.Nom.Hole)
                            {
                                go.GetComponent<DivideBehaviour>().createHole = true;
                            }

                            if (r < 0.025f)
                            {
                                rightDirection = true;
                                go.GetComponent<DivideBehaviour>().buildForward = true;
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            GameObject right = Resources.Load("Prefabs/Right", typeof(GameObject)) as GameObject;
                            go = Instantiate(right, transform.position + transform.forward * 71, right.transform.rotation * player.transform.rotation) as GameObject;

                            if (next == TownBehaviour.Nom.Hole)
                            {
                                go.GetComponent<DivideBehaviour>().createHole = true;
                            }

                            if (r < 0.075f)
                            {
                                go.GetComponent<DivideBehaviour>().buildForward = true;
                            }
                            else
                            {
                                rightDirection = true;
                            }
                        }

                        GameObject[] panneaux = GameObject.FindGameObjectsWithTag("Panneau");

                        for(int i=0;i<panneaux.Length;i++)
                        {
                            Image[] imgsPanneau = panneaux[i].GetComponentsInChildren<Image>();
                            for(int j=0;j<imgsPanneau.Length;j++)
                            {
                                if (imgsPanneau[j].name == "ville")
                                {
                                    switch (next)
                                    {
                                        case TownBehaviour.Nom.Aville:
                                            imgsPanneau[j].sprite = spriteA;
                                            break;
                                        case TownBehaviour.Nom.Btown:
                                            imgsPanneau[j].sprite = spriteB;
                                            break;
                                        case TownBehaviour.Nom.Ccity:
                                            imgsPanneau[j].sprite = spriteC;
                                            break;
                                        case TownBehaviour.Nom.Hole:
                                            imgsPanneau[j].sprite = spriteH;
                                            break;
                                    }
                                        
                                }
                                
                                if (imgsPanneau[j].name == "direction")
                                { 
                                    if (rightDirection && imgsPanneau[j].transform.localScale.x > 0)
                                        imgsPanneau[j].transform.localScale = new Vector3(imgsPanneau[j].transform.localScale.x * -1f, imgsPanneau[j].transform.localScale.y, imgsPanneau[j].transform.localScale.z);
                                    else if(!rightDirection && imgsPanneau[j].transform.localScale.x < 0)
                                        imgsPanneau[j].transform.localScale = new Vector3(imgsPanneau[j].transform.localScale.x * -1f, imgsPanneau[j].transform.localScale.y, imgsPanneau[j].transform.localScale.z);

                                }
                            }
                        }
                    }
                    else
                    {
                        go = Instantiate(Resources.Load("Prefabs/Straight", typeof(GameObject)), transform.position + transform.forward * 60, Quaternion.identity * player.transform.rotation) as GameObject;
                    }
                }
                else
                {
                    go = Instantiate(Resources.Load("Prefabs/Straight", typeof(GameObject)), transform.position + transform.forward * 60, Quaternion.identity * player.transform.rotation) as GameObject;
                }
            }
            
        }
    }
}
