using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AccuVAPI.Configuration;
using AccuVAPI.Contracts;

namespace AccuVAPI.Operations
{
    public interface IActiveDirectoryOperations : IOperationStore
    {
        Task<IEnumerable<ActiveDirectoryUser>> GetAllAsync();
        Task<bool> ValidateUserAsync(string username, string password);
        Task<ActiveDirectoryUser> FindAsync(string userId);
    }


    public class ActiveDirectoryOperations : IActiveDirectoryOperations
    {
        private ActiveDirectorySection Config
        {
            get
            {
                var section = (AccuVapiConfigSection) System.Configuration.ConfigurationManager.GetSection("accuV");
                if(section == null)
                    throw new ConfigurationErrorsException("The accuVAPI section is not defined");
                return section.ActiveDirectoryConfig;
            }
        }

        private string Domain
        {
            get
            {
                string connection = Config.LdapConnection;
                Regex regex = new Regex("LDAP://(.*)/");
                return regex.Match(connection).Groups[1].Value;
            }
        }

        public async Task <IEnumerable<ActiveDirectoryUser>> GetAllAsync()
        {
            return await Task.Run((Func<IEnumerable<ActiveDirectoryUser>>) GetAllAdUsersInternal);
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            return await Task.Run(() => ValidateUserInternal(username, password));
        }

        public Task<ActiveDirectoryUser> FindAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public bool ValidateUserInternal(string username, string password)
        {
            LdapConnection connection = new LdapConnection(Domain);
            try
            {
                connection.Bind(new NetworkCredential(username, password));
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Dispose();
            }
            return true;
        }

        private IEnumerable<ActiveDirectoryUser> GetAllAdUsersInternal()
        {
            var users = new List<ActiveDirectoryUser>();
            DirectoryEntry domainRoot = new DirectoryEntry(Config.LdapConnection);
            domainRoot.Username = Config.UserName;
            domainRoot.Password = Config.Password;
            DirectorySearcher search = new DirectorySearcher(domainRoot);
            search.Filter = "(&(objectClass=user)(objectCategory=person))";
            search.PropertiesToLoad.Add("samaccountname");
            search.PropertiesToLoad.Add("mail");
            search.PropertiesToLoad.Add("usergroup");
            search.PropertiesToLoad.Add("displayname");//first name
            SearchResult result;
            try
            {

                SearchResultCollection resultCollection = search.FindAll();
                if (resultCollection != null)
                {
                    for (int counter = 0; counter < resultCollection.Count; counter++)
                    {
                        result = resultCollection[counter];
                        var user = new ActiveDirectoryUser();
                        if (result.Properties.Contains("samaccountname"))
                            user.UserName = (String)result.Properties["samaccountname"][0];
                        if (result.Properties.Contains("mail"))
                            user.Email = (String)result.Properties["mail"][0];
                        if (result.Properties.Contains("displayname"))
                            user.DisplayName = (String)result.Properties["displayname"][0];

                        users.Add(user);

                    }
                }
            }
            catch (DirectoryServicesCOMException ex)
            {
                throw;
            }

            return users;
        }

    }
}
