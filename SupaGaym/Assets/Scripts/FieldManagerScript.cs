using UnityEngine;

namespace Assets.Scripts
{
    public class FieldManagerScript : MonoBehaviour
    {
        public FieldManagerScript Instance;
        public GameObject[,] FieldArray; 
        public GameObject FieldPrefab;
        public GameObject FieldContainer;

        // Start is called before the first frame update
        private void Awake()
        {
            Instance = this;
            FieldPrefab = Resources.Load<GameObject>("Prefabs/FieldPrefab");
            FieldContainer = new GameObject("FieldContainer");
            
            CreateFieldArray();
        }

        // Update is called once per frame
        private void Update()
        {

        }
    
        public void CreateFieldArray()
        {
            FieldArray = new GameObject[GlobalCore.FIELD_ARRAY_SIZE, GlobalCore.FIELD_ARRAY_SIZE];

            for (int y = 0; y < FieldArray.GetLength(1); y++)
            {
                for (int x = 0; x < FieldArray.GetLength(0); x++)
                {
                    FieldArray[x, y] = GameObject.Instantiate(FieldPrefab);
                    FieldArray[x, y].transform.SetParent(FieldContainer.transform);
                    FieldArray[x, y].name = $"Field [{y},{x}]";
                    FieldArray[x, y].transform.position = new Vector2(x + (0.05f * x), y + (0.05f * y));
                }
            }
        }

    }
}

