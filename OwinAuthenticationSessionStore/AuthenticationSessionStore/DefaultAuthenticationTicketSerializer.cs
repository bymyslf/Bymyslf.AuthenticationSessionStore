namespace Bymyslf.AuthenticationSessionStore
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Bymyslf.AuthenticationSessionStore.Extensions;
    using Microsoft.Owin.Security;
    using Newtonsoft.Json.Linq;

    public class DefaultAuthenticationTicketSerializer : IAuthenticationTicketSerializer
    {
        public AuthenticationTicket Deserialize(string @string)
        {
            if (@string.IsNull())
            {
                return null;
            }

            dynamic jsonTicket = JValue.Parse(@string);

            var claims = new List<Claim>();
            foreach (dynamic claim in jsonTicket.Identity.Claims)
            {
                claims.Add(new Claim(claim.Type.ToString(), claim.Value.ToString()));
            }

            var propertiesDict = new Dictionary<string, string>();
            foreach (dynamic prop in jsonTicket.Properties)
            {
                propertiesDict.Add(prop.Key.ToString(), prop.Value.ToString());
            }

            var identity = new ClaimsIdentity(claims, jsonTicket.Identity.AuthenticationType.ToString());
            var properties = new AuthenticationProperties(propertiesDict);

            return new AuthenticationTicket(identity, properties);
        }

        public string Serialize(AuthenticationTicket ticket)
        {
            dynamic claims = new JArray();
            foreach (dynamic claim in ticket.Identity.Claims)
            {
                dynamic cl = new JObject();
                cl.Type = claim.Type;
                cl.Value = claim.Value;
                claims.Add(cl);
            }

            dynamic properties = new JArray();
            foreach (var prop in ticket.Properties.Dictionary)
            {
                dynamic p = new JObject();
                p.Key = prop.Key;
                p.Value = prop.Value;
                properties.Add(p);
            }

            dynamic jsonTicket = new JObject();
            jsonTicket.Identity = new JObject();
            jsonTicket.Properties = new JObject();

            jsonTicket.Identity.AuthenticationType = ticket.Identity.AuthenticationType;
            jsonTicket.Identity.Claims = claims;
            jsonTicket.Properties = properties;

            return (jsonTicket as JObject).ToString();
        }
    }
}