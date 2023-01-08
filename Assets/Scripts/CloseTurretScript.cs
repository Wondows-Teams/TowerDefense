using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTurretScript : MonoBehaviour
{
    public int segundosBloqueo = 5;
    public bool inCloseObjective;
    public GameObject closeObjectiveAt;
    public worldControl worldControl;

    // Start is called before the first frame update
    void Start()
    {
        worldControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<worldControl>();
        inCloseObjective = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "closeobjective")
        {
            inCloseObjective = true;
            closeObjectiveAt = collision.gameObject;
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "closeobjective")
        {
            inCloseObjective = false;
        }


    }
}
