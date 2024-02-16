using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CanvasManagerScript : MonoBehaviour
    {
        private GameObject _canvasGameObject;
        public RawImage ActivePlayerRawImage;

        private void Awake()
        {
            _canvasGameObject = Instantiate(Resources.Load<GameObject>("Prefabs/CanvasPrefab"));
            ActivePlayerRawImage = _canvasGameObject.transform.Find("ActivePlayerDisplayGameObject").gameObject.transform
                                                               .Find("ActivePlayerImageGameObject").gameObject.GetComponent<RawImage>();
        }
    }
}
