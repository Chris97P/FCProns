using UnityEngine;

namespace Assets.Scripts
{
    public class FieldScript : MonoBehaviour
    {
        private const string _NAME_MOUSE_HOVER_GAME_OBJECT = "MouseHover";
        private const string _NAME_MAIN_COLOR_GAME_OBJECT = "MainColor";
        
        private static Color _baseColorTransparency = Color.black;
        private static Color _baseColorEdge = Color.black;

        private GameObject _edgeChosenGameObject;
        private PlayerScript _playerScriptInstance;
        private bool _isChosen = false;
        private bool _isHighlighted = false;

        public int _arrayPosX;
        public int _arrayPosY;
        public PlayerScript PlayerScriptInstance
        {
            get { return _playerScriptInstance; }
            set { _playerScriptInstance = value; }
        }
        private SpriteRenderer _mouseHoverSpriteRenderer;
        private SpriteRenderer _mainColorSpriteRenderer;

        // Start is called before the first frame update
        void Awake()
        {
            _mainColorSpriteRenderer = gameObject.transform.Find(_NAME_MAIN_COLOR_GAME_OBJECT).GetComponent<SpriteRenderer>();
            _mouseHoverSpriteRenderer = gameObject.transform.Find(_NAME_MOUSE_HOVER_GAME_OBJECT).GetComponent<SpriteRenderer>();
            _edgeChosenGameObject = gameObject.transform.Find("EdgeChosen").gameObject;
        }

        private void OnMouseEnter()
        {
            //_spriteRenderer.color = Color.Lerp(_baseColor, Color.black, _colorTransitionValue);
            _mouseHoverSpriteRenderer.color = new Color(_baseColorTransparency.r, _baseColorTransparency.g, _baseColorTransparency.b, 0.35f);
        }

        private void OnMouseExit()
        {
            _mouseHoverSpriteRenderer.color = new Color(_baseColorTransparency.r, _baseColorTransparency.g, _baseColorTransparency.b, 0f);
        }

        private void OnMouseDown()
        {
            //ToDo:
            //bei Click auf eigenes Feld das alte Expansion Feld inkl. Nachbarn abwählen und das neue wählen (Tausch vom Expansion Feld)

            Debug.Log($"{name} clicked");

            #region ExpansionActivateMode
            if (GameSceneCoreScript.ActivePlayer == _playerScriptInstance)
            {
                ChooseFieldToStartExpansion();
                HighlightDirectNeighbor();
                HighLightIndirectNeighbor();
            }
            #endregion

            #region ExpansionChooseMode
            if (_isHighlighted)
            {
                AssignFieldToPlayer(GameSceneCoreScript.ActivePlayer);
            }
            #endregion
        }

        public void AssignFieldToPlayer(PlayerScript playerInstance)
        {
            _mainColorSpriteRenderer.color = playerInstance.Color;
            PlayerScriptInstance = playerInstance;
        }

        private void ChooseFieldToStartExpansion()
        {            
            if (!_isChosen)
            {
                _isChosen = true;
                LoopThroughGameObjectChildsAndSetSpriteRendererColor(_edgeChosenGameObject, Color.white);
                GameSceneCoreScript.GameMode = GlobalCore.GameMode.ExpansionMode;
            }
            else
            {
                _isChosen = false;
                LoopThroughGameObjectChildsAndSetSpriteRendererColor(_edgeChosenGameObject, _baseColorEdge);
                GameSceneCoreScript.GameMode = GlobalCore.GameMode.PlayMode;
            }
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

                        FieldScript fieldScriptToCheck = GameSceneCoreScript.FieldManagerScriptInstance.FieldArray[_arrayPosX + x, _arrayPosY + y].GetComponent<FieldScript>();
                        if (fieldScriptToCheck._playerScriptInstance is null)
                        {
                            GameObject edgeChosenNeighborGameObject = GameSceneCoreScript.FieldManagerScriptInstance.FieldArray[_arrayPosX + x, _arrayPosY + y].transform.Find("EdgeChosen").gameObject;
                            if (!fieldScriptToCheck._isHighlighted)
                            {
                                fieldScriptToCheck._isHighlighted = true;
                                LoopThroughGameObjectChildsAndSetSpriteRendererColor(edgeChosenNeighborGameObject, _playerScriptInstance.Color);
                            }
                            else
                            {
                                fieldScriptToCheck._isHighlighted = false;
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

                    FieldScript fieldScriptToCheck = GameSceneCoreScript.FieldManagerScriptInstance.FieldArray[_arrayPosX, _arrayPosY + y].GetComponent<FieldScript>();
                    if (fieldScriptToCheck._playerScriptInstance is null)
                    {
                        GameObject edgeChosenNeighborGameObject = GameSceneCoreScript.FieldManagerScriptInstance.FieldArray[_arrayPosX, _arrayPosY + y].transform.Find("EdgeChosen").gameObject;
                        if (!fieldScriptToCheck._isHighlighted)
                        {
                            fieldScriptToCheck._isHighlighted = true;
                            LoopThroughGameObjectChildsAndSetSpriteRendererColor(edgeChosenNeighborGameObject, Color.yellow);
                        }
                        else
                        {
                            fieldScriptToCheck._isHighlighted = false;
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

                    FieldScript fieldScriptToCheck = GameSceneCoreScript.FieldManagerScriptInstance.FieldArray[_arrayPosX + x, _arrayPosY].GetComponent<FieldScript>();
                    if (fieldScriptToCheck._playerScriptInstance is null)
                    {
                            GameObject edgeChosenNeighborGameObject = GameSceneCoreScript.FieldManagerScriptInstance.FieldArray[_arrayPosX + x, _arrayPosY].transform.Find("EdgeChosen").gameObject;
                            if (!fieldScriptToCheck._isHighlighted)
                            {
                                fieldScriptToCheck._isHighlighted = true;
                                LoopThroughGameObjectChildsAndSetSpriteRendererColor(edgeChosenNeighborGameObject, Color.yellow);
                            }
                            else
                            {
                                fieldScriptToCheck._isHighlighted= false;
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

