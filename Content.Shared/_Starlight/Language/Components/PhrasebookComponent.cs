using Content.Shared._Starlight.Language;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class PhrasebookComponent : Component
{
    [DataField, AutoNetworkedField]
    public Dictionary<ProtoId<LanguagePrototype>, HashSet<string>> KnownWords = new();
}