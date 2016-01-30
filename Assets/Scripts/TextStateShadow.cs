using UnityEngine;

public class TextStateShadow : MonoBehaviour
{
    private GUIText _textView;


    void Awake()
	{
	    _textView = GetComponent<GUIText>();
	}

    public void SetText(string text)
    {
        _textView.text = text;
    }
}
