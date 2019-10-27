export enum UserActionTypes {
    CreateUser= '[User] Create User',
    CreateUserSuccess = '[User] Create User Success',
    CreateUserFailure = '[User] Create User Failure',
    CreateMerchantUser= '[MerchantUser] Create MerchantUser',
    CreateMerchantUserSuccess = '[MerchantUser] Create MerchantUser Success',
    CreateMerchantUserFailure = '[MerchantUser] Create MerchantUser Failure',
    SignInUser = '[User] SignIn User',
    SignInUserSuccess = '[User] SignIn User Success',
    SignInUserFailure = '[User] SignIn User Failure',
    SignOutUser = '[User] SignOut User',
    SignOutUserSuccess = '[User] SignOut User Success',
    SignOutUserFailure = '[User] SignOut User Failure',
    ForgotPassword = '[User]Forgot Password',
    ForgotPasswordSuccess = '[User]Forgot Password Success',
    ForgotPasswordFailure = '[User]Forgot Password Failure',
    ResetPassword = '[User] Reset Password',
    ResetPasswordSuccess = '[User] Reset Password Success',
    ResetPasswordFailure = '[User] Reset Password Failure',
};