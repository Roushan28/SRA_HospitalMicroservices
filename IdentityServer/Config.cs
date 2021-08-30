using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
          new Client[]
          {
                   new Client
                   {
                        ClientId = "pharmacydetailsClient",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        AllowedScopes = { "pharmacydetailsAPI" }
                   },
                   new Client
                   {
                        ClientId = "vaccineregiterClient",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        AllowedScopes = { "vaccinesregisterAPI" }
                   }
          };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope("pharmacydetailsAPI", "Pharmacy Details API"),
               new ApiScope("vaccinesregisterAPI", "Vaccines Register API")
           };

     
    }
}
