[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=lbouriez_GenerateReport&metric=alert_status)](https://sonarcloud.io/dashboard?id=lbouriez_GenerateReport)

# Generate Report

This executable is used to create a new PDF file with a TOC based on many other pdf. It will merge them all into one clean.

# How to use

Simply put the executable in the folder where are the main folder
For example, let's say you have:
- Main Folder
  - January
    - Analytical data.pdf
    - Sales data.pdf
  - February
    - Customer.pdf
  - March
    - Logistic.pdf

You need to put GenerateReport.exe into Main Folder and double click on it.

# Generation

Use these commands to publish a clean package.

## Muraflex console app

`dotnet publish -c MuraflexApp /p:PublishProfile=MuraflexBible\Properties\PublishProfiles\FolderProfile.pubxml`

## Console app

`dotnet publish -c ConsoleApp /p:PublishProfile=ConsoleApp\Properties\PublishProfiles\FolderProfile.pubxml`

## WinForm app

`Coming soon...`
