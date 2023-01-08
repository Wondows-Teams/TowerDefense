using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int life;
    public float velocidad = 1.0f;
    private float velocidadAux;
    public Giro.direccion direccionMovimiento;
    private GameObject camara;
    public int bulletDmged = 30;
    public worldControl worldControlx;
    public int dineroPorMuerte;
    GameSoundManager soundManager;
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        velocidadAux = velocidad;
        worldControlx = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<worldControl>();
        camara = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        rb2d = GetComponent<Rigidbody2D>();
        if (this.direccionMovimiento == Giro.direccion.arriba)
        {
            rb2d.velocity = new Vector2(0, velocidad);
        }
        else if (this.direccionMovimiento == Giro.direccion.abajo)
        {
            rb2d.velocity = new Vector2(0, -velocidad);
        }
        else if (this.direccionMovimiento == Giro.direccion.izquierda)
        soundManager = GameObject.Find("SoundManager").GetComponent<GameSoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.direccionMovimiento == Giro.direccion.izquierda)
        {
            rb2d.velocity = new Vector2(-velocidad, 0);
        }
        else if (this.direccionMovimiento == Giro.direccion.derecha)
        {
            rb2d.velocity = new Vector2(velocidad, 0);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        //OLD MOVEMENT
        //if (this.direccionMovimiento == Giro.direccion.izquierda)
        //{
        //    transform.Translate(-velocidad, 0, 0);
        //}
        //else if (this.direccionMovimiento == Giro.direccion.derecha)
        //{
        //    transform.Translate(velocidad, 0, 0);
        //}
        //else if (this.direccionMovimiento == Giro.direccion.arriba)
        //{
        //    transform.Translate(0, velocidad, 0);
        //}
        //else if (this.direccionMovimiento == Giro.direccion.abajo)
        //{
        //    transform.Translate(0,-velocidad, 0);
        //}


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "bullet")
        {
            life -= bulletDmged;
            Destroy(collision.collider.gameObject);
            //Si le hacen suficiente daño muere
            if (life <= 0)
            {
                worldControlx.enemigos.Remove(this.gameObject);
                Destroy(this.gameObject);

                //ganamos dinero
                worldControlx.monedas = worldControlx.monedas + dineroPorMuerte;


            }
        }

        //Si colisiona con una torreta cuerpo a cuerpo...
        if (collision.collider.gameObject.tag == "closeturret")
        {
            GameObject torreta = collision.collider.gameObject;
            int segundos = torreta.GetComponent<CloseTurretScript>().segundosBloqueo;
            torreta.GetComponent<CloseTurretScript>().closeObjectiveAt.GetComponent<ObjectiveScript>().ocupado = false;
            Destroy(torreta);
            soundManager.SelectAudio(3, 1);
            StartCoroutine(EnemigoBloqueado(segundos));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Giro")
        {
            Giro giro = collision.gameObject.GetComponent<Giro>();
            this.direccionMovimiento = giro.direccionGiro;
            if (this.direccionMovimiento == Giro.direccion.arriba)
            {
                rb2d.velocity = new Vector2(0, velocidad);
            }
            else if (this.direccionMovimiento == Giro.direccion.abajo)
            {
                rb2d.velocity = new Vector2(0, -velocidad);
            }
            else if (this.direccionMovimiento == Giro.direccion.izquierda)
            {
                rb2d.velocity = new Vector2(-velocidad, 0);
            }
            else if (this.direccionMovimiento == Giro.direccion.derecha)
            {
                rb2d.velocity = new Vector2(velocidad, 0);
            }
        }

        if (collision.gameObject.tag == "end")
        {
            camara.GetComponent<worldControl>().quitarVida(2);
            Destroy(this.gameObject);
        }
    }

    IEnumerator EnemigoBloqueado(int segundos)
    {   
        velocidad = 0;
        if (this.direccionMovimiento == Giro.direccion.arriba)
        {
            rb2d.velocity = new Vector2(0, velocidad);
        }
        else if (this.direccionMovimiento == Giro.direccion.abajo)
        {
            rb2d.velocity = new Vector2(0, -velocidad);
        }
        else if (this.direccionMovimiento == Giro.direccion.izquierda)
        {
            rb2d.velocity = new Vector2(-velocidad, 0);
        }
        else if (this.direccionMovimiento == Giro.direccion.derecha)
        {
            rb2d.velocity = new Vector2(velocidad, 0);
        }
        yield return new WaitForSeconds(segundos);
        velocidad = velocidadAux;
        if (this.direccionMovimiento == Giro.direccion.arriba)
        {
            rb2d.velocity = new Vector2(0, velocidad);
        }
        else if (this.direccionMovimiento == Giro.direccion.abajo)
        {
            rb2d.velocity = new Vector2(0, -velocidad);
        }
        else if (this.direccionMovimiento == Giro.direccion.izquierda)
        {
            rb2d.velocity = new Vector2(-velocidad, 0);
        }
        else if (this.direccionMovimiento == Giro.direccion.derecha)
        {
            rb2d.velocity = new Vector2(velocidad, 0);
        }
    }
}
