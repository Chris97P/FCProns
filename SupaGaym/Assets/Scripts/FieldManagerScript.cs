using System;
using UnityEngine;
using static Assets.Scripts.GlobalCore;

namespace Assets.Scripts
{
    public class FieldManagerScript : MonoBehaviour
    {
        public FieldManagerScript Instance;
        public GameObject FieldPrefab;
        public GameObject FieldContainer;

        private GameObject[,] _fieldArray;

        public GameObject[,] FieldArray
        {
            get { return _fieldArray; }
            set 
            { 
                _fieldArray = value;
                //GameSceneCoreScript.Instance.PlayerManagerScriptInstance.SetActivePlayer(GameSceneCoreScript.Instance.PlayerManagerScriptInstance.GetNextPlayer());
                GameSceneCoreScript.Instance.CanvasManagerScriptInstance.ShowStartScreen();
                GameSceneCoreScript.Instance.PlayerManagerScriptInstance.Init();
                GameSceneCoreScript.Instance.GameMode = GlobalCore.GameMode.PlayMode;
            }
        }


        public void Init()
        {
            Instance = this;
            FieldPrefab = Resources.Load<GameObject>("Prefabs/FieldPrefab");
            FieldContainer = new GameObject("FieldContainer");

            CreateFieldArray();
        }

        public void CreateFieldArray()
        {
            if (FieldArray != null)
            {
                for (int y = 0; y < FieldArray.GetLength(1); y++)
                {
                    for (int x = 0; x < FieldArray.GetLength(0); x++)
                    {
                        Destroy(FieldArray[x, y]);
                    }
                }
            }

            //FieldArray = new GameObject[GlobalCore.FIELD_ARRAY_SIZE, GlobalCore.FIELD_ARRAY_SIZE];
            GameObject[,] tempFieldArray = new GameObject[GlobalCore.FIELD_ARRAY_SIZE, GlobalCore.FIELD_ARRAY_SIZE];
            
            FieldScript fieldScriptInstance;            

            for (int y = 0; y < tempFieldArray.GetLength(1); y++)
            {
                for (int x = 0; x < tempFieldArray.GetLength(0); x++)
                {
                    tempFieldArray[x, y] = Instantiate(FieldPrefab);
                    tempFieldArray[x, y].transform.SetParent(FieldContainer.transform);
                    tempFieldArray[x, y].name = $"Field [{x},{y}]";
                    fieldScriptInstance = tempFieldArray[x, y].AddComponent<FieldScript>();
                    fieldScriptInstance._arrayPosX = x;
                    fieldScriptInstance._arrayPosY = y;
                    tempFieldArray[x, y].transform.position = new Vector2(x + (0.05f * x), y + (0.05f * y));
                }
            }

            FieldArray = tempFieldArray;
        }

        public void ResetAllHighlightedFields()
        {
            for (int y = 0; y < FieldArray.GetLength(1); y++)
            {
                for (int x = 0; x < FieldArray.GetLength(0); x++)
                {
                    FieldArray[x, y].GetComponent<FieldScript>().ResetHighlightedField();
                }
            }
        }

        public int GetFieldCount(PlayerScript player)
        {
            int count = 0;

            for (int y = 0; y < FieldArray.GetLength(1); y++)
            {
                for (int x = 0; x < FieldArray.GetLength(0); x++)
                {
                    if (FieldArray[x, y].GetComponent<FieldScript>().OwnerPlayerScriptInstance == player)
                        count++;
                }
            }

            return count;
        }
    }
}

