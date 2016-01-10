using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class DestoyableSprite : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;
    private PolygonCollider2D _polygonCollider2D;
    public Shader ShaderMask;
    public Texture2D AlphaMaskTexture;
    private Texture2D _coloredTexture;

    // Use this for initialization

    void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _coloredTexture = _spriteRenderer.sprite.texture;
    }


    void Start ()
	{
	    _polygonCollider2D = gameObject.GetComponent<PolygonCollider2D>();
	    if (_polygonCollider2D == null)
	    {
	        _polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
        }
        AlphaMaskTexture =  MakeAlphaMaskTexture(_coloredTexture);
        var sprite = Sprite.Create(
            AlphaMaskTexture,
            new Rect(0, 0, AlphaMaskTexture.width, AlphaMaskTexture.height), 
            new Vector2(0.5f, 0.5f), 
            40);

        _spriteRenderer.material = new Material(ShaderMask);
        _spriteRenderer.sprite = sprite;

        var materialPropertyBlock = new MaterialPropertyBlock();
        materialPropertyBlock.SetTexture("_ColoredTex", _coloredTexture);
        materialPropertyBlock.SetTexture("_MainTex", AlphaMaskTexture);
        _spriteRenderer.SetPropertyBlock(materialPropertyBlock);
	}

    private Texture2D MakeAlphaMaskTexture( Texture2D sourceTexture)
    {

        var targetTexture2D = new Texture2D(sourceTexture.width, sourceTexture.height, TextureFormat.Alpha8, false);

        for (int i = 0; i < sourceTexture.width; i++)
        {
            for (int j = 0; j < sourceTexture.height; j++)
            {
                var pixel = sourceTexture.GetPixel(i,j);
                targetTexture2D.SetPixel(i,j,new Color(0,0,0,pixel.a));
            }
        }

        targetTexture2D.Apply();
        return targetTexture2D;
    }

    // Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("OnTriggerStay2D "+other.name, other.gameObject);
        BoomManager.Instance.DestroyColliders = true;

        var circleCollider2D = other as CircleCollider2D;
        if (circleCollider2D == null)
        {
            Debug.LogError("Where circle collider?",gameObject);
            return;
        }

        var circleColliderCenter = circleCollider2D.offset;
        Debug.Log("circleCollider2D center:"+circleColliderCenter+" "+circleCollider2D.radius);

        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Where SpriteRenderer?", gameObject);
            return;
        }

        Vector2 spriteRendererCenter = spriteRenderer.bounds.center;
        Vector2 spriteRendererSize = spriteRenderer.bounds.size;
        Debug.Log("spriteRenderer center:"+spriteRendererCenter+" "+spriteRendererSize);

        var pixelPerUnit = Screen.width/Camera.main.orthographicSize;
        Debug.Log("spriteRenderer pixels:" + spriteRendererSize*pixelPerUnit);

        var spriteRendererLeftBottom = (spriteRendererCenter - spriteRendererSize*0.5f);

        var spriteScale = AlphaMaskTexture.height/spriteRendererSize.y;


        var spriteTapPosition = (circleColliderCenter - spriteRendererLeftBottom)* spriteScale;

        for (var x = (int) spriteTapPosition.x; x <= 100 + spriteTapPosition.x; x++)
        {
            for (var y = (int) spriteTapPosition.y; y <= 100 + spriteTapPosition.y; y++)
            {
                var pixel32 = AlphaMaskTexture.GetPixel(x, y);
                AlphaMaskTexture.SetPixel(x, y, Color.clear);
            }
        }
        AlphaMaskTexture.Apply();

        Destroy(_polygonCollider2D);
        _polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
    }
}
