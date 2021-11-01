@echo off
set /p v="Enter package version (format: x.y.z): "
nuget pack Cloudinary.nuspec -version %v%
pause