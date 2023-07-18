:: execute this in a VS dev shell!
::call "C:\Program Files\Microsoft Visual Studio\2022\Community\VC\Auxiliary\Build\vcvars64.bat"

pushd clib
cl /c nativelib.c
lib nativelib.obj
popd

pushd app
dotnet publish -r win-x64 -c Release -p:PublishAot=true 
popd