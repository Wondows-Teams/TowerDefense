using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.


public class SpawnTurretControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public GameObject[] objectives;
    public GameObject prefabTurret1;
    private bool agarrando;
    public GameObject torretaInstanciada;
    public worldControl worldControl;
    public int precioTorreta;


    // Start is called before the first frame update
    void Start()
    {
        worldControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<worldControl>();
        agarrando = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (agarrando)
        {

            // Obtén la posición del cursor del mouse en pantalla
            Vector3 mousePosition = Input.mousePosition;

            // Convierte la posición del cursor del mouse en coordenadas de mundo
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePositionInWorld.z =-3;

            // Establece la posición del game object a la posición del cursor del mouse en coordenadas de mundo
            torretaInstanciada.transform.position = mousePositionInWorld;
        }



    }



    //Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerDown(PointerEventData eventData)
    {
        //Si no tengo dinero me da un aviso
        if (worldControl.monedas < precioTorreta)
        {
            worldControl.alert("Dinero insuficiente");
        }
        //Si tengo dinero me lo resta y puedo hacer lo demas
        else
        {
            worldControl.monedas = worldControl.monedas - precioTorreta;
            agarrando = true;

            // Crea una instancia del prefab en la posición del start
            torretaInstanciada = Instantiate(prefabTurret1, this.transform.position, Quaternion.identity);
        }
       

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //Si he podido comprarla
        if (agarrando)
        {
            //Si sueltas en Objetivo
            if (torretaInstanciada.GetComponent<TurretScript>().inObjective == true)
            {
                //Si el objetivo no esta ocupado
                if (torretaInstanciada.GetComponent<TurretScript>().objectiveAt.GetComponent<ObjectiveScript>().ocupado == false)
                {
                    torretaInstanciada.GetComponent<TurretScript>().objectiveAt.GetComponent<ObjectiveScript>().ocupado = true;
                    Vector3 aux = torretaInstanciada.GetComponent<TurretScript>().objectiveAt.transform.position;
                    aux.z = -3;
                    torretaInstanciada.transform.position = aux;

                    torretaInstanciada.GetComponent<TurretScript>().startShotting();
                }
                else
                {
                    worldControl.alert("Ocupado");

                    //Te devuelve el dinero
                    worldControl.monedas = worldControl.monedas + precioTorreta;

                    //Se destruye
                    Destroy(torretaInstanciada);
                }


            }
            //Si sueltas en Nada
            else
            {
                //Te devuelve el dinero
                worldControl.monedas = worldControl.monedas + precioTorreta;

                //Se destruye
                Destroy(torretaInstanciada);

            }


            agarrando = false;
        }

       

    }



}
