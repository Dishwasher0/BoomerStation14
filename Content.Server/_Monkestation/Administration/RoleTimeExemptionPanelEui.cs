using Content.Server.Administration.Managers;
using Content.Server.EUI;
using Content.Shared._Monkestation.Administration;
using Content.Shared.Administration;
using Content.Shared.Eui;

namespace Content.Server._Monkestation.Administration;

public sealed class RoleTimeExemptionPanelEui : BaseEui
{
    [Dependency] private readonly ILogManager _log = default!;
    [Dependency] private readonly IAdminManager _admins = default!;

    private readonly ISawmill _sawmill;

    private string PlayerName { get; set; } = string.Empty;

    public RoleTimeExemptionPanelEui()
    {
        IoCManager.InjectDependencies(this);

        _sawmill = _log.GetSawmill("admin.role_time_exemptions_eui");
    }

    public override EuiStateBase GetNewState()
    {
        var isAdmin = _admins.HasAdminFlag(Player, AdminFlags.Admin);
        return new RoleTimeExemptionPanelEuiState(PlayerName, [], [], isAdmin); // TODO: Populate
    }

    public override void HandleMessage(EuiMessageBase msg)
    {
        base.HandleMessage(msg);

        switch (msg)
        {
            case RoleTimeExemptionPanelEuiStateMsg.GetPlayerInfoRequest r:
                GetPlayerInfo(r.PlayerUsername);
                break;
            case RoleTimeExemptionPanelEuiStateMsg.SetExemptionsRequest r:
                SetExemptions(r.Exemptions);
                break;
        }
    }

    private void GetPlayerInfo(string username)
    {
        // TODO: Confirm this function is necessary
    }

    private void SetExemptions(RoleTimeExemptions exemptions)
    {

    }
}
