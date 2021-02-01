﻿$solution = "..\"
$output = "\bin\Publish"
$dotnet = "C:\Program Files\dotnet\dotnet.exe"

$projects = @(
    "..\src\App"
    "..\src\SerialPortListener"
)

New-Item -ItemType Directory -Force -Path $output

# Clear previous releases
Remove-Item "$output\*" -Recurse

$projects | %{
    & $dotnet publish $_ -c Release -o ("{0}\{1}" -f $_,$output)
}
