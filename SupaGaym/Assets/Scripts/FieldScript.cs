using UnityEngine;

namespace Assets.Scripts
{
    public class FieldScript : MonoBehaviour
    {
        private const string _NAME_SQUARE_SPRITE_RENDERER = "MouseHoverGameObject";
        private static Color _baseColor = Color.black;
        private static float _colorTransitionValue = 0.18f;
        
        private SpriteRenderer _spriteRenderer;


        // Start is called before the first frame update
        void Awake()
        {
            //if (_baseColor == null)
            //{
            //    ColorUtility.TryParseHtmlString(_BASE_COLOR_HEXCODE, out _baseColor);
            //}
            
            for(int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (gameObject.transform.GetChild(i).gameObject.name == _NAME_SQUARE_SPRITE_RENDERER)
                {
                    _spriteRenderer = gameObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnMouseEnter()
        {
            //_spriteRenderer.color = Color.Lerp(_baseColor, Color.black, _colorTransitionValue);
            _spriteRenderer.color = new Color(_baseColor.r, _baseColor.g, _baseColor.b, 0.35f);
        }

        private void OnMouseExit()
        {
            _spriteRenderer.color = new Color(_baseColor.r, _baseColor.g, _baseColor.b, 0f);
        }
    }
}

