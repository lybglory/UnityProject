@echo off&setlocal enabledelayedexpansion
for /r %%a in (*.txt) do (
set fn=%%~nxa
set fn=!fn:B=C!
rename "%%a" "!fn!"
)
