﻿using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Rigidbody2D rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.
    public GameObject Bazooka;


    private PlayerControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.
    private bool _needUpGun;
    private bool _needDownGun;


    void Awake()
	{
		// Setting up the references.
		anim = transform.root.gameObject.GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerControl>();
	}


	void Update ()
	{
		// If the fire button is pressed...
		if(Input.GetButtonDown("Fire1"))
		{
			Fire();
		}

	    if (_needUpGun)
	    {
	        var localEulerAngles = Bazooka.transform.localEulerAngles;
            localEulerAngles+=new Vector3(0,0,1f);
	        Bazooka.transform.localEulerAngles = localEulerAngles;
	    }

        if (_needDownGun)
        {
            var localEulerAngles = Bazooka.transform.localEulerAngles;
            localEulerAngles += new Vector3(0, 0, -1f);
            Bazooka.transform.localEulerAngles = localEulerAngles;
        }

    }

    public void StartUpGun()
    {
        _needUpGun = true;
    }

    public void StopUpGun()
    {
        _needUpGun = false;
    }

    public void StartDownGun()
    {
        _needDownGun = true;
    }

    public void StopDownGun()
    {
        _needDownGun = false;
    }


    public void Fire()
    {
// ... set the animator Shoot trigger parameter and play the audioclip.
        anim.SetTrigger("Shoot");
        GetComponent<AudioSource>().Play();

        var bazookaAngleRad = Bazooka.transform.eulerAngles.z*Mathf.Deg2Rad;

        // If the player is facing right...
        if (playerCtrl.facingRight)
        {
            // ... instantiate the rocket facing right and set it's velocity to the right. 
            Rigidbody2D bulletInstance =
                Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            bulletInstance.velocity = new Vector2(Mathf.Cos(bazookaAngleRad), Mathf.Sin(bazookaAngleRad))*speed;
        }
        else
        {
            // Otherwise instantiate the rocket facing left and set it's velocity to the left.
            Rigidbody2D bulletInstance =
                Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
            bulletInstance.velocity = new Vector2(-Mathf.Cos(bazookaAngleRad), Mathf.Sin(bazookaAngleRad))*speed;
        }
    }
}
