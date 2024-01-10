using System;
using Ecs.Core.Bootstrap;
using Ecs.Installers.Game.Feature;
using JCMG.EntitasRedux;
using Zenject;

namespace Ecs.Installers
{
    public abstract class AEcsInstaller : MonoInstaller, IDisposable
    {
        private Contexts _contexts;
        
        public override void InstallBindings()
        {
            _contexts = Contexts.SharedInstance;
            
            Container.BindInstance(_contexts).WhenInjectedInto<Bootstrap>();
            Container.BindInterfacesTo<Bootstrap>().AsSingle().NonLazy();
            
            InstallSystems();
        }

        protected abstract void InstallSystems();

        protected void BindContext<T>() where T : IContext
        {
            foreach (var ctx in _contexts.AllContexts)
            {
                if (ctx is T context)
                {
                    Container.BindInterfacesAndSelfTo<T>().FromInstance(context).AsSingle();
                }
            }
        }
        
        protected void BindEventSystem<TEventSystem>()
            where TEventSystem : Feature
        {
            Container.BindInterfacesTo<TEventSystem>().AsSingle().WithArguments(_contexts);
        }
        
        public void Dispose()
        {
        }
    }
}