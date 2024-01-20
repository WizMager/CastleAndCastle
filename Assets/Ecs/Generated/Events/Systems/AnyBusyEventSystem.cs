//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using JCMG.EntitasRedux;

public sealed class AnyBusyEventSystem : JCMG.EntitasRedux.ReactiveSystem<GameEntity>
{
	readonly JCMG.EntitasRedux.IGroup<GameEntity> _listeners;

	public AnyBusyEventSystem(IContext<GameEntity> context) : base(context)
	{
		_listeners = context.GetGroup(GameMatcher.AnyBusyListener);
	}

	protected override JCMG.EntitasRedux.ICollector<GameEntity> GetTrigger(JCMG.EntitasRedux.IContext<GameEntity> context)
	{
		return new SlimCollector<GameEntity>(context, GameComponentsLookup.Busy, EventType.Added);
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.IsBusy;
	}

	protected override void Execute(System.Collections.Generic.IEnumerable<GameEntity> entities)
	{
		foreach (var e in entities)
		{
			
			using(UnityEngine.Pool.ListPool<GameEntity>.Get(out var buffer))
			{
				_listeners.GetEntities(buffer);
				foreach (var listenerEntity in buffer)
					listenerEntity.AnyBusyListener.Invoke(e);
			}
		}
	}
}