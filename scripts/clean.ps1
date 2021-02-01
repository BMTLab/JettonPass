$source = "../"

Get-ChildItem $source -include bin,obj,publish -Recurse | ForEach-Object ($_) { remove-item $_.fullname -Force -Recurse }