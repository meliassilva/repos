using System;
using System.Net.Http;
using System.Threading.Tasks;
using Byui.Something.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Byui.Something.Web.Controllers
{
    
    [AllowAnonymous]
    public class MasterPagesViewComponent : ViewComponent
    {
        private readonly AppSettings _appSettings;
        private readonly IMemoryCache _memoryCache;
        private readonly IHostingEnvironment _env;

        public MasterPagesViewComponent(AppSettings appSettings, IMemoryCache memoryCache, IHostingEnvironment env)
        {
            _appSettings = appSettings;
            _memoryCache = memoryCache;
            _env = env;
        }

        public async Task<IHtmlContent> InvokeAsync(string type)
        {
            string content = null;
            switch (type)
            {
                case "Head":
                    content = await SetContent(_appSettings.HeadFileLocation);
                    break;
                case "Header":
                    content = await SetContent(_appSettings.HeaderFileLocation);
                    break;
                case "Footer":
                    content = await SetContent(_appSettings.FooterFileLocation);
                    break;
            }
            
            var html = new HtmlContentBuilder();
            html.AppendHtml(content);
            return html;
        }
        

        private async Task<string> SetContent(string fileLocation)
        {
            string key = "MasterPages" + fileLocation;
            //return "";
            if (string.IsNullOrEmpty(fileLocation)) return null;
            string content = null;
            try
            {
                if (!_memoryCache.TryGetValue(key, out content))
                {
                    if (fileLocation.StartsWith("~") || fileLocation.StartsWith("/"))
                    {
                        var webRoot = _env.WebRootPath;
                        fileLocation = System.IO.Path.Combine(webRoot, fileLocation);
                    }


                    var uri = new Uri(fileLocation, UriKind.RelativeOrAbsolute);
                    if (uri.IsFile && System.IO.File.Exists(fileLocation))
                    {
                        content = System.IO.File.ReadAllText(fileLocation);
                    }
                    else
                    {
                        var client = new HttpClient();
                        content = await client.GetStringAsync(uri);
                    }
                    // refresh at midnight
                    var date = new DateTimeOffset(DateTime.Today.AddDays(1));
                    _memoryCache.Set(key, content, date);
                }

            }
            catch (Exception ex)
            {
                
            }

            return content;
        }
    }

}
