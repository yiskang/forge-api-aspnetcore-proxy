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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
#endregion

namespace Autodesk.Forge.Proxy
{
    /// <summary>
    /// Proxy Middleware
    /// </summary>
    public class ForgeProxyMiddleware
    {
        private const int DefaultWebSocketBufferSize = 4096;

        private readonly RequestDelegate _next;
        private readonly ProxyOptions _options;

        private static readonly string[] NotForwardedWebSocketHeaders = new[] { "Connection", "Host", "Upgrade", "Sec-WebSocket-Key", "Sec-WebSocket-Version" };

        public ForgeProxyMiddleware(RequestDelegate next, IOptions<ProxyOptions> options)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _next = next;
            _options = options.Value;
        }

        public Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var pathString = context.Request.Path;
            var proxyService = context.RequestServices.GetRequiredService<ForgeProxyService>();
            var opts = (ForgeSharedProxyOptions)proxyService.Options;
            var proxyUri = ((ForgeSharedProxyOptions)proxyService.Options).ProxyUri;
            if (!string.IsNullOrWhiteSpace(proxyUri))
            {
                var path = context.Request.Path.Value.Replace(proxyUri, string.Empty);
                pathString = new PathString(path);
            }

            var uri = new Uri(UriHelper.BuildAbsolute(opts.Scheme, opts.Host, _options.PathBase, pathString, context.Request.QueryString.Add(_options.AppendQuery)));
            return context.ProxyForgeRequest(uri);
        }
    }
}
