using System.Linq;
using Content.Shared._Starlight.Language.Components;
using Content.Shared.Actions;
using Content.Shared.Chat;
using Content.Shared.Hands.Components;
using Robust.Shared.Player;

namespace Content.Shared._Starlight.Language.Systems;

public sealed partial class PartialLanguageSpeakerSystem : EntitySystem
{
    [Dependency] private readonly SharedActionsSystem _actions = default!;
    [Dependency] private readonly SharedLanguageSystem _language = default!;
    [Dependency] private readonly SharedChatSystem _chat = default!;
    [Dependency] private readonly SharedUserInterfaceSystem _ui = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<PhrasebookComponent, BoundUIOpenedEvent>(OnBookOpened);
        Subs.BuiEvents<PhrasebookComponent>(PartialLanguageSpeakerUiKey.Key, subs =>
            {
            subs.Event<PartialLanguageSpeakerSelectMessage>(OnPhraseSelected);
            });
    }

    private void OnOpenAction(Entity<PartialLanguageSpeakerComponent> ent, ref OpenPartialSpeakerActionEvent args)
    {
        if (args.Handled) return;
        
        // Gather what this entity can currently say. This includes words from traits and held books
        var phrases = new List<string>();

        if (TryComp<HandsComponent>(ent, out var hands))
        {
            // call TryGetKnownWords for the language. It's just Bubblish right now for testing.
            if (_language.TryGetKnownWords(ent, "Bubblish", out var known))
                phrases.AddRange(known);
        }
        else
        {
            if (_language.TryGetKnownWords(ent, "Bubblish", out var known))
                phrases.AddRange(known);
        }

        var state = new PartialLanguageSpeakerBuiState
        {
            Phrases = phrases.Distinct(StringComparer.OrdinalIgnoreCase).ToList(),
            Language = "Bubblish"
        };

        _ui.SetUiState(ent.Owner, PartialLanguageSpeakerUiKey.Key, state);
        _ui.OpenUi(ent.Owner, PartialLanguageSpeakerUiKey.Key, ent.Owner);
        args.Handled = true;
    }

    private void OnBookOpened(Entity<PhrasebookComponent> ent, ref BoundUIOpenedEvent args)
    {
        var phrases = new List<string>();
        if (ent.Comp.KnownWords.TryGetValue(ent.Comp.Language, out var words))
            phrases.AddRange(words);
        var state = new PartialLanguageSpeakerBuiState
        {
            Phrases = phrases.Distinct(StringComparer.OrdinalIgnoreCase).ToList(),
            Language = ent.Comp.Language
        };
        _ui.SetUiState(ent.Owner, PartialLanguageSpeakerUiKey.Key, state);
    }

    private void OnPhraseSelected(Entity<PhrasebookComponent> ent, ref PartialLanguageSpeakerSelectMessage msg)
    {
        var actor = msg.Actor;
        if (!ProtoMan.TryIndex<LanguagePrototype>(msg.Language, out var langProto))
            return;
        _chat.TrySendInGameICMessage(actor, msg.Phrase, InGameICChatType.Speak, ChatTransmitRange.Normal, 
            hideLog: false, languageOverride: langProto);
    }
}
public sealed partial class OpenPartialSpeakerActionEvent : InstantActionEvent {}
