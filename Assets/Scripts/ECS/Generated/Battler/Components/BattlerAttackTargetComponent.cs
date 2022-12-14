//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BattlerEntity {

    public ECS.Components.AttackTargetComponent attackTarget { get { return (ECS.Components.AttackTargetComponent)GetComponent(BattlerComponentsLookup.AttackTarget); } }
    public bool hasAttackTarget { get { return HasComponent(BattlerComponentsLookup.AttackTarget); } }

    public void AddAttackTarget(int newValue) {
        var index = BattlerComponentsLookup.AttackTarget;
        var component = (ECS.Components.AttackTargetComponent)CreateComponent(index, typeof(ECS.Components.AttackTargetComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAttackTarget(int newValue) {
        var index = BattlerComponentsLookup.AttackTarget;
        var component = (ECS.Components.AttackTargetComponent)CreateComponent(index, typeof(ECS.Components.AttackTargetComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAttackTarget() {
        RemoveComponent(BattlerComponentsLookup.AttackTarget);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class BattlerMatcher {

    static Entitas.IMatcher<BattlerEntity> _matcherAttackTarget;

    public static Entitas.IMatcher<BattlerEntity> AttackTarget {
        get {
            if (_matcherAttackTarget == null) {
                var matcher = (Entitas.Matcher<BattlerEntity>)Entitas.Matcher<BattlerEntity>.AllOf(BattlerComponentsLookup.AttackTarget);
                matcher.componentNames = BattlerComponentsLookup.componentNames;
                _matcherAttackTarget = matcher;
            }

            return _matcherAttackTarget;
        }
    }
}
