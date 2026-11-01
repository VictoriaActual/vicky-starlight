using Content.Shared._Starlight.Language;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;
using Robust.Shared.Prototypes;

namespace Content.Client._Starlight.Language;

public sealed class PartialLanguageSpeakerBui : BoundUserInterface
{
    private PartialLanguageSpeakerWindow? _window;
    private ProtoId<LanguagePrototype> _currentLanguage;
    public PartialLanguageSpeakerBui(EntityUid owner, Enum uiKey) : base(owner, uiKey) {}
    protected override void Open()
    {
        base.Open();
        _window = new PartialLanguageSpeakerWindow();
        // Window only knows phrase, BUI adds the language we stashed from UpdateState
        _window.OnPhraseSelected += phrase =>
        {
            SendMessage(new PartialLanguageSpeakerSelectMessage
            {
                Phrase = phrase,
                Language = _currentLanguage
            });
        };
        _window.OpenCentered();
    }
    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);
        if (state is not PartialLanguageSpeakerBuiState msg)
            return;
        _currentLanguage = msg.Language;
        _window?.UpdatePhrases(msg.Phrases);
    }
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _window?.Close();
    }
}
public sealed class PartialLanguageSpeakerWindow : DefaultWindow
{
    public Action<string>? OnPhraseSelected;
    private readonly BoxContainer _container;
    public PartialLanguageSpeakerWindow()
    {
        Title = "Phrasebook";
        MinSize = new System.Numerics.Vector2(300, 200);
        _container = new BoxContainer { Orientation = BoxContainer.LayoutOrientation.Vertical, VerticalExpand = true };
        Contents.AddChild(new ScrollContainer { Children = { _container }, VerticalExpand = true });
    }
    public void UpdatePhrases(List<string> phrases)
    {
        _container.RemoveAllChildren();
        foreach (var p in phrases)
        {
            var btn = new Button { Text = p };
            var copy = p; // capture for lambda
            btn.OnPressed += _ => OnPhraseSelected?.Invoke(copy);
            _container.AddChild(btn);
        }
    }
}
