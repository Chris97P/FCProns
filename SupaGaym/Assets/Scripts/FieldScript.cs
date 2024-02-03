using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class FieldScript : MonoBehaviour
    {
        private const string _NAME_SQUARE_SPRITE_RENDERER = "MouseHoverGameObject";
        
        private static Color _baseColorTransparency = Color.black;
        private static float _colorTransitionValue = 0.18f;
        private static Color _baseColorEdge = Color.black;

        private GameObject _edgeGameObject;
        private PlayerScript _playerScriptInstance;
        private bool _isChosen = false;
        public PlayerScript PlayerScriptInstance
        {
            get { return _playerScriptInstance; }
            set { _playerScriptInstance = value; }
        }
        private SpriteRenderer _spriteRenderer;


        // Start is called before the first frame update
        void Awake()
        {
            _spriteRenderer = gameObject.transform.Find(_NAME_SQUARE_SPRITE_RENDERER).GetComponent<SpriteRenderer>();
            _edgeGameObject = gameObject.transform.Find("Edge").gameObject;
        }

        private void OnMouseEnter()
        {
            //_spriteRenderer.color = Color.Lerp(_baseColor, Color.black, _colorTransitionValue);
            _spriteRenderer.color = new Color(_baseColorTransparency.r, _baseColorTransparency.g, _baseColorTransparency.b, 0.35f);
        }

        private void OnMouseExit()
        {
            _spriteRenderer.color = new Color(_baseColorTransparency.r, _baseColorTransparency.g, _baseColorTransparency.b, 0f);
        }

        private void OnMouseDown()
        {
            Debug.Log($"{name} clicked");

            if (GameSceneCoreScript.ActivePlayer == _playerScriptInstance)
            {
                SpriteRenderer[] spriteRendererArray = _edgeGameObject.transform.GetComponentsInChildren<SpriteRenderer>();

                for (int i = 0; i < spriteRendererArray.Length; i++)
                {
                    if (!_isChosen)
                        SetSpriteRendererColor(spriteRendererArray[i], _playerScriptInstance.Color);
                    else
                        SetSpriteRendererColor(spriteRendererArray[i], _baseColorEdge);                
                }
                _isChosen = !_isChosen;
            }
        }

        private void SetSpriteRendererColor(SpriteRenderer spriteRenderer, Color color)
        {
            spriteRenderer.color = color;
        }
    }
}

