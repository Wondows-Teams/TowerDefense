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
    public string tipoTorreta;
    GameSoundManager soundManager;


    // Start is called before the first frame update
    void Start()
    {
        worldControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<worldControl>();
        agarrando = false;
        soundManager = GameObject.Find("SoundManager").GetComponent<GameSoundManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (agarrando)
        {

            // Obt�n la posici�n del cursor del mouse en pantalla
            Vector3 mousePosition = Input.mousePosition;

            // Convierte la posici�n del cursor del mouse en coordenadas de mundo
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePositionInWorld.z =-3;

            // Establece la posici�n del game object a la posici�n del cursor del mouse en coordenadas de mundo
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
            soundManager.SelectAudio(2, 1);
        } 
        else if(tipoTorreta == "Triple"
            && worldControl.TorretasTriplesGeneradas >= 1)
        {
            worldControl.alert("M�ximo de una torreta triple");
            soundManager.SelectAudio(2, 1);
        }
        else if (tipoTorreta == "Normal"
           && worldControl.TorretasNormalesGeneradas >= 3)
        {
            worldControl.alert("M�ximo de tres torretas normales");
            soundManager.SelectAudio(2, 1);
        }
        //Si tengo dinero me lo resta y puedo hacer lo demas
        else
        {
            worldControl.monedas = worldControl.monedas - precioTorreta;
            agarrando = true;

            // Crea una instancia del prefab en la posici�n del start
            torretaInstanciada = Instantiate(prefabTurret1, this.transform.position, Quaternion.identity);
            if (tipoTorreta == "Normal") {
                worldControl.TorretasNormalesGeneradas++;
            } else if (tipoTorreta == "Triple") {
                worldControl.TorretasTriplesGeneradas++;
            }
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
                    soundManager.SelectAudio(1, 1);
                }
                else
                {
                    worldControl.alert("Ocupado");

                    //Te devuelve el dinero
                    worldControl.monedas = worldControl.monedas + precioTorreta;

                    //Se destruye
                    Destroy(torretaInstanciada);
                    if (tipoTorreta == "Normal")
                    {
                        worldControl.TorretasNormalesGeneradas--;
                    }
                    else if (tipoTorreta == "Triple")
                    {
                        worldControl.TorretasTriplesGeneradas--;
                    }
                }


            }
            //Si sueltas en Nada
            else
            {
                //Te devuelve el dinero
                worldControl.monedas = worldControl.monedas + precioTorreta;

                //Se destruye
                Destroy(torretaInstanciada);
                if (tipoTorreta == "Normal")
                {
                    worldControl.TorretasNormalesGeneradas--;
                }
                else if (tipoTorreta == "Triple")
                {
                    worldControl.TorretasTriplesGeneradas--;
                }
            }


            agarrando = false;
        }

       

    }



}
