using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseClickPosition : MonoBehaviour {
    private Camera _mainCamera;
    private List<CircleCollider2D> _colliders = new List<CircleCollider2D>();
    public bool DestroyColliders = false;

    // Use this for initialization
	void Start ()
	{
	    _mainCamera = Camera.main;
	}

    // Update is called once per frame
	void Update () {

	    if (DestroyColliders)
	    {
	        DestroyColliders = false;
            Debug.Log("Destroy collider");
	        for(var i = 0; i < _colliders.Count; i++)
	        {
	            var circleCollider2D = _colliders[i];
	            Destroy(circleCollider2D);
	        }
	    }

	    if (Input.GetButtonDown("Fire1"))
	    {
	        Debug.Log(Input.mousePosition.ToString(),this);
	        var mouseWordPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
	        var circleCollider2D = gameObject.AddComponent<CircleCollider2D>();
            circleCollider2D.offset= mouseWordPosition;
	        circleCollider2D.radius = 1;
	        circleCollider2D.isTrigger = true;
            _colliders.Add(circleCollider2D);
            Debug.Log("Added collider");
	    }
	}
}
