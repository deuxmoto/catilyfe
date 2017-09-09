# TODO: git commit to check if there are any uncommitted or unsynced changes
# echo "Ensure that all changes are committed and pushed to remote"

echo "Remoting into server, run ./update-server.sh to update..."
ssh -t justin@52.191.141.202 "cd ~/catilyfe/frontend/server; bash;"
