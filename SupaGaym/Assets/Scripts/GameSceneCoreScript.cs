using UnityEngine;


namespace Assets.Scripts
{
    public class GameSceneCoreScript : MonoBehaviour
    {
        public static FieldManagerScript FieldManagerScriptInstance;
        public PlayerScript Player1;
        public PlayerScript Player2;
        [SerializeField] public static PlayerScript ActivePlayer;

        // Start is called before the first frame update
        void Awake()
        {
            FieldManagerScriptInstance = gameObject.AddComponent<FieldManagerScript>();
            
            InitializeCamera();
            
            Player1 = new PlayerScript((int)GlobalCore.StartPosTopLeft.X, (int)GlobalCore.StartPosTopLeft.Y, Color.green, "Chrisi");
            AssignFieldToPlayer(Player1.StartingPosX, Player1.StartingPosY, Player1.Color, Player1);

            Player2 = new PlayerScript((int)GlobalCore.StartPosBottomRight.X, (int)GlobalCore.StartPosBottomRight.Y, Color.red, "Flo");
            AssignFieldToPlayer(Player2.StartingPosX, Player2.StartingPosY, Player2.Color, Player2);

            ActivePlayer = Player1;
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

        private void AssignFieldToPlayer(int xPos, int yPos, Color color, PlayerScript playerInstance)
        {
            for (int y = 0; y < FieldManagerScriptInstance.FieldArray.GetLength(1); y++ )
            {
                for (int x = 0; x < FieldManagerScriptInstance.FieldArray.GetLength(0); x++)
                {
                    if (y == yPos &&  x == xPos)
                    {
                        FieldManagerScriptInstance.FieldArray[x, y].GetComponent<SpriteRenderer>().color = color;
                        FieldManagerScriptInstance.FieldArray[x, y].GetComponent<FieldScript>().PlayerScriptInstance = playerInstance;
                    }
                }
            }
        }
    }
}


