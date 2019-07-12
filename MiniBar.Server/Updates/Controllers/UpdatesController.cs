using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Common.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SharedEntities.DTO.Updates;

namespace Updates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdatesController : ApiControllerBase
    {
        IHostingEnvironment HostingEnvironment;
        public UpdatesController(IServiceProvider serviceProvider, IHostingEnvironment env) : base(serviceProvider)
        {
            HostingEnvironment = env;
        }

        private string GetFullPath(string relativePath)
        {
            return Path.Combine(HostingEnvironment.WebRootPath, relativePath);
        }

        const string MajorFolderFormat = "v{0}";

        [HttpGet("check/{major}")]
        public List<AssemblyDTO> GetLatestFileInfo(int major)
        {
            string majorFolder = GetFullPath(String.Format(MajorFolderFormat, major));
            if (Directory.Exists(majorFolder))
            {
                DirectoryInfo majorDirectory = new DirectoryInfo(majorFolder);
                var sorted = majorDirectory.GetDirectories().ToList();
                sorted.Sort(new Comparison<DirectoryInfo>((x, y) => {
                    string[] xversions = x.Name.Split('.');
                    if (xversions.Length != 3)
                        return -1;
                    string[] yversions = y.Name.Split('.');
                    if (yversions.Length != 3)
                        return 1;
                    int xMinor = Int32.Parse(xversions[0]);
                    int xBuild = Int32.Parse(xversions[1]);
                    int xRevision = Int32.Parse(xversions[2]);
                    int yMinor = Int32.Parse(yversions[0]);
                    int yBuild = Int32.Parse(yversions[1]);
                    int yRevision = Int32.Parse(yversions[2]);
                    if (xMinor > yMinor) return 1;
                    else if (xMinor < yMinor) return -1;
                    else
                    {
                        if (xBuild > yBuild) return 1;
                        else if (xBuild < yBuild) return -1;
                        else
                        {
                            if (xRevision > yRevision) return 1;
                            else if (xRevision < yRevision) return -1;
                            else
                            {
                                return 0;
                            }
                        }
                    }
                }));
                DirectoryInfo assembliesDir = sorted.LastOrDefault();
                if(assembliesDir != null)
                {
                    return assembliesDir.GetFiles().Select(f => {
                        Assembly assembly = Assembly.LoadFrom(f.FullName);
                        Version version = assembly.GetName().Version;
                        return new AssemblyDTO {
                            Name = f.Name,
                            Build = version.Build,
                            Major = version.Major,
                            Minor = version.Minor,
                            Revision = version.Revision,
                            FullName = assembly.FullName
                        };
                    }).ToList();
                }
            }
            throw new ArgumentException("No files");
        }

        //[HttpGet("texts/distinct")]
        //public async Task<List<string>> GetAllTextKeys()
        //{
        //    return await this.ServiceProvider.GetService<ILanguageManager>().GetAllTextKeysAsync();
        //}
    }
}
