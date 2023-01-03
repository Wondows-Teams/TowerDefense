using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class worldControl : MonoBehaviour
{
    public int monedas = 50;
    public int vida = 100;
    public GameObject prefab;
    public GameObject prefab2;
    public TMP_Text textoCanvas;
    public TMP_Text textoCanvasMoney;
    public GameObject start;
    public float spawnInterval = 5.0f;
    public float moneyInterval = 9.0f;
    public List<GameObject> enemigos;
    public int counterToBigEnemy = 3;
    private int counterToBigEnemyAux = 3;
    public int moneyAutoCantidad;
    public TMP_Text alertText;

    // Start is called before the first frame update
    void Start()
    {
        
        counterToBigEnemyAux = counterToBigEnemy;
        InvokeRepeating("Spawn", spawnInterval, spawnInterval);
        InvokeRepeating("GetMoney", moneyInterval, moneyInterval);
    }
    public void alert(string msg)
    {
        alertText.text = "ALERT: " + msg;
        Invoke("OffAlert", 1);

    }
    public void OffAlert()
    {
        alertText.text = "";
    }
    void GetMoney()
    {

        monedas += moneyAutoCantidad;


    }
        void Spawn()
        {
        counterToBigEnemy--;

        if (counterToBigEnemy == 0)
        {
            counterToBigEnemy = counterToBigEnemyAux;
            // Crea una instancia del prefab2 en la posición del start
            enemigos.Add(Instantiate(prefab2, start.transform.position, Quaternion.identity));
        }
        else
        {
            // Crea una instancia del prefab1 en la posición del start
            enemigos.Add(Instantiate(prefab, start.transform.position, Quaternion.identity));
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        textoCanvas.text = "Life: "+ vida.ToString();
        if (vida < 0) { 
        //Finalizar pantalla
        }
        textoCanvasMoney.text = "Money: " + monedas.ToString();
    }

    public void quitarVida(int vida) 
    {
        this.vida -= vida;
    }
}
