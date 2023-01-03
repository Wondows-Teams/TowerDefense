using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int life;
    public float velocidad = 1.0f;
    public Giro.direccion direccionMovimiento;
    private GameObject camara;
    public int bulletDmged = 30;
    public worldControl worldControlx;
    public int dineroPorMuerte;
    // Start is called before the first frame update
    void Start()
    {
        worldControlx = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<worldControl>();
        camara = GameObject.FindGameObjectsWithTag("MainCamera")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (this.direccionMovimiento == Giro.direccion.izquierda)
        {
            transform.Translate(-velocidad, 0, 0);
        }
        else if (this.direccionMovimiento == Giro.direccion.derecha)
        {
            transform.Translate(velocidad, 0, 0);
        }
        else if (this.direccionMovimiento == Giro.direccion.arriba)
        {
            transform.Translate(0, velocidad, 0);
        }
        else if (this.direccionMovimiento == Giro.direccion.abajo)
        {
            transform.Translate(0,-velocidad, 0);
        }
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Giro")
        {
            Giro giro = collision.gameObject.GetComponent<Giro>();
            this.direccionMovimiento = giro.direccionGiro;
        }

        if (collision.gameObject.tag == "end")
        {
            camara.GetComponent<worldControl>().quitarVida(2);
            Destroy(this.gameObject);
        }
    }
}
