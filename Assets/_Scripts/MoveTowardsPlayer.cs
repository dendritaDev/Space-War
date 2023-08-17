using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    private Transform player;

    public float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuBehavior.isPaused)
        {
            Vector3 direction = player.position - this.transform.position;
            direction.Normalize();

            this.transform.position += direction * speed * Time.deltaTime;
        }
            


    }
}
