using Godot;

public class HUD : CanvasLayer
{
    [Signal]
    public delegate void StartGame();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Timer>("MessageTimer").Connect("timeout", this, nameof(OnMessageTimerTimeout));
        GetNode<Button>("StartButton").Connect("pressed", this, nameof(OnStartButtonPressed));

        ShortCut shortCut = new ShortCut();
        InputEventAction eventAction = new InputEventAction();

        eventAction.Action = "ui_select";
        shortCut.Shortcut = eventAction;
        
        GetNode<Button>("StartButton").Shortcut = shortCut;
    }

    public void ShowMessage(string text)
    {
        var messageLabel = GetNode<Label>("MessageLabel");
        messageLabel.Text = text;
        messageLabel.Show();

        GetNode<Timer>("MessageTimer").Start();
    }

    async public void ShowGameOver()
    {
        ShowMessage("Game Over");

        var messageTimer = GetNode<Timer>("MessageTimer");
        await ToSignal(messageTimer, "timeout");

        var messageLabel = GetNode<Label>("MessageLabel");
        messageLabel.Text = "Dodge the\nCreeps!";
        messageLabel.Show();

        GetNode<Button>("StartButton").Show();
    }

    public void UpdateScore(int score)
    {
        GetNode<Label>("ScoreLabel").Text = score.ToString();
    }

    public void OnStartButtonPressed()
    {
        GetNode<Button>("StartButton").Hide();
        EmitSignal("StartGame");
    }

    public void OnMessageTimerTimeout()
    {
        GetNode<Label>("MessageLabel").Hide();
    }
}
