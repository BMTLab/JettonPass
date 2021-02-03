$source = "../"

Get-ChildItem $source -include bin,obj,build,publish -Recurse | ForEach-Object ($_) { remove-item $_.fullname -Force -Recurse }