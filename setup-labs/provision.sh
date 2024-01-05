#!/usr/bin/env bash

# Environment Variables
export SUBSCRIPTION_ID=""
export RESOURCE_GROUP="optimal-blue-rg"
export LOCATION="eastus"
export SERVICE_PRINCIPAL_NAME="serviceprincipal"
export GITHUB_REPO="https://github.com/johndohoneyjr/CoPilot-Lab-Files"

# for alias in WSL - alias expansion needed in scripts, not interactive
shopt -s expand_aliases


# Enter the Student Name and store in a variable
echo "Enter the Student Name: "
read StudentName
# concatenate the Student name to the resource group name
export RESOURCE_GROUP=$StudentName-$RESOURCE_GROUP
echo $RESOURCE_GROUP
# export the Student name as an environment variable
export Student_NAME=$StudentName

export Student=$(echo "$Student_NAME" | tr '[:lower:]' '[:upper:]')

# check for existence of environment variable WSL_DISTRO_NAME
# Make sure github cli is installed -- for adding the secret to GH Actions
# https://github.com/cli/cli
#
if [[ -z "${WSL_DISTRO_NAME}" ]]; then
  echo "Not running in WSL, skipping WSL specific commands"
else 
  alias gh="gh.exe"
fi


# Make sure github cli is installed -- for adding the secret to GH Actions
# https://github.com/cli/cli
#
command -v gh >/dev/null 2>&1 || { echo >&2 "I require gh cli for this script, but it's not installed, download gh at: https://github.com/cli/cli .  Aborting."; exit 1; }

# Make sure jq is installed -- very handy command line tool
# https://stedolan.github.io/jq/download/
#
command -v jq >/dev/null 2>&1 || { echo >&2 "I require jq for this script, but it's not installed, download jq at: https://stedolan.github.io/jq/download/.  Aborting."; exit 1; }

if [[ -z "${SUBSCRIPTION_ID}" ]]; then
  clear
  echo "Please set SUBSCRIPTION_ID to the account you wish to use"
  exit 1
else 
  echo "Using Subscription ID=$SUBSCRIPTION_ID for the following command set"
fi

if [[ -z "${RESOURCE_GROUP}" ]]; then
  clear
  echo "Please set RESOURCE_GROUP for the account you wish to use"
  exit 1
else 
  echo "Using Resource Group =$RESOURCE_GROUP for the following commands"
fi

if [[ -z "${LOCATION}" ]]; then
  clear
  echo "Please set LOCATION for the location you wish to use for set up, locations availaable..."
  az account list-locations | jq .[].metadata.pairedRegion[].name
  exit 1
else 
  echo "Using Resource Group =$LOCATION for the following commands"
fi

if [[ -z "${SERVICE_PRINCIPAL_NAME}" ]]; then
  clear
  echo "Please set SERVICE_PRINCIPAL_NAME for the account you wish to use"
  exit 1
else 
  echo "Using Service Principal Name = $SERVICE_PRINCIPAL_NAME for the following commands"
fi

if [[ -z "${GITHUB_REPO}" ]]; then
  clear
  echo "Please set the GITHUB_REPO you wish to use to set the Service Principal as a secret"
  exit 1
else 
  echo "Using Github Repo = $GITHUB_REPO for GH Actions secrets"
fi

# Login to Owner account
echo ""
echo "Logging you into your account ..."
az login  ## for CodeSpaces --use-device-code
az account set --subscription $SUBSCRIPTION_ID

# Create the Resource Group
echo "Creating resource group - $RESOURCE_GROUP"
az group create --name $RESOURCE_GROUP --location $LOCATION

echo "Creating Resource Group Scoped Service Principal..."
export SERVICE_PRINCIPAL_NAME=$Student-$SERVICE_PRINCIPAL_NAME
az ad sp create-for-rbac --name $SERVICE_PRINCIPAL_NAME --role Contributor --scopes /subscriptions/$SUBSCRIPTION_ID/resourceGroups/$RESOURCE_GROUP  --sdk-auth > gh-secret.json
export clientID=$(cat gh-secret.json | jq -r .clientId)
export PASSWORD=$(cat gh-secret.json | jq -r .clientSecret)
export TENANT_ID=$(cat gh-secret.json | jq -r .tenantId)
export SUB_ID=$(cat gh-secret.json | jq -r .subscriptionId)


echo "az login --service-principal --username $clientID --password $PASSWORD --tenant $TENANT_ID" > $Student-login.txt

# Authenticate to Github 
echo ""
echo "Logging into Github to set the service principal as a secret for Github actions automation.."
gh auth login

#
echo "Setting the Service Principal Github Secret for automation..."
gh secret set $STUDENT"_CLUSTER_SERVICE_PRINCIPAL" -a codespaces -r $GITHUB_REPO < gh-secret.json

if [ $? -eq 0 ]; then
   echo "Displaying the secret file...gh-secret.json, after you add ClientID and Secret, delete file..."
   cat ./gh-secret.json
else
   echo "You need to manually add the Service Principal with the label CLUSTER_SERVICE_PRINCIPAL to the Repo Secrets"
   echo "The JSON file is located in $PWD, and the file is gh-secret.json"
fi

echo "Setting Subscription ID Github Secret for Github Actions automation..."
gh secret set  $STUDENT"_SUBSCRIPTION_ID" -a codespaces -r $GITHUB_REPO --body $SUBSCRIPTION_ID

echo "Setting Resource Group Name as Github Secret for Github Actions automation..."
gh secret set  $STUDENT"_RESOURCE_GROUP"  -a codespaces -r $GITHUB_REPO --body $RESOURCE_GROUP

echo "Setting the Login for client access for client  automation..."
gh secret set $STUDENT"_CLIENT_LOGIN" -a codespaces -r $GITHUB_REPO < $Student-login.txt

# Clean up
rm $Student-login.txt
rm gh-secret.json