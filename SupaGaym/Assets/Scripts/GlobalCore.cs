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
         * !!ActivePlayer wurde ausgelagert in neues Script "PlayerManagerScript" => muss überall im Code angepasst werden (es geht nix mehr)!!
         * 
         *****/

        #region GameObjectNames
        
        public const int FIELD_ARRAY_SIZE = 8;
        public const int CAMERA_STANDARD_HEIGHT = -10;

        #endregion

        #region ENUMS
        //#region StartPositions
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
        //#endregion

        public enum GameMode
        {
            None = 0,
            PlayMode = 1,
            ExpansionMode = 2,
        }

        public enum FieldStatus
        {
            Default = 0,
            ExpansionStart = 1,
            Neighbor = 2
        }
        #endregion


    }
}


