param(
    [string]$ProjectPath = ".",                      # Project path
    [string]$OutputDir = ".\bin\Release\Blog",       # Publish output directory
    [string]$Configuration = "Release",              # Build configuration
    [string]$Runtime = "linux-x64",                  # Target runtime (linux-x64, win-x64, osx-x64, etc.)
    [string]$RemoteUser = "taozixiongtai",              # Remote username
    [string]$RemoteHost = "localhost",       # Remote server IP or hostname
    [string]$RemotePath = "/home/taozixiongtai/blog-publish",      # Remote deployment directory
    [string]$ServiceName = "blog.service"       # systemd service name
)

Write-Host "Start packaging ASP.NET Core project..." -ForegroundColor Cyan

# 1. Clean old publish directory
if (Test-Path $OutputDir) {
    Write-Host "Cleaning old publish directory: $OutputDir" -ForegroundColor Yellow
    Remove-Item -Recurse -Force $OutputDir
}

# 2. dotnet publish
Write-Host "Running dotnet publish..." -ForegroundColor Green
dotnet publish $ProjectPath -c $Configuration -r $Runtime --self-contained false -o $OutputDir

if ($LASTEXITCODE -eq 0) {
    Write-Host "publish success -----" -ForegroundColor Green
} else {
    Write-Error "publish fail -----"
    exit 1
}

# Stop service
Write-Host "Stopping systemd service $ServiceName..." -ForegroundColor Cyan
ssh   "$RemoteUser@$RemoteHost" "sudo systemctl stop $ServiceName"

#Çå¿ÕÎÄ¼þ
Write-Host "clreaFile" -ForegroundColor Cyan
ssh  "$RemoteUser@$RemoteHost" "sudo rm -rf $RemotePath/*"

 # Upload file
Write-Host "Uploading file to $RemoteHost..." -ForegroundColor Yellow
scp -r  "$OutputDir\*" "${RemoteUser}@${RemoteHost}:${RemotePath}"

# Start service
Write-Host "Starting systemd service $ServiceName..." -ForegroundColor Cyan
ssh "$RemoteUser@$RemoteHost" "sudo systemctl start $ServiceName"

# Check status
Write-Host "Checking service status..." -ForegroundColor Yellow
ssh "$RemoteUser@$RemoteHost" "sudo systemctl status $ServiceName --no-pager"
 
