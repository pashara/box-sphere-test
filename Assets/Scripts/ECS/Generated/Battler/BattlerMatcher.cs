//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ContextMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class BattlerMatcher {

    public static Entitas.IAllOfMatcher<BattlerEntity> AllOf(params int[] indices) {
        return Entitas.Matcher<BattlerEntity>.AllOf(indices);
    }

    public static Entitas.IAllOfMatcher<BattlerEntity> AllOf(params Entitas.IMatcher<BattlerEntity>[] matchers) {
          return Entitas.Matcher<BattlerEntity>.AllOf(matchers);
    }

    public static Entitas.IAnyOfMatcher<BattlerEntity> AnyOf(params int[] indices) {
          return Entitas.Matcher<BattlerEntity>.AnyOf(indices);
    }

    public static Entitas.IAnyOfMatcher<BattlerEntity> AnyOf(params Entitas.IMatcher<BattlerEntity>[] matchers) {
          return Entitas.Matcher<BattlerEntity>.AnyOf(matchers);
    }
}
