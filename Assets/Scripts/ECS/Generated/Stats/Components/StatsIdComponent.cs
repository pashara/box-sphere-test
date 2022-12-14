//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class StatsEntity {

    public ECS.Components.IdComponent id { get { return (ECS.Components.IdComponent)GetComponent(StatsComponentsLookup.Id); } }
    public bool hasId { get { return HasComponent(StatsComponentsLookup.Id); } }

    public void AddId(int newValue) {
        var index = StatsComponentsLookup.Id;
        var component = (ECS.Components.IdComponent)CreateComponent(index, typeof(ECS.Components.IdComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceId(int newValue) {
        var index = StatsComponentsLookup.Id;
        var component = (ECS.Components.IdComponent)CreateComponent(index, typeof(ECS.Components.IdComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveId() {
        RemoveComponent(StatsComponentsLookup.Id);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class StatsEntity : IIdEntity { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class StatsMatcher {

    static Entitas.IMatcher<StatsEntity> _matcherId;

    public static Entitas.IMatcher<StatsEntity> Id {
        get {
            if (_matcherId == null) {
                var matcher = (Entitas.Matcher<StatsEntity>)Entitas.Matcher<StatsEntity>.AllOf(StatsComponentsLookup.Id);
                matcher.componentNames = StatsComponentsLookup.componentNames;
                _matcherId = matcher;
            }

            return _matcherId;
        }
    }
}
