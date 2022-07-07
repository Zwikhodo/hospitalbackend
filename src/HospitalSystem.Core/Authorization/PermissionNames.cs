namespace HospitalSystem.Authorization
{
    public static class PermissionNames
    {
        public const string Pages_Tenants = "Pages.Tenants";

        public const string Pages_Users = "Pages.Users";
        public const string Pages_Users_Activation = "Pages.Users.Activation";
        public const string Pages_UpdateInventory = "Pages.UpdateInventory";
        public const string Pages_Roles = "Pages.Roles";
        //Doctor Permissions
        public const string Pages_PrescribedTests = "Pages.PrescribedTests";
        public const string Pages_Overview = "Pages.Overview";
        public const string Pages_PatientReport = "Pages.PatientReport";
        public const string Pages_Procedures = "Pages.Procedures";

        //Nurse Permissions
        public const string Pages_Examination = "Pages.Examination";
        public const string Pages_Admission = "Pages.Admission";

        //Receptionist Permissions
        public const string Pages_CreateAppointment = "Pages.CreateAppointment";
        public const string Pages_BillCreate = "Pages.BillCreate";

    }
}
