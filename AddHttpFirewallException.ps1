# AddHttpFirewallException.ps1
#
# Adds firewall exception for http

#  Arguments : HostName, specifies the FQDN of machine or domain
param
(
    [string] $HostName = $(throw "HostName is required.")
)

if (!(Get-NetFirewallRule | where {$_.Name -eq "Http"})) {
    New-NetFirewallRule -Name "Http" -DisplayName "Http" -Protocol tcp -LocalPort 80 -Action Allow -Enabled True
}
