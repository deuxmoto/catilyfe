ng build
git add dist
git commit -m "DEPLOY: Update dist folder"
cd ..
git branch -D deploy
git subtree split --prefix=frontend/dist --branch=deploy
git push origin +deploy
cd frontend
