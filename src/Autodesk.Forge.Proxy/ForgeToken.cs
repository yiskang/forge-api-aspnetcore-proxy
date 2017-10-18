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
using Newtonsoft.Json;
#endregion

namespace Autodesk.Forge.Proxy
{
  public class ForgeToken
  {
    [JsonProperty("token_type")]
    public string TokenType { get; set; }

    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
  }
}