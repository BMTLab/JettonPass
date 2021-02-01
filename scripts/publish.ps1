$project = "..\src\App"
$soluction = "..\"
$output = "..\src\App\bin\Publish"
$dotnet = "C:\Program Files\dotnet\dotnet.exe"

New-Item -ItemType Directory -Force -Path $output -Confirm:$true

# Clear previous releases
Remove-Item "$output\*" -Recurse -Confirm:$true

& $dotnet publish $project -c Release -o $output