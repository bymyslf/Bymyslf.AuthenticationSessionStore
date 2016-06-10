namespace Bymyslf.AuthenticationSessionStore
{
    using Microsoft.Owin.Security;

    public interface IAuthenticationTicketSerializer
    {
        string Serialize(AuthenticationTicket ticket);

        AuthenticationTicket Deserialize(string @string);
    }
}