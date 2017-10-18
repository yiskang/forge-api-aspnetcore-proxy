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
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using Autodesk.Forge.Proxy;
#endregion

namespace Autodesk.Forge.Proxy
{
    public class ForgeProxyService : ProxyService
    {
        private ForgeToken token;

        private DateTime expiration;

        public ForgeProxyService(IOptions<ForgeSharedProxyOptions> options)
                : base(options) {}

        public ForgeToken Token {
            get {
                if( token == null || DateTime.Now >= expiration )
                {
                    token = this.FetchToken().Result;
                    expiration = DateTime.Now.AddSeconds( token.ExpiresIn - 600 );
                }
                return token;
            }
        }

        internal async Task<ForgeToken> FetchToken()
        {
            var requestMessage = new HttpRequestMessage();

            var opts = ((ForgeSharedProxyOptions)Options);
            
            var uri = new Uri(UriHelper.BuildAbsolute(opts.Scheme, opts.Host, "/authentication/v1/authenticate"));
            requestMessage.Headers.Host = uri.Authority;
            requestMessage.RequestUri = uri;
            requestMessage.Method = HttpMethod.Post;

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", opts.ClientId),
                new KeyValuePair<string, string>("client_secret", opts.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("scope", "viewables:read"),
            });
            requestMessage.Content = content;

            var result = await Client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
            var json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ForgeToken>(json);
        }
    }
}
