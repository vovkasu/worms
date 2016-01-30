using UnityEngine;

public class TextState : MonoBehaviour
{
    public TextStateShadow TextStateShadow;
    private GUIText _textView;


    void Awake ()
	{
	    _textView = GetComponent<GUIText>();
	}


    public void SetText(string text)
    {
        _textView.text = text;
        TextStateShadow.SetText(text);
    }

}
