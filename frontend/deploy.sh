distRepo="./catilyfe-frontend"
supplementalDist="./supplemental-dist-files"

if [ ! -d "$distRepo" ]; then
    (>&2 echo "The dist repo '$distRepo' doesn't exist! You're probably Peter trying to deploy the frontend. You can't.")
    exit
fi

# Remove all existing dist files
cd "$distRepo"
git rm -r ./*
cd ..
echo "Done removing dist"

# Build n copy janx
if ! ng build --target production; then
    exit
fi
cp -r ./dist/* "$distRepo"
cp -r "$supplementalDist"/* "$distRepo"
echo "Done building n copying janx"

# Add and commit to deployment repo
cd "$distRepo"
git add .
git commit -m "Deploy janx"
git push
