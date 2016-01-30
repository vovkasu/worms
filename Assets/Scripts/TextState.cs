using UnityEngine;

public class TextState : MonoBehaviour
{
    public TextStateShadow TextStateShadow;
    public GUIText TextView;


    void Awake ()
	{
	    TextView = GetComponent<GUIText>();
	}


    public void SetText(string text)
    {
        TextView.text = text;
        TextStateShadow.SetText(text);
    }

}
