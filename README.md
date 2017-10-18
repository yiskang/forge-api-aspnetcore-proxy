![Platforms](https://img.shields.io/badge/platform-windows%20%7C%20osx%20%7C%20linux-lightgray.svg)
![.NET-Standard](https://img.shields.io/badge/.NET%20Standard-2.0-blue.svg)
![.NET-Core](https://img.shields.io/badge/.NET%20Core-2.0-blue.svg)
[![ASP.NET-Core-MVC](https://img.shields.io/badge/ASP.NET%20Core%20MVC-2.0-blue.svg)](https://asp.net/)
[![License](https://img.shields.io/:license-apache-blue.svg)](http://www.apache.org/licenses/LICENSE-2.0)

[![oAuth2](https://img.shields.io/badge/oAuth2-v1-green.svg)](http://developer.autodesk.com/)
[![Model-Derivative](https://img.shields.io/badge/Model%20Derivative-v2-green.svg)](http://developer.autodesk.com/)
[![Viewer](https://img.shields.io/badge/Viewer-v3.1-green.svg)](http://developer.autodesk.com/)

# Forge ASP.NET Core Proxy

## Overview
Securing your Forge Viewer token behind a ASP.NET Core proxy.

This project is modified from .NET Foundation's [ASP.NET Core Proxy](https://github.com/aspnet/Proxy).

### Requirements
* .NETStandard 2.0 or later
* A registered app on the <a href="https://developer.autodesk.com/myapps" target="_blank">Forge Developer portal</a>.
* Building the proxy library requires [Visual Studio Code](https://www.visualstudio.com/downloads/) to be installed.

### Dependencies
- [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json/) 10.0.3 or later
- [Microsoft.AspNetCore.WebSockets](https://www.nuget.org/packages/Microsoft.AspNetCore.WebSockets/) 2.0.0 or later
- [Microsoft.Extensions.Options](https://www.nuget.org/packages/Microsoft.Extensions.Options/) 2.0.0 or later

### Run the samples from sources
Run the following command to generate the DLL:
```shell
dotnet run samples/Autodesk.Forge.Proxy.Samples/Autodesk.Forge.Proxy.Samples.csproj
```

## Tutorial
Follow this tutorial to see a step-by-step authentication guide, and examples of how to use the Forge Proxy.

### Create an App
Create an app on the <a href="https://developer.autodesk.com/myapps" target="_blank">Forge Developer portal</a>.
Note the client key and client secret.

### Forge Credentials Setup
Copy your Forge credentials into your ASP.NET Core MVC app's `appsettings.json`, then replace corresponding fields.
```json
"Credentials": {
  "ClientId": "<YOUR_CLIENT_ID>",
  "ClientSecret": "<YOUR_CLIENT_SECRET>"
}
```

### ASP.NET Core MVC App Setup
Modify your `Startup` to this way to read settings from `appsettings.json`

```csharp
public Startup(IHostingEnvironment env)
{
    Environment = env;

    var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();

    Configuration = builder.Build();
}

public IHostingEnvironment Environment { get; set; }

public IConfiguration Configuration { get; }
```

### ASP.NET Core MVC App Service Setup
Add following lines to `ConfigureServices` in your `Startup.cs`.
```csharp
services.AddForgeProxy(options => {
    // The proxy enter point uri
    options.ProxyUri = @"/forgeproxy";

    // Get Forge credentials from app configuration
    var clientId = Configuration.GetSection("Credentials:ClientId").Value;
    var clientSecret = Configuration.GetSection("Credentials:ClientSecret").Value;

    if( string.IsNullOrWhiteSpace(clientId) || clientId == "<YOUR_CLIENT_ID>" )
    {
        throw new ArgumentNullException("The value of Credentials:ClientId in appsettings.json is invalid");
    }

    if( string.IsNullOrWhiteSpace(clientSecret) || clientId == "<YOUR_CLIENT_SECRET>" )
    {
        throw new ArgumentNullException("The value of Credentials:ClientSecret in appsettings.json is invalid");
    }

    // Set Forge credentials up
    options.ClientId = clientId;
    options.ClientSecret = clientSecret;

    // Add extra header to HTTP request
    options.PrepareRequest = ( originalRequest, message ) =>
    {
        message.Headers.Add("X-Forwarded-Host", originalRequest.Host.Host);
        return Task.FromResult(0);
    };
});

```

### Configure ASP.NET Core MVC App
Add following lines to `Configure` in your `Startup.cs`
```csharp
app.UseForgeProxy();
```

### Configure viewer endpoint
Initialize your viewer app in this way:
```javascript
Autodesk.Viewing.Initializer(options, function onInitialized() {
  // Change derivative endpoint to Proxy endpoint
  Autodesk.Viewing.endpoint.setApiEndpoint(window.location.origin + '/forgeproxy', '', true);
  viewerApp = new Autodesk.Viewing.ViewingApplication('viewer');
  viewerApp.registerViewer(viewerApp.k3D, Autodesk.Viewing.Private.GuiViewer3D, null);
  viewerApp.loadDocument(documentId, onDocumentLoadSuccess, onDocumentLoadFailure);
});
```

## Written by

Written by [Yi-Sheng Kang](https://www.linkedin.com/in/yi-sheng-kang-b4398492)

## License

See the [LICENSE](LICENSE) file for license rights and limitations (Apache-2.0).