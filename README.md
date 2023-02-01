# Sharpnado.MaterialFrame.Maui

<p align="left"><img src="Docs/material_frame.png" height="180"/>

Get it from NuGet:

[![Nuget](https://img.shields.io/nuget/v/Sharpnado.MaterialFrame.svg)](https://www.nuget.org/packages/Sharpnado.MaterialFrame)

| Supported platforms        |
|----------------------------|
| :heavy_check_mark: Android | 
| :heavy_check_mark: iOS     |


![Presentation](Docs/github_banner.png)

## Initialization

* On Core project in `MauiProgram.cs`:

For the namespace xaml schema to work (remove duplicates xml namespace: [see this xamarin doc](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/xaml/custom-namespace-schemas)), you need to call tabs and shadows initializers from the `App.xaml.cs` file like this:

```csharp
public static MauiApp CreateMauiApp()
{

var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
     .UseSharpnadoMaterialFrame(false)
    ...
}
```


