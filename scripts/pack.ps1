$output = "\bin\Publish"
$projects = @(
    "..\src\App"
    "..\src\SerialPortListener"
)

$projects | ForEach-Object {
    $project = ("{0}\{1}" -f $_,$output)
    $name = $_.Split('\')[2]
    
    Compress-Archive -Path $project\* -DestinationPath ("{0}\{1}.zip" -f $project,$name) -Update
}
