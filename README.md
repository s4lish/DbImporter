# C# Data Import Project

This C# project allows you to import data from Excel, CSV, or JSON files into a SQL Server database. It provides the flexibility to either import data into an existing table with column mapping or create a new table.

## Features

- Import data from Excel, CSV, or JSON files.
- Choose a target SQL Server table for data import.
- Map columns from the source file to the target table.
- Option to create a new table if needed.

## Prerequisites

Before using the project, ensure the following prerequisites are met:

- .NET Core 8 (before was 7) SDK installed
- SQL Server instance available
- EPPlus and System.Data.SqlClient packages installed (or use NuGet package manager to install)

## Getting Started

1. Clone the repository to your local machine:

   ```bash
   git clone https://github.com/s4lish/DbImporter.git
