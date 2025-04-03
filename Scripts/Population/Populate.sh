#!/usr/bin/env bash

SCRIPT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

nix-shell -p powershell --run "pwsh -File $SCRIPT_DIR/Populate.ps1"
