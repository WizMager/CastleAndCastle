//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
[JCMG.EntitasRedux.DontGenerate(false)]
public sealed class CoinsListenerComponent : JCMG.EntitasRedux.IComponent, System.IDisposable
{
	public event OnGameCoins Delegate;

	public bool IsEmpty => Delegate == null;

	public void Invoke(GameEntity entity, int value) => Delegate(entity, value);

	void System.IDisposable.Dispose() => Delegate = null;
}
