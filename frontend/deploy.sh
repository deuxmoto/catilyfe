# TODO: git commit to check if there are any uncommitted or unsynced changes
# echo "Ensure that all changes are committed and pushed to remote"

ssh justin@52.191.141.202
cd ~/catilyfe/frontend
git pull
echo "Updating static files..."
./server/update-static-files.sh
exit
