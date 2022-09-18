//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public ECS.Components.IdComponent id { get { return (ECS.Components.IdComponent)GetComponent(InputComponentsLookup.Id); } }
    public bool hasId { get { return HasComponent(InputComponentsLookup.Id); } }

    public void AddId(int newValue) {
        var index = InputComponentsLookup.Id;
        var component = (ECS.Components.IdComponent)CreateComponent(index, typeof(ECS.Components.IdComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceId(int newValue) {
        var index = InputComponentsLookup.Id;
        var component = (ECS.Components.IdComponent)CreateComponent(index, typeof(ECS.Components.IdComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveId() {
        RemoveComponent(InputComponentsLookup.Id);
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
public partial class InputEntity : IIdEntity { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherId;

    public static Entitas.IMatcher<InputEntity> Id {
        get {
            if (_matcherId == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.Id);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherId = matcher;
            }

            return _matcherId;
        }
    }
}
