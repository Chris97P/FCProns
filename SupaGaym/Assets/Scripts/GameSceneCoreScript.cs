using UnityEngine;


namespace Assets.Scripts
{
    public class GameSceneCoreScript : MonoBehaviour
    {
        public FieldManagerScript FieldManagerScript;
        public PlayerScript Player1;
        public PlayerScript Player2;

        // Start is called before the first frame update
        void Awake()
        {
            FieldManagerScript = gameObject.AddComponent<FieldManagerScript>();
            
            InitializeCamera();
            
            Player1 = new PlayerScript((int)GlobalCore.StartPosTopLeft.X, (int)GlobalCore.StartPosTopLeft.Y, Color.green);
            SetStartPosition(Player1);

            Player2 = new PlayerScript((int)GlobalCore.StartPosBottomRight.X, (int)GlobalCore.StartPosBottomRight.Y, Color.red);
            SetStartPosition(Player2);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void InitializeCamera()
        {
            float x = (GlobalCore.FIELD_ARRAY_SIZE / 2) - 0.5f;
            float y = (GlobalCore.FIELD_ARRAY_SIZE / 2) - 0.5f;
            Camera.main.transform.position = new Vector3(x, y, GlobalCore.CAMERA_STANDARD_HEIGHT); 
        }

        private void SetStartPosition(PlayerScript playerScript)
        {
            for (int y = 0; y < FieldManagerScript.FieldArray.GetLength(1); y++ )
            {
                for (int x = 0; x < FieldManagerScript.FieldArray.GetLength(0); x++)
                {
                    if (y == playerScript.StartingPosY &&  x == playerScript.StartingPosX)
                    {
                        FieldManagerScript.FieldArray[x, y].GetComponent<SpriteRenderer>().color = playerScript.PlayerColor;
                    }
                }
            }
        }
    }
}


