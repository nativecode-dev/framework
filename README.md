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

# License

Copyright 2017 NativeCode Development support@nativecode.com

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
associated documentation files (the "Software"), to deal in the Software without restriction,
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial
portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT
OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
