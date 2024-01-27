using Ecs.Core.Interfaces;
using Zenject;

namespace Game.Services.TimeProvider
{
    public class UnityTimerProvider : ITimeProvider, 
        ITickable, 
        IFixedTickable
    {
        public float Time => UnityEngine.Time.time;
        public float DeltaTime { get; private set; }
        public float UnscaledDeltaTime => UnityEngine.Time.unscaledDeltaTime;
        public float FixedDeltaTime { get; private set; }

        public float TimeScale
        {
            get => UnityEngine.Time.timeScale;
            set => UnityEngine.Time.timeScale = value;
        }

        public void Tick()
        {
            DeltaTime = UnityEngine.Time.deltaTime;
           
        }

        public void FixedTick()
        {
            FixedDeltaTime = UnityEngine.Time.fixedDeltaTime;
        }
    }
}