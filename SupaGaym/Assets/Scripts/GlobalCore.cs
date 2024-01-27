using UnityEngine;

namespace Assets.Scripts
{
    public class GlobalCore
    {
        /*****
         * -----
         * Notes
         * -----
         * 
         * To-Do:
         * Spielererstellung auslagern (in Menü/GUI),
         * Regions in GlobalCore anpassen
         * 
         *****/

        #region GameObjectNames
        
        public const int FIELD_ARRAY_SIZE = 8;
        public const int CAMERA_STANDARD_HEIGHT = -10;
        
        #endregion

        #region ENUMS
        public enum StartPosTopLeft
        {
            X = 0,
            Y = FIELD_ARRAY_SIZE - 1
        }

        public enum StartPosTopRight
        {
            X = FIELD_ARRAY_SIZE - 1,
            Y = FIELD_ARRAY_SIZE - 1
        }

        public enum StartPosBottomLeft
        {
            X = 0,
            Y = 0
        }

        public enum StartPosBottomRight
        {
            X = FIELD_ARRAY_SIZE - 1,
            Y = 0
        }
        #endregion
    }
}


