using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerScript
    {
        public int StartingPosX;
        public int StartingPosY;
        public Color PlayerColor;

        public PlayerScript(int posX, int posY, Color color)
        {
            StartingPosX = posX;
            StartingPosY = posY;
            PlayerColor = color;
        }
    }
}

