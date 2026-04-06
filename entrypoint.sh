#!/bin/bash

# citim secretul si il exportam in env var
export ConnectionStrings__DbConnectionString=$(cat /run/secrets/sqlConnectionString)
export Meili__MeiliSearchKey=$(cat /run/secrets/meiliSearchKey)

# pornim aplicatia
exec dotnet CocktailParty.dll