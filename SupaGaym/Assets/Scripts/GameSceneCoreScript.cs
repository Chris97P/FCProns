using UnityEngine;


namespace Assets.Scripts
{
    public class GameSceneCoreScript : MonoBehaviour
    {
        public static FieldManagerScript FieldManagerScriptInstance;

        private static GlobalCore.GameMode _gameMode;

        public static GlobalCore.GameMode GameMode
        {
            get { return _gameMode; }
            set 
            {
                _gameMode = value;
                if (value == GlobalCore.GameMode.PlayMode)
                {
                    FieldManagerScriptInstance.ResetAllHighlightedFields();
                }
            }
        }

        public PlayerManagerScript PlayerManagerScriptInstance;
        public static GameSceneCoreScript Instance;



        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;

            FieldManagerScriptInstance = gameObject.AddComponent<FieldManagerScript>();
            PlayerManagerScriptInstance = gameObject.AddComponent<PlayerManagerScript>();
            
            InitializeCamera();

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


