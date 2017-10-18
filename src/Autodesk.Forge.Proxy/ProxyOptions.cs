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
using Microsoft.AspNetCore.Http;
#endregion

namespace Autodesk.Forge.Proxy
{
    /// <summary>
    /// Proxy Options
    /// </summary>
    public class ProxyOptions
    {
        /// <summary>
        /// Destination uri scheme
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Destination uri host
        /// </summary>
        public HostString Host { get; set; }

        /// <summary>
        /// Destination uri path base to which current Path will be appended
        /// </summary>
        public PathString PathBase { get; set; }

        /// <summary>
        /// Query string parameters to append to each request
        /// </summary>
        public QueryString AppendQuery { get; set; }
    }
}
