using UnityEngine;


namespace Assets.Scripts
{
    public class GameSceneCoreScript : MonoBehaviour
    {
        public static FieldManagerScript FieldManagerScriptInstance;
        public static PlayerScript ActivePlayer;
        public static GlobalCore.GameMode GameMode;

        public PlayerScript Player1;
        public PlayerScript Player2;
        

        // Start is called before the first frame update
        void Awake()
        {
            FieldManagerScriptInstance = gameObject.AddComponent<FieldManagerScript>();
            
            InitializeCamera();
            
            Player1 = new PlayerScript((int)GlobalCore.StartPosTopLeft.X, (int)GlobalCore.StartPosTopLeft.Y, Color.green, "Chrisi");
            FieldManagerScriptInstance.FieldArray[Player1.StartingPosX, Player1.StartingPosY].GetComponent<FieldScript>().AssignFieldToPlayer(Player1);

            Player2 = new PlayerScript((int)GlobalCore.StartPosBottomRight.X, (int)GlobalCore.StartPosBottomRight.Y, Color.red, "Flo");
            FieldManagerScriptInstance.FieldArray[Player2.StartingPosX, Player2.StartingPosY].GetComponent<FieldScript>().AssignFieldToPlayer(Player2);

            ActivePlayer = Player1;
            GameMode = GlobalCore.GameMode.PlayMode;
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
    }
}


