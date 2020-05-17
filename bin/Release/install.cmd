@echo off
echo "Deleting WebScraper Service..."
sc delete WebScraper
echo "-------------------------------------------------"
echo "Uninstalling WebScraper Service..."
C:\Users\ahmed\source\repos\WebScraper\WebScraper\bin\Release\WebScraper.exe --uninstall
echo "-------------------------------------------------"
echo "Installing WebScraper Service..."
C:\Users\ahmed\source\repos\WebScraper\WebScraper\bin\Release\WebScraper.exe --install
echo "-------------------------------------------------"
echo "Starting WebScraper Service..."
net start WebScraper
echo "-------------------------------------------------"
pause