# Keator Instalatorów

## Flow chart
![image](https://user-images.githubusercontent.com/35073455/180638933-705253c9-8339-44e9-bcff-4f049949a364.png)

### Opis elementów:
Schemat instalacji- json tworzony przez użytkownika zawierający w sobie informacje o:
- Wymaganiach systemowych
- Wymaganych wcześniej zainstalowanych programach (Prerequisitach)
- Instalatorach, skryptach do uruchomienia oraz szczegółach ich uruchomień (command line argumenty, kolejność instalacji)

Skrypt, Instalator programu1, 2- Pliki do uruchomienia przez instalator, indywidualne instalatory, skrypty użytkownika zdefiniowane w schemacie instalacji.

Kreator- Aplikacja konsolowa, mogąca działać np w pipelinie tworząca zipa oraz instalator jako wynik działania

Zip- paczka zawieająca w sobie wszystkie pliki potrzebne do instalacji:
- schemat
- pliki uruchomieniowe (skrypty, instalatory)
- dodatki potrzebne do prawidłowego działania Instalatora

Instalator- Aplikacja dostarczana użytkownikowi końcowemu wraz z zipem, obsługująca cały proces instalacji wg definicji ze schematu.

### Możliwości rozszerzenia:
UI do zarządzania schematami instalacji, ułatwiający tworzenie schematów, tworzenie instalatora "jednym przyciskiem".
Może także przygotowywać skrypty do automatycznej obsługi kreatora.

## Schemat instalacji omówienie:
Schemat jako plik json musi zostać stworzony przez użytkownika.
Przykładowy json:
```
{
  "Name": "Nazwa",
  "Author": "Autor",
  "Description": "Opis",
  "InstallerVersion": "1.2.3.4",
  "SchemaVersion": "2.1.3.1",
  "Prerequisites": [
    {
      "UpgradeCode": "{f3d05053-6f2b-4dc4-8a58-7229f0bfaf04}",
      "Name": "Nazwa",
      "RequiredVersion": "1.5.3.6"
    },
    {
      "UpgradeCode": "{1da34eb4-4ea5-422d-a66c-8ba80ae2b5c1}",
      "Name": "PrzykladowyProgram",
      "RequiredVersion": "6.8.3.0"
    }
  ],
  "SystemRequirements": {
    "WindowsVersion": "10",
    "RegistryKeys": [
      {
        "Path": "HKEY_CURRENT_USER\\Microsoft\\Windows\\CurrentVersion\\Explorer\\StartupApproved\\Run",
        "Key": "OneDriveSetup",
        "Value": "true"
      },
      {
        "Path": "HKEY_LOCAL_MACHINE\\SOFTWARE\\Clients\\StartMenuInternet\\Google Chrome\\InstallInfo",
        "Key": "IconsVisible",
        "Value": "1"
      }
    ]
  },
  "FilesToExecute": [
    {
      "DefaultCommandLineArgumeters": "--silent -r -u",
      "FilePath": "C:/build/Installer.exe",
      "Order": 1
    },
    {
      "DefaultCommandLineArgumeters": "-FirstRun True -Project Installer",
      "FilePath": "C:/scripts/SomeScript.ps1",
      "Order": 2
    }
  ]
}
```
Omówienie:
- Name - nazwa instalacji. Pole opcjonalne
- Author - autor instalacji. Pole opcjonalne
- Description - opis instalacji. Pole opcjonalne
- InstallerVersion - wersja instalatora. Pole obowiązkowe
- SchemaVersion - wersja schematu. Pole opcjonalne
- Prerequisites - Lista wymaganych wcześniej zainstalowanych programów. Pole opcjonalne
  - UpgradeCode - upgrade code (indywidualny GUID instalatora). Pole obowiązkowe
  - Name - nazwa instalacji. Pole opcjonalne
  - RequiredVersion - wymagana wersja instalacji. Pole obowiązkowe
- SystemRequirements - obiekt wymagań systemowych. Pole opcjonalne
  - WindowsVersion - wymagana wersja systemu windows jako wersja buildu systemu. Pole opcjonalne
  - RegistryKeys - lista wymaganych kluczy w rejestrze. Pole opcjonalne
    - Path - ścieżka rejestru. Pole obowiązkowe
    - Key - klucz rejestru. Pole obowiązkowe
    - Value - wymagana wartosc. Pole obowiązkowe
    - ErrorMessage - wiadomosc zwrotna jeżeli wymaganie nie zostało spełnione. Pole obowiązkowe
- FilesToExecute - definicje plikow do uruchomienia przez instalator. Pole obowiązkowe
  - DefaultCommandLineArgumeters - parametry lini komend które zostaną dodane przy uruchamianiu danego pliku. Pole opcjonalne
  - FilePath - ścieżka do pliku (może być absolutna, ścieżki zostają zmodyfikowane podczas dostarczania schematu do paczki zip). Pole obowiązkowe
  - Order - kolejność jako numer, wg której uruchamiane będą pliki. Pole obowiązkowe
