using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class FieldScript : MonoBehaviour
    {

        #region Konstanten
        private const string _NAME_MOUSE_HOVER_GAME_OBJECT = "MouseHover";
        private const string _NAME_MAIN_COLOR_GAME_OBJECT = "MainColor";
        #endregion

        #region Statische Variablen
        private static Color _baseColorTransparency = Color.black;
        private static Color _baseColorEdge = Color.black;
        private static Color _baseColorField = Color.white;
        #endregion

        #region Variablen
        private GameObject _edgeChosenGameObject;
        private PlayerScript _ownerPlayerScriptInstance;
        private GlobalCore.FieldStatus _fieldStatus = GlobalCore.FieldStatus.Unselected;
        private SpriteRenderer _mouseHoverSpriteRenderer;
        private SpriteRenderer _mainColorSpriteRenderer;
        private PlayerManagerScript _playerManagerScriptInstance;

        public int _arrayPosX;
        public int _arrayPosY;
        #endregion

        #region Properties
        public PlayerScript PlayerScriptInstance
        {
            get { return _ownerPlayerScriptInstance; }
            set { _ownerPlayerScriptInstance = value; }
        }
        #endregion


        // Start is called before the first frame update
        void Awake()
        {
            _playerManagerScriptInstance = GameSceneCoreScript.Instance.PlayerManagerScriptInstance;
            _mainColorSpriteRenderer = gameObject.transform.Find(_NAME_MAIN_COLOR_GAME_OBJECT).GetComponent<SpriteRenderer>();
            _mouseHoverSpriteRenderer = gameObject.transform.Find(_NAME_MOUSE_HOVER_GAME_OBJECT).GetComponent<SpriteRenderer>();
            _edgeChosenGameObject = gameObject.transform.Find("EdgeChosen").gameObject;
        }

        #region Events
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

            UnityEngine.Debug.Log($"{name} clicked");

            #region Aktionen wenn im PlayMode
            if (GameSceneCoreScript.Instance.GameMode == GlobalCore.GameMode.PlayMode)
            {
                if (GameSceneCoreScript.Instance.PlayerManagerScriptInstance.ActivePlayer == _ownerPlayerScriptInstance)
                {
                    SelectUnselectFieldToStartExpansion();
                }
            }
            #endregion

            #region Aktionen wenn im ExpansionMode
            else if (GameSceneCoreScript.Instance.GameMode == GlobalCore.GameMode.ExpansionMode)
            {
                if (_fieldStatus == GlobalCore.FieldStatus.DirectNeighbor)
                {
                    AssignFieldToPlayer(GameSceneCoreScript.Instance.PlayerManagerScriptInstance.ActivePlayer);
                    _playerManagerScriptInstance.SetActivePlayer(_playerManagerScriptInstance.GetNextPlayer());
                }
                else if (_fieldStatus == GlobalCore.FieldStatus.IndirectNeighbor)
                {
                    UnassignExpansionField();
                    AssignFieldToPlayer(GameSceneCoreScript.Instance.PlayerManagerScriptInstance.ActivePlayer);
                    _playerManagerScriptInstance.SetActivePlayer(_playerManagerScriptInstance.GetNextPlayer());
                }
                else if (_fieldStatus == GlobalCore.FieldStatus.ExpansionStart)
                {
                    SelectUnselectFieldToStartExpansion();
                }

                GameSceneCoreScript.Instance.GameMode = GlobalCore.GameMode.PlayMode;
            }
            #endregion
        }
        #endregion

        #region Methoden
        void UnassignExpansionField()
        {
            for (int y = 0; y < GlobalCore.FIELD_ARRAY_SIZE; y++)
            {
                for (int x = 0; x < GlobalCore.FIELD_ARRAY_SIZE; x++)
                {
                    if (GameSceneCoreScript.Instance.FieldManagerScriptInstance.FieldArray[x, y].GetComponent<FieldScript>()._fieldStatus == GlobalCore.FieldStatus.ExpansionStart)
                    {
                        GameSceneCoreScript.Instance.FieldManagerScriptInstance.FieldArray[x, y].GetComponent<FieldScript>().ResetField();
                    }
                }
            }
        }

        private void ResetField()
        {
            _ownerPlayerScriptInstance = null;
            SetSpriteRendererColor(_mainColorSpriteRenderer, _baseColorField);

            ResetHighlightedField();
        }

        public void ResetHighlightedField()
        {
            _fieldStatus = GlobalCore.FieldStatus.Unselected;
            LoopThroughGameObjectChildsAndSetSpriteRendererColor(_edgeChosenGameObject, _baseColorEdge);
        }

        public void AssignFieldToPlayer(PlayerScript playerInstance)
        {
            _mainColorSpriteRenderer.color = playerInstance.Color;
            PlayerScriptInstance = playerInstance;
        }

        private void SelectUnselectFieldToStartExpansion()
        {
            if (_fieldStatus == GlobalCore.FieldStatus.Unselected)
            {
                _fieldStatus = GlobalCore.FieldStatus.ExpansionStart;
                LoopThroughGameObjectChildsAndSetSpriteRendererColor(_edgeChosenGameObject, _baseColorField);
                HighlightUnhighligtDirectNeighbor();
                HighlightUnhighlightIndirectNeighbor();

                GameSceneCoreScript.Instance.GameMode = GlobalCore.GameMode.ExpansionMode;
            }
            else if (_fieldStatus == GlobalCore.FieldStatus.ExpansionStart)
            {
                _fieldStatus = GlobalCore.FieldStatus.Unselected;
                LoopThroughGameObjectChildsAndSetSpriteRendererColor(_edgeChosenGameObject, _baseColorEdge);

                HighlightUnhighligtDirectNeighbor();
                HighlightUnhighlightIndirectNeighbor();

                GameSceneCoreScript.Instance.GameMode = GlobalCore.GameMode.PlayMode;
            }
        }

        private void HighlightUnhighligtDirectNeighbor()
        {
            for (int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    if (_arrayPosX + x < GlobalCore.FIELD_ARRAY_SIZE && _arrayPosY + y < GlobalCore.FIELD_ARRAY_SIZE)
                    {
                        if (_arrayPosX + x < 0 || _arrayPosY + y < 0)
                            continue;

                        FieldScript fieldScriptToCheck = GameSceneCoreScript.Instance.FieldManagerScriptInstance.FieldArray[_arrayPosX + x, _arrayPosY + y].GetComponent<FieldScript>();
                        if (fieldScriptToCheck._ownerPlayerScriptInstance is null)
                        {
                            GameObject edgeChosenNeighborGameObject = GameSceneCoreScript.Instance.FieldManagerScriptInstance.FieldArray[_arrayPosX + x, _arrayPosY + y].transform.Find("EdgeChosen").gameObject;
                            if (fieldScriptToCheck._fieldStatus == GlobalCore.FieldStatus.Unselected)
                            {
                                fieldScriptToCheck._fieldStatus = GlobalCore.FieldStatus.DirectNeighbor;
                                LoopThroughGameObjectChildsAndSetSpriteRendererColor(edgeChosenNeighborGameObject, _ownerPlayerScriptInstance.Color);
                            }
                            else if (fieldScriptToCheck._fieldStatus == GlobalCore.FieldStatus.DirectNeighbor)
                            {
                                fieldScriptToCheck._fieldStatus = GlobalCore.FieldStatus.Unselected;
                                LoopThroughGameObjectChildsAndSetSpriteRendererColor(edgeChosenNeighborGameObject, _baseColorEdge);
                            }
                        }
                    }
                }
            }
        }

        private void HighlightUnhighlightIndirectNeighbor()
        {
            for (int y = -2; y < 3; y += 4)
            {
                if (_arrayPosY + y < GlobalCore.FIELD_ARRAY_SIZE)
                {
                    if (_arrayPosY + y < 0)
                        continue;

                    FieldScript fieldScriptToCheck = GameSceneCoreScript.Instance.FieldManagerScriptInstance.FieldArray[_arrayPosX, _arrayPosY + y].GetComponent<FieldScript>();
                    if (fieldScriptToCheck._ownerPlayerScriptInstance is null)
                    {
                        GameObject edgeChosenNeighborGameObject = GameSceneCoreScript.Instance.FieldManagerScriptInstance.FieldArray[_arrayPosX, _arrayPosY + y].transform.Find("EdgeChosen").gameObject;
                        if (fieldScriptToCheck._fieldStatus == GlobalCore.FieldStatus.Unselected)
                        {
                            fieldScriptToCheck._fieldStatus = GlobalCore.FieldStatus.IndirectNeighbor;
                            LoopThroughGameObjectChildsAndSetSpriteRendererColor(edgeChosenNeighborGameObject, Color.yellow);
                        }
                        else if (fieldScriptToCheck._fieldStatus == GlobalCore.FieldStatus.IndirectNeighbor)
                        {
                            fieldScriptToCheck._fieldStatus = GlobalCore.FieldStatus.Unselected;
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

                    FieldScript fieldScriptToCheck = GameSceneCoreScript.Instance.FieldManagerScriptInstance.FieldArray[_arrayPosX + x, _arrayPosY].GetComponent<FieldScript>();
                    if (fieldScriptToCheck._ownerPlayerScriptInstance is null)
                    {
                        GameObject edgeChosenNeighborGameObject = GameSceneCoreScript.Instance.FieldManagerScriptInstance.FieldArray[_arrayPosX + x, _arrayPosY].transform.Find("EdgeChosen").gameObject;
                        if (fieldScriptToCheck._fieldStatus == GlobalCore.FieldStatus.Unselected)
                        {
                            fieldScriptToCheck._fieldStatus = GlobalCore.FieldStatus.IndirectNeighbor;
                            LoopThroughGameObjectChildsAndSetSpriteRendererColor(edgeChosenNeighborGameObject, Color.yellow);
                        }
                        else if (fieldScriptToCheck._fieldStatus == GlobalCore.FieldStatus.IndirectNeighbor)
                        {
                            fieldScriptToCheck._fieldStatus = GlobalCore.FieldStatus.Unselected;
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
        #endregion
    }
}

