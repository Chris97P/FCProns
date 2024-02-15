using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerScript
    {
        public int StartingPosX;
        public int StartingPosY;
        public Color Color;
        public string Name;

        public PlayerScript(int startPosX, int startPosY, Color color, string name)
        {
            StartingPosX = startPosX;
            StartingPosY = startPosY;
            Color = color;
            Name = name;
        }
    }
}

