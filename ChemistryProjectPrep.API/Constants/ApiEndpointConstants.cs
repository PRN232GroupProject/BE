namespace ChemistryProjectPrep.API.Constants
{
    public class ApiEndpointConstants
    {
        static ApiEndpointConstants() { }

        public const string ApiEndpoint = "api";
        public const string AuthEndpoint = ApiEndpoint + "/auth";
        public const string UserEndpoint = ApiEndpoint + "/users";
        public const string TestEndpoint = ApiEndpoint + "/tests";
        public static class Auth
        {
            public const string LoginEndpoint = AuthEndpoint + "/login";
            public const string RegisterEndpoint = AuthEndpoint + "/register";
            public const string SendResetCodeEndpoint = AuthEndpoint + "/reset";
            public const string VerifyResetCodeEndpoint = AuthEndpoint + "/verify/reset";
            public const string DisableAccountEndpoint = AuthEndpoint + "/disable";
            public const string VerifyDisableCodeEndpoint = AuthEndpoint + "/verify/disable";
            public const string LogoutEndpoint = AuthEndpoint + "/logout";
        }

        public static class User
        {
            public const string GetAllUsersEndpoint = UserEndpoint;
            public const string GetUserEndpoint = UserEndpoint + "/{id}";
            public const string GetCurrentUserEndpoint = UserEndpoint + "/current";
            public const string GetUsersByRoleEndpoint = GetAllUsersEndpoint + "/{role}";
            public const string CreateUserEndpoint = UserEndpoint;
            public const string UpdateUserEndpoint = UserEndpoint;
            public const string DeleteUserEndpoint = UserEndpoint;
            public const string UpdateProfileEndpoint = UserEndpoint + "/profile";
            public const string UpdatePasswordEndpoint = UserEndpoint + "/change-password";
        }
        public static class Test
        {
            public const string GetAllTestsEndpoint = TestEndpoint;
            public const string GetTestByIdEndpoint = TestEndpoint + "/{id}";
            public const string CreateTestEndpoint = TestEndpoint;
            public const string UpdateTestEndpoint = TestEndpoint + "/{id}";
            public const string DeleteTestEndpoint = TestEndpoint + "/{id}";
            public const string AddQuestionsToTestEndpoint = TestEndpoint + "/{id}/questions";
            public const string RemoveQuestionFromTestEndpoint = TestEndpoint + "/{id}/questions/{questionId}";
            public const string GetTestsCreatedByMeEndpoint = TestEndpoint + "/created-by-me";
        }
    }
}
