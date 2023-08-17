using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int health = 2;

    public Transform explosion;

    public AudioClip hitSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name.Contains("Laser"))
        {
            LaserBehavior laser = collision.gameObject.GetComponent("LaserBehavior") as LaserBehavior;
            health -= laser.damage;
            collision.gameObject.SetActive(false);

            GetComponent<AudioSource>().PlayOneShot(hitSound); //si solo usara 1 audio, se podia utilizar directamente la funcion Play() y se llamaria al que esta asignado en audiosource
        }
        
        if(health <= 0)
        {
            Destroy(this.gameObject);

            GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            controller.KillEnemy();
            controller.IncreaseScore(10);
            if(explosion != null)
            {
                GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;

                Destroy(exploder, 2.0f);
            }
        }

        if(collision.gameObject.name.Contains("nave"))
        {

        }



    }

   
}
