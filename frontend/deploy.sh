# ng build
# git add dist
# git commit -m "DEPLOY: Update dist folder"
# git push

# TODO: git commit to check if there are any uncommitted or unsynced changes
echo "Ensure that all changes are committed and pushed to remote"

# TODO: need to persist sessions between logins on vm
ssh justin@52.191.141.202
cd catilyfe
git pull
echo "Running 'ng build'..."
ng build
echo "Copying dist to server folder..."
cp -r ./dist/* ./server/public
# Don't need to restart node server, since we're just serving static content??
exit
