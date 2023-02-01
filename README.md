# Sharpnado.MaterialFrame.Maui

## this project is only an adaptation for net maui

<p align="left"><img src="Docs/material_frame.png" height="180"/>

Get it from NuGet:

[![Nuget](https://img.shields.io/nuget/v/Sharpnado.MaterialFrame.svg)](https://www.nuget.org/packages/Sharpnado.MaterialFrame)



official project [Sharpnado.MaterialFrame](https://github.com/roubachof/Sharpnado.MaterialFrame)
| Supported platforms        |
|----------------------------|
| :heavy_check_mark: Android | 
| :heavy_check_mark: iOS     |


![Presentation](Docs/github_banner.png)

## Initialization

* On project in `App.xaml`:


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

* On project in `MauiProgram.cs`:

```csharp
<?xml version = "1.0" encoding = "UTF-8" ?>
<Application xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:Sharpnado.MaterialFrame.Maui"
             xmlns:rv="clr-namespace:Sharpnado.MaterialFrame.Maui.Resources.Styles;assembly=Sharpnado.MaterialFrame.Maui"
             x:Class="MaterialFrame.App">
    
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Resources/Styles/Colors.xaml" />
                <ResourceDictionary Source="Resources/Styles/Styles.xaml" />
                <rv:Colors />
                <rv:MaterialFrame />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>

</Application>

```


