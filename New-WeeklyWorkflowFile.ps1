param(
    [string]$WeeklyWorkflowRoot = $PSScriptRoot,
    [switch]$Force,
    [switch]$OpenInCode
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

function Get-IsoLikeWeekNumber {
    param([datetime]$Date)

    $calendar = [System.Globalization.CultureInfo]::InvariantCulture.Calendar
    return $calendar.GetWeekOfYear(
        $Date,
        [System.Globalization.CalendarWeekRule]::FirstFourDayWeek,
        [System.DayOfWeek]::Monday
    )
}

$now = Get-Date
$year = $now.Year
$week = Get-IsoLikeWeekNumber -Date $now

$weekFolderName = "Week $week"
$weekFolderPath = Join-Path -Path $WeeklyWorkflowRoot -ChildPath $weekFolderName
$weekFileName = "Week-$week.md"
$weekFilePath = Join-Path -Path $weekFolderPath -ChildPath $weekFileName

if (-not (Test-Path -Path $weekFolderPath)) {
    New-Item -Path $weekFolderPath -ItemType Directory | Out-Null
}

if ((Test-Path -Path $weekFilePath) -and -not $Force) {
    Write-Output "Weekly file already exists: $weekFilePath"
    Write-Output "Use -Force to overwrite."
    exit 0
}

$content = @(
    "# $($now.ToString('dd-MM-yyyy'))"
    ""
    "## Week $week Status Update ($($now.ToString('yyyy-MM-dd')))"
    "- Completed in Week $($week):"
    "- Carried to Week $($week + 1):"
    ""
    "# Tasks"
    ""
)

Set-Content -Path $weekFilePath -Value $content -Encoding UTF8
Write-Output "Created weekly file: $weekFilePath"

if ($OpenInCode) {
    $codeCommand = Get-Command -Name code -ErrorAction SilentlyContinue
    if ($null -ne $codeCommand) {
        & $codeCommand.Path $weekFilePath
    }
}
