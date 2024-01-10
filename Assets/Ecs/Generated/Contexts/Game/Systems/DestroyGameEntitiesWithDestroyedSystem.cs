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

public sealed class DestroyGameEntitiesWithDestroyedSystem : ICleanupSystem
{
	private readonly IGroup<GameEntity> _group;

	public DestroyGameEntitiesWithDestroyedSystem(IContext<GameEntity> context)
	{
		_group = context.GetGroup(GameMatcher.Destroyed);
	}

	/// <summary>
	/// Performs cleanup logic after other systems have executed.
	/// </summary>
	public void Cleanup()
	{
		using var _ = UnityEngine.Pool.ListPool<GameEntity>.Get(out var buffer);
		_group.GetEntities(buffer);
		for (var i = 0; i < buffer.Count; ++i)
		{
			buffer[i].Destroy();
		}
	}
}
