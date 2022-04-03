using UI;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [Inject] private GUIManager _guiManager;

        public void Initialize()
        {
            _guiManager.ShowStartScreen();
        }
    }
}
