using System.Linq;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;

namespace AnomalousZonePlugin.Classes.RemoteKeycard
{
    public static class Permissions
    {
        public static bool HasKeycardPermission(this Player player, KeycardPermissions permissions, bool requiresAllPermissions = false)
        {
            if (player.IsEffectActive<AmnesiaItems>() || player.IsEffectActive<AmnesiaVision>())
            {
                return false;
            }
            return requiresAllPermissions ?
                player.Items.Any(item => item is Keycard keycard && keycard.Permissions.HasFlag(permissions)) :
                player.Items.Any(item => item is Keycard keycard && keycard.Permissions.HasFlag(permissions));
        }
    }
}