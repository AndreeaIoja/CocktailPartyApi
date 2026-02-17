#!/bin/bash

# citim secretul si il exportam in env var
export ConnectionStrings__DbConnectionString=$(cat /run/secrets/sqlConnectionString)
export Meili__MeiliSearchPassword=$(cat /run/secrets/meilisearchPassword)

# pornim aplicatia
exec dotnet CocktailParty.dll
