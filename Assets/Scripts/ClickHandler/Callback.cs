using Game.Bonuses;
using UnityEngine;

namespace ClickHandler
{
    public class Callback
    {
        public string Key;
        public float Distance;
        public Vector3 Point;
        public SquareBonus SquareBonus;

        public Callback(string key)
        {
            Key = key;
        }
        
        public Callback(string key, float distance)
        {
            Key = key;
            Distance = distance;
        }
        
        public Callback(string key, SquareBonus squareBonus)
        {
            Key = key;
            SquareBonus = squareBonus;
        }
        
        public Callback(string key, Vector3 point)
        {
            Key = key;
            Point = point;
        }
    }
}
