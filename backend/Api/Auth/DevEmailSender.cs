using Microsoft.AspNetCore.Identity;

namespace Api.Auth;

public sealed class DevEmailSender<TUser> : IEmailSender<TUser> where TUser : class
{
    public Task SendConfirmationLinkAsync(TUser user, string email, string confirmationLink)
    {
        Console.WriteLine($"[DEV EMAIL] Confirmation for {email}: {confirmationLink}");
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(TUser user, string email, string resetLink)
    {
        Console.WriteLine($"[DEV EMAIL] Reset link for {email}: {resetLink}");
        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(TUser user, string email, string resetCode)
    {
        Console.WriteLine($"[DEV EMAIL] Reset code for {email}: {resetCode}");
        return Task.CompletedTask;
    }
}
