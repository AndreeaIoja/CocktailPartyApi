#!/bin/bash

# citim secretul si il exportam in env var
export ConnectionStrings__DbConnectionString=$(cat /run/secrets/sqlConnectionString)
export Meili__MeiliSearchKey=$(cat /run/secrets/meiliSearchKey)
export AppSettings__Token=$(cat /run/secrets/tokenKey)
export AppSettings__Audience=$(cat /run/secrets/audienceKey) 
export AppSettings__Issuer=$(cat /run/secrets/issuerKey) 

# pornim aplicatia
exec dotnet CocktailParty.dll