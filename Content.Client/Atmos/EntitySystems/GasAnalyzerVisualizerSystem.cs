using Content.Client.Atmos.Components;
using Content.Shared.Atmos.Components;
using Robust.Client.GameObjects;

namespace Content.Client.Atmos.EntitySystems;

/// <summary>
/// This handles...
/// </summary>
public sealed class GasAnalyzerVisualizerSystem : VisualizerSystem<GasAnalyzerVisualsComponent>
{
    protected override void OnAppearanceChange(EntityUid uid, GasAnalyzerVisualsComponent component, ref AppearanceChangeEvent args)
    {
        if (!TryComp<SpriteComponent>(uid, out var sprite))
            return;

        if (!args.Component.TryGetData<GasAnalyzerVisualState>(GasAnalyzerVisuals.VisualState, out var visualState))
            return;

        switch (visualState)
        {
            case GasAnalyzerVisualState.Off:
                sprite.LayerSetState(0, component.StateOff);
                break;
            case GasAnalyzerVisualState.Working:
                sprite.LayerSetState(0, component.StateWorking);
                break;
            default:
                sprite.LayerSetState(0, component.StateOff);
                break;
        }
    }
}
