
[![NuGet](https://img.shields.io/nuget/v/Vhc.CoreUi.svg)](https://www.nuget.org/packages/Vhc.CoreUi)
[![License](https://img.shields.io/github/license/churivibhav/CoreUi.svg)](https://github.com/churivibhav/CoreUi/blob/master/LICENSE)

# CoreUi
Application builder library for .NET Framework Windows Forms and Console Applications, inspired by .NET Core 

CoreUi is built to bring ASP.NET Core dependancy injection and host builder pattern to any .NET standard application.

CoreUi can be used in console or Windows Forms applications built with .NET Framework 4.6.1+ or with .NET Core console applications.

## Usage

### Simple console application
`AppHostBuilder` is used to prepare the application.
The action passed to the `Run()` method is executed by the host.

```c#
    class Program
    {
        static void Main(string[] args) =>
            new AppHostBuilder().Build().Run(app => Console.WriteLine("Hello World!"));
    }
```

### Console application
Services and command-line arguments are registered at the `AppHostBuilder`.
To use them, `IAppHost app` is provided by the host in the `Run()` lambda function. 

```c#
    static void Main(string[] args)
    {
        IAppHost host = new AppHostBuilder()
            .UseArguments(args)
            .ConfigureServices(s => s.AddSingleton<IDemo, Demo>() )
            .Build();

        host.Run(app =>
        {
            var demo = app.Services.GetRequiredService<IDemo>();
            app.Arguments.ToList().ForEach(arg => Console.WriteLine(arg));
            Console.ReadKey();
        });
    }
  ```
