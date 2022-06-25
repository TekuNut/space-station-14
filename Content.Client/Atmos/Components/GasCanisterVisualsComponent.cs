namespace Content.Client.Atmos.Components;

[RegisterComponent]
public sealed class GasCanisterVisualsComponent : Component
{
    [DataField("pressureStates")]
    public string[] StatePressure = {"", "", "", ""};

    [DataField("insertedTankState")]
    public string InsertedTankState = string.Empty;
}
