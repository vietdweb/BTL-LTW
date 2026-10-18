@echo off
title He Thong Quan Ly Sinh Vien - EduPortal
color 0B
cls

echo ======================================================================
echo       HE THONG QUAN LY SINH VIEN ^& DAO TAO (ASP.NET CORE MVC)
echo ======================================================================
echo.
echo [*] Dang khoi dong Web Server...
echo [*] Trinh duyet se tu dong mo tai: http://localhost:5021
echo [*] Nhan Ctrl + C hoac dong cua so nay de tat server khi dung xong.
echo.
echo ======================================================================

cd /d "%~dp0"

:: Mo trinh duyet
start http://localhost:5021

:: Chay project chi dinh ro file csproj
dotnet run --project "BTL-LTW/BTL-LTW.csproj" --launch-profile "http"

if %ERRORLEVEL% NEQ 0 (
    echo.
    echo [!] Co loi xay ra khi khoi dong ung dung.
    pause
)
