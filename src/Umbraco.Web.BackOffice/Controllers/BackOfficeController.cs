﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Core.Configuration;
using Umbraco.Core.Hosting;
using Umbraco.Core.IO;
using Umbraco.Core.Runtime;
using Umbraco.Core.WebAssets;
using Umbraco.Web.BackOffice.Filters;
using Umbraco.Web.Common.ActionResults;
using Umbraco.Web.WebAssets;

namespace Umbraco.Web.BackOffice.Controllers
{
    public class BackOfficeController : Controller
    {
        private readonly IRuntimeMinifier _runtimeMinifier;
        private readonly IGlobalSettings _globalSettings;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BackOfficeController(IRuntimeMinifier runtimeMinifier, IGlobalSettings globalSettings, IHostingEnvironment hostingEnvironment)
        {
            _runtimeMinifier = runtimeMinifier;
            _globalSettings = globalSettings;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns the JavaScript main file including all references found in manifests
        /// </summary>
        /// <returns></returns>
        [MinifyJavaScriptResult(Order = 0)]
        public async Task<IActionResult> Application()
        {
            var result = await _runtimeMinifier.GetScriptForLoadingBackOfficeAsync(_globalSettings, _hostingEnvironment);

            return new JavaScriptResult(result);
        }
    }
}