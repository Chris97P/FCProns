using System;
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

        private GameObject _edgeMainGameObject;
        private GameObject _edgeChosenGameObject;
        private PlayerScript _playerScriptInstance;
        private bool _isChosen = false;
        private bool _isInExpansionMode = false;

        public int _arrayPosX;
        public int _arrayPosY;
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
            _edgeMainGameObject = gameObject.transform.Find("EdgeMain").gameObject;
            _edgeChosenGameObject = gameObject.transform.Find("EdgeChosen").gameObject;
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
                ChooseFieldToStartExpansion();
                HighlightDirectNeighbor();
                HighLightIndirectNeighbor();
            }


        }

        private void ChooseFieldToStartExpansion()
        {            
            if (!_isChosen)
            {
                LoopThroughGameObjectChildsAndSetSpriteRendererColor(_edgeChosenGameObject, Color.white);
                _isInExpansionMode = true;
            }
            else
            {
                LoopThroughGameObjectChildsAndSetSpriteRendererColor(_edgeChosenGameObject, _baseColorEdge);
                _isInExpansionMode = false;
            }
            _isChosen = !_isChosen;
        }

        private void HighlightDirectNeighbor()
        {
            for (int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    if (_arrayPosX + x < GlobalCore.FIELD_ARRAY_SIZE && _arrayPosY + y < GlobalCore.FIELD_ARRAY_SIZE)
                    {
                        if (_arrayPosX + x < 0 || _arrayPosY + y < 0) 
                            continue;

                        if (GameSceneCoreScript.FieldManagerScriptInstance.FieldArray[_arrayPosX + x, _arrayPosY + y].GetComponent<FieldScript>()._playerScriptInstance is null)
                        {
                            GameObject edgeChosenNeighborGameObject = GameSceneCoreScript.FieldManagerScriptInstance.FieldArray[_arrayPosX + x, _arrayPosY + y].transform.Find("EdgeChosen").gameObject;
                            if (_isInExpansionMode )
                            {
                                LoopThroughGameObjectChildsAndSetSpriteRendererColor(edgeChosenNeighborGameObject, _playerScriptInstance.Color);
                            }
                            else
                            {
                                LoopThroughGameObjectChildsAndSetSpriteRendererColor(edgeChosenNeighborGameObject, _baseColorEdge);
                            }
                        }
                    }
                }
            }
        }

        private void HighLightIndirectNeighbor()
        {
            for (int y = -2; y < 3; y += 4)
            {
                if (_arrayPosY + y < GlobalCore.FIELD_ARRAY_SIZE)
                {
                    if (_arrayPosY + y < 0)
                        continue;

                    if (GameSceneCoreScript.FieldManagerScriptInstance.FieldArray[_arrayPosX, _arrayPosY + y].GetComponent<FieldScript>()._playerScriptInstance is null)
                    {
                        GameObject edgeChosenNeighborGameObject = GameSceneCoreScript.FieldManagerScriptInstance.FieldArray[_arrayPosX, _arrayPosY + y].transform.Find("EdgeChosen").gameObject;
                        if (_isInExpansionMode)
                        {
                            LoopThroughGameObjectChildsAndSetSpriteRendererColor(edgeChosenNeighborGameObject, Color.yellow);
                        }
                        else
                        {
                            LoopThroughGameObjectChildsAndSetSpriteRendererColor(edgeChosenNeighborGameObject, _baseColorEdge);
                        }
                    }
                }

            }    
            for (int x = -2; x < 3; x += 4)
                {
                    if (_arrayPosX + x < GlobalCore.FIELD_ARRAY_SIZE)
                    {
                        if (_arrayPosX + x < 0)
                            continue;

                        if (GameSceneCoreScript.FieldManagerScriptInstance.FieldArray[_arrayPosX + x, _arrayPosY].GetComponent<FieldScript>()._playerScriptInstance is null)
                        {
                            GameObject edgeChosenNeighborGameObject = GameSceneCoreScript.FieldManagerScriptInstance.FieldArray[_arrayPosX + x, _arrayPosY].transform.Find("EdgeChosen").gameObject;
                            if (_isInExpansionMode)
                            {
                                LoopThroughGameObjectChildsAndSetSpriteRendererColor(edgeChosenNeighborGameObject, Color.yellow);
                            }
                            else
                            {
                                LoopThroughGameObjectChildsAndSetSpriteRendererColor(edgeChosenNeighborGameObject, _baseColorEdge);
                            }
                        }
                    }
                }            
        }

        private void LoopThroughGameObjectChildsAndSetSpriteRendererColor(GameObject gameObjectToLoopThrough, Color newColor)
        {
            SpriteRenderer[] spriteRendererArray = gameObjectToLoopThrough.transform.GetComponentsInChildren<SpriteRenderer>();

            for (int i = 0; i < spriteRendererArray.Length; i++)
            {
                SetSpriteRendererColor(spriteRendererArray[i], newColor);
            }
        }

        private void SetSpriteRendererColor(SpriteRenderer spriteRenderer, Color color)
        {
            spriteRenderer.color = color;
        }
    }
}

