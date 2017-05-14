ng build
git add dist
git commit -m "DEPLOY: Update dist folder"
cd ..
git subtree split --prefix=frontend/dist --branch=deploy
git push
cd frontend
