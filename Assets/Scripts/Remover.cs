using UnityEngine;
using System.Collections;

public class Remover : MonoBehaviour
{
	public GameObject splash;


	void OnTriggerEnter2D(Collider2D col)
	{

        if(col.gameObject.tag == "BoomManager") return;

		// If the player hits the trigger...
		if(col.gameObject.tag == "Player")
		{
		    var playerControl = col.gameObject.GetComponent<PlayerHealth>();

            // .. stop the Health Bar following the player
            if (playerControl.HealthBar.gameObject.activeSelf)
			{
                playerControl.HealthBar.gameObject.SetActive(false);
			}

			// ... instantiate the splash where the player falls in.
			Instantiate(splash, col.transform.position, transform.rotation);
			// ... destroy the player.
			Destroy (col.gameObject);
		}
		else
		{
			// ... instantiate the splash where the enemy falls in.
			Instantiate(splash, col.transform.position, transform.rotation);

			// Destroy the enemy.
			Destroy (col.gameObject);	
		}
	}

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
	}
}
