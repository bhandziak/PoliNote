using PoliNote.Services.auth;

namespace PoliNote.Services.Auth
{
    public class IsOwnerService(AuthService authService, IHttpContextAccessor httpContextAccessor)
    {
        public bool CanUserEditOrDelete(int resourceOwnerId)
        {
            var currentUserId = authService.GetCurrentUserId();

            // not logged in
            if (currentUserId == null)
                return false;

            // admin check
            var user = httpContextAccessor.HttpContext?.User;
            if (user != null && user.IsInRole("Admin"))
            {
                return true;
            }

            // owner check
            return resourceOwnerId == currentUserId;
        }
    }
}
