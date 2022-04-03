using Game.Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CoreSceneInstaller : MonoInstaller
    {
        [SerializeField] private ClickHandler _clickHandler;
        public override void InstallBindings()
        {
            BindClickHandler();
        }

        private void BindClickHandler()
        {
            BindAsSingle(_clickHandler);
        }

        private void BindAsSingle<T>(T obj)
        {
            Container
                .Bind<T>()
                .FromInstance(obj)
                .AsSingle();
        }
    }
}
