using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Content.Server.Database;
using Content.Shared._Monkestation.Players;
using Content.Shared.Database;
using Robust.Shared.Network;
using Robust.Shared.Player;

namespace Content.Server._Monkestation.Administration.Managers;

public sealed class RoleTimeExemptionManager : IRoleTimeExemptionManager, IPostInjectInit
{
    [Dependency] private readonly ILogManager _logManager = default!;
    [Dependency] private readonly INetManager _netManager = default!;
    [Dependency] private readonly UserDbDataManager _userDbData = default!;

    private ISawmill _sawmill = default!;

    private readonly Dictionary<ICommonSession, List<BanDef>> _cachedRoleTimeExemptions = new();

    private const string SawmillId = "admin.roletimeexemptions";

    public void Initialize()
    {
        _netManager.RegisterNetMessage<MsgRoleTimeExemptions>();
        // TODO: Support db messages for cross server role time exemption updates
        // currently out of scope because that's a lot of effort and we only have the one server, they can relog

        _userDbData.AddOnLoadPlayer(CachePlayerData);
        _userDbData.AddOnPlayerDisconnect(ClearPlayerData);
    }

    private async Task CachePlayerData(ICommonSession player, CancellationToken cancel)
    {
        var flags = await _db.GetBanExemption(player.UserId, cancel);

        var netChannel = player.Channel;
        ImmutableArray<byte>? hwId = netChannel.UserData.HWId.Length == 0 ? null : netChannel.UserData.HWId;
        var modernHwids = netChannel.UserData.ModernHWIds;
        var roleBans = await _db.GetBansAsync(
            netChannel.RemoteEndPoint.Address,
            player.UserId,
            hwId,
            modernHwids,
            false,
            type: BanType.Role);

        var userRoleBans = new List<BanDef>();
        foreach (var ban in roleBans)
        {
            userRoleBans.Add(ban);
        }

        cancel.ThrowIfCancellationRequested();
        _cachedBanExemptions[player] = flags;
        _cachedRoleBans[player] = userRoleBans;

        SendRoleBans(player);
    }

    private void ClearPlayerData(ICommonSession player)
    {
        _cachedBanExemptions.Remove(player);
    }

    public void PostInject()
    {
        _sawmill = _logManager.GetSawmill(SawmillId);
    }
}
