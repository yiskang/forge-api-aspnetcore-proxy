//
// Copyright (c) Autodesk, Inc. All rights reserved
// Copyright (c) .NET Foundation. All rights reserved.
//
// Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// 
// Forge Proxy
// by Eason Kang - Autodesk Developer Network (ADN)
//

#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
#endregion

namespace Autodesk.Forge.Proxy.Samples.Controllers
{
    public class ViewerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
