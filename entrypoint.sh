#!/bin/bash

# citim secretul si il exportam in env var
export ConnectionStrings__DbConnectionString=$(cat /run/secrets/sqlConnectionString)

# pornim aplicatia
exec dotnet CocktailParty.dll