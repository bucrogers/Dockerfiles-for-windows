Show-NetFirewallRule | where {$_.enabled -eq ‘true’ -AND $_.direction -eq ‘inbound’}| select displayname
