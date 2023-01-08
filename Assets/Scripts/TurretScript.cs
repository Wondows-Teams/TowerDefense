using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool inObjective;
    public GameObject objectiveAt;
    public GameObject enemyShooting;
    public GameObject ballPrefab; // El prefab de la bola que quieres disparar
    public float shootInterval = 3.0f; // El intervalo entre disparos, en segundos
    public bool shooting;
    public worldControl worldControl;
    public float maxDistance;
    public float shootForce;
    public bool tripleShoot;
    public float angleTriple;
    [SerializeField] GameSoundManager audioManager;

    void Start()
    {
        worldControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<worldControl>();
        inObjective = false;
        shooting = false;
        audioManager = GameObject.Find("SoundManager").GetComponent<GameSoundManager>();    
    }
    public void startShotting()
    {
        // Usa InvokeRepeating para llamar al método "ShootBall" cada "shootInterval" segundos, empezando después de 0 segundos
        InvokeRepeating("ShootBall", 0, shootInterval);
        shooting = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Si esta disparando les apunta
        if (shooting && enemyShooting != null)
        {
            Vector3 direction = (enemyShooting.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    public GameObject FindClosest(Vector2 position, GameObject[] objects)
    {
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                float distance = Vector2.Distance(position, obj.transform.position);
                if (distance < closestDistance)
                {
                    closest = obj;
                    closestDistance = distance;
                }
            }
           
        }

        return closest;
    }
    void ShootBall()
    {
        //Compruebo si el enemigo objetivo esta demasiado lejos solo si sigue vivo
        if(enemyShooting != null)
        {

            float distanceObjective = Vector2.Distance(this.transform.position, enemyShooting.transform.position);
            if(distanceObjective > maxDistance)
            {
                enemyShooting = null;
            } 
        }

        //Si no tengo objetivo actual eligo como objetivo al enemigo mas cercano
        if (enemyShooting == null)
        {
           enemyShooting = FindClosest(this.transform.position,worldControl.enemigos.ToArray());
        }

        //Compruebo si el nuevo enemigo seleccionado esta demasiado lejos
        if (enemyShooting != null)
        {
            float distanceObjective2 = Vector2.Distance(this.transform.position, enemyShooting.transform.position);
            if (distanceObjective2 > maxDistance)
            {
                enemyShooting = null;

            }
        }
        


        if (enemyShooting != null)
        {
            //Rota 
            Vector3 direction = (enemyShooting.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);


            // Instancia una nueva bola como hijo del GameObject
            GameObject ball = Instantiate(ballPrefab, this.transform.position, Quaternion.identity);
            audioManager.SelectAudio(0, 1);

            // Añade una fuerza a la bola para que salga disparada
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * 0.005f*shootForce);

            //Si hay disparo triple entonces se disparan dos bolas mas
            if (tripleShoot)
            {
                //shoot2
                // Instancia una nueva bola como hijo del GameObject
                GameObject ball2 = Instantiate(ballPrefab, this.transform.position, Quaternion.identity);
                // Añade una fuerza a la bola para que salga disparada
                Rigidbody2D rb2 = ball2.GetComponent<Rigidbody2D>();
                Vector3 aux = transform.right;
                aux = Quaternion.Euler(0, 0, angleTriple) * aux;
                rb2.AddForce(aux * 0.005f * shootForce);

                //shoot3
                // Instancia una nueva bola como hijo del GameObject
                GameObject ball3 = Instantiate(ballPrefab, this.transform.position, Quaternion.identity);
                // Añade una fuerza a la bola para que salga disparada
                Rigidbody2D rb3 = ball3.GetComponent<Rigidbody2D>();
                Vector3 aux2 = transform.right;
                aux2 = Quaternion.Euler(0, 0, -angleTriple) * aux2;
                rb3.AddForce(aux2 * 0.005f * shootForce);

            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "objective")
        {
            inObjective = true;
            objectiveAt = collision.gameObject;
        }

       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "objective")
        {
            inObjective = false;
        }


    }
}
