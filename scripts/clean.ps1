$source = "../src"

Get-ChildItem $source -include bin,obj -Recurse | foreach ($_) { remove-item $_.fullname -Force -Recurse }