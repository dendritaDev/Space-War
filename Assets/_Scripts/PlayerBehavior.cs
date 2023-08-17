using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float playerSpeed;

    private float currentspeed = 0.0f;

    private Vector3 lastMovement = new();

    public Transform laser;
    public float laserDistance = 0.1f; //Distancia por delante de la nave que sale
    public float timeBetweenFires = 0.3f;
    private float timeUntilNextFire = 0.0f;
    public List<KeyCode> shootButton = new List<KeyCode>();

    public AudioClip shootSound;
    private AudioSource audioSource;

    AmmoManager ammoManager;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ammoManager = FindObjectOfType<AmmoManager>();
        if (ammoManager == null)
        {
            Debug.LogError("No se ha encontrado el componente AmmoManager en la escena.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenuBehavior.isPaused)
        {
            Rotate();
            Move();
            Fire();
        }
     
    }

    void Rotate()
    {
        Vector3 worldPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);

        Vector3 spaceShipPos = this.transform.position;

        //Calculamos la distancia en x e y desde la nave a la posicion del raton
        float dx = spaceShipPos.x - worldPos.x;
        float dy = spaceShipPos.y - worldPos.y;

        //Calculamos el angulo que hay entre dx y dy para saber cuanto tiene que rotar

        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg; //Mathf siempre fuciona con radianes por eso multiplicamos por Rad2Deg

        Quaternion rot = Quaternion.Euler(new Vector3(0,0,angle + 90)); //+90 por la diferencia de donde se empiezan a contar los angnulos, unity empieza en verticla y el calculo que hemos hecho
                                                                        //de arctan2 desde el horizontal

        this.transform.rotation = rot;


    }

    private void Move()
    {
        Vector3 movement = new Vector3();

        movement.x += Input.GetAxis("Horizontal");
        movement.y += Input.GetAxis("Vertical");

        movement.Normalize();
        
        if(movement.magnitude > 0)
        {
            currentspeed = playerSpeed;

            //X = V * T
            this.transform.Translate(movement * Time.deltaTime * currentspeed, Space.World);

            lastMovement = movement;
        }
        else //Inercia del último movimiento
        {
            this.transform.Translate(movement * Time.deltaTime * currentspeed, Space.World);
            currentspeed *= 0.975f; //Irá disminuyendo un 10% cada frame
        }
    }

    void Fire()
    {
        foreach(KeyCode key in shootButton)
        {
            if(Input.GetKey(key) && timeUntilNextFire < 0)
            {
                timeUntilNextFire = timeBetweenFires;
                ShootLaser();
                break;
            }
        }

        timeUntilNextFire -= Time.deltaTime;
    }

    void ShootLaser()
    {
        audioSource.PlayOneShot(shootSound);

        Vector3 laserPos = this.transform.position;

        float rotationAngle = this.transform.localEulerAngles.z - 90; //90 para compensar la diferencia

       //laserPos.x += Mathf.Cos(rotationAngle * Mathf.Deg2Rad) * laserDistance;
       //laserPos.y += Mathf.Sin(rotationAngle * Mathf.Deg2Rad) * laserDistance;


        ammoManager.Shoot(laserPos, this.transform.rotation);

    }


}
