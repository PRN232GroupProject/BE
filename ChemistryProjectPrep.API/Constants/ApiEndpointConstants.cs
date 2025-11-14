namespace ChemistryProjectPrep.API.Constants
{
    public class ApiEndpointConstants
    {
        static ApiEndpointConstants() { }

        public const string RootEndpoint = "/api";
        public const string ApiVersion = "/v1";
        public const string ApiEndpoint = RootEndpoint + ApiVersion;
        public const string AuthEndpoint = ApiEndpoint + "/auth";
        public const string UserEndpoint = ApiEndpoint + "/users";

        public static class Auth
        {
            public const string LoginEndpoint = ApiEndpoint + "/login";
            public const string RegisterEndpoint = ApiEndpoint + "/register";
            public const string SendResetCodeEndpoint = ApiEndpoint + "/reset";
            public const string VerifyResetCodeEndpoint = ApiEndpoint + "/verify/reset";
            public const string DisableAccountEndpoint = ApiEndpoint + "/disable";
            public const string VerifyDisableCodeEndpoint = ApiEndpoint + "/verify/disable";
            public const string LogoutEndpoint = ApiEndpoint + "/logout";
        }

        public static class User
        {
            public const string GetUserEndpoint = UserEndpoint + "/{id}";
            public const string GetCurrentUserEndpoint = ApiEndpoint + "/current";
            public const string GetAllUsersEndpoint = UserEndpoint;
            public const string GetUsersByRoleEndpoint = GetAllUsersEndpoint + "/{role}";
            public const string CreateUserEndpoint = UserEndpoint;
            public const string UpdateUserEndpoint = UserEndpoint;
            public const string DeleteUserEndpoint = UserEndpoint;
        }
    }
}
