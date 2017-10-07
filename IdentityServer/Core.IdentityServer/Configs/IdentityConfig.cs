using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Core.IdentityServer.Configs
{
    public static class IdentityConfig
    {
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username =  "Frank",
                    Password = "pwd",

                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Frank"),
                        new Claim("family_name", "Underwook")
                    }
                },

				new TestUser
				{
					SubjectId = "2",
					Username =  "Claire",
					Password = "pwd",

					Claims = new List<Claim>
					{
						new Claim("given_name", "Claire"),
						new Claim("family_name", "Underwook")
					}
				}
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {

            };
        }
    }
}
