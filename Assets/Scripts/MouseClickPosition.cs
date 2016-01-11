using UnityEngine;
using Assets.Scripts;

public class MouseClickPosition : MonoBehaviour {
    private Camera _mainCamera;
    public int BoomId = 0;

    // Use this for initialization
	void Start ()
	{
	    _mainCamera = Camera.main;
	}

    // Update is called once per frame
	void Update () {

	    if (Input.GetButtonDown("Fire1"))
	    {
	        Debug.Log(Input.mousePosition.ToString(),this);
	        var mouseWordPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
	        BoomManager.Instance.AddBoom(mouseWordPosition,BoomId);
	    }
	}
}
