using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerManagerScript : MonoBehaviour
    {
        public PlayerScript Player1;
        public PlayerScript Player2;
        public PlayerScript ActivePlayer;

        private List<PlayerScript> _playerList;
       

        void Awake()
        {
            Player1 = new PlayerScript((int)GlobalCore.StartPosTopLeft.X, (int)GlobalCore.StartPosTopLeft.Y, Color.green, "Chrisi");
            GameSceneCoreScript.FieldManagerScriptInstance.FieldArray[Player1.StartingPosX, Player1.StartingPosY].GetComponent<FieldScript>().AssignFieldToPlayer(Player1);

            Player2 = new PlayerScript((int)GlobalCore.StartPosBottomRight.X, (int)GlobalCore.StartPosBottomRight.Y, Color.red, "Flo");
            GameSceneCoreScript.FieldManagerScriptInstance.FieldArray[Player2.StartingPosX, Player2.StartingPosY].GetComponent<FieldScript>().AssignFieldToPlayer(Player2);

            _playerList.Add(Player1);
            _playerList.Add(Player2);
        }

        public void SetActivePlayer(PlayerScript activePlayer)
        {
            ActivePlayer = activePlayer;
        }

        public PlayerScript GetNextPlayer()
        {
            int _indexOfActivePlayer = _playerList.IndexOf(ActivePlayer);
            return _playerList[_indexOfActivePlayer + 1];
        }
    }
}
