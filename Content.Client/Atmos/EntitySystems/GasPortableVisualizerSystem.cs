using Content.Client.Atmos.Components;
using Content.Shared.Atmos.Piping.Unary.Components;
using Robust.Client.GameObjects;

namespace Content.Client.Atmos.EntitySystems;

public sealed class GasPortableVisualizerSystem : VisualizerSystem<GasPortableVisualsComponent>
{
    /// <inheritdoc/>
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<GasPortableVisualsComponent, ComponentInit>(OnComponentInit);
    }

    private void OnComponentInit(EntityUid uid, GasPortableVisualsComponent component, ComponentInit args)
    {
        if (!TryComp<SpriteComponent>(uid, out var sprite))
            return;

        if (component.StateConnected == null)
            return;

        sprite.LayerMapSet(GasPortableVisualLayers.ConnectedToPort, sprite.AddLayerState(component.StateConnected));
        sprite.LayerSetVisible(GasPortableVisualLayers.ConnectedToPort, false);
    }

    protected override void OnAppearanceChange(EntityUid uid, GasPortableVisualsComponent component, ref AppearanceChangeEvent args)
    {
        if (TryComp<SpriteComponent>(uid, out var sprite)
            && args.Component.TryGetData(GasPortableVisuals.ConnectedState, out bool isConnected))
        {
            sprite.LayerSetVisible(GasPortableVisualLayers.ConnectedToPort, isConnected);
        }
    }
}

public enum GasPortableVisualLayers : byte
{
    ConnectedToPort,
}
