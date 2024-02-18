using UnityEngine;

namespace Assets.Scripts
{
    public class ButtonHandlerScript : MonoBehaviour
    {

        public void OnButtonRestartClick()
        {            
            GameSceneCoreScript.Instance.FieldManagerScriptInstance.CreateFieldArray();            
        }
    }
}
