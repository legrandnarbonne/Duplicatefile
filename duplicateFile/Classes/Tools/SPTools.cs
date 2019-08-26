using Microsoft.SharePoint.Client;

namespace duplicateFile.Classes
{
    public static class SPTools
    {
        public static user getFileLastModifiedUser(string siteURL, string fileURL)
        {
            string relativeURL = fileURL.Substring(2);//remove drive letter
            relativeURL = relativeURL.Replace("\\", "/");

            ClientContext clientContext = new ClientContext(siteURL);
            Web site = clientContext.Web;
            var file = site.GetFileByServerRelativeUrl(relativeURL);
            var modifier = file.ModifiedBy;

            clientContext.Load(file);
            clientContext.Load(modifier);
            clientContext.ExecuteQuery();

            return new user()
            {
                login = modifier.LoginName,
                title = modifier.Title,
                mail = modifier.Email
            };
        }
    }

    public class user
    {
        public string login;
        public string mail;
        public string title;
    }
}