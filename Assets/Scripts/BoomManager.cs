using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BoomManager : MonoBehaviour
    {
        public List<BoomTemplate> BoomTemplates = new List<BoomTemplate>();

        public static BoomManager Instance;
        private readonly List<CircleCollider2D> _colliders = new List<CircleCollider2D>();
        public bool DestroyColliders = false;

        private void Awake()
        {
            Instance = this;
        }

        void Update()
        {

            if (DestroyColliders)
            {
                DestroyColliders = false;
                Debug.Log("Destroy collider");
                for (var i = 0; i < _colliders.Count; i++)
                {
                    var circleCollider2D = _colliders[i];
                    Destroy(circleCollider2D);
                }
            }
        }

        public void AddBoom(Vector3 mouseWordPosition)
        {
            var circleCollider2D = gameObject.AddComponent<CircleCollider2D>();
            circleCollider2D.offset = mouseWordPosition;
            circleCollider2D.radius = 1;
            circleCollider2D.isTrigger = true;
            _colliders.Add(circleCollider2D);
            Debug.Log("Added collider");
        }
    }
}