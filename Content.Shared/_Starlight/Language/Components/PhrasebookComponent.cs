using Content.Shared._Starlight.Language;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

[RegisterComponent, NetworkedComponent]
public sealed partial class PhrasebookComponent : Component
{
    [DataField(required: true)]
    public ProtoId<LanguagePrototype> Language;

    [DataField]
    public Dictionary<ProtoId<LanguagePrototype>, HashSet<string>> KnownWords = new();
}