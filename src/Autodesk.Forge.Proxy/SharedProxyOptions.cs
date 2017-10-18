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
    public class SharedProxyOptions
    {
        private int? _webSocketBufferSize;

        /// <summary>
        /// Message handler used for http message forwarding.
        /// </summary>
        public HttpMessageHandler MessageHandler { get; set; }

        /// <summary>
        /// Allows to modify HttpRequestMessage before it is sent to the Message Handler.
        /// </summary>
        public Func<HttpRequest, HttpRequestMessage, Task> PrepareRequest { get; set; }

        /// <summary>
        /// True to retrieving access token from server side, it'll ignore client sides'.
        /// </summary>
        public bool UseServerSideAuthorization { get; set; } = true;

        /// <summary>
        /// Keep-alive interval for proxied Web Socket connections.
        /// </summary>
        public TimeSpan? WebSocketKeepAliveInterval { get; set; }

        /// <summary>
        /// Internal send and receive buffers size for proxied Web Socket connections.
        /// </summary>
        public int? WebSocketBufferSize
        {
            get { return _webSocketBufferSize; }
            set
            {
                if (value.HasValue && value.Value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _webSocketBufferSize = value;
            }
        }
    }
}
