using System;
using System.Linq;
using System.Web.Security;

namespace ClinicaPokemon.Models
{
    public class SiteRole : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var context = new ClinicaDbContext())
            {
                var user = context.Utenti.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    return new string[] { };
                }
                switch (user.Ruolo)
                {
                    case 1:
                        return new string[] { "Utente" };
                    case 2:
                        return new string[] { "Veterinario" };
                    case 3:
                        return new string[] { "Dottore" };
                    case 4:
                        return new string[] { "Admin" };
                    default:
                        return new string[] { };
                }
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}