﻿using System;
namespace firstapp.ENUMS
{
    public enum Pages
    {
        Landing,
        AfterLogin,
        RefreshSession
    }
    public enum ServerReplyStatus
    {
        Success,
        Fail,
        UserNameAlreadyUsed,
        PasswordRequirementsFailed,
        NotConfirmed,
        InvalidPassword,
        UserNotFound,
        Unknown
    }
    public enum AuthType
    {
        SignUp,
        SignIn,
        ForgotPassword,
        ResetPass,
        RefreshSession
    }
    public enum CognitoResult
    {
        Unknown,
        Ok,
        PasswordChangeRequred,
        SignupOk,
        NotAuthorized,
        Error,
        UserNotFound,
        UserNameAlreadyUsed,
        PasswordRequirementsFailed,
        NotConfirmed,
        InvalidPassword
    }
    public enum SessionStatus
    {
        LoggedOut,
        LoggedInWithActiveSession,
        LoggedInWithExpiredSession
    }

    public enum ServerAction
    {
        UserProfileUpdate,
        DeviceConnect,
        DeviceDelete,
        LoadControllers,
        LoadMacros,
        RecordMacro,
        UpdateMacro,
        DeleteMacro,
        UpdateConfig,
        LoadConfig,
        AddBundle,
        LoadBundle,
        DeleteBundle,
        NullVal
    }

}
