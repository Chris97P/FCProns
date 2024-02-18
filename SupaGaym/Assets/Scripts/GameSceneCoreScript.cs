using UnityEngine;


namespace Assets.Scripts
{
    public class GameSceneCoreScript : MonoBehaviour
    {
        #region Statische Variablen
        private static GlobalCore.GameMode _gameMode;
        public static GameSceneCoreScript Instance;
        #endregion

        #region Public Variablen
        public FieldManagerScript FieldManagerScriptInstance;
        public PlayerManagerScript PlayerManagerScriptInstance;
        public CanvasManagerScript CanvasManagerScriptInstance;
        #endregion

        #region Properties
        public GlobalCore.GameMode GameMode
        {
            get { return _gameMode; }
            set
            {
                _gameMode = value;
                if (value == GlobalCore.GameMode.PlayMode)
                {
                    this.FieldManagerScriptInstance.ResetAllHighlightedFields();
                }
                else if (value == GlobalCore.GameMode.FinishMode)
                {
                    this.CanvasManagerScriptInstance.ShowFinishScreen();
                }
            }
        }
        #endregion

        
        void Awake()
        {
            Instance = this;

            FieldManagerScriptInstance = gameObject.AddComponent<FieldManagerScript>();
            PlayerManagerScriptInstance = gameObject.AddComponent<PlayerManagerScript>();
            CanvasManagerScriptInstance = gameObject.AddComponent<CanvasManagerScript>();

            FieldManagerScriptInstance.Init();
            //PlayerManagerScriptInstance.Init();

            InitializeCamera();

            TestScript.SetAllEmptyFieldsToActivePlayer();
        }

        private void InitializeCamera()
        {
            float x = (GlobalCore.FIELD_ARRAY_SIZE / 2) - 0.5f;
            float y = (GlobalCore.FIELD_ARRAY_SIZE / 2) - 0.5f + 0.7f;
            Camera.main.transform.position = new Vector3(x, y, GlobalCore.CAMERA_STANDARD_HEIGHT); 
        }
    }
}


