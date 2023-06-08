using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class worldControl : MonoBehaviour
{
    public int enemiesAppeared = 0;
    public int monedas = 50;
    public int vida = 100;
    public int maxEnemies;
    public GameObject prefab;
    public GameObject prefab2;
    public GameObject defeatScreen;
    public GameObject victoryScreen;
    public TMP_Text textoCanvas;
    public TMP_Text textoCanvasMoney;
    public TMP_Text textoEnemigos; 
    public GameObject start;
    public GameObject start2;
    public GameObject start3;
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
        if (enemiesAppeared < maxEnemies)
        {
            counterToBigEnemy--;

            if (counterToBigEnemy == 0)
            {
                counterToBigEnemy = counterToBigEnemyAux;
                // Crea una instancia del EnemigoGrande en la posición del start para cada start
                GameObject enemigo = Instantiate(prefab2, start.transform.position, Quaternion.identity);
                enemigo.GetComponent<Enemy>().direccionMovimiento = start.GetComponent<direccionStart>().direccionSalida;
                if (enemigo.GetComponent<Enemy>().direccionMovimiento == Giro.direccion.izquierda)
                {
                    enemigo.GetComponent<Enemy>().FlipXY();
                }
                enemigos.Add(enemigo);

                GameObject enemigo2 = Instantiate(prefab2, start2.transform.position, Quaternion.identity);
                enemigo2.GetComponent<Enemy>().direccionMovimiento = start2.GetComponent<direccionStart>().direccionSalida;
                if (enemigo2.GetComponent<Enemy>().direccionMovimiento == Giro.direccion.izquierda)
                {
                    enemigo2.GetComponent<Enemy>().FlipXY();
                }
                enemigos.Add(enemigo2);

                if(start3 != null)
                {
                    GameObject enemigo3 = Instantiate(prefab2, start3.transform.position, Quaternion.identity);
                    enemigo3.GetComponent<Enemy>().direccionMovimiento = start3.GetComponent<direccionStart>().direccionSalida;
                    if (enemigo3.GetComponent<Enemy>().direccionMovimiento == Giro.direccion.izquierda)
                    {
                        enemigo3.GetComponent<Enemy>().FlipXY();
                    }
                    enemigos.Add(enemigo3);
                }
                
            }
            else
            {
                // Crea una instancia del EnemigoPeque en la posición del start
                GameObject enemigo = Instantiate(prefab, start.transform.position, Quaternion.identity);
                enemigo.GetComponent<Enemy>().direccionMovimiento = start.GetComponent<direccionStart>().direccionSalida;
                if(enemigo.GetComponent<Enemy>().direccionMovimiento == Giro.direccion.derecha || enemigo.GetComponent<Enemy>().direccionMovimiento == Giro.direccion.arriba){
                    enemigo.GetComponent<Enemy>().FlipXY();
                }
                enemigos.Add(enemigo);

                GameObject enemigo2 = Instantiate(prefab, start2.transform.position, Quaternion.identity);
                enemigo2.GetComponent<Enemy>().direccionMovimiento = start2.GetComponent<direccionStart>().direccionSalida;
                enemigos.Add(enemigo2);
                if (enemigo2.GetComponent<Enemy>().direccionMovimiento == Giro.direccion.derecha || enemigo2.GetComponent<Enemy>().direccionMovimiento == Giro.direccion.arriba)
                {
                    enemigo2.GetComponent<Enemy>().FlipXY();
                }

                if (start3 != null)
                {
                    GameObject enemigo3 = Instantiate(prefab, start3.transform.position, Quaternion.identity);
                    enemigo3.GetComponent<Enemy>().direccionMovimiento = start3.GetComponent<direccionStart>().direccionSalida;
                    enemigos.Add(enemigo3);
                    if (enemigo3.GetComponent<Enemy>().direccionMovimiento == Giro.direccion.derecha || enemigo3.GetComponent<Enemy>().direccionMovimiento == Giro.direccion.arriba)
                    {
                        enemigo3.GetComponent<Enemy>().FlipXY();
                    }
                }

            }

            if (start3 != null)
            {
                enemiesAppeared += 3;
            }
            else
            {
                enemiesAppeared += 2;
            }

        } else
        {
            //Si se ha ganado, activa la pantalla de victoria
            if (enemigos.Count == 0)
            {
                victoryScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        textoCanvas.text = "Life: "+ vida.ToString();
        if (vida < 0) {
            //Finalizar pantalla
            defeatScreen.SetActive(true);
            Time.timeScale = 0;
        }
        textoCanvasMoney.text = "Money: " + monedas.ToString();
        textoEnemigos.text = "Enemies Spawned: " + enemiesAppeared.ToString();
    }

    public void quitarVida(int vida, GameObject destroyed) 
    {
        this.vida -= vida;
        enemigos.Remove(destroyed);
    }
}
