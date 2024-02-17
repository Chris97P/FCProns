using UnityEngine;

namespace Assets.Scripts
{
    public static class TestScript
    {     
        public static void SetAllEmptyFieldsToActivePlayer()
        {
            for (int y = 0; y < GameSceneCoreScript.Instance.FieldManagerScriptInstance.FieldArray.GetLength(1); y++)
            {
                for (int x = 0; x < GameSceneCoreScript.Instance.FieldManagerScriptInstance.FieldArray.GetLength(0); x++)
                {   
                    if (y == GlobalCore.FIELD_ARRAY_SIZE - 1 && x == GlobalCore.FIELD_ARRAY_SIZE - 1)
                    {
                        continue;
                    }

                    if (GameSceneCoreScript.Instance.FieldManagerScriptInstance.FieldArray[x, y].GetComponent<FieldScript>().OwnerPlayerScriptInstance is null)
                    {
                        GameSceneCoreScript.Instance.FieldManagerScriptInstance.FieldArray[x, y].GetComponent<FieldScript>().AssignFieldToPlayer(GameSceneCoreScript.Instance.PlayerManagerScriptInstance.ActivePlayer, false);
                    }                    
                }
            }
        }
    }
}
