$output = "..\build"
$projects = @(
    "..\src\SerialPortListener"
	"..\src\Launcher"
    "..\src\App"
)

& ./clean.ps1

$projects | ForEach-Object {
    dotnet publish $_ -c Release -o $output
}
