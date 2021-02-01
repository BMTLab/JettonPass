$output = "\bin\Publish"
$projects = @(
    "..\src\App"
    "..\src\SerialPortListener"
)

New-Item -ItemType Directory -Force -Path $output

./clean.ps1

$projects | ForEach-Object {
    $project = ("{0}\{1}" -f $_,$output)
    $name = $_.Split('\')[2]
    
    dotnet publish $_ -c Release -o $project
    Compress-Archive -Path $project\* -DestinationPath ("{0}\{1}.zip" -f $project,$name) -Update
}
