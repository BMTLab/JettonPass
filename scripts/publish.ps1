$output = "\bin\Publish"
$projects = @(
    "..\src\App"
    "..\src\SerialPortListener"
)

New-Item -ItemType Directory -Force -Path $output

$projects | ForEach-Object {
    & dotnet publish $_ -c Release -o ("{0}\{1}" -f $_,$output)
}
