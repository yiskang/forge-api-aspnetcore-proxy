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
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
#endregion

namespace Autodesk.Forge.Proxy
{
    /// <summary>
    /// Shared Proxy Options
    /// </summary>
    public class ForgeSharedProxyOptions : SharedProxyOptions
    {
        private static HostString HOST = new HostString("developer.api.autodesk.com");

        /// <summary>
        /// Proxy uri
        /// </summary>
        public string ProxyUri { get; set; }

        /// <summary>
        /// Autodesk Forge client id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Autodesk Forge client secret
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Destination uri scheme
        /// </summary>
        public string Scheme {
            get {
                return "https";
            }
        }

        /// <summary>
        /// Destination uri host
        /// </summary>
        public HostString Host {
            get {
                return HOST;
            }
        }
    }
}
