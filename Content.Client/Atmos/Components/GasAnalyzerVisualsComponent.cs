namespace Content.Client.Atmos.Components;

/// <summary>
/// This is used for...
/// </summary>
[RegisterComponent]
public sealed class GasAnalyzerVisualsComponent : Component
{
    [DataField("state_off")]
    public string? StateOff;
    [DataField("state_working")]
    public string? StateWorking;
}
