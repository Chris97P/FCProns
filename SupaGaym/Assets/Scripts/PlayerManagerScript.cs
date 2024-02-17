using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerManagerScript : MonoBehaviour
    {
        public PlayerScript Player1;
        public PlayerScript Player2;
        //public PlayerScript ActivePlayer;
        //public PlayerScript NextPlayer;
        private readonly List<PlayerScript> _playerList = new();

        #region Properties

        private PlayerScript _activePlayer;

        public PlayerScript ActivePlayer
        {
            get { return _activePlayer; }
            set 
            { 
                _activePlayer = value;
                GameSceneCoreScript.Instance.CanvasManagerScriptInstance.ActivePlayerRawImage.color = value.Color;
            }
        }

        #endregion


        public void Init()
        {
            Player1 = new PlayerScript((int)GlobalCore.StartPosTopLeft.X, (int)GlobalCore.StartPosTopLeft.Y, Color.green, "Chrisi");
            GameSceneCoreScript.Instance.FieldManagerScriptInstance.FieldArray[Player1.StartingPosX, Player1.StartingPosY].GetComponent<FieldScript>().AssignFieldToPlayer(Player1);

            Player2 = new PlayerScript((int)GlobalCore.StartPosBottomRight.X, (int)GlobalCore.StartPosBottomRight.Y, Color.red, "Flo");
            GameSceneCoreScript.Instance.FieldManagerScriptInstance.FieldArray[Player2.StartingPosX, Player2.StartingPosY].GetComponent<FieldScript>().AssignFieldToPlayer(Player2);


            _playerList.Add(Player1);
            _playerList.Add(Player2);

            SetActivePlayer(Player1);
        }

        public List<PlayerScript> GetPlayerList()
        {
            return _playerList;
        }

        public void SetActivePlayer(PlayerScript activePlayer)
        {
            ActivePlayer = activePlayer;
        }

        public PlayerScript GetNextPlayer()
        {
            int _indexOfActivePlayer = _playerList.IndexOf(ActivePlayer);

            if (_indexOfActivePlayer == _playerList.Count - 1)
                return _playerList[0];

            return _playerList[_indexOfActivePlayer + 1];
        }
    }
}
