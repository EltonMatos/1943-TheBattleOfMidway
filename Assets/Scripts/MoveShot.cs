﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShot : MonoBehaviour
{
    private float vel = 9;
    [SerializeField]
    private int direction = 0;

    public GameObject explosionShot;
    

    public float Vel
    {
        get { return vel; }
        set { vel = value; }
    }

    
    void Move()
    {
        if (PlaneController.instance.up == Upgrades.CommonShot || PlaneController.instance.up == Upgrades.SuperShell)
        {
            Vector3 aux = transform.position;
            aux.y += vel * Time.deltaTime;
            transform.position = aux;
        }

        if (PlaneController.instance.up == Upgrades.Auto )
        {
            Vector3 aux = transform.position;
            aux.y += 0.25f;
            transform.position = aux;
        }

        if (PlaneController.instance.up == Upgrades.ShotGun || PlaneController.instance.up == Upgrades.WayShot)
        {
            //direita
            if (direction == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, PlaneController.instance.targetShotRight.position, 0.2f);
            }
            //esquerda
            if(direction == 2)
            {
                transform.position = Vector2.MoveTowards(transform.position, PlaneController.instance.targetShotLeft.position, 0.2f);                
            }
            //centralizado
            if(direction == 0)
            {
                Vector3 aux = transform.position;                
                aux.y += vel * Time.deltaTime;
                transform.position = aux;
            }
        }
    }

    private void Update()
    {
        if(OndeEstou.instance.fase == 1) Move();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("TiroInimigo") || col.gameObject.CompareTag("TiroInimigo2"))
        {
            GameObject shot = Instantiate(explosionShot, transform.position, Quaternion.identity) as GameObject;
            Destroy(col.gameObject);
            StartCoroutine(ExplodeShot(shot));
        }
    }

    IEnumerator ExplodeShot(GameObject shot)
    {
        yield return new WaitForSeconds(2f);
        Destroy(shot);
    }
}
