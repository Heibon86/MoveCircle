using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Player
{
    public class CallbackClick
    {
        public string Key;
        public Vector3 Point;

        public CallbackClick(string key, Vector3 point)
        {
            Key = key;
            Point = point;
        }
    }
}
