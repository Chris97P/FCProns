using UnityEngine;


namespace Assets.Scripts
{
    public class GameSceneCoreScript : MonoBehaviour
    {
        public FieldManagerScript FieldManagerScript;

        // Start is called before the first frame update
        void Awake()
        {
            FieldManagerScript = gameObject.AddComponent<FieldManagerScript>();
            
            InitializeCamera();

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


