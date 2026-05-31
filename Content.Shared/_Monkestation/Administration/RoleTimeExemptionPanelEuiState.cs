using Content.Shared.Eui;
using Content.Shared.Roles;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._Monkestation.Administration;

[Serializable, NetSerializable]
public sealed class RoleTimeExemptionPanelEuiState(
    string playerName,
    ProtoId<JobPrototype>[] jobExemptions,
    ProtoId<AntagPrototype>[] antagExemptions,
    bool hasAdmin)
    : EuiStateBase
{
    public string PlayerName { get; set; } = playerName;
    public ProtoId<JobPrototype>[] JobExemptions { get; set; } = jobExemptions;
    public ProtoId<AntagPrototype>[] AntagExemptions { get; set; } = antagExemptions;
}

public static class RoleTimeExemptionPanelEuiStateMsg
{
    [Serializable, NetSerializable]
    public sealed class SetExemptionsRequest(RoleTimeExemptions exemptions) : EuiMessageBase
    {
        public RoleTimeExemptions Exemptions { get; } = exemptions;
    }

    [Serializable, NetSerializable]
    public sealed class GetPlayerInfoRequest(string username) : EuiMessageBase
    {
        public string PlayerUsername { get; set; } = username;
    }
}

/// <summary>
///     Contains all the data related to a particular playtime exemption action created by the PlaytimeExemptionPanel window.
/// </summary>
[Serializable, NetSerializable]
public sealed record RoleTimeExemptions(
    string? Target,
    ProtoId<JobPrototype>[]? ExemptJobs,
    ProtoId<AntagPrototype>[]? ExemptAntags)
{
    public readonly string? Target = Target;
    public readonly ProtoId<JobPrototype>[]? ExemptJobs = ExemptJobs;
    public readonly ProtoId<AntagPrototype>[]? ExemptAntags = ExemptAntags;
}
