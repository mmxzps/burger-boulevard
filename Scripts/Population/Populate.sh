#!/usr/bin/env bash

SCRIPT_DIR=$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &>/dev/null && pwd)

pwsh -File $SCRIPT_DIR/Populate.ps1
