# Fetch all branches from the remote repository
git fetch --all

# Get the list of all remote branches
$branches = git branch -r | Where-Object { $_ -notmatch "->" }

foreach ($branch in $branches) {
    # Remove 'origin/' part from the branch name
    $branchName = $branch.Trim() -replace '^origin/', ''

    Write-Host "Switching to branch: $branchName"

    try {
        # Checkout the branch
        git checkout $branchName

        # Pull the latest changes
        git pull origin $branchName

        Write-Host "Successfully pulled changes for branch: $branchName"
    }
    catch {
        Write-Host "Error pulling changes for branch: $branchName"
        Write-Host "Error details: $_"

        # Continue to the next branch
        continue
    }
}

# Switch back to the main branch (or any branch you prefer)
git checkout dev
