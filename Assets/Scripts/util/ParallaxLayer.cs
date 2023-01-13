using UnityEngine;

public class ParallaxLayer : MonoBehaviour
    {
        public Vector3 movementScale = Vector3.one;

        private float width;
        private float cameraWidth;
        private Transform _camera;

        void Awake()
        {
            _camera = Camera.main.transform;
            width = GetComponent<SpriteRenderer>().bounds.size.x;
            var cameraBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
            cameraWidth = cameraBounds.x;
        }

        void LateUpdate()
        {
            if (_camera.position.x > transform.position.x + width) {
                
                transform.position = new Vector3(_camera.position.x + 19, transform.position.y, transform.position.z);
            } else {
                //transform.position = Vector3.Scale(_camera.position, movementScale);
            }
        }

    }