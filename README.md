# how do i install it?
go to the [releases page](https://github.com/x45k/better-browser-v2/releases) and download the .zip file, extract it, and run the .exe file (the first launch of the app may be slow, but future launches will be quicker)

# is the v1 or v2 version better
[v1](https://github.com/x45k/better-browser):
- has tab support

[v2](https://github.com/x45k/better-browser-v2):  
- Standalone EXE rather than the giant mess of folders in v1  
- Built on WebView2  
- Cleaner UI  

# commands (ignore if you dont know what your doing):

dotnet run -f net9.0-windows10.0.19041.0

dotnet publish -f net9.0-windows10.0.19041.0 -c Release -o ./publish

dotnet publish -f net9.0-windows10.0.19041.0 -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:UseMonoRuntime=false -o ./publish 