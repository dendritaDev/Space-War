using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    public float speed = 6.0f;

    public int damage = 1;

    public float lifeTime = 3.0f;

    private void Update()
    {
        this.transform.Translate(Vector3.up * speed * Time.deltaTime); //El vector3.forward en 2d es el up
    }

    private void OnEnable()
    {
        Invoke(nameof(Die), lifeTime);
    }

    void Die()
    {
        gameObject.SetActive(false);
    }


}
