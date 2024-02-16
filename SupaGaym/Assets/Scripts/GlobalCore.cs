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
         * FinishMode:
         * Durchs FeldArray laufen und alle Felder des näpchsten SPielers checken ob noch Felder ohne Besitzer in reichweite sind, wenn ja Finish und Felder zählen
         * 
         *****/

        #region Allgemeines
        
        public const int FIELD_ARRAY_SIZE = 8;
        public const int CAMERA_STANDARD_HEIGHT = -10;

        #endregion

        #region ENUMS
        #region StartPositions
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

        public enum GameMode
        {
            None = 0,
            PlayMode = 1,
            ExpansionMode = 2,
            FinishMode = 3
        }

        public enum FieldStatus
        {
            Unselected = 0,
            ExpansionStart = 1,
            DirectNeighbor = 2,
            IndirectNeighbor = 3
        }
        #endregion


    }
}


