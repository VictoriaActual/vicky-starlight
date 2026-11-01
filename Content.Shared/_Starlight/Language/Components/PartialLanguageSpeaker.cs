using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._Starlight.Language.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class PartialLanguageSpeakerComponent : Component
{
    [DataField, AutoNetworkedField]
    public EntProtoId OpenAction = "ActionOpenPartialSpeaker";

    [DataField, AutoNetworkedField]
    public EntityUid? OpenActionEntity;
}
