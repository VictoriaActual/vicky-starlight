using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._Starlight.Language;

[Serializable, NetSerializable]
public enum PartialLanguageSpeakerUiKey : byte
{
    Key
}

[Serializable, NetSerializable]
public sealed class PartialLanguageSpeakerBuiState : BoundUserInterfaceState
{
    public List<string> Phrases = new();
    public ProtoId<LanguagePrototype> Language; // which language this book is
}

[Serializable, NetSerializable]
public sealed class PartialLanguageSpeakerSelectMessage : BoundUserInterfaceMessage
{
    public string Phrase = string.Empty;
    public ProtoId<LanguagePrototype> Language; // pass it back
}
