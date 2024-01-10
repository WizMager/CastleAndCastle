//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity
{
	public Ecs.Common.Components.UidComponent Uid { get { return (Ecs.Common.Components.UidComponent)GetComponent(InputComponentsLookup.Uid); } }
	public bool HasUid { get { return HasComponent(InputComponentsLookup.Uid); } }

	public void AddUid(Ecs.Extensions.UidGenerator.Uid newValue)
	{
		var index = InputComponentsLookup.Uid;
		var component = (Ecs.Common.Components.UidComponent)CreateComponent(index, typeof(Ecs.Common.Components.UidComponent));
		#if !ENTITAS_REDUX_NO_IMPL
		component.Value = newValue;
		#endif
		AddComponent(index, component);
	}

	public void ReplaceUid(Ecs.Extensions.UidGenerator.Uid newValue)
	{
		var index = InputComponentsLookup.Uid;
		var component = (Ecs.Common.Components.UidComponent)CreateComponent(index, typeof(Ecs.Common.Components.UidComponent));
		#if !ENTITAS_REDUX_NO_IMPL
		component.Value = newValue;
		#endif
		ReplaceComponent(index, component);
	}

	public void CopyUidTo(Ecs.Common.Components.UidComponent copyComponent)
	{
		var index = InputComponentsLookup.Uid;
		var component = (Ecs.Common.Components.UidComponent)CreateComponent(index, typeof(Ecs.Common.Components.UidComponent));
		#if !ENTITAS_REDUX_NO_IMPL
		component.Value = copyComponent.Value;
		#endif
		ReplaceComponent(index, component);
	}

	public void RemoveUid()
	{
		RemoveComponent(InputComponentsLookup.Uid);
	}
}

//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity : IUidEntity { }

//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class InputMatcher
{
	static JCMG.EntitasRedux.IMatcher<InputEntity> _matcherUid;

	public static JCMG.EntitasRedux.IMatcher<InputEntity> Uid
	{
		get
		{
			if (_matcherUid == null)
			{
				var matcher = (JCMG.EntitasRedux.Matcher<InputEntity>)JCMG.EntitasRedux.Matcher<InputEntity>.AllOf(InputComponentsLookup.Uid);
				matcher.ComponentNames = InputComponentsLookup.ComponentNames;
				_matcherUid = matcher;
			}

			return _matcherUid;
		}
	}
}
