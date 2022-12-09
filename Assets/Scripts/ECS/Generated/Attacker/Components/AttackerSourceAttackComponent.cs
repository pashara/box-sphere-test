//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AttackerEntity {

    public ECS.Components.SourceAttack sourceAttack { get { return (ECS.Components.SourceAttack)GetComponent(AttackerComponentsLookup.SourceAttack); } }
    public bool hasSourceAttack { get { return HasComponent(AttackerComponentsLookup.SourceAttack); } }

    public void AddSourceAttack(int newBattlerId) {
        var index = AttackerComponentsLookup.SourceAttack;
        var component = (ECS.Components.SourceAttack)CreateComponent(index, typeof(ECS.Components.SourceAttack));
        component.BattlerId = newBattlerId;
        AddComponent(index, component);
    }

    public void ReplaceSourceAttack(int newBattlerId) {
        var index = AttackerComponentsLookup.SourceAttack;
        var component = (ECS.Components.SourceAttack)CreateComponent(index, typeof(ECS.Components.SourceAttack));
        component.BattlerId = newBattlerId;
        ReplaceComponent(index, component);
    }

    public void RemoveSourceAttack() {
        RemoveComponent(AttackerComponentsLookup.SourceAttack);
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
public sealed partial class AttackerMatcher {

    static Entitas.IMatcher<AttackerEntity> _matcherSourceAttack;

    public static Entitas.IMatcher<AttackerEntity> SourceAttack {
        get {
            if (_matcherSourceAttack == null) {
                var matcher = (Entitas.Matcher<AttackerEntity>)Entitas.Matcher<AttackerEntity>.AllOf(AttackerComponentsLookup.SourceAttack);
                matcher.componentNames = AttackerComponentsLookup.componentNames;
                _matcherSourceAttack = matcher;
            }

            return _matcherSourceAttack;
        }
    }
}