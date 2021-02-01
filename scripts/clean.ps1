$source = "../src"

Get-ChildItem $source -include bin,obj -Recurse | ForEach-Object ($_) { remove-item $_.fullname -Force -Recurse } -Confirm:$true