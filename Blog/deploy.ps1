param(
    [string]$ProjectPath = ".",                      # Project path
    [string]$OutputDir = ".\bin\Release\Blog",       # Publish output directory
    [string]$ZipFile = ".\bin\Release\Blog.zip",     # Zip file name
    [string]$Configuration = "Release",              # Build configuration
    [string]$Runtime = "linux-x64"                   # Target runtime (linux-x64, win-x64, osx-x64, etc.)
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

# 3. Delete old zip
if (Test-Path $ZipFile) {
    Write-Host "Removing old zip file: $ZipFile" -ForegroundColor Yellow
    Remove-Item $ZipFile -Force
}

# 4. Create new zip
Write-Host "Creating new zip file: $ZipFile ..." -ForegroundColor Green
Compress-Archive -Path "$OutputDir\*" -DestinationPath $ZipFile

Write-Host "Packaging completed successfully. Output: $ZipFile" -ForegroundColor Cyan
