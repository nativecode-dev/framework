# NativeCode

## Installation

First, you want to install the platform independent packages.

```bash
Install-Package NativeCode.Core
Install-Package NativeCode.Core.Data
```

If you are writing a Windows desktop applications, you should also install
the desktop assemblies for .NET.

```bash
Install-Package NativeCode.Core.DotNet
```

If you are writing a web application with WebForms, you will require the
Web package as well. 

```bash
Install-Package NativeCode.Core.Web
```

In addition to the Web package, if your application is an MVC or WebAPI
application, be sure to install and use these platforms.

```bash
Install-Package NativeCode.Web.AspNet.Mvc
Install-Package NativeCode.Web.AspNet.WebApi
```


If you require access to the WIN32 API, you should also install the Win32
package, which contains common WIN32 API methods.

```bash
Install-Package NativeCode.Core.DotNet.Win32
```
