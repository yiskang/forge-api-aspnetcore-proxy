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
using Autodesk.Forge.Proxy;
#endregion

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ProxyServiceCollectionExtensions
    {
        public static IServiceCollection AddProxy(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddSingleton<ProxyService>();
        }

        public static IServiceCollection AddProxy(this IServiceCollection services, Action<SharedProxyOptions> configureOptions)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            services.Configure(configureOptions);
            return services.AddSingleton<ProxyService>();
        }

        public static IServiceCollection AddForgeProxy(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddSingleton<ForgeProxyService>();
        }

        public static IServiceCollection AddForgeProxy(this IServiceCollection services, Action<ForgeSharedProxyOptions> configureOptions)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            services.Configure(configureOptions);
            return services.AddSingleton<ForgeProxyService>();
        }
    }
}
