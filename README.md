# cloudinary-umbraco-module
This will give you the possibility to reroute your media folder to cloudinary.com using "Auto upload mapping" feature. 

After you have created an account with cloudinary.com you will need to register the module in ```web.config``` and add these keys in ```<appSettings>``` section

To register the module add this inside ```<httpModules>``` section:
  
```
<add name="CloudinaryModule" type="Cloudinary.CloudinaryModule, Cloudinary" />
```
  
  
Inside ```<system.webServer> <modules runAllManagedModulesForAllRequests="true">``` add this:
  
```
<remove name="CloudinaryModule" />

<add name="CloudinaryModule" type="Cloudinary.CloudinaryModule, Cloudinary" />
```


Inside ```<appSettings>``` section add these values (make sure you created upload mapping on cloudinary first):

```
<add key="CloudinaryUrl" value="https://res.cloudinary.com/youraccount/image/upload/media/" />

<add key="CloudinaryCropFormat" value="f_auto" />`
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