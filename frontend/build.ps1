param
(
    [string]$DeployMessage,
    [Switch]$Deploy,
    [switch]$SkipBuild
)

$currentdirectory = Get-Location

if([System.Io.Path]::GetFileName($currentdirectory.Path) -ne 'frontend')
{
    return 'Failure. This directory is wrong. Must be run from "frontend"'
}

if($Deploy -and -not $DeployMessage)
{
    Write-Error 'Deployment message must exist to do a deployment'
    return
}

if(-not $SkipBuild)
{
    ## Build angular crap
    Write-Output "Starting ng Build"
    ng set --global warnings.versionMismatch=false
    ng build
}
else
{
    Write-Output 'Skipping build'
}

## Short circuit here if not deploying
if(-not $Deploy)
{
    return
}

Write-Output "Beggining production deployment"

git add dist
git commit -m $DeployMessage

# Check if the commit failed
if($?)
{
    Push-Location ..
    git branch -D deploy > $null
    git subtree split --prefix=frontend/dist --branch=deploy
    git push origin +deploy
    Pop-Location
}
else
{
    Write-Warning "Nothing deployed. Git doesnt think there are any changes"
}