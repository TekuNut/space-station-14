using Content.Client.Atmos.Components;
using Content.Client.Computer;
using Content.Shared.Atmos.Piping.Binary.Components;
using Robust.Client.GameObjects;

namespace Content.Client.Atmos.EntitySystems;

public sealed class GasCanisterVisualizerSystem : VisualizerSystem<GasCanisterVisualsComponent>
{
    /// <inheritdoc/>
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<GasCanisterVisualsComponent, ComponentInit>(OnComponentInit);
    }

    private void OnComponentInit(EntityUid uid, GasCanisterVisualsComponent component, ComponentInit args)
    {
        if (!TryComp<SpriteComponent>(uid, out var sprite))
            return;

        sprite.LayerMapSet(GasCanisterVisualLayers.PressureLight, sprite.AddLayerState(component.StatePressure[0]));
        sprite.LayerSetShader(GasCanisterVisualLayers.PressureLight, "unshaded");
        sprite.LayerMapSet(GasCanisterVisualLayers.TankInserted, sprite.AddLayerState(component.InsertedTankState));
        sprite.LayerSetVisible(GasCanisterVisualLayers.TankInserted, false);
    }

    protected override void OnAppearanceChange(EntityUid uid, GasCanisterVisualsComponent component, ref AppearanceChangeEvent args)
    {
        if(!TryComp<SpriteComponent>(uid, out var sprite))
           return;

        // Update the canister lights.
        if (args.Component.TryGetData(GasCanisterVisuals.PressureState, out int pressureState)
            && (pressureState >= 0 && pressureState < component.StatePressure.Length))
        {
           sprite.LayerSetState(GasCanisterVisualLayers.PressureLight, component.StatePressure[pressureState]);
        }

        if(args.Component.TryGetData(GasCanisterVisuals.TankInserted, out bool inserted))
            sprite.LayerSetVisible(GasCanisterVisualLayers.TankInserted, inserted);
    }
}

public enum GasCanisterVisualLayers
{
    PressureLight,
    TankInserted,
}
