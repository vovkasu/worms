using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BoomManager : MonoBehaviour
    {
        public List<BoomTemplate> BoomTemplates = new List<BoomTemplate>();

        public static BoomManager Instance;
        private readonly Dictionary<CircleCollider2D, int> _collidersIdMap = new Dictionary<CircleCollider2D, int>();
        public bool DestroyColliders = false;
        private Camera _mainCamera;
        public float _pixelPerUnit;

        private void Awake()
        {
            Instance = this;
            _mainCamera = Camera.main;
            _pixelPerUnit = Screen.width / _mainCamera.orthographicSize;

        }

        void Update()
        {

            if (DestroyColliders)
            {
                DestroyColliders = false;
                Debug.Log("Destroy collider");

                foreach (var circleCollider2D in _collidersIdMap.Keys)
                {
                    Destroy(circleCollider2D);
                }
                _collidersIdMap.Clear();

            }
        }

        public void AddBoom(Vector3 mouseWordPosition, int boomId)
        {
            var boomTemplate = BoomTemplates[boomId];
            var circleCollider2D = gameObject.AddComponent<CircleCollider2D>();
            circleCollider2D.offset = mouseWordPosition;
            circleCollider2D.radius = boomTemplate.ColliderDiameterPixels*0.5f/_pixelPerUnit;
            circleCollider2D.isTrigger = true;
            _collidersIdMap.Add(circleCollider2D, boomId);
            Debug.Log("Added collider");
        }

        public BoomTemplate GeTemplate(CircleCollider2D circleCollider2D)
        {
            return BoomTemplates[_collidersIdMap[circleCollider2D]];
        }
    }
}