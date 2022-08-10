using System.Collections.Immutable;
using Content.Shared.Storage;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Shared.EntityList;

[Prototype("entityLootTable")]
public sealed class EntityLootTablePrototype : IPrototype
{
    [IdDataField]
    public string ID { get; } = default!;

    [DataField("entries")]
    public ImmutableList<EntitySpawnEntry> Entries = ImmutableList<EntitySpawnEntry>.Empty;

    /// <inheritdoc cref="EntitySpawnCollection.GetSpawns"/>
    public List<string?> GetSpawns(IRobustRandom? random = null)
    {
        return EntitySpawnCollection.GetSpawns(Entries, random);
    }
}
