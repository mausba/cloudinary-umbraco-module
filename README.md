# cloudinary-umbraco-module
This will give you the possibility to reroute your media folder to cloudinary.com using "Auto upload mapping" feature. 

## License
The code is released under the MIT License. There is no limitation for both academic and commercial usage.

## Deployment
1.) Increase version in **Project Properties -> Assembly Information**

2.) Build _Cloudinary_ project using **Release** configuration

3.) Increase version in **Cloudinary.nuspec**

4.) Run **nuget.bat** and enter new version in format x.y.z

5.) Upload package to https://www.nuget.org

## Instructions
After you have created an account with https://cloudinary.com and installed nuget package, you will need to change this value in ```web.config``` :

```
<add key="CloudinaryUrl" value="https://res.cloudinary.com/youraccount/image/upload/media/" />
```

After that all your links to /media/ will be routed to cloudinary.

If you want to crop the image use this extension method:

```
public static class IPublishedContentExtensions
{
    public static string Crop(this IPublishedContent image, int width)
    {
        if (image == null) return string.Empty;
        return image.Url.Replace("/media/", "/media/{{crop-" + width + "/{{m}}/");
    }
}
```

```
<img src="@Umbraco.TypedMedia(id).Crop(100)" />
```
