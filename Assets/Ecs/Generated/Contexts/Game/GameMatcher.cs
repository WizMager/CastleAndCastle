//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher
{
	public static JCMG.EntitasRedux.IAllOfMatcher<GameEntity> AllOf(params int[] indices)
	{
		return JCMG.EntitasRedux.Matcher<GameEntity>.AllOf(indices);
	}

	public static JCMG.EntitasRedux.IAllOfMatcher<GameEntity> AllOf(params JCMG.EntitasRedux.IMatcher<GameEntity>[] matchers)
	{
		return JCMG.EntitasRedux.Matcher<GameEntity>.AllOf(matchers);
	}

	public static JCMG.EntitasRedux.IAnyOfMatcher<GameEntity> AnyOf(params int[] indices)
	{
		return JCMG.EntitasRedux.Matcher<GameEntity>.AnyOf(indices);
	}

	public static JCMG.EntitasRedux.IAnyOfMatcher<GameEntity> AnyOf(params JCMG.EntitasRedux.IMatcher<GameEntity>[] matchers)
	{
		return JCMG.EntitasRedux.Matcher<GameEntity>.AnyOf(matchers);
	}
}
