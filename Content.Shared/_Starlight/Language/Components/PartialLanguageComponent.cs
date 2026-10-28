using Content.Shared._Starlight.Language;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class PartialLanguageComponent : Component
{
    // Language ID matches to words the owner recognizes
    [DataField, AutoNetworkedField]
    public Dictionary<ProtoId<LanguagePrototype>, HashSet<string>> KnownWords = new();
}
